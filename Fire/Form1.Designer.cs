namespace AxonSimpleUI
{
    partial class AxonSimpleUIForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TitleDraggable = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Label();
            this.MinimizeButton = new System.Windows.Forms.Label();
            this.Inject = new System.Windows.Forms.Button();
            this.ExecuteButton = new System.Windows.Forms.Button();
            this.ClearButton = new System.Windows.Forms.Button();
            this.OpenButton = new System.Windows.Forms.Button();
            this.MonacoEditor = new System.Windows.Forms.WebBrowser();
            this.StatusTimer = new System.Windows.Forms.Timer(this.components);
            this.SelectDllDropdown = new System.Windows.Forms.ComboBox();
            this.ReloadDllButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TitleDraggable
            // 
            this.TitleDraggable.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.TitleDraggable.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TitleDraggable.ForeColor = System.Drawing.Color.White;
            this.TitleDraggable.Location = new System.Drawing.Point(0, 0);
            this.TitleDraggable.Name = "TitleDraggable";
            this.TitleDraggable.Size = new System.Drawing.Size(559, 26);
            this.TitleDraggable.TabIndex = 0;
            this.TitleDraggable.Text = "Fire";
            this.TitleDraggable.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TitleDraggable_MouseDown);
            // 
            // CloseButton
            // 
            this.CloseButton.AutoSize = true;
            this.CloseButton.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.ForeColor = System.Drawing.Color.White;
            this.CloseButton.Location = new System.Drawing.Point(534, 0);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(26, 26);
            this.CloseButton.TabIndex = 1;
            this.CloseButton.Text = "X";
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // MinimizeButton
            // 
            this.MinimizeButton.AutoSize = true;
            this.MinimizeButton.Font = new System.Drawing.Font("Comic Sans MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimizeButton.ForeColor = System.Drawing.Color.White;
            this.MinimizeButton.Location = new System.Drawing.Point(511, 0);
            this.MinimizeButton.Name = "MinimizeButton";
            this.MinimizeButton.Size = new System.Drawing.Size(20, 26);
            this.MinimizeButton.TabIndex = 2;
            this.MinimizeButton.Text = "-";
            this.MinimizeButton.Click += new System.EventHandler(this.MinimizeButton_Click);
            // 
            // Inject
            // 
            this.Inject.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.Inject.FlatAppearance.BorderSize = 0;
            this.Inject.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Inject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Inject.ForeColor = System.Drawing.Color.White;
            this.Inject.Location = new System.Drawing.Point(12, 285);
            this.Inject.Name = "Inject";
            this.Inject.Size = new System.Drawing.Size(75, 23);
            this.Inject.TabIndex = 4;
            this.Inject.Text = "Inject";
            this.Inject.UseVisualStyleBackColor = false;
            this.Inject.Click += new System.EventHandler(this.Inject_Click);
            // 
            // ExecuteButton
            // 
            this.ExecuteButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.ExecuteButton.FlatAppearance.BorderSize = 0;
            this.ExecuteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ExecuteButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ExecuteButton.ForeColor = System.Drawing.Color.White;
            this.ExecuteButton.Location = new System.Drawing.Point(397, 285);
            this.ExecuteButton.Name = "ExecuteButton";
            this.ExecuteButton.Size = new System.Drawing.Size(75, 23);
            this.ExecuteButton.TabIndex = 5;
            this.ExecuteButton.Text = "Execute";
            this.ExecuteButton.UseVisualStyleBackColor = false;
            this.ExecuteButton.Click += new System.EventHandler(this.ExecuteButton_Click);
            // 
            // ClearButton
            // 
            this.ClearButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.ClearButton.FlatAppearance.BorderSize = 0;
            this.ClearButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ClearButton.ForeColor = System.Drawing.Color.White;
            this.ClearButton.Location = new System.Drawing.Point(478, 285);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(75, 23);
            this.ClearButton.TabIndex = 6;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = false;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // OpenButton
            // 
            this.OpenButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.OpenButton.FlatAppearance.BorderSize = 0;
            this.OpenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OpenButton.ForeColor = System.Drawing.Color.White;
            this.OpenButton.Location = new System.Drawing.Point(93, 285);
            this.OpenButton.Name = "OpenButton";
            this.OpenButton.Size = new System.Drawing.Size(75, 23);
            this.OpenButton.TabIndex = 7;
            this.OpenButton.Text = "Open";
            this.OpenButton.UseVisualStyleBackColor = false;
            this.OpenButton.Click += new System.EventHandler(this.OpenButton_Click);
            // 
            // MonacoEditor
            // 
            this.MonacoEditor.Location = new System.Drawing.Point(5, 29);
            this.MonacoEditor.MinimumSize = new System.Drawing.Size(20, 20);
            this.MonacoEditor.Name = "MonacoEditor";
            this.MonacoEditor.ScriptErrorsSuppressed = true;
            this.MonacoEditor.Size = new System.Drawing.Size(553, 250);
            this.MonacoEditor.TabIndex = 8;
            // 
            // StatusTimer
            // 
            this.StatusTimer.Enabled = true;
            this.StatusTimer.Tick += new System.EventHandler(this.StatusTimer_Tick);
            // 
            // SelectDllDropdown
            // 
            this.SelectDllDropdown.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(70)))));
            this.SelectDllDropdown.Cursor = System.Windows.Forms.Cursors.Default;
            this.SelectDllDropdown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SelectDllDropdown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
            this.SelectDllDropdown.ForeColor = System.Drawing.Color.White;
            this.SelectDllDropdown.FormattingEnabled = true;
            this.SelectDllDropdown.Location = new System.Drawing.Point(351, 5);
            this.SelectDllDropdown.Name = "SelectDllDropdown";
            this.SelectDllDropdown.Size = new System.Drawing.Size(121, 21);
            this.SelectDllDropdown.TabIndex = 9;
            this.SelectDllDropdown.Text = "all";
            this.SelectDllDropdown.SelectedIndexChanged += new System.EventHandler(this.SelectDllDropdown_SelectedIndexChanged);
            // 
            // ReloadDllButton
            // 
            this.ReloadDllButton.BackColor = System.Drawing.Color.Transparent;
            this.ReloadDllButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ReloadDllButton.FlatAppearance.BorderSize = 0;
            this.ReloadDllButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ReloadDllButton.Font = new System.Drawing.Font("Wingdings 3", 14.25F);
            this.ReloadDllButton.ForeColor = System.Drawing.Color.White;
            this.ReloadDllButton.Location = new System.Drawing.Point(478, 0);
            this.ReloadDllButton.Name = "ReloadDllButton";
            this.ReloadDllButton.Size = new System.Drawing.Size(27, 26);
            this.ReloadDllButton.TabIndex = 10;
            this.ReloadDllButton.Text = "Q";
            this.ReloadDllButton.UseVisualStyleBackColor = false;
            this.ReloadDllButton.Click += new System.EventHandler(this.ReloadDllButton_Click);
            // 
            // AxonSimpleUIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ClientSize = new System.Drawing.Size(565, 317);
            this.Controls.Add(this.ReloadDllButton);
            this.Controls.Add(this.SelectDllDropdown);
            this.Controls.Add(this.MonacoEditor);
            this.Controls.Add(this.OpenButton);
            this.Controls.Add(this.ClearButton);
            this.Controls.Add(this.ExecuteButton);
            this.Controls.Add(this.Inject);
            this.Controls.Add(this.MinimizeButton);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.TitleDraggable);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AxonSimpleUIForm";
            this.Text = "Fire";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label TitleDraggable;
        private System.Windows.Forms.Label CloseButton;
        private System.Windows.Forms.Label MinimizeButton;
        private System.Windows.Forms.Button Inject;
        private System.Windows.Forms.Button ExecuteButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.Button OpenButton;
        private System.Windows.Forms.WebBrowser MonacoEditor;
        private System.Windows.Forms.Timer StatusTimer;
        private System.Windows.Forms.ComboBox SelectDllDropdown;
        private System.Windows.Forms.Button ReloadDllButton;
    }
}

