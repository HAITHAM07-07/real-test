using BUSINESS_DVLD;
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

namespace DVLD_Project.Applications.Application_Type
{
    public partial class EditApplicationtypes : Form
    {
        int idapp;
        clsApplicationtypes app;
        public EditApplicationtypes(int id)
        {
            InitializeComponent();
            idapp = id;
        }

        private void EditApplicationtypes_Load(object sender, EventArgs e)
        {
         app = clsApplicationtypes.Find(idapp);
            if (app != null) 
            {

            
                label2.Text = app.IDApplicationtypes.ToString();
                textBox1.Text = app.ApplicationtypesName;
                textBox2.Text= app.Applicationtypesfees.ToString();


            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren() == false)
            {
                return;
            }
            app.Applicationtypesfees = Convert.ToDecimal(textBox2.Text);
            app.ApplicationtypesName = textBox1.Text;
            if (app.Save()) {
                MessageBox.Show("the data is edit Successfully ", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            ;
            
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "this filed is required !");
                e.Cancel = true;
                textBox1.Focus();

            }
            else
            {
                errorProvider1.SetError(textBox1, "");
                e.Cancel = false;

            }
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow only digits and Backspace
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Block the key
            }
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "this filed is required !");
                e.Cancel = true;
                textBox2.Focus();

            }
            else
            {
                errorProvider1.SetError(textBox2, "");
                e.Cancel = false;

            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
