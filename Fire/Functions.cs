using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

namespace AxonSimpleUI
{
    class Functions
    {
        public static int dllSeleced = 0;
        public static void Inject()
        {
            if (NamedPipes.NamedPipeExist(NamedPipes.luapipename))//check if the pipe exist
            {
                MessageBox.Show("Already injected!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);//if the pipe exist that's mean that we don't need to inject
                return;
            }
            else if (!NamedPipes.NamedPipeExist(NamedPipes.luapipename))//check if the pipe don't exist
            {
                switch (Injector.DllInjector.GetInstance.Inject("RobloxPlayerBeta", AppDomain.CurrentDomain.BaseDirectory + ExploitDefinitions.exploits[dllSeleced].dll))//Process name and dll directory
                {
                    case Injector.DllInjectionResult.DllNotFound://if can't find the dll
                        MessageBox.Show($"Couldn't find {ExploitDefinitions.exploits[dllSeleced].dll}", "Dll was not found!", MessageBoxButtons.OK, MessageBoxIcon.Error);//display messagebox to tell that dll was not found
                        return;
                    case Injector.DllInjectionResult.GameProcessNotFound://if can't find the process
                        MessageBox.Show("Couldn't find RobloxPlayerBeta.exe!", "Target process was not found!", MessageBoxButtons.OK, MessageBoxIcon.Error);//display messagebox to tell that proccess was not found
                        return;
                    case Injector.DllInjectionResult.InjectionFailed://if injection fails(this don't work or only on special cases)
                        MessageBox.Show("Injection Failed!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);//display messagebox to tell that injection failed
                        return;
                }
                //Thread.Sleep(3000);//pause the ui for 3 seconds
                if (!NamedPipes.NamedPipeExist(NamedPipes.luapipename))//check if the pipe dont exist
                {
                    //MessageBox.Show("Ignore This...", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);//display that the pipe was not found so the injection was unsuccessful
                }
            }
        }

        public static OpenFileDialog openfiledialog = new OpenFileDialog
        {
            Filter = "Script File|*.txt;*.lua|All files (*.*)|*.*",//add txt,lua and all files filter
            FilterIndex = 1,//choose what filter will be the default
            RestoreDirectory = true,//restore the last used directory
            Title = "Axon Open Script"//OpenFileDialog Tittle
        };//Initialize OpenFileDialog
    }

    static class ExploitDefinitions
    {
        public static Exploit[] exploits = {
        new Exploit("Krnl", "Krnl.dll", ""),
        new Exploit("WeAreDevs API", "WeAreDevs_API.dll", "https://cdnwrd2.com/r/2/1594685754691/WeAreDevs_API.dll", true)};
        
        public static bool isDllDownloaded(int expIndex)
        {
            return File.Exists(exploits[expIndex].dll);
        }

        public static WebClient DownloadDll(int expIndex)
        {
            string assetloc = exploits[expIndex].sourceUrl;
            if (assetloc == "")
            {
                MessageBox.Show("This dll can not be fetched automatically :O\n" +
                    "Please download and put requested dll in the executable folder.\n" +
                    "(" + AppDomain.CurrentDomain.BaseDirectory + ")");
                return null;
            }

            WebClient webClient = new WebClient();
            webClient.DownloadFile(exploits[expIndex].sourceUrl, AppDomain.CurrentDomain.BaseDirectory + exploits[expIndex].dll);
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(CompletedDownload);
            return webClient;
        }

        private static void CompletedDownload(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("Failed to download .dll file. :O\nDownload was Cancelled");
            }

            if (e.Error != null)
            {
                MessageBox.Show("Failed to download .dll file. :O\n" + e.Error.ToString());
            }
        }
    }

    class Exploit
    {
        public string name;
        public string dll;
        public string sourceUrl;
        public bool autoInjects;

        public Exploit(string name, string dll, string sourceUrl="", bool autoInjects=false)
        {
            this.name = name;
            this.dll = dll;
            this.sourceUrl = sourceUrl;
            this.autoInjects = autoInjects;
        }

        public virtual void Inject() {} //Only important for APIs like We Are Devs V1 that insist on injecting themselves
        public virtual bool isInjected() { return false; }
        public virtual void Execute() {}
    }
}
