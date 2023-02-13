
namespace DemoUpdate
{
    partial class frmMessageBoxConnect
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
            this.lblNotifycation = new System.Windows.Forms.Label();
            this.btnTryAgain = new System.Windows.Forms.Button();
            this.txtDetailError = new System.Windows.Forms.TextBox();
            this.btnContinue = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblNotifycation
            // 
            this.lblNotifycation.AutoSize = true;
            this.lblNotifycation.Location = new System.Drawing.Point(12, 9);
            this.lblNotifycation.Name = "lblNotifycation";
            this.lblNotifycation.Size = new System.Drawing.Size(365, 13);
            this.lblNotifycation.TabIndex = 1;
            this.lblNotifycation.Text = "Error Connect! Please check your connection to update to the latest version";
            // 
            // btnTryAgain
            // 
            this.btnTryAgain.Location = new System.Drawing.Point(297, 34);
            this.btnTryAgain.Name = "btnTryAgain";
            this.btnTryAgain.Size = new System.Drawing.Size(75, 23);
            this.btnTryAgain.TabIndex = 7;
            this.btnTryAgain.Text = "Try Again";
            this.btnTryAgain.UseVisualStyleBackColor = true;
            this.btnTryAgain.Click += new System.EventHandler(this.btnTryAgain_Click);
            // 
            // txtDetailError
            // 
            this.txtDetailError.Location = new System.Drawing.Point(10, 63);
            this.txtDetailError.Multiline = true;
            this.txtDetailError.Name = "txtDetailError";
            this.txtDetailError.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDetailError.Size = new System.Drawing.Size(464, 144);
            this.txtDetailError.TabIndex = 6;
            // 
            // btnContinue
            // 
            this.btnContinue.Location = new System.Drawing.Point(378, 34);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(75, 23);
            this.btnContinue.TabIndex = 5;
            this.btnContinue.Text = "Continue";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // frmMessageBoxConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 232);
            this.Controls.Add(this.btnTryAgain);
            this.Controls.Add(this.txtDetailError);
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.lblNotifycation);
            this.Name = "frmMessageBoxConnect";
            this.Text = "frmMessageBoxConnect";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNotifycation;
        private System.Windows.Forms.Button btnTryAgain;
        private System.Windows.Forms.TextBox txtDetailError;
        private System.Windows.Forms.Button btnContinue;
    }
}