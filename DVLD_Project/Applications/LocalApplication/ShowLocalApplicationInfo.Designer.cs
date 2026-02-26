namespace DVLD_Project.Applications.LocalApplication
{
    partial class ShowLocalApplicationInfo
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
            this.button1 = new System.Windows.Forms.Button();
            this.uctlShowLocalApplicationInfo1 = new DVLD_Project.Applications.LocalApplication.uctlShowLocalApplicationInfo();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.DeepSkyBlue;
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Image = global::DVLD_Project.Properties.Resources.Close_32;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(703, 411);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(110, 36);
            this.button1.TabIndex = 1;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // uctlShowLocalApplicationInfo1
            // 
            this.uctlShowLocalApplicationInfo1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.uctlShowLocalApplicationInfo1.LocalappID = 0;
            this.uctlShowLocalApplicationInfo1.Location = new System.Drawing.Point(12, 12);
            this.uctlShowLocalApplicationInfo1.Name = "uctlShowLocalApplicationInfo1";
            this.uctlShowLocalApplicationInfo1.Size = new System.Drawing.Size(1099, 467);
            this.uctlShowLocalApplicationInfo1.TabIndex = 0;
            // 
            // ShowLocalApplicationInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(853, 481);
            this.ControlBox = false;
            this.Controls.Add(this.button1);
            this.Controls.Add(this.uctlShowLocalApplicationInfo1);
            this.Name = "ShowLocalApplicationInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ShowLocalApplicationInfo";
            this.ResumeLayout(false);

        }

        #endregion

        private uctlShowLocalApplicationInfo uctlShowLocalApplicationInfo1;
        private System.Windows.Forms.Button button1;
    }
}