using ScintillaNET;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AxonSimpleUI
{
	public class CustomTabControl : TabControl
	{
		private readonly StringFormat CenterSringFormat = new StringFormat
		{
			Alignment = (dynamic)StringAlignment.Near,
			LineAlignment = (dynamic)StringAlignment.Center
		};

		private Color activeColor = (dynamic)Color.FromArgb(36, 36, 36);

		private Color backTabColor = (dynamic)Color.FromArgb(0, 0, 0);

		private Color borderColor = (dynamic)Color.FromArgb(30, 30, 30);

		private Color closingButtonColor = (dynamic)Color.WhiteSmoke;

		private string closingMessage;

		private Color headerColor = (dynamic)Color.FromArgb(45, 45, 48);

		private Color horizLineColor = (dynamic)Color.FromArgb(36, 36, 36);

		private TabPage predraggedTab;

		public TabPage contextTab;

		private Color textColor = Color.FromArgb(255, 255, 255);

		public Color selectedTextColor = Color.FromArgb(255, 255, 255);

		//public static this.Form Form1;

		private int count = 1;

		public bool ShowClosingButton
		{
			get;
			set;
		}

		[Category("Colors")]
		[Browsable(true)]
		[Description("The color of the selected page")]
		public Color ActiveColor
		{
			get
			{
				return (dynamic)activeColor;
			}
			set
			{
				activeColor = value;
			}
		}

		[Category("Colors")]
		[Browsable(true)]
		[Description("The color of the background of the tab")]
		public Color BackTabColor
		{
			get
			{
				return (dynamic)backTabColor;
			}
			set
			{
				backTabColor = value;
			}
		}

		[Category("Colors")]
		[Browsable(true)]
		[Description("The color of the border of the control")]
		public Color BorderColor
		{
			get
			{
				return (dynamic)borderColor;
			}
			set
			{
				borderColor = value;
			}
		}

		[Category("Colors")]
		[Browsable(true)]
		[Description("The color of the closing button")]
		public Color ClosingButtonColor
		{
			get
			{
				return (dynamic)closingButtonColor;
			}
			set
			{
				closingButtonColor = value;
			}
		}

		[Category("Options")]
		[Browsable(true)]
		[Description("The message that will be shown before closing.")]
		public string ClosingMessage
		{
			get
			{
				return (dynamic)closingMessage;
			}
			set
			{
				closingMessage = value;
			}
		}

		[Category("Colors")]
		[Browsable(true)]
		[Description("The color of the header.")]
		public Color HeaderColor
		{
			get
			{
				return (dynamic)headerColor;
			}
			set
			{
				headerColor = value;
			}
		}

		[Category("Colors")]
		[Browsable(true)]
		[Description("The color of the horizontal line which is located under the headers of the pages.")]
		public Color HorizontalLineColor
		{
			get
			{
				return (dynamic)horizLineColor;
			}
			set
			{
				horizLineColor = value;
			}
		}

		[Category("Options")]
		[Browsable(true)]
		[Description("Show a Yes/No message before closing?")]
		public bool ShowClosingMessage
		{
			get;
			set;
		}

		[Category("Colors")]
		[Browsable(true)]
		[Description("The color of the title of the page")]
		public Color SelectedTextColor
		{
			get
			{
				return (dynamic)selectedTextColor;
			}
			set
			{
				selectedTextColor = value;
			}
		}

		[Category("Colors")]
		[Browsable(true)]
		[Description("The color of the title of the page")]
		public Color TextColor
		{
			get
			{
				return (dynamic)textColor;
			}
			set
			{
				textColor = value;
			}
		}

		[DllImport("user32.dll")]
		private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

		public CustomTabControl()
		{
			SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, value: true);
			DoubleBuffered = true;
			base.SizeMode = TabSizeMode.Normal;
			base.ItemSize = new Size(240, 16);
			AllowDrop = true;
			base.Selecting += TabChanging;
		}

		protected override void CreateHandle()
		{
			base.CreateHandle();
			base.Alignment = TabAlignment.Top;
			SendMessage(base.Handle, 4913, IntPtr.Zero, (IntPtr)16);
		}

		protected override void OnDragOver(DragEventArgs drgevent)
		{
			if (predraggedTab != null)
			{
				TabPage tabPage = (TabPage)drgevent.Data.GetData(typeof(TabPage));
				TabPage pointedTab = GetPointedTab();
				int num = base.TabPages.IndexOf(tabPage);
				if (tabPage != null && num != base.TabCount)
				{
					TabPage tabPage2 = base.TabPages[base.TabCount - 1];
					if (tabPage != tabPage2 && tabPage == predraggedTab && pointedTab != null)
					{
						drgevent.Effect = DragDropEffects.Move;
						if (pointedTab != tabPage2 && pointedTab != tabPage)
						{
							ReplaceTabPages(tabPage, pointedTab);
						}
					}
				}
			}
			base.OnDragOver(drgevent);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			predraggedTab = GetPointedTab();
			Point location = e.Location;
			for (int i = 0; i < (dynamic)base.TabCount; i++)
			{
				dynamic val = GetTabRect(i);
				val.Offset(val.Width - 15, 2);
				val.Width = 10;
				val.Height = 10;
				dynamic val2 = !val.Contains(location);
				if (val2 || e.Button != MouseButtons.Left)
				{
					continue;
				}
				if (i != base.TabCount - 1)
				{
					predraggedTab = null;
					TabPage tabPage = base.TabPages[i];
					if (!(((dynamic)ShowClosingMessage) ? true : false) || 1 == 0)
					{
						if (tabPage.Controls.Count > 0)
						{
							tabPage.Controls[0].Dispose();
						}
						base.TabPages.RemoveAt(i);
						break;
					}
					CloseTab(tabPage);
				}
				else if (GetTabRect(base.TabCount - 1).Contains(e.Location))
				{
					AddEvent();
					predraggedTab = null;
					break;
				}
			}
			base.OnMouseDown(e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && predraggedTab != null)
			{
				DoDragDrop(predraggedTab, DragDropEffects.Move);
			}
			base.OnMouseMove(e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			predraggedTab = null;
			if (e.Button == MouseButtons.Right)
			{
				//Form.TabContextMenu.Show(Cursor.Position);
			}
			base.OnMouseUp(e);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			Graphics graphics = e.Graphics;
			graphics.SmoothingMode = SmoothingMode.HighQuality;
			graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			graphics.Clear((dynamic)headerColor);
			try
			{
				base.SelectedTab.BackColor = (dynamic)backTabColor;
			}
			catch
			{
			}
			try
			{
				base.SelectedTab.BorderStyle = BorderStyle.None;
			}
			catch
			{
			}
			for (int i = 0; i <= base.TabCount - 1; i++)
			{
				TabPage tabPage = base.TabPages[i];
				int num2 = tabPage.Width = (int)e.Graphics.MeasureString(tabPage.Text, Font).Width + 16;
				int num3 = num2;
				Rectangle rectangle = new Rectangle(new Point(GetTabRect(i).Location.X + 2, GetTabRect(i).Location.Y), new Size(GetTabRect(i).Width, GetTabRect(i).Height));
				Rectangle rectangle2 = new Rectangle(rectangle.Location, new Size(rectangle.Width, rectangle.Height));
				Brush brush = new SolidBrush((dynamic)closingButtonColor);
				if (i != base.SelectedIndex)
				{
					graphics.DrawString(tabPage.Text, Font, new SolidBrush((dynamic)textColor), rectangle2, (dynamic)CenterSringFormat);
				}
				else
				{
					graphics.FillRectangle(new SolidBrush((dynamic)headerColor), rectangle2);
					graphics.FillRectangle(new SolidBrush(Color.FromArgb(36, 36, 36)), new Rectangle(rectangle.X - 5, rectangle.Y - 3, rectangle.Width, rectangle.Height + 5));
					graphics.DrawString(tabPage.Text, Font, new SolidBrush((dynamic)selectedTextColor), rectangle2, (dynamic)CenterSringFormat);
				}
				if (i != base.TabCount - 1)
				{
					if ((dynamic)ShowClosingButton)
					{
						e.Graphics.DrawString("X", Font, brush, rectangle2.Right - 17, 3f);
					}
				}
				else
				{
					using (Font font = new Font(SystemFonts.DefaultFont.FontFamily, 14f, FontStyle.Bold))
					{
						e.Graphics.DrawString("+", font, brush, rectangle2.Right - 22, rectangle2.Top / 2 - 4);
					}
				}
				brush.Dispose();
			}
			graphics.DrawLine(new Pen(Color.FromArgb(36, 36, 36), 5f), new Point(0, 19), new Point(base.Width, 19));
			graphics.FillRectangle(new SolidBrush((dynamic)backTabColor), new Rectangle(0, 20, base.Width, base.Height - 20));
			graphics.DrawRectangle(new Pen((dynamic)borderColor, 2), new Rectangle(0, 0, base.Width, base.Height));
			graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		}

		private TabPage GetPointedTab()
		{
			for (int i = 0; i <= (dynamic)base.TabPages.Count - 1; i++)
			{
				if (GetTabRect(i).Contains(this.PointToClient((dynamic)Cursor.Position)))
				{
					return (dynamic)base.TabPages[i];
				}
			}
			return null;
		}

		private void ReplaceTabPages(TabPage Source, TabPage Destination)
		{
			dynamic val = base.TabPages.IndexOf(Source);
			dynamic val2 = base.TabPages.IndexOf(Destination);
			dynamic val3 = val == -1;
			if ((!(val3 ? true : false) || 1 == 0) && (!((val3 | (val2 == -1)) ? true : false) || 1 == 0))
			{
				base.TabPages[val2] = Source;
				base.TabPages[val] = Destination;
				if ((dynamic)base.SelectedIndex == val)
				{
					base.SelectedIndex = val2;
				}
				else if ((dynamic)base.SelectedIndex == val2)
				{
					base.SelectedIndex = val;
				}
				Refresh();
			}
		}

		public void CloseTab(TabPage tab)
		{
			Scintilla scintilla = tab.Controls[0] as Scintilla;
			int num = (dynamic)base.TabPages.IndexOf(tab);
			if (num != 0 || base.TabCount > 2)
			{
				base.TabPages.RemoveAt(num);
				count--;
				return;
			}
			_ = base.TabPages[0];
			tab.Text = (dynamic)"Untitled.lua";
			scintilla.Text = "";
			scintilla.Refresh();
		}

		private void DragOverEnterHandler(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = (dynamic)DragDropEffects.Copy;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		private void DragDropHandler(object sender, DragEventArgs e)
		{
			string[] array = (string[])e.Data.GetData(DataFormats.FileDrop, autoConvert: false);
			string[] array2 = array;
			foreach (string text in array2)
			{
				this.AddEvent(Path.GetFileNameWithoutExtension(text), File.ReadAllText((dynamic)text));
			}
		}

		public Scintilla NewEditor(string script)
		{
			Scintilla scintilla = new Scintilla();
			scintilla.AllowDrop = true;
			scintilla.AutomaticFold = ((dynamic)AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);
			scintilla.BackColor = Color.Black;
			scintilla.BorderStyle = BorderStyle.None;
			scintilla.Lexer = Lexer.Lua;
			scintilla.Name = "scintilla";
			scintilla.Dock = DockStyle.Fill;
			scintilla.ScrollWidth = 1;
			scintilla.TabIndex = 0;
			scintilla.Styles[32].Size = 15;
			scintilla.Styles[32].Size = 15;
			scintilla.Styles[32].Size = 15;
			scintilla.SetSelectionBackColor(true, (dynamic)Color.FromArgb(17, 177, 255));
			scintilla.SetSelectionForeColor(true, (dynamic)Color.Black);
			scintilla.Margins[1].Width = 0;
			scintilla.StyleResetDefault();
			scintilla.Styles[32].Font = (dynamic)"Consolas";
			scintilla.Styles[32].Size = 10;
			scintilla.Styles[32].BackColor = (dynamic)Color.FromArgb(40, 40, 40);
			scintilla.Styles[32].ForeColor = (dynamic)Color.White;
			scintilla.StyleClearAll();
			scintilla.Styles[11].ForeColor = (dynamic)Color.White;
			scintilla.Styles[1].ForeColor = (dynamic)Color.FromArgb(79, 81, 98);
			scintilla.Styles[2].ForeColor = (dynamic)Color.FromArgb(79, 81, 98);
			scintilla.Styles[3].ForeColor = (dynamic)Color.FromArgb(58, 64, 34);
			scintilla.Styles[4].ForeColor = (dynamic)Color.FromArgb(165, 112, 255);
			scintilla.Styles[6].ForeColor = (dynamic)Color.FromArgb(255, 192, 115);
			scintilla.Styles[7].ForeColor = (dynamic)Color.FromArgb(255, 192, 115);
			scintilla.Styles[8].ForeColor = (dynamic)Color.FromArgb(255, 192, 115);
			scintilla.Styles[9].ForeColor = (dynamic)Color.FromArgb(138, 175, 238);
			scintilla.Styles[10].ForeColor = (dynamic)Color.White;
			scintilla.Styles[5].ForeColor = (dynamic)Color.FromArgb(255, 60, 122);
			scintilla.Styles[13].ForeColor = (dynamic)Color.FromArgb(89, 255, 172);
			scintilla.Styles[13].Bold = true;
			scintilla.Styles[14].ForeColor = (dynamic)Color.FromArgb(89, 255, 172);
			scintilla.Styles[14].Bold = true;
			scintilla.Lexer = Lexer.Lua;
			scintilla.SetProperty("fold", "1");
			scintilla.SetProperty("fold.compact", "1");
			scintilla.Margins[0].Width = 15;
			scintilla.Margins[0].Type = MarginType.Number;
			scintilla.Margins[1].Type = MarginType.Symbol;
			scintilla.Margins[1].Mask = 4261412864u;
			scintilla.Margins[1].Sensitive = true;
			scintilla.Margins[1].Width = 8;
			for (int i = 25; i <= 31; i++)
			{
				scintilla.Markers[i].SetForeColor(Color.White);
				scintilla.Markers[i].SetBackColor(Color.White);
			}
			scintilla.Markers[30].Symbol = MarkerSymbol.BoxPlus;
			scintilla.Markers[31].Symbol = MarkerSymbol.BoxMinus;
			scintilla.Markers[25].Symbol = MarkerSymbol.BoxPlusConnected;
			scintilla.Markers[27].Symbol = MarkerSymbol.TCorner;
			scintilla.Markers[26].Symbol = MarkerSymbol.BoxMinusConnected;
			scintilla.Markers[29].Symbol = MarkerSymbol.VLine;
			scintilla.Markers[28].Symbol = MarkerSymbol.LCorner;
			scintilla.Styles[33].BackColor = Color.FromArgb(40, 40, 40);
			scintilla.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);
			scintilla.SetFoldMarginColor(use: true, Color.FromArgb(40, 40, 40));
			scintilla.SetFoldMarginHighlightColor(use: true, Color.FromArgb(40, 40, 40));
			scintilla.SetKeywords(0, (dynamic)"and break do else elseif end false for function if in local nil not or repeat return then true until while");
			scintilla.SetKeywords(1, (dynamic)"warn CFrame CFrame.fromEulerAnglesXYZ Synapse Decompile Synapse Copy Synapse Write CFrame.Angles CFrame.fromAxisAngle CFrame.new gcinfo os os.difftime os.time tick UDim UDim.new Instance Instance.Lock Instance.Unlock Instance.new pairs NumberSequence NumberSequence.new assert tonumber getmetatable Color3 Color3.fromHSV Color3.toHSV Color3.fromRGB Color3.new load Stats _G UserSettings Ray Ray.new coroutine coroutine.resume coroutine.yield coroutine.status coroutine.wrap coroutine.create coroutine.running NumberRange NumberRange.new PhysicalProperties Physicalnew printidentity PluginManager loadstring NumberSequenceKeypoint NumberSequenceKeypoint.new Version Vector2 Vector2.new wait game. game.Players game.ReplicatedStorage Game delay spawn string string.sub string.upper string.len string.gfind string.rep string.find string.match string.char string.dump string.gmatch string.reverse string.byte string.format string.gsub string.lower CellId CellId.new Delay version stats typeof UDim2 UDim2.new table table.setn table.insert table.getn table.foreachi table.maxn table.foreach table.concat table.sort table.remove settings LoadLibrary require Vector3 Vector3.FromNormalId Vector3.FromAxis Vector3.new Vector3int16 Vector3int16.new setmetatable next ypcall ipairs Wait rawequal Region3int16 Region3int16.new collectgarbage game newproxy Spawn elapsedTime Region3 Region3.new time xpcall shared rawset tostring print Workspace Vector2int16 Vector2int16.new workspace unpack math math.log math.noise math.acos math.huge math.ldexp math.pi math.cos math.tanh math.pow math.deg math.tan math.cosh math.sinh math.random math.randomseed math.frexp math.ceil math.floor math.rad math.abs math.sqrt math.modf math.asin math.min math.max math.fmod math.log10 math.atan2 math.exp math.sin math.atan ColorSequenceKeypoint ColorSequenceKeypoint.new pcall getfenv ColorSequence ColorSequence.new type ElapsedTime select Faces Faces.new rawget debug debug.traceback debug.profileend debug.profilebegin Rect Rect.new BrickColor BrickColor.Blue BrickColor.White BrickColor.Yellow BrickColor.Red BrickColor.Gray BrickColor.palette BrickColor.New BrickColor.Black BrickColor.Green BrickColor.Random BrickColor.DarkGray BrickColor.random BrickColor.new setfenv dofile Axes Axes.new error loadfile ");
			scintilla.SetKeywords(2, (dynamic)"getrawmetatable loadstring getnamecallmethod setreadonly islclosure getgenv unlockModule lockModule mousemoverel debug.getupvalue debug.getupvalues debug.setupvalue debug.getmetatable debug.getregistry setclipboard setthreadcontext getthreadcontext checkcaller getgc debug.getconstant getrenv getreg ");
			scintilla.ScrollWidth = 1;
			scintilla.ScrollWidthTracking = true;
			scintilla.CaretForeColor = Color.White;
			scintilla.BackColor = Color.White;
			scintilla.BorderStyle = BorderStyle.None;
			scintilla.TextChanged += scintilla1_TextChanged;
			scintilla.WrapIndentMode = WrapIndentMode.Indent;
			scintilla.WrapVisualFlagLocation = WrapVisualFlagLocation.EndByText;
			scintilla.BorderStyle = BorderStyle.None;
			scintilla.Text = script;
			return scintilla;
		}

		private void scintilla1_TextChanged(object sender, EventArgs e)
		{
			Scintilla scintilla = (Scintilla)sender;
			int length = scintilla.Lines.Count.ToString().Length;
			scintilla.Margins[0].Width = scintilla.TextWidth(10, (dynamic)new string('9', length + 1)) + 2;
		}

		public void addnewtab()
		{
			int num = base.TabCount - 1;
			base.TabPages.Insert(num, $"Script{base.TabCount}.lua");
			base.TabPages[num].Controls.Add(NewEditor(""));
			base.SelectedIndex = num;
		}

		public Scintilla GetWorkingTextEditor()
		{
			if (base.SelectedTab == null)
			{
				addnewtab();
				return base.SelectedTab.Controls[0] as Scintilla;
			}
			return base.SelectedTab.Controls[0] as Scintilla;
		}

		public void AddEvent(string name = "Script.lua", string content = "")
		{
			if (string.IsNullOrEmpty(GetWorkingTextEditor().Text) && !string.IsNullOrEmpty(content))
			{
				addnewtab();
				base.SelectedTab.Text = (dynamic)("Script " + count.ToString() + ".lua");
				base.SelectedTab.Controls[0].Text = content;
				base.SelectedTab.Controls[0].Refresh();
			}
			else
			{
				addnewtab();
				if (name.Contains("Script" + count.ToString() + ".lua"))
				{
					base.SelectedTab.Text = (dynamic)("Script " + count.ToString() + ".lua");
				}
			}
			count++;
		}

		public void TabChanging(object sender, TabControlCancelEventArgs e)
		{
			if (e.TabPageIndex == (dynamic)base.TabCount - 1)
			{
				e.Cancel = true;
			}
		}

		public string OpenSaveDialog(TabPage tab, string text)
		{
			using (SaveFileDialog saveFileDialog = new SaveFileDialog())
			{
				saveFileDialog.Filter = (dynamic)"Lua Files (*.lua)|*.lua|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
				saveFileDialog.RestoreDirectory = true;
				saveFileDialog.FileName = tab.Text;
				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					File.WriteAllText(saveFileDialog.FileName, text);
					return new FileInfo(saveFileDialog.FileName).Name;
				}
				return tab.Text;
			}
		}


		public bool OpenFileDialog(TabPage tab)
		{
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.Filter = (dynamic)"Lua Files (*.lua)|*.lua|Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
				openFileDialog.RestoreDirectory = true;
				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					GetWorkingTextEditor().Text = File.ReadAllText(openFileDialog.FileName);
					tab.Text = (dynamic)Path.GetFileName(openFileDialog.FileName);
					return true;
				}
				return false;
			}
		}
	}
}