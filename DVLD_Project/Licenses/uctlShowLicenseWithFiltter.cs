using BUSINESS_DVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Licenses
{
    public partial class uctlShowLicenseWithFiltter : UserControl
    {
        public  clsLicense license;
        public bool FiltterHide;
        public int idlicense;
        public bool found = false;
        public uctlShowLicenseWithFiltter()
        {
            InitializeComponent();
        pictureBox14.Click += MyButton_Click;
        }

        public event EventHandler ButtonClicked;
        public void pictureBox14_Click_1(object sender, EventArgs e)
        {
            loaddatalicenseinfo();
        }
    

        private void MyButton_Click(object sender, EventArgs e)
        {
            ButtonClicked?.Invoke(this, EventArgs.Empty);
        }

        public void loaddatalicenseinfo()
        {
           if (FiltterHide == false)
            {

            if (int.TryParse(textBox1.Text.ToString(), out int id))
            {

                license = clsLicense.Find(id);
                if (license == null)
                {
                        found = false;
                        MessageBox.Show("There no any License with ID [ " + id + " ]", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                    found = false;
                    MessageBox.Show("Please enter Number only", "Erorr", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
      }
            else
            {
                license = clsLicense.Find(idlicense);
            }

            found = true;
            clsApplications Applications = clsApplications.Find(license.ApplicationID);

            clsDetainedLicense detainedLicense = clsDetainedLicense.FindByLicenseID(license.LicenseID);


            lblClass.Text = license.classinfo.ClassName;
            lblFullName.Text = Applications.Personinfo.FirstName.ToString() + " " + Applications.Personinfo.SecondName.ToString() + " " + Applications.Personinfo.ThirdName.ToString() + " " + Applications.Personinfo.LastName.ToString();
            lblLicenseID.Text = license.LicenseID.ToString();
            lblNationalNo.Text = Applications.Personinfo.NationalNo.ToString();
            if (Applications.Personinfo.Gendor == 0)
            {
                lblGendor.Text = "Male";

            }
            else
            {
                lblGendor.Text = "Female";

            }
            lblIssueDate.Text = license.IssueDate.ToString("MM/dd/yyyy");

            switch (license.IssueReason)
            {
                case 1:
                    lblIssueReason.Text = "New License";
                    break;
                case 2:
                    lblIssueReason.Text = "Renew License";
                    break;
                case 3:
                    lblIssueReason.Text = "Replacement for Damaged";
                    break;
                case 4:
                    lblIssueReason.Text = "Replacement for Lost";
                    break;
            }

            lblNotes.Text = license.Notes;

            if (license.IsActive)
            {
                lblIsActive.Text = "Yes";

            }
            else
            {
                lblIsActive.Text = "No";

            }
            lblDateOfBirth.Text = Applications.Personinfo.DateOfBirth.ToString("MM/dd/yyyy");
            lblDriverID.Text = license.DriverID.ToString();
            lblExpirationDate.Text = license.ExpirationDate.ToString("MM/dd/yyyy");
            if (detainedLicense != null)
            {

                if (detainedLicense.IsReleased)
                {
                    lblIsDetained.Text = "No";

                }
                else
                {
                    lblIsDetained.Text = "Yes";

                }
            }
            else
            {
                lblIsDetained.Text = "No";
            }


            pbPersonImage.ImageLocation = Applications.Personinfo.ImagePath;
        }

        public void uctlShowLicenseWithFiltter_Load(object sender, EventArgs e)
        {

            if (FiltterHide == true)
            {
                groupBox1.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void Loadfiltter()
        {

            if (FiltterHide == true)
            {
                groupBox1.Enabled = false;
            }
            else
            {
                groupBox1.Enabled = true;
            }
        }

    }
}
