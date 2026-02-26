using BuisnessDVLD;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DVLD.Global;

namespace DVLD_Project.People
{
    public partial class AddAndEditPersoncs : Form
    {
            public delegate void Databack(int id);
            public event Databack databack1;


            enum Emode { addmode = 1, updatemode = 0 };
            Emode emode = Emode.addmode;



            public int _number;
        public AddAndEditPersoncs(int number)
        {
            InitializeComponent();
            _number = number;
            if (_number == -1)
            {
                emode = Emode.addmode;

            }
            else
            {
                emode = Emode.updatemode;
            }
        }

   
           
            clsBuisnessPeople People;

            private void Load_countryName()
            {
                DataTable dt = new DataTable();
                dt = clsBuisnessCountry.getalldatacountry();
                foreach (DataRow dr in dt.Rows)
                {

                    comboBox1.Items.Add(dr["CountryName"]);


                }
            }

         
          

          

          






        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddAndEditPersoncs_Load(object sender, EventArgs e)
        {
            Load_countryName();

            radioButton1.Checked = true;
            dateTimePicker1.MaxDate = DateTime.Now - TimeSpan.FromDays(6570);
            comboBox1.SelectedIndex = 167;
            if (emode == Emode.addmode)
            {

                linkLabel2.Visible = false;
                label15.Text = "Add New People";
                People = new clsBuisnessPeople();
                return;
            }
            else
            {
                People = clsBuisnessPeople.Find(_number);
                if (People == null)
                {

                    MessageBox.Show(" The id = " + _number + "is not found ", "Erorr ");
                    return;

                }
                label15.Text = "Edit information ";
                textBox5.Text = People.NationalNo;
                textBox1.Text = People.FirstName;
                textBox2.Text = People.SecondName;
                textBox3.Text = People.ThirdName;
                textBox4.Text = People.LastName;
                textBox8.Text = People.Phone;
                textBox7.Text = People.Address;
                textBox6.Text = People.Email;
                label13.Text = _number.ToString();
                comboBox1.SelectedIndex = People.NationalityCountryID  ;
                dateTimePicker1.Value = People.DateOfBirth;
                if (People.Gendor == 0)
                {
                    radioButton1.Checked = true;
                }
                else
                {
                    radioButton2.Checked = true;
                }

                if (People.ImagePath != null)
                {
                    pictureBox10.ImageLocation = People.ImagePath;
                }
                else
                {
                    pictureBox10.Image = Resources.EMPTY;

                }
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select a Photo";
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp|All Files|*.*";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    string selectedPath = ofd.FileName;  // هنا مسار الصورة
                    linkLabel2.Visible = true;
                    pictureBox10.Load(selectedPath);  // حطها بالصندوق
                                                      // أو إذا تحب تخزن الصورة في Image بدل Path:
                                                      // pictureBox1.Image = Image.FromFile(selectedPath);
                }
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel2.Visible = false;

            pictureBox10.ImageLocation = null;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                MessageBox.Show(" put mouse in red circle to see the problem ! ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (HandleImagePeople() == false)
            { return; }



            People.NationalNo = textBox5.Text;
            People.FirstName = textBox1.Text;
            People.SecondName = textBox2.Text;
            People.ThirdName = textBox3.Text;
            People.LastName = textBox4.Text;
            People.Phone = textBox8.Text;
            People.Address = textBox7.Text;
            People.Email = textBox6.Text;
            People.NationalityCountryID = comboBox1.SelectedIndex  ;
            People.DateOfBirth = Convert.ToDateTime(dateTimePicker1.Text);
            if (radioButton1.Checked == true)
            {

                People.Gendor = 0;
            }
            else
            {
                People.Gendor = 1;
            }

            People.ImagePath = pictureBox10.ImageLocation;


           if(People.Save())
            {
                MessageBox.Show("Data Save Saccessfully","information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                button1.Enabled = false;
            databack1?.Invoke(People.PersonID);

            emode = Emode.updatemode;
            label15.Text = "Edit  ID = [ " + People.PersonID + " ]";
            label13.Text = People.PersonID.ToString();
            }


        }




        


            private void textBox6_TextChanged_1(object sender, EventArgs e)
            {
                if (string.IsNullOrEmpty(textBox6.Text.Trim()))
                {
                    errorProvider1.SetError(textBox6, "");
                    return;
                }
                else
                {
                    String ST1 = textBox6.Text;
                    if (ST1.ToLower().Contains("@gmail.com") == false)
                    {
                        errorProvider1.SetError(textBox6, "The Email  you entered is incorrecr");

                        textBox6.Focus();


                    }
                    else
                    {
                        errorProvider1.SetError(textBox6, "");

                    }
                }

            }

            private void textBox5_Validating(object sender, CancelEventArgs e)
            {

                if (string.IsNullOrEmpty(textBox5.Text.Trim()))
                {
                    e.Cancel = true;
                    errorProvider1.SetError(textBox5, "This field is required!");
                    return;
                }
                else
                {
                    errorProvider1.SetError(textBox5, null);
                }

                if (clsBuisnessPeople.FindNationalNo(textBox5.Text) && textBox5.Text != People.NationalNo)
                {
                    MessageBox.Show(" This NationalNo [" + textBox5.Text + "] is Existing in DataBase  ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    errorProvider1.SetError(textBox5, "This NationalNo [ " + textBox5.Text + " ] is Existing in DataBase");
                    textBox5.Focus();
                }
                else
                {
                    errorProvider1.SetError(textBox5, "");
                }


            }

            private void validatedTextBox(object sender, CancelEventArgs e)
            {
                TextBox textbox = ((TextBox)sender);

                if (string.IsNullOrEmpty(textbox.Text))
                {
                    errorProvider1.SetError(textbox, "this filed is required !");
                    e.Cancel = true;
                    textbox.Focus();

                }
                else
                {
                    errorProvider1.SetError(textbox, "");
                    e.Cancel = false;

                }

            }


            private bool HandleImagePeople()
            {

                if (People.ImagePath != pictureBox10.ImageLocation)
                {
                    if (People.ImagePath != "")
                    {
                        try
                        {

                            File.Delete(People.ImagePath);
                        }
                        catch//(//IOException iox)
                        {

                        }
                    }
                }


                if (pictureBox10.ImageLocation != null)
                {
                    string sourceimagefile = pictureBox10.ImageLocation;

                    if (clsutil.CopyImageToProjectImageFolder(ref sourceimagefile) == true)
                    {
                        pictureBox10.ImageLocation = sourceimagefile;
                        return true;


                    }
                    else
                    {
                        MessageBox.Show("Error copy image file", "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                return true;








            }
    }
  }

