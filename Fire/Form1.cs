using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace AxonSimpleUI
{
    public partial class AxonSimpleUIForm : Form
    {
        int unfocusedTicks = 0;
        int changeNameAfterTicks = 600; //One minute

        public string getRandomWordOfType(string type)
        {
            Random random = new Random(System.DateTime.Now.Second * System.DateTime.Now.Millisecond);
            StreamReader fileReader = new StreamReader("keywords.txt");
            int lines = 233300;
            int lineGoal = random.Next(0, lines);
            for (int line = 0; line < lineGoal; line++){
                fileReader.ReadLine();
            }
            string returnLine = "";
            int attempts = 0;
            while (attempts < 15)
            {
                returnLine = fileReader.ReadLine();
                if (returnLine.EndsWith(type))
                {
                    return returnLine.Remove(returnLine.Length - type.Length);
                }
            }
            throw new Exception("bad, ur mum gei, fle can't find line with ending " + type);
        }

        public string makeNewName()
        {
            myName = StringExtensions.FirstCharToUpper(getRandomWordOfType("\\A")) + " " + StringExtensions.FirstCharToUpper(getRandomWordOfType("\\N"));
            this.Text = myName;
            return myName;
        }

        public string myName;

        public AxonSimpleUIForm()
        {
            InitializeComponent();
            TitleDraggable.Text = makeNewName() + " (Not Injected)";
            MonacoEditor.Url = new Uri(string.Format("file:///{0}/monaco-editor/index.html", Directory.GetCurrentDirectory()));
            MonacoEditor.Document.BackColor = System.Drawing.SystemColors.ControlDark;
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void TitleDraggable_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void CloseButton_Click(object sender, EventArgs e) => Environment.Exit(0);

        private void MinimizeButton_Click(object sender, EventArgs e) => WindowState = FormWindowState.Minimized;

        private void Inject_Click(object sender, EventArgs e)
        {
            Functions.Inject();
            TitleDraggable.Text = this.myName + " (Injecting...)";
        }

        private void ExecuteButton_Click(object sender, EventArgs e)
        {
            if (NamedPipes.NamedPipeExist(NamedPipes.luapipename))//check if the pipe exist
            {
                NamedPipes.LuaPipe(MonacoEditor.Document.InvokeScript("GetMonacoEditorText").ToString());//lua pipe function to send the script
            }
            else
            {
                MessageBox.Show($"Inject {Functions.exploitdllname} before Using this!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);//if the pipe can't be found a messagebox will appear
                return;
            }
        }

        private void ClearButton_Click(object sender, EventArgs e) => MonacoEditor.Document.InvokeScript("SetMonacoEditorText", new object[] { "" });//Clear the MonacoEditor

        private void OpenButton_Click(object sender, EventArgs e)
        {
            if (Functions.openfiledialog.ShowDialog() == DialogResult.OK)//check if the user clicked Ok/open
            {
                try
                {
                    MonacoEditor.Document.InvokeScript("SetMonacoEditorText", new object[]
                    {
                        File.ReadAllText(Functions.openfiledialog.FileName)
                    });//load all the text in the MonacoEditor

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: Could not read file from disk. Original error: {ex.Message}");//display if got error
                }
            }
        }

        private void StatusTimer_Tick(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                unfocusedTicks = 0;
            }
            else
            {
                unfocusedTicks += 1;
            }

            if (unfocusedTicks > changeNameAfterTicks)
            {
                makeNewName();
                unfocusedTicks = 0;
            }

            if (NamedPipes.NamedPipeExist(NamedPipes.luapipename))
            {
                TitleDraggable.Text = this.myName + " (Injected)";
            }
            else
            {
                TitleDraggable.Text = this.myName + " (Not Injected)";
            }
            File.WriteAllText("cout.txt", MonacoEditor.DocumentText);
        }

        private void SelectDllDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ReloadDllButton_Click(object sender, EventArgs e)
        {

        }
    }
    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }
    }
}
