using BuisnessDVLD;
using BUSINESS_DVLD;
using DVLD_Project.Global;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Test.TestTries
{
    public partial class TakeTest : Form
    {
        private int appointment;
        private int idlocal;
        bool islocked;

        private clsTestTypes.Etest typetest;
        public TakeTest(clsTestTypes.Etest type,int idlocal,int appointmenttest,bool islocked)
        {
            this.typetest = type;   
            this.idlocal = idlocal;
            this.islocked=islocked;
            this.appointment = appointmenttest;
            InitializeComponent();
            ChangeStatusOfForm(typetest);
            _islocked(islocked);
        }


        private void ChangeStatusOfForm(clsTestTypes.Etest type)
        {

            switch (type)
            {

                case clsTestTypes.Etest.visioTest:
                    pbTestTypeImage.Image = Resources.Vision_512;
                    groupBox1.Text = "Vision Test ";
                    break;
                case clsTestTypes.Etest.writtenTest:
                    pbTestTypeImage.Image = Resources.Written_Test_512;
                    groupBox1.Text = "Written Test ";
                    break;
                case clsTestTypes.Etest.streetTest:
                    pbTestTypeImage.Image = Resources.driving_test_512;
                    groupBox1.Text = "Street Test ";
                    break;


            }




        }
        private void _islocked(bool islock)
        {

            switch (islock)
            {

                case true:

                    lblUserMessage.Visible = true;
                    btnSave.Enabled = false;
                    rbFail.Enabled = false;
                    rbPass.Enabled= false;
                    txtNotes.Enabled = false;
                    break;
                case false:
                    lblUserMessage.Visible = false;
                    btnSave.Enabled =        true;
                    rbFail.Enabled =         true;
                    rbPass.Enabled =         true;
                    txtNotes.Enabled = true;
                    break;


            }




        }



        private void TakeTest_Load(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication localapp = clsLocalDrivingLicenseApplication.Find(idlocal);
            clsApplications app = clsApplications.Find(localapp.ApplicationID);
            clsTestAppointment appointment = clsTestAppointment.Find(this.appointment);

            rbFail.Checked = true;
            lblLocalDrivingLicenseAppID.Text=idlocal.ToString();
            lblDrivingClass.Text = localapp.ClassInfo.ClassName;
            lblFullName.Text = app.Personinfo.FirstName + " " + app.Personinfo.SecondName + " " + app.Personinfo.ThirdName + " " + app.Personinfo.LastName;
            lblTrial.Text =Convert.ToString( clsTestAppointment.isappointmenthere(idlocal, typetest));
            lblDate.Text=appointment.AppointmentDate.ToString();    
            lblFees.Text=appointment.Fees.ToString();
            if (!islocked)
            {
                lblTestID.Text = "Not Taken Yet";
            }
            else
            {
                lblTestID.Text = Convert.ToString(clsTest.IsExists(appointment.AppointmentID));
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            clsTest test=new clsTest();
            test.Notes=txtNotes.Text;
            if (rbFail.Checked)
            {
                test.TestResult = 0;
            }
            else
            {
                test.TestResult = 1;

            }
            test.CreatedByUserID=clsGlobal.CurrentUser.UserID;
            test.TestAppointmentID = appointment;
            if(MessageBox.Show("Are you sure do you want to save this result ? ","Information",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning)== DialogResult.OK)
            {
           if(test.Save())
                {
                    MessageBox.Show("Data save successfully ? ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clsTestAppointment appointment1 = clsTestAppointment.Find(appointment);
                    appointment1.IsLocked = true;
                    appointment1.Save();
                }

            }
            this.Close();
        }
    }
}
