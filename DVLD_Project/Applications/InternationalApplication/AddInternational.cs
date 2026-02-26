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

namespace DVLD_Project.Applications.InternationalApplication
{
    public partial class AddInternational : Form
    {
        public AddInternational()
        {
            InitializeComponent();
            linkLabel1.Enabled = false;
            linkLabel2.Enabled = false;
            button2.Enabled=false;
        uctlShowLicenseWithFiltter1.ButtonClicked += MyUserControl1_ButtonClicked;
        }



        
    

          private void MyUserControl1_ButtonClicked(object sender, EventArgs e)
        {

            if (uctlShowLicenseWithFiltter1.found == false) { return; }
            
            label14.Text = uctlShowLicenseWithFiltter1.license.LicenseID.ToString();


            linkLabel2.Enabled = true;
            button2.Enabled = true;
            app = clsApplications.Find(uctlShowLicenseWithFiltter1.license.ApplicationID);


        }
    

        int id;
        clsApplications app;


        private void AddInternational_Load(object sender, EventArgs e)
        {
            uctlShowLicenseWithFiltter1.FiltterHide = false;
            uctlShowLicenseWithFiltter1.idlicense = 0; // this mean the id in textbox ;
        

            clsApplicationtypes apptype = clsApplicationtypes.Find(6);
            label8.Text = apptype.Applicationtypesfees.ToString();
            label4.Text=DateTime.Now.ToString("dd/MM/yyyy");
            label6.Text = DateTime.Now.ToString("dd/MM/yyyy");
            label13.Text = clsGlobal.CurrentUser.Username;
            label11.Text = DateTime.Now.AddYears(1).ToString("dd/MM/yyyy");
        

     
        }




        private void button2_Click(object sender, EventArgs e)
        {
            int idinternational = clsInternationalLicense.IsExists(uctlShowLicenseWithFiltter1.license.LicenseID);

            


            if (idinternational > 0)
            {

                MessageBox.Show("The Person Already Have Active International License With ID [ "+ idinternational + " ]", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if (uctlShowLicenseWithFiltter1.license.classinfo.LicenseClassID != 3)
            {
                MessageBox.Show(" You cannot issue license with class [ "+ uctlShowLicenseWithFiltter1.license.classinfo.ClassName + " ] only Ordinary class ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (uctlShowLicenseWithFiltter1.license.IsActive == false)
            {
                MessageBox.Show("The Local License is Deactivate You Cannot issue International ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            clsDetainedLicense detainedLicense = clsDetainedLicense.FindByLicenseID(uctlShowLicenseWithFiltter1.license.LicenseID);
            if (detainedLicense != null)
            {

            if (detainedLicense.IsReleased == false)
            {
                MessageBox.Show("The Local License is Detained You Cannot issue International ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            }


            clsApplicationtypes apptype = clsApplicationtypes.Find(6);//  information of Application  international
            clsApplications newAPP = new clsApplications();

            newAPP.ApplicationStatus = 3; // Completed
            newAPP.ApplicantPersonID = app.ApplicantPersonID;
            newAPP.ApplicationDate = DateTime.Now;
            newAPP.LastStatusDate= DateTime.Now;
            newAPP.PaidFees =(float)apptype.Applicationtypesfees;
            newAPP.CreatedByUserID= clsGlobal.CurrentUser.UserID;
            newAPP.ApplicationTypeID = 6;
            if (newAPP.Save() == false) { return; }
            clsInternationalLicense newInternationalLicense = new clsInternationalLicense();
            newInternationalLicense.APPID = newAPP.ApplicationID;
            newInternationalLicense.DriverID = uctlShowLicenseWithFiltter1.license.DriverID;
            newInternationalLicense.IssuedLocalID = uctlShowLicenseWithFiltter1.license.LicenseID;
            newInternationalLicense.IssuedDate = DateTime.Now;
            newInternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);
            newInternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            newInternationalLicense.IsActive = true;
            if(newInternationalLicense.Save() == false) return;
            MessageBox.Show("Data Save Saccessfully with International License ID = [ " + newInternationalLicense.Internationid + " ]", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            label2.Text = newAPP.ApplicationID.ToString();
            label16.Text = newInternationalLicense.Internationid.ToString();
            id= newInternationalLicense.Internationid;
            linkLabel1.Enabled = true;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();  
        }

     

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            ShowInternationalLicense frm = new ShowInternationalLicense(id);
            frm.ShowDialog();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            HistoryPersonform frm = new HistoryPersonform(app.ApplicantPersonID);
            frm.ShowDialog();       
        }

    


 

   

    }
}
