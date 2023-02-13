
namespace DemoUpdate
{
    partial class frmDownLoadAndInstall
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
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblCurrentFile = new System.Windows.Forms.Label();
            this.lblPercent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 122);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(776, 31);
            this.progressBar1.TabIndex = 0;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(390, 9);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(35, 13);
            this.lblStatus.TabIndex = 1;
            this.lblStatus.Text = "label1";
            // 
            // lblCurrentFile
            // 
            this.lblCurrentFile.AutoSize = true;
            this.lblCurrentFile.Location = new System.Drawing.Point(16, 90);
            this.lblCurrentFile.Name = "lblCurrentFile";
            this.lblCurrentFile.Size = new System.Drawing.Size(35, 13);
            this.lblCurrentFile.TabIndex = 2;
            this.lblCurrentFile.Text = "label2";
            // 
            // lblPercent
            // 
            this.lblPercent.AutoSize = true;
            this.lblPercent.Location = new System.Drawing.Point(16, 103);
            this.lblPercent.Name = "lblPercent";
            this.lblPercent.Size = new System.Drawing.Size(35, 13);
            this.lblPercent.TabIndex = 3;
            this.lblPercent.Text = "label3";
            // 
            // frmDownLoadAndInstall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 181);
            this.Controls.Add(this.lblPercent);
            this.Controls.Add(this.lblCurrentFile);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.progressBar1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDownLoadAndInstall";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmDownLoadAndInstall";
            this.Shown += new System.EventHandler(this.frmDownLoadAndInstall_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblCurrentFile;
        private System.Windows.Forms.Label lblPercent;
    }
}