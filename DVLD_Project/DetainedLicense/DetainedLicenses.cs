using BUSINESS_DVLD;
using DVLD_Project.Global;
using DVLD_Project.Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD_Project.DetainedLicense
{
    public partial class DetainedLicenses : Form
    {

        public DetainedLicenses()
        {
            InitializeComponent();
            linkLabel1.Enabled = false;
            linkLabel2.Enabled = false;
            button2.Enabled = false;
            uctlShowLicenseWithFiltter1.ButtonClicked += MyUserControl1_ButtonClicked;
        }

        private void MyUserControl1_ButtonClicked(object sender, EventArgs e)
        {

            if (uctlShowLicenseWithFiltter1.found == true)
            {

                if (uctlShowLicenseWithFiltter1.license.IsActive == false)
                {
                    MessageBox.Show("the license is deactivate you cannot detained it !","information",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                button2.Enabled = true;
                label16.Text = uctlShowLicenseWithFiltter1.license.LicenseID.ToString();
                linkLabel1.Enabled = true;
                linkLabel2.Enabled = true;
                if (clsDetainedLicense.IsExists(uctlShowLicenseWithFiltter1.license.LicenseID))
                {
                    label2.Text = "[????]";
                    MessageBox.Show("Select License IS Detained already, Choose another one .", "information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button2.Enabled = false;
                    return;
                }
             
            }


        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsApplications app = clsApplications.Find(uctlShowLicenseWithFiltter1.license.ApplicationID);
            HistoryPersonform frm = new HistoryPersonform(app.ApplicantPersonID);
            frm.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowLicense frm = new ShowLicense(uctlShowLicenseWithFiltter1.license.LicenseID);
            frm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DetainedLicenses_Load(object sender, EventArgs e)
        {
            label13.Text = clsGlobal.CurrentUser.Username;
            label4.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show(" put mouse in red circle to see the problem ! ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            clsDetainedLicense detainedLicense = new clsDetainedLicense();

            detainedLicense.LicenseID = uctlShowLicenseWithFiltter1.license.LicenseID;
            detainedLicense.DetainedDate = DateTime.Now;
            detainedLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            detainedLicense.PaidFees = Convert.ToDecimal(maskedTextBox1.Text);
            detainedLicense.IsReleased = false;
            detainedLicense.ReleasDate = null;
            detainedLicense.ReleasByUserID = 0;//null
            detainedLicense.ReleasApplication = 0;//null


            if (detainedLicense.Save() == false) return;
            MessageBox.Show("Data Save Saccessfully ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            button2.Enabled = false;
            label2.Text = detainedLicense.DetainedID.ToString();
            uctlShowLicenseWithFiltter1.FiltterHide = true;
            uctlShowLicenseWithFiltter1.Loadfiltter();
        }

        private void maskedTextBox1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(maskedTextBox1.Text))
            {
                errorProvider1.SetError(maskedTextBox1, "should have number here");
                maskedTextBox1.Focus();
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(maskedTextBox1, "");
                e.Cancel = false;
            }
        }
    }
}
