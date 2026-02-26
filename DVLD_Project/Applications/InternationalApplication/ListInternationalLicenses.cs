using BUSINESS_DVLD;
using DVLD_Project.Licenses;
using DVLD_Project.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Applications.InternationalApplication
{
    public partial class ListInternationalLicenses : Form
    {
        public ListInternationalLicenses()
        {
            InitializeComponent();
        }

        string column = "ApplicationID";
        string letter = "";
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddInternational frm = new AddInternational();
            frm.ShowDialog();
        }





        private void ListInternationalLicenses_Load(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            comboBox2.Visible = false;
            dataGridView1.DataSource= clsInternationalLicense.GetAllDataBaseFromInternational(column, letter);
            label3.Text = dataGridView1.RowCount.ToString();
        }

        private void showPersonDetalisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsApplications applications = clsApplications.Find((int)dataGridView1.CurrentRow.Cells[1].Value);
            ShowPersonDetiles frm = new ShowPersonDetiles(applications.ApplicantPersonID);
            frm.ShowDialog();
        }

        private void showLicenseDetalisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowInternationalLicense frm = new ShowInternationalLicense((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();   
        }

        private void showPersonLicensesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsApplications applications = clsApplications.Find((int)dataGridView1.CurrentRow.Cells[1].Value);
            HistoryPersonform frm = new HistoryPersonform(applications.ApplicantPersonID);
            frm.ShowDialog();   
        }




        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (comboBox1.SelectedIndex == 0)
            {
                textBox1.Visible = false;
                comboBox2.Visible = false;
            }
            else
            {
                switch (comboBox1.SelectedIndex) 
                {

                    case 1:
                        textBox1.Visible = true;
                        comboBox2.Visible = false;
                        column = "InternationalLicenseID";
                        break;
                    case 2:
                        textBox1.Visible = true;
                        comboBox2.Visible = false;
                        column = "IssuedUsingLocalLicenseID";
                        break;
                    case 3:
                        textBox1.Visible = false;
                        comboBox2.Visible = true;
                        column = "IsActive";
            
                        break;

                }
            }



        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = clsInternationalLicense.GetAllDataBaseFromInternational(column, textBox1.Text);
            label3.Text = dataGridView1.RowCount.ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string isactivestatus = "";
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    isactivestatus = "";
                    break;
                case 1:
                    isactivestatus = "1";
                    break;
                case 2:
                    isactivestatus = "0";
                    break;
            }
            dataGridView1.DataSource = clsInternationalLicense.GetAllDataBaseFromInternational(column, isactivestatus);
            label3.Text = dataGridView1.RowCount.ToString();
        }
    }
}
