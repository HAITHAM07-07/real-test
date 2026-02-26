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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD_Project.Users
{
    public partial class ChangePasswordForm : Form
    {
        public ChangePasswordForm(int personid,int userid)
        {
            InitializeComponent();
            uctlShowDetailesUser1.Userid=userid;
            uctlShowDetailesUser1.Personid = personid;
            uctlShowDetailesUser1.LoadData();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (textBox1.Text != uctlShowDetailesUser1._User.Password||textBox1.Text==null)
            {
                errorProvider1.SetError(textBox1, "it is not same password !");
                e.Cancel = true;
                textBox3.Focus();

            }
            else
            {
                errorProvider1.SetError(textBox1, "");
                e.Cancel = false;

            }

        }

        private void textBox3_Validating(object sender, CancelEventArgs e)
        {
            if (textBox3.Text != textBox2.Text || textBox3.Text == null)
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

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "this is  required  !");
                e.Cancel = true;
                textBox2.Focus();

            }
            else
            {
                errorProvider1.SetError(textBox2, "");
                e.Cancel = false;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (ValidateChildren() == false)
            {
                return;
            }


            uctlShowDetailesUser1._User.Password = textBox2.Text;
          if(uctlShowDetailesUser1._User.Save() == true)
            {
                textBox1.Text = null;
                textBox3.Text = null;
                textBox2.Text = null;
                MessageBox.Show("the password is Changed ", "information");
                this.Close();

            }
        }
    }
}
