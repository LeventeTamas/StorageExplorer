namespace StorageExplorer.View
{
    partial class MainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsl_session = new System.Windows.Forms.ToolStripLabel();
            this.tscb_session = new System.Windows.Forms.ToolStripComboBox();
            this.tsb_saveSession = new System.Windows.Forms.ToolStripButton();
            this.tsb_renameSession = new System.Windows.Forms.ToolStripButton();
            this.tsb_deleteSession = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsb_newWindow = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsl_layout = new System.Windows.Forms.ToolStripLabel();
            this.tscb_layout = new System.Windows.Forms.ToolStripComboBox();
            this.tsb_resetLayout = new System.Windows.Forms.ToolStripButton();
            this.tsmi_session = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_newSession = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_saveSession = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_renameSession = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_deleteSession = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmi_newWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_options = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_settings = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsl_session,
            this.tscb_session,
            this.tsb_saveSession,
            this.tsb_renameSession,
            this.tsb_deleteSession,
            this.toolStripSeparator1,
            this.tsb_newWindow,
            this.toolStripSeparator4,
            this.tsl_layout,
            this.tscb_layout,
            this.tsb_resetLayout});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(901, 35);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsl_session
            // 
            this.tsl_session.Name = "tsl_session";
            this.tsl_session.Size = new System.Drawing.Size(49, 32);
            this.tsl_session.Text = "Session:";
            // 
            // tscb_session
            // 
            this.tscb_session.AutoSize = false;
            this.tscb_session.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscb_session.Items.AddRange(new object[] {
            "(default)",
            "Képszerkesztés",
            "Programozás",
            "Videóvágás"});
            this.tscb_session.Name = "tscb_session";
            this.tscb_session.Size = new System.Drawing.Size(220, 23);
            this.tscb_session.SelectedIndexChanged += new System.EventHandler(this.Tscb_session_SelectedIndexChanged);
            // 
            // tsb_saveSession
            // 
            this.tsb_saveSession.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_saveSession.Image = global::StorageExplorer.Properties.Resources.icon_save;
            this.tsb_saveSession.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_saveSession.Name = "tsb_saveSession";
            this.tsb_saveSession.Size = new System.Drawing.Size(28, 32);
            this.tsb_saveSession.Text = "Save Session";
            this.tsb_saveSession.Click += new System.EventHandler(this.Tsb_saveSession_Click);
            // 
            // tsb_renameSession
            // 
            this.tsb_renameSession.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_renameSession.Image = global::StorageExplorer.Properties.Resources.icon_rename;
            this.tsb_renameSession.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_renameSession.Name = "tsb_renameSession";
            this.tsb_renameSession.Size = new System.Drawing.Size(28, 32);
            this.tsb_renameSession.Text = "Rename Session";
            this.tsb_renameSession.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // tsb_deleteSession
            // 
            this.tsb_deleteSession.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_deleteSession.Image = global::StorageExplorer.Properties.Resources.icon_delete;
            this.tsb_deleteSession.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_deleteSession.Name = "tsb_deleteSession";
            this.tsb_deleteSession.Size = new System.Drawing.Size(28, 32);
            this.tsb_deleteSession.Text = "Delete Session";
            this.tsb_deleteSession.ToolTipText = "Delete session";
            this.tsb_deleteSession.Click += new System.EventHandler(this.tsb_deleteSession_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 35);
            // 
            // tsb_newWindow
            // 
            this.tsb_newWindow.Image = global::StorageExplorer.Properties.Resources.icon_add;
            this.tsb_newWindow.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_newWindow.Name = "tsb_newWindow";
            this.tsb_newWindow.Size = new System.Drawing.Size(106, 32);
            this.tsb_newWindow.Text = "New Window";
            this.tsb_newWindow.ToolTipText = "New Window";
            this.tsb_newWindow.Click += new System.EventHandler(this.Tsb_newWindow_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 35);
            // 
            // tsl_layout
            // 
            this.tsl_layout.Name = "tsl_layout";
            this.tsl_layout.Size = new System.Drawing.Size(46, 32);
            this.tsl_layout.Text = "Layout:";
            // 
            // tscb_layout
            // 
            this.tscb_layout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscb_layout.Items.AddRange(new object[] {
            "Manual",
            "Vertical",
            "Horizontal"});
            this.tscb_layout.Name = "tscb_layout";
            this.tscb_layout.Size = new System.Drawing.Size(121, 35);
            this.tscb_layout.SelectedIndexChanged += new System.EventHandler(this.Tscb_layout_SelectedIndexChanged);
            // 
            // tsb_resetLayout
            // 
            this.tsb_resetLayout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsb_resetLayout.Image = global::StorageExplorer.Properties.Resources.icon_refresh;
            this.tsb_resetLayout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsb_resetLayout.Name = "tsb_resetLayout";
            this.tsb_resetLayout.Size = new System.Drawing.Size(28, 32);
            this.tsb_resetLayout.Text = "Reset Layout";
            this.tsb_resetLayout.Click += new System.EventHandler(this.tsb_ResetLayout_Click);
            // 
            // tsmi_session
            // 
            this.tsmi_session.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_newSession,
            this.toolStripSeparator2,
            this.tsmi_saveSession,
            this.tsmi_renameSession,
            this.tsmi_deleteSession,
            this.toolStripSeparator5,
            this.tsmi_newWindow});
            this.tsmi_session.Name = "tsmi_session";
            this.tsmi_session.Size = new System.Drawing.Size(58, 20);
            this.tsmi_session.Text = "Session";
            // 
            // tsmi_newSession
            // 
            this.tsmi_newSession.Name = "tsmi_newSession";
            this.tsmi_newSession.Size = new System.Drawing.Size(180, 22);
            this.tsmi_newSession.Text = "New Session";
            this.tsmi_newSession.Click += new System.EventHandler(this.tsmi_newSession_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // tsmi_saveSession
            // 
            this.tsmi_saveSession.Name = "tsmi_saveSession";
            this.tsmi_saveSession.Size = new System.Drawing.Size(180, 22);
            this.tsmi_saveSession.Text = "Save session...";
            this.tsmi_saveSession.Click += new System.EventHandler(this.tsmi_saveSession_Click);
            // 
            // tsmi_renameSession
            // 
            this.tsmi_renameSession.Name = "tsmi_renameSession";
            this.tsmi_renameSession.Size = new System.Drawing.Size(180, 22);
            this.tsmi_renameSession.Text = "Rename session...";
            this.tsmi_renameSession.Click += new System.EventHandler(this.Tsmi_renameSession_Click);
            // 
            // tsmi_deleteSession
            // 
            this.tsmi_deleteSession.Name = "tsmi_deleteSession";
            this.tsmi_deleteSession.Size = new System.Drawing.Size(180, 22);
            this.tsmi_deleteSession.Text = "Delete session";
            this.tsmi_deleteSession.Click += new System.EventHandler(this.tsmi_deleteSession_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(177, 6);
            // 
            // tsmi_newWindow
            // 
            this.tsmi_newWindow.Name = "tsmi_newWindow";
            this.tsmi_newWindow.Size = new System.Drawing.Size(180, 22);
            this.tsmi_newWindow.Text = "New window";
            this.tsmi_newWindow.Click += new System.EventHandler(this.tsmi_newWindow_Click);
            // 
            // tsmi_options
            // 
            this.tsmi_options.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_settings});
            this.tsmi_options.Name = "tsmi_options";
            this.tsmi_options.Size = new System.Drawing.Size(93, 20);
            this.tsmi_options.Text = "Configuration";
            // 
            // tsmi_settings
            // 
            this.tsmi_settings.Name = "tsmi_settings";
            this.tsmi_settings.Size = new System.Drawing.Size(180, 22);
            this.tsmi_settings.Text = "Options";
            this.tsmi_settings.Click += new System.EventHandler(this.tsmi_settings_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_session,
            this.tsmi_options});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(901, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(901, 567);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Storage Explorer V1.0";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainWindow_FormClosed);
            this.ResizeEnd += new System.EventHandler(this.MainWindow_ResizeEnd);
            this.Resize += new System.EventHandler(this.MainWindow_Resize);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripComboBox tscb_session;
        private System.Windows.Forms.ToolStripLabel tsl_session;
        private System.Windows.Forms.ToolStripButton tsb_saveSession;
        private System.Windows.Forms.ToolStripButton tsb_deleteSession;
        private System.Windows.Forms.ToolStripButton tsb_newWindow;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_session;
        private System.Windows.Forms.ToolStripMenuItem tsmi_newSession;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmi_saveSession;
        private System.Windows.Forms.ToolStripMenuItem tsmi_renameSession;
        private System.Windows.Forms.ToolStripMenuItem tsmi_deleteSession;
        private System.Windows.Forms.ToolStripMenuItem tsmi_options;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmi_settings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripComboBox tscb_layout;
        private System.Windows.Forms.ToolStripLabel tsl_layout;
        private System.Windows.Forms.ToolStripButton tsb_resetLayout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem tsmi_newWindow;
        private System.Windows.Forms.ToolStripButton tsb_renameSession;
    }
}

