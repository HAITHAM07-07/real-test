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

namespace DVLD_Project.Applications.InternationalApplication
{
    public partial class ShowInternationalLicense : Form
    {
        int iNTERNATIONALID;
        public ShowInternationalLicense(int internationalLicenseID)
        {
            this.iNTERNATIONALID = internationalLicenseID;
            InitializeComponent();
        }

        private void ShowInternationalLicense_Load(object sender, EventArgs e)
        {
            clsInternationalLicense internationalLicense = clsInternationalLicense.Find(iNTERNATIONALID);
            
            if (internationalLicense != null)
            {
            
                clsApplications app= clsApplications.Find(internationalLicense.APPID);

                lblFullName.Text = app.Personinfo.FirstName + " " + app.Personinfo.SecondName + " " + app.Personinfo.ThirdName + " " + app.Personinfo.LastName;
                lblLicenseID.Text = internationalLicense.Internationid.ToString();
                label9.Text = internationalLicense.IssuedLocalID.ToString();
                lblNationalNo.Text = app.Personinfo.NationalNo;
                lblIssueDate.Text = internationalLicense.IssuedDate.ToString("dd/MM/yyyy");
                lblIsActive.Text = internationalLicense.APPID.ToString();
                lblDateOfBirth.Text=app.Personinfo.DateOfBirth.ToString("dd/MM/yyyy");
                lblDriverID.Text = internationalLicense.DriverID.ToString();
                lblExpirationDate.Text = internationalLicense.ExpirationDate.ToString("dd/MM/yyyy");
                label11.Text = internationalLicense.IsActive == true ? "Yes" : "No";
                pbPersonImage.ImageLocation = app.Personinfo.ImagePath;





            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
