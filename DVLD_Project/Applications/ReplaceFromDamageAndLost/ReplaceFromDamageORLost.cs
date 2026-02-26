using BUSINESS_DVLD;
using DVLD_Project.Global;
using DVLD_Project.Licenses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Applications.ReplaceFromDamageAndLost
{
    public partial class ReplaceFromDamageORLost : Form
    {
        public ReplaceFromDamageORLost()
        {
            InitializeComponent();
            linkLabel1.Enabled = false;
            linkLabel2.Enabled = false;
            button2.Enabled = false;
            radioButton1.Checked = true;
            uctlShowLicenseWithFiltter1.ButtonClicked += MyUserControl1_ButtonClicked;

        }

        int id;
        clsApplicationtypes apptype;
        clsApplications app;
        clsLicenseClasses licenseClasses;

        private void MyUserControl1_ButtonClicked(object sender, EventArgs e)
        {
        if(uctlShowLicenseWithFiltter1.found == false) { return; }

            linkLabel2.Enabled = true;
            app = clsApplications.Find(uctlShowLicenseWithFiltter1.license.ApplicationID);
            label14.Text = uctlShowLicenseWithFiltter1.license.LicenseID.ToString();
            licenseClasses = clsLicenseClasses.Find(uctlShowLicenseWithFiltter1.license.LicenseClass);
            button2.Enabled = true;
        
        
        
        
        
        
        }









        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (uctlShowLicenseWithFiltter1.license.IsActive == false)
            {
                MessageBox.Show("this License is deactivate ,you cannot replace  it ", " information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

            }
            clsApplications applications = new clsApplications();
            applications.ApplicantPersonID = app.ApplicantPersonID;
            applications.ApplicationDate = DateTime.Now;
            applications.PaidFees = (float)apptype.Applicationtypesfees;
            applications.ApplicationStatus = 3;
            applications.ApplicationTypeID = apptype.IDApplicationtypes;
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
            if (radioButton1.Checked)
            {

            license.IssueReason = 3;
            }
            else
            {
                license.IssueReason = 4;
            }
                license.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            if (license.Save())
            {


                uctlShowLicenseWithFiltter1.license.IsActive = false;
                if (!uctlShowLicenseWithFiltter1.license.Save()) { return; }
                clsInternationalLicense internationalLicense = new clsInternationalLicense();
                internationalLicense._updatelicense(uctlShowLicenseWithFiltter1.license.LicenseID);

                MessageBox.Show("Data save saccessfully with id license = ["+license.LicenseID+"]", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);



            }
            linkLabel1.Enabled = true;
            label2.Text = applications.ApplicationID.ToString();
            label16.Text = license.LicenseID.ToString();
            button2.Enabled = false;
            uctlShowLicenseWithFiltter1.FiltterHide = true;
            uctlShowLicenseWithFiltter1.Loadfiltter();
            id=license.LicenseID;















        }

        private void ReplaceFromDamageORLost_Load(object sender, EventArgs e)
        {
            uctlShowLicenseWithFiltter1.FiltterHide = false;
            uctlShowLicenseWithFiltter1.idlicense = 0;// textbox

            apptype = clsApplicationtypes.Find(4);
            label8.Text = apptype.Applicationtypesfees.ToString();
            label4.Text = DateTime.Now.ToString("dd/MM/yyyy");
            label13.Text = clsGlobal.CurrentUser.Username;


        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            apptype = clsApplicationtypes.Find(4);
            label8.Text = apptype.Applicationtypesfees.ToString();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            apptype = clsApplicationtypes.Find(3);
            label8.Text = apptype.Applicationtypesfees.ToString();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            HistoryPersonform frm = new HistoryPersonform(app.ApplicantPersonID);
            frm.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowLicense frm = new ShowLicense(id);
            frm .ShowDialog();
        }
    }
}
