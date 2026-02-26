using DVLD_Project.Properties;
using DVLD_Project.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Project.Global;
using System.Security.Cryptography;
using DVLD_Project.Applications.Application_Type;
using DVLD_Project.Test.TestTypes;
using DVLD_Project.Applications.LocalApplication;
using DVLD_Project.Drivers;
using DVLD_Project.Applications.InternationalApplication;
using DVLD_Project.Applications.RenewApplication;
using DVLD_Project.Applications.ReplaceFromDamageAndLost;
using DVLD_Project.DetainedLicense;

namespace DVLD_Project
{
    public partial class MainScreen : Form
    {
        LogInScreen _login;
        public MainScreen(LogInScreen frm)
        {
            InitializeComponent();

            _login = frm;

            button14.Visible = false;
            button15.Visible = false;
            button16.Visible = false;
            pictureBox20.Visible = false;
            pictureBox22.Visible = false;
            pictureBox21.Visible = false;

            paapp.Visible = false;
            paServices.Visible = false;
            patypeoflicenses.Visible = false;
            paDetained.Visible = false;
            paManageapp.Visible = false;

        }

        private bool TestVisible()
        {

            if (pictureBox20.Visible == true || pictureBox21.Visible == true || pictureBox22.Visible == true



          || paServices.Visible == true || paapp.Visible == true || paDetained.Visible == true || paManageapp.Visible == true

            )

            { return true; }

            return false;


        }

        private void button_MouseLeave(object sender, EventArgs e)
        {
            MouseLeave((Button)sender);

        }
        private void button_Click(object sender, EventArgs e)
        {
            if (TestVisible() == true) { return; }
            Click((Button)sender);

        }
        private void button_MouseEnter(object sender, EventArgs e)
        {
            MouseEnter((Button)sender);
        }
        private void button_Leave(object sender, EventArgs e)
        {
            Leave((Button)sender);
        }


        private void MouseLeave(Button BTN)
        {
            BTN.ForeColor = Color.White;
            BTN.Font.Bold.Equals(false);
            BTN.Font = new Font(BTN.Font.FontFamily, 12);
        }
        private void Click(Button BTN)
        {
            BTN.ForeColor = Color.Black;
            BTN.Font = new Font(BTN.Font.FontFamily, 13);
            BTN.Image = Resources.GOLD;
            change_backGroundPictuer_gold(BTN);


            if (BTN.Tag.ToString() == "8")
            {
                button14.Visible = true;
                button15.Visible = true;
                button16.Visible = true;
                pictureBox20.Visible = true;
                pictureBox22.Visible = true;
                pictureBox21.Visible = true;
            }
            if (BTN.Tag.ToString() == "11")
            {
                paapp.Visible = true;


            }
            if (BTN.Tag.ToString() == "10")
            {
                ManagePeople frm = new ManagePeople();
                frm.ShowDialog();

                Leave(btnpeople);
            }
            if(BTN.Tag.ToString() == "9")
            {
                ManageUsers frm = new ManageUsers();
                frm.ShowDialog();
                Leave(btnuser);

            }
            if (BTN.Tag.ToString() == "7")
            {
                ListDrivers frm = new ListDrivers();

                frm.ShowDialog();
                Leave(btndrive);

            }


        }
        private void MouseEnter(Button BTN)
        {
            BTN.ForeColor = Color.Gold;
            BTN.Font.Bold.Equals(true);
            BTN.Font = new Font(BTN.Font.FontFamily, 13);
        }
        private void Leave(Button BTN)
        {


            if (TestVisible() == true)
            {
                return;
            }
            BTN.ForeColor = Color.White;
            BTN.Font = new Font(BTN.Font.FontFamily, 12);
            BTN.Image = Resources.iconsyria2;
            change_backGroundPictuer_zete(BTN);


            if (BTN.Tag.ToString() == "8")
            {
                button14.Visible = false;
                button15.Visible = false;
                button16.Visible = false;
                pictureBox20.Visible = false;
                pictureBox22.Visible = false;
                pictureBox21.Visible = false;
            }

            if (BTN.Tag.ToString() == "11")
            {
                paapp.Visible = false;
            }


        }







        private void change_backGroundPictuer_gold(Button btn)
        {


            // i saved data in tag on each button
            switch (btn.Tag.ToString()) 
            {
                case "8":
                    pictureBox8.BackgroundImage = Resources.GOLD;
                    break;
                case "9":
                    pictureBox9.BackgroundImage = Resources.GOLD;
                    break;
                case "10":
                    pictureBox10.BackgroundImage = Resources.GOLD;
                    break;
                case "7":
                    pictureBox7.BackgroundImage = Resources.GOLD;
                    break;
                case "11":
                    pictureBox11.BackgroundImage = Resources.GOLD;
                    break;

            }

        }
        private void change_backGroundPictuer_zete(Button btn)
        {

            switch (btn.Tag.ToString())
            {
                case "8":
                    pictureBox8.BackgroundImage = Resources.iconsyria2;
                    break;
                case "9":
                    pictureBox9.BackgroundImage = Resources.iconsyria2;
                    break;
                case "10":
                    pictureBox10.BackgroundImage = Resources.iconsyria2;
                    break;
                case "7":
                    pictureBox7.BackgroundImage = Resources.iconsyria2;
                    break;
                case "11":
                    pictureBox11.BackgroundImage = Resources.iconsyria2;
                    break;

            }




        }






