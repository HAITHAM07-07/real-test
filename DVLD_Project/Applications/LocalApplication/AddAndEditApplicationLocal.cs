using BiusnessDVLD;
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

namespace DVLD_Project.Applications.LocalApplication
{
    public partial class AddAndEditApplicationLocal : Form
    {

        clsApplicationtypes apptype = clsApplicationtypes.Find(1);
        clsApplications app;
        clsUser user;
        clsLocalDrivingLicenseApplication localapp;

        int idlocal;
        
        enum Emode { addmode = 1, updatemode = 0 };
        Emode emode = Emode.addmode;


        public AddAndEditApplicationLocal(int number)
        {
            InitializeComponent();
            LicenseClasses_Load();
            idlocal = number;
            if (idlocal == -1) 
            {
                emode = Emode.addmode;
                uctlShowPersonDetilesWithFilter1.isEditMode = false;
                tabApplicationInformation.Enabled = false;
                button4.Enabled = true;
                button1.Enabled = false;
                app = new clsApplications();
                localapp = new clsLocalDrivingLicenseApplication();

            }
            else
            {
                localapp = clsLocalDrivingLicenseApplication.Find(idlocal);
                if (localapp != null)
                {
                    emode = Emode.updatemode;
                    label1.Text = " Edit Local Driving License Application";
                    app = clsApplications.Find(localapp.ApplicationID);
                    uctlShowPersonDetilesWithFilter1.ID = app.ApplicantPersonID;
                    uctlShowPersonDetilesWithFilter1.isEditMode=true;
                    label5.Text = localapp.LocalDrivingLicenseApplicationID.ToString();
                    label4.Text = app.ApplicationDate.ToString("dd/MM/yyyy");
                    comboBox1.SelectedIndex = localapp.LicenseClassID;
                    label8.Text = app.PaidFees.ToString();
                    user = clsUser.Find(app.CreatedByUserID); 
                    label9.Text = user.Username;

                } 



            }
        }





        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(emode == Emode.addmode)
            {
                if (uctlShowPersonDetilesWithFilter1.isfound == false)
                {
                    MessageBox.Show("You have to select a person at First ! ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

            }

            button1.Enabled = true;
            tabApplicationInformation.Enabled = true;
            button3.Enabled = true;
            tabControl1.SelectedIndex = 1;
        }

      

        private void AddAndEditApplicationLocal_Load(object sender, EventArgs e)
        {
            if (emode == Emode.addmode)
            {

                label4.Text = DateTime.Now.ToString("dd/MM/yyyy");
                comboBox1.SelectedIndex = 3;
                label8.Text = apptype.Applicationtypesfees.ToString();
                label9.Text = clsGlobal.CurrentUser.Username;
            }


        }


        void LicenseClasses_Load()
        {
            DataTable dt = new DataTable();
            dt = clsLicenseClasses.GetallLicenseClasses();
            comboBox1.Items.Add("none");
            foreach (DataRow dr in dt.Rows)
            {

                comboBox1.Items.Add(dr["ClassName"]);

            }
          
          }

        private void button4_Click(object sender, EventArgs e)
        { 
            tabControl1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int islicensethere=  clsLocalDrivingLicenseApplication.IsExistslicense(uctlShowPersonDetilesWithFilter1.ID, comboBox1.SelectedIndex);

            if (islicensethere != -1)
            {
                MessageBox.Show("You Have To Change The  Class Type At First This Person Have License the Same class Type  ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBox1.SelectedIndex == 0)
            {
                MessageBox.Show("You Have To Select a Class Type At First !! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int numbersamenumber = clsLocalDrivingLicenseApplication.IsExists(uctlShowPersonDetilesWithFilter1.ID, comboBox1.SelectedIndex);

            if (numbersamenumber != -1)
            {
                MessageBox.Show("You Have To Change The  Class Type At First This Person Have Application the Same class Type with id  [ " + numbersamenumber + "] ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            app.ApplicantPersonID = uctlShowPersonDetilesWithFilter1.ID;
            app.ApplicationTypeID = apptype.IDApplicationtypes;
            if(emode== Emode.addmode)
            {

            app.ApplicationStatus = 1;
            app.LastStatusDate = DateTime.Now;
            app.PaidFees = Convert.ToSingle(apptype.Applicationtypesfees);
            app.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            localapp.LicenseClassID = comboBox1.SelectedIndex;
            }





            if (app.Save()==true )
            {
                localapp.ApplicationID = app.ApplicationID;
                if(localapp.Save() == true)
                {

                MessageBox.Show("Data Save Sussaccfuly", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                emode = Emode.updatemode;
                label1.Text = " Edit Local Driving License Application";
                label5.Text = localapp.LocalDrivingLicenseApplicationID.ToString();
                }
            }


        }





    }
   }

