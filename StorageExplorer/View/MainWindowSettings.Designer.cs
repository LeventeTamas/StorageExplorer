namespace StorageExplorer.View
{
    partial class MainWindowSettings
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
            this.btCancel = new System.Windows.Forms.Button();
            this.btOk = new System.Windows.Forms.Button();
            this.lbLanguage = new System.Windows.Forms.Label();
            this.chbHiddenFiles = new System.Windows.Forms.CheckBox();
            this.chbAutosave = new System.Windows.Forms.CheckBox();
            this.cbLanguage = new System.Windows.Forms.ComboBox();
            this.gbFileExplorer = new System.Windows.Forms.GroupBox();
            this.gbSessions = new System.Windows.Forms.GroupBox();
            this.gbGlobal = new System.Windows.Forms.GroupBox();
            this.gbFileExplorer.SuspendLayout();
            this.gbSessions.SuspendLayout();
            this.gbGlobal.SuspendLayout();
            this.SuspendLayout();
            // 
            // btCancel
            // 
            this.btCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btCancel.Location = new System.Drawing.Point(418, 216);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new System.Drawing.Size(100, 32);
            this.btCancel.TabIndex = 2;
            this.btCancel.Text = "Cancel";
            this.btCancel.UseVisualStyleBackColor = true;
            // 
            // btOk
            // 
            this.btOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btOk.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btOk.Location = new System.Drawing.Point(312, 216);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(100, 32);
            this.btOk.TabIndex = 3;
            this.btOk.Text = "Save";
            this.btOk.UseVisualStyleBackColor = true;
            // 
            // lbLanguage
            // 
            this.lbLanguage.AutoSize = true;
            this.lbLanguage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLanguage.Location = new System.Drawing.Point(15, 34);
            this.lbLanguage.Name = "lbLanguage";
            this.lbLanguage.Size = new System.Drawing.Size(76, 18);
            this.lbLanguage.TabIndex = 5;
            this.lbLanguage.Text = "Language:";
            // 
            // chbHiddenFiles
            // 
            this.chbHiddenFiles.AutoSize = true;
            this.chbHiddenFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbHiddenFiles.Location = new System.Drawing.Point(22, 23);
            this.chbHiddenFiles.Name = "chbHiddenFiles";
            this.chbHiddenFiles.Size = new System.Drawing.Size(142, 22);
            this.chbHiddenFiles.TabIndex = 6;
            this.chbHiddenFiles.Text = "Show hidden files";
            this.chbHiddenFiles.UseVisualStyleBackColor = true;
            this.chbHiddenFiles.CheckedChanged += new System.EventHandler(this.ChbHiddenFiles_CheckedChanged);
            // 
            // chbAutosave
            // 
            this.chbAutosave.AutoSize = true;
            this.chbAutosave.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbAutosave.Location = new System.Drawing.Point(22, 23);
            this.chbAutosave.Name = "chbAutosave";
            this.chbAutosave.Size = new System.Drawing.Size(136, 22);
            this.chbAutosave.TabIndex = 7;
            this.chbAutosave.Text = "Enable autosave";
            this.chbAutosave.UseVisualStyleBackColor = true;
            this.chbAutosave.CheckedChanged += new System.EventHandler(this.ChbAutosave_CheckedChanged);
            // 
            // cbLanguage
            // 
            this.cbLanguage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLanguage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLanguage.FormattingEnabled = true;
            this.cbLanguage.Location = new System.Drawing.Point(160, 31);
            this.cbLanguage.Name = "cbLanguage";
            this.cbLanguage.Size = new System.Drawing.Size(337, 26);
            this.cbLanguage.TabIndex = 8;
            this.cbLanguage.SelectedIndexChanged += new System.EventHandler(this.cbLanguage_SelectedIndexChanged);
            // 
            // gbFileExplorer
            // 
            this.gbFileExplorer.Controls.Add(this.chbHiddenFiles);
            this.gbFileExplorer.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFileExplorer.Location = new System.Drawing.Point(15, 155);
            this.gbFileExplorer.Name = "gbFileExplorer";
            this.gbFileExplorer.Size = new System.Drawing.Size(503, 55);
            this.gbFileExplorer.TabIndex = 9;
            this.gbFileExplorer.TabStop = false;
            this.gbFileExplorer.Text = "File explorer windows";
            // 
            // gbSessions
            // 
            this.gbSessions.Controls.Add(this.chbAutosave);
            this.gbSessions.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbSessions.Location = new System.Drawing.Point(15, 91);
            this.gbSessions.Name = "gbSessions";
            this.gbSessions.Size = new System.Drawing.Size(503, 58);
            this.gbSessions.TabIndex = 10;
            this.gbSessions.TabStop = false;
            this.gbSessions.Text = "Sessions";
            // 
            // gbGlobal
            // 
            this.gbGlobal.Controls.Add(this.lbLanguage);
            this.gbGlobal.Controls.Add(this.cbLanguage);
            this.gbGlobal.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbGlobal.Location = new System.Drawing.Point(15, 11);
            this.gbGlobal.Name = "gbGlobal";
            this.gbGlobal.Size = new System.Drawing.Size(503, 74);
            this.gbGlobal.TabIndex = 11;
            this.gbGlobal.TabStop = false;
            this.gbGlobal.Text = "Global settings";
            // 
            // MainWindowSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 258);
            this.Controls.Add(this.gbGlobal);
            this.Controls.Add(this.gbSessions);
            this.Controls.Add(this.gbFileExplorer);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.btCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainWindowSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.gbFileExplorer.ResumeLayout(false);
            this.gbFileExplorer.PerformLayout();
            this.gbSessions.ResumeLayout(false);
            this.gbSessions.PerformLayout();
            this.gbGlobal.ResumeLayout(false);
            this.gbGlobal.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btCancel;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Label lbLanguage;
        private System.Windows.Forms.CheckBox chbHiddenFiles;
        private System.Windows.Forms.CheckBox chbAutosave;
        private System.Windows.Forms.ComboBox cbLanguage;
        private System.Windows.Forms.GroupBox gbFileExplorer;
        private System.Windows.Forms.GroupBox gbSessions;
        private System.Windows.Forms.GroupBox gbGlobal;
    }
}