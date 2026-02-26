using BUSINESS_DVLD;
using DVLD_Project.Global;
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
    public partial class IssueLicenseFirstTimeLocal : Form
    {
        int localid;
        public IssueLicenseFirstTimeLocal(int localid)
        {
            InitializeComponent();
            this.localid=localid;
            uctlShowLocalApplicationInfo1.LocalappID = localid;
            uctlShowLocalApplicationInfo1.LoadData();
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.Find(localid);
            clsApplications app = clsApplications.Find(localDrivingLicenseApplication.ApplicationID);
            clsLicenseClasses licenseclass = clsLicenseClasses.Find(localDrivingLicenseApplication.LicenseClassID);

            int isdriverthere  =  clsDriver.IsExists(app.Personinfo.PersonID);
         
          
            if (isdriverthere <= 0)
            {
            clsDriver drivernew = new clsDriver();
            drivernew.PersonID = app.Personinfo.PersonID;
            drivernew.CreateByUserID = clsGlobal.CurrentUser.UserID;
            drivernew.CreateDate = DateTime.Now;
            if (!drivernew.Save())
            {
                return;
            }

            }
          

            clsLicense newlicense = new clsLicense();

            newlicense.ApplicationID = app.ApplicationID;
            if (isdriverthere < 0)
            {
                clsDriver dr = clsDriver.Find(app.Personinfo.PersonID);
                newlicense.DriverID = dr.DriverID;
            }
            else
            {
                newlicense.DriverID = isdriverthere;

            }
            newlicense.LicenseClass = localDrivingLicenseApplication.LicenseClassID;
            newlicense.IssueDate = DateTime.Now;
            newlicense.ExpirationDate = DateTime.Now.AddYears(licenseclass.DefaultValidityLength);
            newlicense.Notes = txtNotes.Text;
            newlicense.PaidFees =(decimal) licenseclass.ClassFees;
            newlicense.IsActive = true;
            newlicense.IssueReason = 1;// issue first time
            newlicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            if (newlicense.Save())
            {
                app.ApplicationStatus = 3;
                app.Save();
                MessageBox.Show("License issued saccessfully with LicenseID = " + newlicense.LicenseID, "information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }

        }
    }
}
