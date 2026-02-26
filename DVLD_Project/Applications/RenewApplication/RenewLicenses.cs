using BUSINESS_DVLD;
using DVLD_Project.Global;
using DVLD_Project.Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Applications.RenewApplication
{
    public partial class RenewLicenses : Form
    {
        public RenewLicenses()
        {
            InitializeComponent();
            button2.Enabled = false;
            linkLabel1.Enabled = false;
            linkLabel2.Enabled = false;
         uctlShowLicenseWithFiltter1.ButtonClicked += MyUserControl1_ButtonClicked;
        }
    

          private void MyUserControl1_ButtonClicked(object sender, EventArgs e)
        {
                    if(uctlShowLicenseWithFiltter1.license == null) { return; }
            if (uctlShowLicenseWithFiltter1.found == true)
            {
                linkLabel2.Enabled = true;
                label14.Text = uctlShowLicenseWithFiltter1.license.LicenseID.ToString();
                app = clsApplications.Find(uctlShowLicenseWithFiltter1.license.ApplicationID);
                licenseClasses = clsLicenseClasses.Find(uctlShowLicenseWithFiltter1.license.classinfo.LicenseClassID);
                label11.Text = (DateTime.Now.AddYears(licenseClasses.DefaultValidityLength)).ToString("dd/MM/yyyy");
                label8.Text = apptypes.Applicationtypesfees.ToString();
                label19.Text = licenseClasses.ClassFees.ToString();
                label21.Text = ((float)apptypes.Applicationtypesfees + licenseClasses.ClassFees).ToString();
                if(uctlShowLicenseWithFiltter1.license.ExpirationDate >= DateTime.Now)
                {
                    button2.Enabled = false;
                    MessageBox.Show("Select License is not yet expiraed , it will expire on : "+ uctlShowLicenseWithFiltter1.license.ExpirationDate+" ", "Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }

                button2.Enabled = true;

            }
        }
        clsApplications app;
        clsApplicationtypes apptypes;
        clsLicenseClasses licenseClasses;

        int id;
        private void RenewLicenses_Load(object sender, EventArgs e)
        {
            uctlShowLicenseWithFiltter1.FiltterHide = false;
            uctlShowLicenseWithFiltter1.idlicense = 0; // textbox
                apptypes = clsApplicationtypes.Find(2); // renew


            label4.Text = DateTime.Now.ToString("dd/MM/yyyy");
            label6.Text = DateTime.Now.ToString("dd/MM/yyyy");
            label13.Text=clsGlobal.CurrentUser.Username;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            HistoryPersonform frm = new HistoryPersonform(app.ApplicantPersonID);
            frm.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            ShowLicense frm = new ShowLicense(id);
            frm.ShowDialog();   


        }

        private void button2_Click(object sender, EventArgs e)
        {

            if(uctlShowLicenseWithFiltter1.license.IsActive== false)
            {
                MessageBox.Show("this License is deactivate ,you cannot renew it ", " information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }

            clsApplications applications = new clsApplications();
            applications.ApplicantPersonID = app.ApplicantPersonID;
            applications.ApplicationDate = DateTime.Now;
            applications.PaidFees = (float)apptypes.Applicationtypesfees;
            applications.ApplicationStatus = 3;
            applications.ApplicationTypeID = 2;
            applications.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            applications.LastStatusDate = DateTime.Now;
            if (!applications.Save()) { return; }
            clsLicense license = new clsLicense();
            license.ApplicationID = applications.ApplicationID;
            license.DriverID = uctlShowLicenseWithFiltter1.license.DriverID;
            license.LicenseClass = uctlShowLicenseWithFiltter1.license.LicenseClass;
            license.IssueDate = DateTime.Now;
            license.ExpirationDate = (DateTime.Now.AddYears(licenseClasses.DefaultValidityLength));
            license.Notes = textBox1.Text;
            license.PaidFees = (decimal)uctlShowLicenseWithFiltter1.license.classinfo.ClassFees;
            license.IsActive = true;
            license.IssueReason = 2;
            license.CreatedByUserID=clsGlobal.CurrentUser.UserID;
            if (license.Save())
            {


                uctlShowLicenseWithFiltter1.license.IsActive = false;
                if (!uctlShowLicenseWithFiltter1.license.Save()) { return; }
                clsInternationalLicense internationalLicense= new clsInternationalLicense();
                internationalLicense._updatelicense(uctlShowLicenseWithFiltter1.license.LicenseID);

                MessageBox.Show("Data save saccessfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);



            }
            linkLabel1.Enabled = true;
            label2.Text = applications.ApplicationID.ToString() ;
            label16.Text = license.LicenseID.ToString();
            id = license.LicenseID;
            button2.Enabled = false;
            uctlShowLicenseWithFiltter1.FiltterHide = true;
            uctlShowLicenseWithFiltter1.Loadfiltter();

        }
    }
}
