using BiusnessDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DVLD_Project.Users
{
    public partial class AddAndEditUser : Form
    {
        enum Emode { addmode = 1, updatemode = 0 };
        Emode emode = Emode.addmode;
        int numberiduser;

        public AddAndEditUser(int number )
        {
            InitializeComponent();
            button3.Enabled = false;
            numberiduser = number;
            if(number == -1)
            {
                uctlShowPersonDetilesWithFilter1.isEditMode = false;
                tabPage2.Enabled=false;
                emode = Emode.addmode;
                label6.Text = "Add User";
                _User = new clsUser();
            }
            else
            {
                uctlShowPersonDetilesWithFilter1.isEditMode = true;
                emode = Emode.updatemode;
                label6.Text = "Edit information";
                _User = clsUser.Find(numberiduser);
                if (_User == null)
                {

                    MessageBox.Show(" The id = " + numberiduser + " is not found ", "Erorr ");
                    return;

                }
                button3.Enabled =true;
                tabPage2.Enabled = true;
                uctlShowPersonDetilesWithFilter1.ID = _User.PersonID;
                textBox1.Text = _User.Username;
                textBox2.Text= _User.Password;
                if (_User.IsActive == true)
                {
                    checkBox1.Checked = true;

                }
                else
                {
                    checkBox1.Checked = false;
                }

            }
            

        }

        clsUser _User;


        private void AddAndEditUser_Load(object sender, EventArgs e)
        {


        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (emode == Emode.addmode)
            {
                if (uctlShowPersonDetilesWithFilter1.isfound == false)
                {
                    MessageBox.Show("You have to select a person at First ! ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;

                }
                // check User in Database first 
                if (clsUser.isExists(uctlShowPersonDetilesWithFilter1.ID))
                {
                    MessageBox.Show("This User IS in Database You Have to Get Someone else ! ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            tabPage2.Enabled = true;
            button3.Enabled = true;
            tabControl1.SelectedIndex = 1;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (!this.ValidateChildren())
            {
                MessageBox.Show(" put mouse in red circle to see the problem ! ", "Information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.Username = textBox1.Text;
            _User.Password = textBox2.Text;
            if (checkBox1.Checked == true)
            {
                _User.IsActive = true;
            }
            else
            {
                _User.IsActive=false;
            }
            _User.PersonID = uctlShowPersonDetilesWithFilter1.ID;

           if( _User.Save())
                {

            uctlShowPersonDetilesWithFilter1.isEditMode = true;
            label6.Text = "Edit information";
            label2.Text = _User.UserID.ToString();
            emode = Emode.updatemode;
                }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox3.Text))
            {
                errorProvider1.SetError(textBox3, "this filed is required !");
                e.Cancel = true;
                textBox3.Focus();

            }
            else
            {
                errorProvider1.SetError(textBox3, "");
                e.Cancel = false;

            }
            if (textBox3.Text!=textBox2.Text)
            {
                errorProvider1.SetError(textBox3, "it is not confirm password !");
                e.Cancel = true;
                textBox3.Focus();

            }
            else
            {
                errorProvider1.SetError(textBox3, "");
                e.Cancel = false;

            }





        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
        }

   

    
      }
}
