using BiusnessDVLD;
using BuisnessDVLD;
using BUSINESS_DVLD;
using DVLD_Project.Global;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Test.TestTries
{
    public partial class AddEditAppointments : Form
    {

         private    int idlocalapp;
         private    clsTestTypes.Etest Type;
         private    int testappointmentid;
         private    bool isretake;
         private    int retaked;


        clsLocalDrivingLicenseApplication localapp;
        clsApplications app;
        clsApplications appretake;
        clsTestAppointment testAppointment;
        clsApplicationtypes apptype;
        clsTestTypes testType;



        enum emode  {addmode= 1,editmode =2};
        emode Emode;



        public AddEditAppointments(int idlocal,clsTestTypes.Etest type,int idappointment,bool islocked)
        {
            InitializeComponent();
            
             testappointmentid = idappointment;
             Type = type;
             idlocalapp = idlocal;
            this. retaked = clsTestAppointment.isappointmenthere(idlocalapp, Type);
            if (idappointment ==-1) 
            {

                testAppointment = new clsTestAppointment();
                Emode = emode.addmode;
            }
            else                   
            {
                Emode = emode.editmode;
                testAppointment         = clsTestAppointment.Find(testappointmentid);
            }


                localapp = clsLocalDrivingLicenseApplication.Find(idlocalapp);
                app                        = clsApplications.Find(localapp.ApplicationID);
                apptype                = clsApplicationtypes.Find(7);//retake application
                testType                      = clsTestTypes.Find(Type);
            ChangeStatusOfForm(type);
            isretaketest();
            islockedmethod(islocked);
      

        }

       


        private void ChangeStatusOfForm(clsTestTypes.Etest type)
        {

            switch (type)
            {

                case clsTestTypes.Etest.visioTest:
                    pictureBox1.Image = Resources.Vision_512;
                    groupBox1.Text = "Vision Test ";
                    break;
                case clsTestTypes.Etest.writtenTest:
                    pictureBox1.Image = Resources.Written_Test_512;
                    groupBox1.Text = "Written Test";
                    break;
                case clsTestTypes.Etest.streetTest:
                    pictureBox1.Image = Resources.driving_test_512;
                    groupBox1.Text = "Street Test ";
                    break;


            }




        }

        private void islockedmethod(bool locK)
        {

            if (locK)
            {
                labeltaken.Visible = true;
                button1.Enabled = false;
                dateTimePicker1.Enabled = false;
            }
            else
            {
                labeltaken.Visible = false;
                button1.Enabled = true;
                dateTimePicker1.Enabled = true;
            }

            




        }

        private void isretaketest()
        {

            if (retaked >= 1 )
            {
                label1.Text = "Schedule Retake Test";
                isretake = true;
                groupBox2.Enabled = true;
                appretake = new clsApplications();
            }
            else
            {
                label1.Text = "Schedule Test";
                isretake = false;
                groupBox2.Enabled = false;
            }






        }






        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddEditAppointments_Load(object sender, EventArgs e)
        {




            label3.Text = localapp.LocalDrivingLicenseApplicationID.ToString();
            label4.Text = localapp.ClassInfo.ClassName;
            label5.Text =  app.Personinfo.FirstName + " " + app.Personinfo.SecondName + " " + app.Personinfo.ThirdName;
            label19.Text = app.Personinfo.LastName;
            label6.Text = clsTestAppointment.isappointmenthere(idlocalapp, Type).ToString();
            if(Emode == emode.addmode)
            {
                dateTimePicker1.Text = DateTime.Now.ToString();
            }
            else
            {
                dateTimePicker1.Text = testAppointment.AppointmentDate.ToString();
            }

            label8.Text = testType.TestTypesfees.ToString();
            if(Emode == emode.addmode&&isretake==false)
            {
                label16.Text = "[???]";
                label17.Text ="0";
                label15.Text = Convert.ToString(testType.TestTypesfees + 0);
            }
            else
            {
                if (testAppointment.RetakeID == "")
                {
                    label16.Text = "[???]";
                }
                else
                {
                    label16.Text = testAppointment.RetakeID.ToString();

                }

            label17.Text = apptype.Applicationtypesfees.ToString();
            label15.Text = Convert.ToString(testType.TestTypesfees + apptype.Applicationtypesfees);

            }
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
      
            if (isretake)
            {

            appretake.ApplicantPersonID = app.ApplicantPersonID;
            appretake.ApplicationDate = DateTime.Now;
            appretake.ApplicationTypeID = 7;
            appretake.ApplicationStatus = 3;
            appretake.LastStatusDate = DateTime.Now;
            appretake.PaidFees = (float)apptype.Applicationtypesfees;
            appretake.CreatedByUserID = clsGlobal.CurrentUser.UserID;
               if (!appretake.Save())
                {
                    return;
                }
            }

          
            testAppointment.Testtypeid = Convert.ToInt32(Type);
            testAppointment.localappid = idlocalapp;
            testAppointment.AppointmentDate = dateTimePicker1.Value;
            testAppointment.Fees = testType.TestTypesfees;
            testAppointment.createbyuser = clsGlobal.CurrentUser.UserID;
            if (Emode == emode.addmode)
            {
                testAppointment.IsLocked = false;
            }
            else
            {
                testAppointment.IsLocked = testAppointment.IsLocked;
            }
            if (isretake)
            {

                testAppointment.RetakeID = appretake.ApplicationID.ToString();
            }
            else
            {
                testAppointment.RetakeID = "";
            }
            if (testAppointment.Save())
            {
                MessageBox.Show("the data save saccessfully", "Information !", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Close();
        }



    }
}
