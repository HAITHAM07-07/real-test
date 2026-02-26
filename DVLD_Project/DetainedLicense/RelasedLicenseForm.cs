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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.DetainedLicense
{
    public partial class RelasedLicenseForm : Form
    {
        int licenseid;
        clsApplicationtypes apptype;
        clsDetainedLicense detainedLicense;
        clsApplications app;
        public RelasedLicenseForm(int licenseid = 0)
        {
           this. licenseid = licenseid;
            InitializeComponent();
            linkLabel1.Enabled = false;
            linkLabel2.Enabled = false;
            button2.Enabled = false;
            uctlShowLicenseWithFiltter1.ButtonClicked += MyUserControl1_ButtonClicked;
        }




        private void MyUserControl1_ButtonClicked(object sender, EventArgs e)
        {
            
            if(uctlShowLicenseWithFiltter1.found == true)
            {
                button2.Enabled=true;
                label16.Text = uctlShowLicenseWithFiltter1.license.LicenseID.ToString();
                linkLabel1.Enabled = true;
                linkLabel2.Enabled = true; 
                app = clsApplications.Find(uctlShowLicenseWithFiltter1.license.ApplicationID);
                if (!clsDetainedLicense.IsExists(uctlShowLicenseWithFiltter1.license.LicenseID))
                {
                    label2.Text ="[????]";
                    label11.Text = "[????]";
                    label19.Text = "[????]";
                    label4.Text = "[????]";
                    MessageBox.Show("Select License IS NOT Detained , Choose another one .", "information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button2.Enabled = false;
                    return;
                }

                 detainedLicense = clsDetainedLicense.FindByLicenseIDandisdetained(uctlShowLicenseWithFiltter1.license.LicenseID);
                label2.Text = detainedLicense.DetainedID.ToString();
                label11.Text = detainedLicense.PaidFees.ToString();
                label19.Text = (detainedLicense.PaidFees + apptype.Applicationtypesfees).ToString();
                label4.Text = detainedLicense.DetainedDate.ToString("dd/MM/yyyy");








            }
     

        }
        private void RelasedLicenseForm_Load(object sender, EventArgs e)
        {

            apptype = clsApplicationtypes.Find(5);
            label8.Text = apptype.Applicationtypesfees.ToString();
            label13.Text = clsGlobal.CurrentUser.Username;

            
            if (licenseid > 0)
            {
                uctlShowLicenseWithFiltter1.FiltterHide = true;
                uctlShowLicenseWithFiltter1.Loadfiltter();
                uctlShowLicenseWithFiltter1.idlicense = licenseid;
                uctlShowLicenseWithFiltter1.loaddatalicenseinfo();
             
                
                    button2.Enabled = true;
                    label16.Text = uctlShowLicenseWithFiltter1.license.LicenseID.ToString();
                    linkLabel1.Enabled = true;
                    linkLabel2.Enabled = true;
                    app = clsApplications.Find(uctlShowLicenseWithFiltter1.license.ApplicationID);
                    if (!clsDetainedLicense.IsExists(uctlShowLicenseWithFiltter1.license.LicenseID))
                    {
                        label2.Text = "[????]";
                        label11.Text = "[????]";
                        label19.Text = "[????]";
                        label4.Text = "[????]";
                        MessageBox.Show("Select License IS NOT Detained , Choose another one .", "information ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        button2.Enabled = false;
                        return;
                    }

                    detainedLicense = clsDetainedLicense.FindByLicenseIDandisdetained(uctlShowLicenseWithFiltter1.license.LicenseID);
                    label2.Text = detainedLicense.DetainedID.ToString();
                    label11.Text = detainedLicense.PaidFees.ToString();
                    label19.Text = (detainedLicense.PaidFees + apptype.Applicationtypesfees).ToString();
                    label4.Text = detainedLicense.DetainedDate.ToString("dd/MM/yyyy");
                }

            
        }

     

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //history
            clsApplications app = clsApplications.Find(uctlShowLicenseWithFiltter1.license.ApplicationID);
            HistoryPersonform frm = new HistoryPersonform(app.ApplicantPersonID);
            frm.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // show license
            ShowLicense frm = new ShowLicense(uctlShowLicenseWithFiltter1.license.LicenseID);
            frm.ShowDialog();
        }



        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {

      
            clsApplications newAPP = new clsApplications();

            newAPP.ApplicationStatus = 3; // Completed
            newAPP.ApplicantPersonID = app.ApplicantPersonID;
            newAPP.ApplicationDate = DateTime.Now;
            newAPP.LastStatusDate = DateTime.Now;
            newAPP.PaidFees = (float)apptype.Applicationtypesfees;
            newAPP.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            newAPP.ApplicationTypeID = 5;
            if (newAPP.Save() == false) { return; }

            detainedLicense.IsReleased = true;
            detainedLicense.ReleasDate = DateTime.Now;
            detainedLicense.ReleasByUserID = clsGlobal.CurrentUser.UserID;
            detainedLicense.ReleasApplication = newAPP.ApplicationID;


            if (detainedLicense.Save() == false) return;
            MessageBox.Show("Data Save Saccessfully ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            label15.Text = newAPP.ApplicationID.ToString();
            button2.Enabled = false;
            uctlShowLicenseWithFiltter1.FiltterHide = true ;
            uctlShowLicenseWithFiltter1.Loadfiltter();
        }
    }
}
