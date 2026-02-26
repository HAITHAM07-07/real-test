namespace DVLD_Project.Users
{
    partial class ShowUserInformation
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
            this.uctlShowDetailesUser1 = new DVLD_Project.Users.uctlShowDetailesUser();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // uctlShowDetailesUser1
            // 
            this.uctlShowDetailesUser1.Location = new System.Drawing.Point(1, 58);
            this.uctlShowDetailesUser1.Name = "uctlShowDetailesUser1";
            this.uctlShowDetailesUser1.Personid = 0;
            this.uctlShowDetailesUser1.Size = new System.Drawing.Size(684, 391);
            this.uctlShowDetailesUser1.TabIndex = 0;
            this.uctlShowDetailesUser1.Userid = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(249, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(162, 24);
            this.label1.TabIndex = 1;
            this.label1.Text = "Information User";
            // 
            // ShowUserInformation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.uctlShowDetailesUser1);
            this.Name = "ShowUserInformation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShowUserInformation";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private uctlShowDetailesUser uctlShowDetailesUser1;
        private System.Windows.Forms.Label label1;
    }
}