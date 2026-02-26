using BiusnessDVLD;
using BUSINESS_DVLD;
using DVLD_Project.Licenses;
using DVLD_Project.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Applications.LocalApplication
{
    public partial class uctlShowLocalApplicationInfo : UserControl
    {
        public uctlShowLocalApplicationInfo()
        {
            InitializeComponent();
            linkLabel1.Enabled = false;
        }

         public int  LocalappID { get; set; }
         int personid;
         private clsLocalDrivingLicenseApplication applocal;
         private clsLocalDrivingLicenseApplications_View localapppassed;
        public void LoadData()
        {
            applocal = clsLocalDrivingLicenseApplication.Find(LocalappID);
            if (applocal != null) 
            {
                label24.Text = applocal.LocalDrivingLicenseApplicationID.ToString();

                label17.Text=applocal.ClassInfo.ClassName.ToString();


                localapppassed = clsLocalDrivingLicenseApplications_View.Find(LocalappID);
                label9.Text= localapppassed.passedtestcount.ToString() + "/3";


                bool HaveLicense = clsLicense.IsExists(applocal.ApplicationID);
                if (HaveLicense) 
                {
                    linkLabel1.Enabled = true;
                }


                label2.Text=applocal.ApplicationID.ToString();
                // app info
                clsApplications application = clsApplications.Find(applocal.ApplicationID);

                if (application.ApplicationStatus == 3)
                {
                    label4.Text = "Complete";

                }
                else if (application.ApplicationStatus == 2) 
                {
                    label4.Text = "Cancelled";
                }
                else
                {
                    label4.Text = "New";
                }


                label6.Text = application.PaidFees.ToString();



                clsApplicationtypes applicationtypes = clsApplicationtypes.Find(application.ApplicationTypeID);
                label8.Text = applicationtypes.ApplicationtypesName.ToString();


                label13.Text = application.Personinfo.FirstName+" " + application.Personinfo.SecondName + " " + application.Personinfo.ThirdName + " " + application.Personinfo.LastName;

                label15.Text = application.ApplicationDate.ToString("dd/MM/yyyy");

                label20.Text = application.LastStatusDate.ToString("dd/MM/yyyy");

                clsUser user = clsUser.Find(application.CreatedByUserID);

                label14.Text = user.Username;

                personid = application.ApplicantPersonID;

            }


        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
                ShowPersonDetiles frm = new ShowPersonDetiles(personid);
                frm.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsLicense idlicense = clsLicense.FindByappID(applocal.ApplicationID);


            ShowLicense showLicense = new ShowLicense(idlicense.LicenseID);
            showLicense.ShowDialog();
        }
    }
}
