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

namespace DVLD_Project.Test.TestTypes
{
    public partial class EditTestTypes : Form
    {
        int _id;
        clsTestTypes testtype;

        public EditTestTypes(int id)
        {
            InitializeComponent();
            _id = id;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditTestTypes_Load(object sender, EventArgs e)
        {
            testtype = clsTestTypes.Find((clsTestTypes.Etest)_id);
            if (testtype != null) 
            {
                label2.Text = testtype.TestTypeID.ToString();
                textBox1.Text = testtype.TestTypesName;
                textBox2.Text=testtype.TestTypesfees.ToString();
                textBox3.Text=  testtype.TestTypeDescription;
            
            
            
            
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (ValidateChildren() == false)
            {
                return;
            }

            testtype.TestTypesName = textBox1.Text;
            testtype.TestTypesfees =Convert.ToDecimal( textBox2.Text);
            testtype.TestTypeDescription= textBox3.Text;
            if (testtype.Save())
            {
                MessageBox.Show("the data is edit Successfully ", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }

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

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
         
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Block the key
            }
        }
    }
}