        // all buttons

        private void button16_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            this.Hide();
            this.Close();
           _login.Show();
           
        }

        private void button15_Click(object sender, EventArgs e)
        {
            ChangePasswordForm frm = new ChangePasswordForm(clsGlobal.CurrentUser.PersonID, clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            ShowUserInformation frm = new ShowUserInformation(clsGlobal.CurrentUser.PersonID, clsGlobal.CurrentUser.UserID);
            frm.ShowDialog();
        }

        private void leavefainlly_Click_1(object sender, EventArgs e)
        {
            paapp.Visible = false;
            Leave(btnapp);
        }

        private void button13_Click_1(object sender, EventArgs e)
        {
            paServices.BringToFront();
            paServices.Visible = true;
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            paManageapp.Visible = true;
            paManageapp.BringToFront();
        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            paDetained.Visible = true;
            paDetained.BringToFront();
        }

        private void pictureBox28_Click_1(object sender, EventArgs e)
        {
            paDetained.Visible = false;

        }

        private void pictureBox26_Click_1(object sender, EventArgs e)
        {
            paManageapp.Visible = false;
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            patypeoflicenses.Visible = true;
            patypeoflicenses.BringToFront();
        }

        private void pictureBox25_Click_1(object sender, EventArgs e)
        {
            paServices.Visible = false;
        }

        private void pictureBox27_Click_1(object sender, EventArgs e)
        {
            patypeoflicenses.Visible = false;
        }

        private void pictureBox1_Click_1(object sender, EventArgs e)
        {
            button14.Visible = false;
            button15.Visible = false;
            button16.Visible = false;
            pictureBox20.Visible = false;
            pictureBox22.Visible = false;
            pictureBox21.Visible = false;
            Leave(btnsetting);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnManageApplicationType_Click(object sender, EventArgs e)
        {
 
            ApplicationTypeList frm = new ApplicationTypeList();
            frm.ShowDialog();

        }

        private void btnManageTestTypes_Click(object sender, EventArgs e)
        {
            TestTypesList frm = new TestTypesList();
            frm.ShowDialog();
        }

        private void btnLocalLicenseApplications_Click(object sender, EventArgs e)
        {
            ListLocalApplicationlicense frm = new ListLocalApplicationlicense();
            frm.ShowDialog();
        }

        private void btnInternationalLicenseApplications_Click(object sender, EventArgs e)
        {
            ListInternationalLicenses frm = new ListInternationalLicenses();
            frm.ShowDialog();
        }

        private void btnReplacementforLostorDamagedLicense_Click(object sender, EventArgs e)
        {
            ReplaceFromDamageORLost frm = new ReplaceFromDamageORLost();
            frm.ShowDialog();

        }

        private void btnRenewDrivingLicense_Click(object sender, EventArgs e)
        {
            RenewLicenses ffem = new RenewLicenses();
            ffem.ShowDialog();
        }

        private void btnRelaseDetaunedDrivingLicense_Click(object sender, EventArgs e)
        {
            RelasedLicenseForm FRM = new RelasedLicenseForm();
            FRM.ShowDialog();
           
        }

        private void btnRetakeTest_Click(object sender, EventArgs e)
        {
            ListLocalApplicationlicense frm = new ListLocalApplicationlicense();
            frm.ShowDialog();
        }

        private void btnLocalLicense_Click(object sender, EventArgs e)
        {
            AddAndEditApplicationLocal frm = new AddAndEditApplicationLocal(-1);
            frm.ShowDialog();
        }

        private void btnInternationalLicense_Click(object sender, EventArgs e)
        {
            AddInternational frm = new AddInternational();
            frm.ShowDialog();
            
        }

        private void btnManageDetainedLicenses_Click_1(object sender, EventArgs e)
        {
            ListDetainedLicenses frm = new ListDetainedLicenses();  
            frm.ShowDialog();

        }

        private void btnDetainLicense_Click_1(object sender, EventArgs e)
        {
            DetainedLicenses frm = new DetainedLicenses();
            frm.ShowDialog();
        }

        private void btnReleaseDetainedLicense_Click_1(object sender, EventArgs e)
        {
            RelasedLicenseForm FRM = new RelasedLicenseForm();
            FRM.ShowDialog();
           
        }
    }
}
