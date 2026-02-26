using BUSINESS_DVLD;
using System;
using System.CodeDom;
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
    public partial class ShowLicense : Form
    {
    
        int licenseid;
        int personid;
        int appid;
        clsLicense license;
        public ShowLicense(int licenseid,int personid=-1,int appid=-1)
        {
            this.licenseid = licenseid;
            this.personid = personid;
            this.appid = appid;
            InitializeComponent();
        }
      



        private void ShowLicense_Load(object sender, EventArgs e)
        {
            if (personid == -1&&appid==-1)
            {

            license = clsLicense.Find(licenseid);
            }
            else if(appid==-1)
            {
            license = clsLicense.FindBypersonID(personid);
            }
            else
            {
                license = clsLicense.FindByappID(appid);
            }

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
            lblIssueDate.Text= license.IssueDate.ToString("MM/dd/yyyy");
        
            switch (license.IssueReason) 
            {
                case 1:lblIssueReason.Text = "New License";
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
            lblDateOfBirth.Text = Applications.Personinfo.DateOfBirth.ToString("dd/MM/yyyy");
            lblDriverID.Text= license.DriverID.ToString();
            lblExpirationDate.Text = license.ExpirationDate.ToString("dd/MM/yyyy");
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
