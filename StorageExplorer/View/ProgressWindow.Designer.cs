namespace StorageExplorer.View
{
    partial class ProgressWindow
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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.lbCounter = new System.Windows.Forms.Label();
            this.lbTotalProgress = new System.Windows.Forms.Label();
            this.lbDestination = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.lbCurrentItem = new System.Windows.Forms.Label();
            this.lbItemProgPerc = new System.Windows.Forms.Label();
            this.lbTotalProgPerc = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 104);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(469, 23);
            this.progressBar1.TabIndex = 0;
            this.progressBar1.Click += new System.EventHandler(this.progressBar1_Click);
            // 
            // progressBar2
            // 
            this.progressBar2.Location = new System.Drawing.Point(12, 153);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(469, 23);
            this.progressBar2.TabIndex = 1;
            this.progressBar2.Click += new System.EventHandler(this.progressBar2_Click);
            // 
            // lbCounter
            // 
            this.lbCounter.AutoSize = true;
            this.lbCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCounter.Location = new System.Drawing.Point(12, 9);
            this.lbCounter.Name = "lbCounter";
            this.lbCounter.Size = new System.Drawing.Size(31, 20);
            this.lbCounter.TabIndex = 2;
            this.lbCounter.Text = "0/0";
            this.lbCounter.Click += new System.EventHandler(this.label1_Click);
            // 
            // lbTotalProgress
            // 
            this.lbTotalProgress.AutoSize = true;
            this.lbTotalProgress.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalProgress.Location = new System.Drawing.Point(12, 130);
            this.lbTotalProgress.Name = "lbTotalProgress";
            this.lbTotalProgress.Size = new System.Drawing.Size(114, 20);
            this.lbTotalProgress.TabIndex = 3;
            this.lbTotalProgress.Text = "Total progress:";
            this.lbTotalProgress.Click += new System.EventHandler(this.label2_Click);
            // 
            // lbDestination
            // 
            this.lbDestination.AutoSize = true;
            this.lbDestination.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbDestination.Location = new System.Drawing.Point(12, 44);
            this.lbDestination.Name = "lbDestination";
            this.lbDestination.Size = new System.Drawing.Size(94, 20);
            this.lbDestination.TabIndex = 4;
            this.lbDestination.Text = "Destination:";
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(216, 194);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(107, 30);
            this.button1.TabIndex = 5;
            this.button1.Text = "Mégse";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // lbCurrentItem
            // 
            this.lbCurrentItem.AutoSize = true;
            this.lbCurrentItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCurrentItem.Location = new System.Drawing.Point(12, 81);
            this.lbCurrentItem.Name = "lbCurrentItem";
            this.lbCurrentItem.Size = new System.Drawing.Size(45, 20);
            this.lbCurrentItem.TabIndex = 6;
            this.lbCurrentItem.Text = "Item:";
            // 
            // lbItemProgPerc
            // 
            this.lbItemProgPerc.AutoSize = true;
            this.lbItemProgPerc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbItemProgPerc.Location = new System.Drawing.Point(487, 105);
            this.lbItemProgPerc.Name = "lbItemProgPerc";
            this.lbItemProgPerc.Size = new System.Drawing.Size(63, 20);
            this.lbItemProgPerc.TabIndex = 7;
            this.lbItemProgPerc.Text = "000.0%";
            // 
            // lbTotalProgPerc
            // 
            this.lbTotalProgPerc.AutoSize = true;
            this.lbTotalProgPerc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbTotalProgPerc.Location = new System.Drawing.Point(487, 155);
            this.lbTotalProgPerc.Name = "lbTotalProgPerc";
            this.lbTotalProgPerc.Size = new System.Drawing.Size(63, 20);
            this.lbTotalProgPerc.TabIndex = 8;
            this.lbTotalProgPerc.Text = "000.0%";
            // 
            // ProgressWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(551, 236);
            this.Controls.Add(this.lbTotalProgPerc);
            this.Controls.Add(this.lbItemProgPerc);
            this.Controls.Add(this.lbCurrentItem);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbDestination);
            this.Controls.Add(this.lbTotalProgress);
            this.Controls.Add(this.lbCounter);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "ProgressWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Copy...";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Label lbCounter;
        private System.Windows.Forms.Label lbTotalProgress;
        private System.Windows.Forms.Label lbDestination;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label lbCurrentItem;
        private System.Windows.Forms.Label lbItemProgPerc;
        private System.Windows.Forms.Label lbTotalProgPerc;
    }
}