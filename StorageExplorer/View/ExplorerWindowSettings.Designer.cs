namespace StorageExplorer.View
{
    partial class ExplorerWindowSettings
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
            this.lbFontColor = new System.Windows.Forms.Label();
            this.lbFontSize = new System.Windows.Forms.Label();
            this.lbFontFamily = new System.Windows.Forms.Label();
            this.lbBackColor = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.btnFontColor = new System.Windows.Forms.Button();
            this.btnBackColor = new System.Windows.Forms.Button();
            this.numFontSize = new System.Windows.Forms.NumericUpDown();
            this.cbFontFamily = new System.Windows.Forms.ComboBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).BeginInit();
            this.SuspendLayout();
            // 
            // lbFontColor
            // 
            this.lbFontColor.AutoSize = true;
            this.lbFontColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFontColor.Location = new System.Drawing.Point(13, 79);
            this.lbFontColor.Name = "lbFontColor";
            this.lbFontColor.Size = new System.Drawing.Size(80, 18);
            this.lbFontColor.TabIndex = 0;
            this.lbFontColor.Text = "Font color:";
            // 
            // lbFontSize
            // 
            this.lbFontSize.AutoSize = true;
            this.lbFontSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFontSize.Location = new System.Drawing.Point(12, 47);
            this.lbFontSize.Name = "lbFontSize";
            this.lbFontSize.Size = new System.Drawing.Size(73, 18);
            this.lbFontSize.TabIndex = 1;
            this.lbFontSize.Text = "Font size:";
            // 
            // lbFontFamily
            // 
            this.lbFontFamily.AutoSize = true;
            this.lbFontFamily.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFontFamily.Location = new System.Drawing.Point(12, 15);
            this.lbFontFamily.Name = "lbFontFamily";
            this.lbFontFamily.Size = new System.Drawing.Size(84, 18);
            this.lbFontFamily.TabIndex = 2;
            this.lbFontFamily.Text = "Font family:";
            // 
            // lbBackColor
            // 
            this.lbBackColor.AutoSize = true;
            this.lbBackColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbBackColor.Location = new System.Drawing.Point(13, 112);
            this.lbBackColor.Name = "lbBackColor";
            this.lbBackColor.Size = new System.Drawing.Size(130, 18);
            this.lbBackColor.TabIndex = 3;
            this.lbBackColor.Text = "Background color:";
            // 
            // btnFontColor
            // 
            this.btnFontColor.BackColor = System.Drawing.Color.Black;
            this.btnFontColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFontColor.ForeColor = System.Drawing.Color.Black;
            this.btnFontColor.Location = new System.Drawing.Point(149, 75);
            this.btnFontColor.Name = "btnFontColor";
            this.btnFontColor.Size = new System.Drawing.Size(75, 27);
            this.btnFontColor.TabIndex = 4;
            this.btnFontColor.UseVisualStyleBackColor = false;
            this.btnFontColor.Click += new System.EventHandler(this.btnFontColor_Click);
            // 
            // btnBackColor
            // 
            this.btnBackColor.BackColor = System.Drawing.Color.White;
            this.btnBackColor.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackColor.ForeColor = System.Drawing.Color.White;
            this.btnBackColor.Location = new System.Drawing.Point(149, 108);
            this.btnBackColor.Name = "btnBackColor";
            this.btnBackColor.Size = new System.Drawing.Size(75, 27);
            this.btnBackColor.TabIndex = 5;
            this.btnBackColor.UseVisualStyleBackColor = false;
            this.btnBackColor.Click += new System.EventHandler(this.btnBackColor_Click);
            // 
            // numFontSize
            // 
            this.numFontSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numFontSize.Location = new System.Drawing.Point(149, 45);
            this.numFontSize.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numFontSize.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numFontSize.Name = "numFontSize";
            this.numFontSize.Size = new System.Drawing.Size(75, 24);
            this.numFontSize.TabIndex = 6;
            this.numFontSize.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numFontSize.ValueChanged += new System.EventHandler(this.numFontSize_ValueChanged);
            // 
            // cbFontFamily
            // 
            this.cbFontFamily.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cbFontFamily.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.cbFontFamily.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFontFamily.FormattingEnabled = true;
            this.cbFontFamily.Location = new System.Drawing.Point(149, 12);
            this.cbFontFamily.Name = "cbFontFamily";
            this.cbFontFamily.Size = new System.Drawing.Size(215, 26);
            this.cbFontFamily.TabIndex = 7;
            this.cbFontFamily.SelectedIndexChanged += new System.EventHandler(this.cbFontFamily_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(162, 152);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 32);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(268, 152);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 32);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // ExplorerWindowSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 194);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cbFontFamily);
            this.Controls.Add(this.numFontSize);
            this.Controls.Add(this.btnBackColor);
            this.Controls.Add(this.btnFontColor);
            this.Controls.Add(this.lbBackColor);
            this.Controls.Add(this.lbFontFamily);
            this.Controls.Add(this.lbFontSize);
            this.Controls.Add(this.lbFontColor);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ExplorerWindowSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "File explorer window settings";
            this.Load += new System.EventHandler(this.ExplorerWindowSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numFontSize)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbFontColor;
        private System.Windows.Forms.Label lbFontSize;
        private System.Windows.Forms.Label lbFontFamily;
        private System.Windows.Forms.Label lbBackColor;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button btnFontColor;
        private System.Windows.Forms.Button btnBackColor;
        private System.Windows.Forms.NumericUpDown numFontSize;
        private System.Windows.Forms.ComboBox cbFontFamily;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
    }
}