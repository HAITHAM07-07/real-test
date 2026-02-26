using BuisnessDVLD;
using DVLD_Project.People;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class uctlShowDetilesPerson : UserControl
    {
        public uctlShowDetilesPerson()
        {
            InitializeComponent();
        }

        public int number { get; set; }

        public string nationalNo { get; set; }


        public bool ISFOUND = false;


        public void LOADDATA()
        {
            clsBuisnessPeople People = clsBuisnessPeople.Find(number);
            if (People != null)
            {

                ISFOUND = true;

        label13.Text = People.PersonID.ToString();
                label5.Text = People.FirstName + "   " + People.SecondName + "   " + People.ThirdName + "   " + People.LastName;
                label14.Text = People.NationalNo;
                if (People.Gendor == 0)
                {
                    label6.Text = "Male";
                    pictureBox3.Image = Resources.Man_32;
                }
                else
                {
                    label6.Text = "Female";
                    pictureBox3.Image = Resources.Woman_32;

                }
                label15.Text = People.Email;
                label7.Text = People.Address;
                label4.Text = People.DateOfBirth.ToString();
                label20.Text = People.Phone;
                label21.Text = clsBuisnessCountry.getnamecountrybyid(People.NationalityCountryID+2);




                pictureBox9.ImageLocation = People.ImagePath;


            }
        }
        public void LOADDATAnationalno()
        {
            clsBuisnessPeople People = clsBuisnessPeople.Find(nationalNo);
            if (People != null)
            {

                ISFOUND = true;

                label13.Text = People.PersonID.ToString();
                number = People.PersonID;
                label5.Text = People.FirstName + "   " + People.SecondName + "   " + People.ThirdName + "   " + People.LastName;
                label14.Text = People.NationalNo;
                if (People.Gendor == 0)
                {
                    label6.Text = "Male";
                    pictureBox3.Image = Resources.Man_32;
                }
                else
                {
                    label6.Text = "Female";
                    pictureBox3.Image = Resources.Woman_32;

                }
                label15.Text = People.Email;
                label7.Text = People.Address;
                label4.Text = People.DateOfBirth.ToString();
                label20.Text = People.Phone;
                label21.Text = clsBuisnessCountry.getnamecountrybyid(People.NationalityCountryID+2);




                pictureBox9.ImageLocation = People.ImagePath;


            }
        }


      
      

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddAndEditPersoncs frm = new AddAndEditPersoncs(number);
            frm.ShowDialog();
            LOADDATA();
        }
    }
}
