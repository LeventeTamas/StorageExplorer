namespace StorageExplorer.View
{
    partial class ExplorerWindow
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
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView1 = new System.Windows.Forms.ListView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiNewFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNewFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRename = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tslbLocation = new System.Windows.Forms.ToolStripLabel();
            this.tstb_Path = new System.Windows.Forms.ToolStripTextBox();
            this.tsbt_Navigate = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbt_Settings = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnHeader1
            // 
            this.columnHeader1.Tag = "col_name";
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 215;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Tag = "col_extension";
            this.columnHeader2.Text = "Extension";
            this.columnHeader2.Width = 80;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Tag = "col_size";
            this.columnHeader3.Text = "Size";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader3.Width = 120;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Tag = "col_date";
            this.columnHeader4.Text = "Date";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader4.Width = 160;
            // 
            // listView1
            // 
            this.listView1.AllowDrop = true;
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.AutoArrange = false;
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 67);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(789, 473);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.listView1_ColumnClick);
            this.listView1.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.listView1_ItemDrag);
            this.listView1.DragDrop += new System.Windows.Forms.DragEventHandler(this.listView1_DragDrop);
            this.listView1.DragEnter += new System.Windows.Forms.DragEventHandler(this.listView1_DragEnter);
            this.listView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ListView1_KeyUp);
            this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ListView1_MouseDoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiNewFile,
            this.tsmiNewFolder,
            this.toolStripSeparator2,
            this.tsmiCopy,
            this.tsmiCut,
            this.tsmiPaste,
            this.tsmiRename,
            this.tsmiDelete,
            this.tsmiProperties});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(169, 186);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // tsmiNewFile
            // 
            this.tsmiNewFile.Image = global::StorageExplorer.Properties.Resources.icon_new_file;
            this.tsmiNewFile.Name = "tsmiNewFile";
            this.tsmiNewFile.Size = new System.Drawing.Size(168, 22);
            this.tsmiNewFile.Text = "Új fájl";
            this.tsmiNewFile.Click += new System.EventHandler(this.newFile_Click);
            // 
            // tsmiNewFolder
            // 
            this.tsmiNewFolder.Image = global::StorageExplorer.Properties.Resources.icon_new_folder;
            this.tsmiNewFolder.Name = "tsmiNewFolder";
            this.tsmiNewFolder.Size = new System.Drawing.Size(168, 22);
            this.tsmiNewFolder.Text = "Új mappa";
            this.tsmiNewFolder.Click += new System.EventHandler(this.tsmiNewFolder_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(165, 6);
            // 
            // tsmiCopy
            // 
            this.tsmiCopy.Image = global::StorageExplorer.Properties.Resources.icon_copy;
            this.tsmiCopy.Name = "tsmiCopy";
            this.tsmiCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.tsmiCopy.Size = new System.Drawing.Size(168, 22);
            this.tsmiCopy.Text = "Másolás";
            this.tsmiCopy.Click += new System.EventHandler(this.tsmiCopy_Click);
            // 
            // tsmiCut
            // 
            this.tsmiCut.Image = global::StorageExplorer.Properties.Resources.icon_cut;
            this.tsmiCut.Name = "tsmiCut";
            this.tsmiCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.tsmiCut.Size = new System.Drawing.Size(168, 22);
            this.tsmiCut.Text = "Kivágás";
            this.tsmiCut.Click += new System.EventHandler(this.tsmiCut_Click);
            // 
            // tsmiPaste
            // 
            this.tsmiPaste.Image = global::StorageExplorer.Properties.Resources.icon_paste;
            this.tsmiPaste.Name = "tsmiPaste";
            this.tsmiPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.tsmiPaste.Size = new System.Drawing.Size(168, 22);
            this.tsmiPaste.Text = "Beillesztés";
            this.tsmiPaste.Click += new System.EventHandler(this.tsmiPaste_Click);
            // 
            // tsmiRename
            // 
            this.tsmiRename.Image = global::StorageExplorer.Properties.Resources.icon_rename;
            this.tsmiRename.Name = "tsmiRename";
            this.tsmiRename.Size = new System.Drawing.Size(168, 22);
            this.tsmiRename.Text = "Átnevezés";
            this.tsmiRename.Click += new System.EventHandler(this.tsmiRename_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Image = global::StorageExplorer.Properties.Resources.icon_delete_file;
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.tsmiDelete.Size = new System.Drawing.Size(168, 22);
            this.tsmiDelete.Text = "Törlés";
            this.tsmiDelete.Click += new System.EventHandler(this.TsmiDelete_Click);
            // 
            // tsmiProperties
            // 
            this.tsmiProperties.Image = global::StorageExplorer.Properties.Resources.icon_properties;
            this.tsmiProperties.Name = "tsmiProperties";
            this.tsmiProperties.Size = new System.Drawing.Size(168, 22);
            this.tsmiProperties.Text = "Tulajdonságok";
            this.tsmiProperties.Click += new System.EventHandler(this.tsmiTulajdonsagok_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.CanOverflow = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslbLocation,
            this.tstb_Path,
            this.tsbt_Navigate,
            this.toolStripSeparator1,
            this.tsbt_Settings});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(789, 35);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tslbLocation
            // 
            this.tslbLocation.Name = "tslbLocation";
            this.tslbLocation.Size = new System.Drawing.Size(56, 32);
            this.tslbLocation.Text = "Location:";
            // 
            // tstb_Path
            // 
            this.tstb_Path.AutoSize = false;
            this.tstb_Path.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tstb_Path.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tstb_Path.Name = "tstb_Path";
            this.tstb_Path.Size = new System.Drawing.Size(600, 23);
            this.tstb_Path.Text = "C:\\";
            this.tstb_Path.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbPath_KeyUp);
            // 
            // tsbt_Navigate
            // 
            this.tsbt_Navigate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbt_Navigate.Image = global::StorageExplorer.Properties.Resources.icon_jump;
            this.tsbt_Navigate.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbt_Navigate.Name = "tsbt_Navigate";
            this.tsbt_Navigate.Size = new System.Drawing.Size(28, 32);
            this.tsbt_Navigate.Text = "Navigate";
            this.tsbt_Navigate.Click += new System.EventHandler(this.tsbt_Navigate_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 35);
            // 
            // tsbt_Settings
            // 
            this.tsbt_Settings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbt_Settings.Image = global::StorageExplorer.Properties.Resources.icon_settings;
            this.tsbt_Settings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbt_Settings.Name = "tsbt_Settings";
            this.tsbt_Settings.Size = new System.Drawing.Size(28, 32);
            this.tsbt_Settings.Text = "Settings";
            this.tsbt_Settings.Click += new System.EventHandler(this.tsbt_Settings_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.toolStrip2.Location = new System.Drawing.Point(0, 35);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(789, 25);
            this.toolStrip2.TabIndex = 3;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // ExplorerWindow
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(789, 540);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.listView1);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.Name = "ExplorerWindow";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ExplorerWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExplorerWindow_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ExplorerWindow_FormClosed);
            this.ResizeEnd += new System.EventHandler(this.ExplorerWindow_ResizeEnd);
            this.LocationChanged += new System.EventHandler(this.ExplorerWindow_LocationChanged);
            this.Enter += new System.EventHandler(this.ExplorerWindow_Enter);
            this.Resize += new System.EventHandler(this.ExplorerWindow_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel tslbLocation;
        private System.Windows.Forms.ToolStripTextBox tstb_Path;
        private System.Windows.Forms.ToolStripButton tsbt_Navigate;
        private System.Windows.Forms.ToolStripButton tsbt_Settings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiCopy;
        private System.Windows.Forms.ToolStripMenuItem tsmiCut;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiNewFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmiProperties;
        private System.Windows.Forms.ToolStripMenuItem tsmiPaste;
        private System.Windows.Forms.ToolStripMenuItem tsmiRename;
    }
}