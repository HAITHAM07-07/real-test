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

namespace DVLD_Project.DetainedLicense
{
    public partial class ListDetainedLicenses : Form
    {
        public ListDetainedLicenses()
        {
            InitializeComponent();
            textBox1.Visible = false;
            comboBox2.Visible = false;
        }

        string column = "DetainID";
        string letters = "";

        clsLicense license;






        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


         void Loaddata()
        {
            dataGridView1.DataSource = clsDetainedLicense.GetallDataDetainedlicenses(column,letters);
            label4.Text = dataGridView1.RowCount.ToString();
        }

        private void ListDetainedLicenses_Load(object sender, EventArgs e)
        {
            Loaddata();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();

            switch (comboBox1.SelectedIndex) 
            {

                case 0:
                    textBox1.Visible = false;
                    comboBox2.Visible = false;
                    dataGridView1.DataSource = clsDetainedLicense.GetallDataDetainedlicenses(column,"");
                    label4.Text = dataGridView1.RowCount.ToString();
                    break;
                case 1:
                    textBox1.Visible = true;
                    comboBox2.Visible = false;
                    column = "DetainID";
                    break;
                case 2:
                    textBox1.Visible = true;
                    comboBox2.Visible = false;
                    column = "LicenseID";
                    break;
                case 3:
                    textBox1.Visible = false;
                    comboBox2.Visible = true;
                    column = "IsReleased";
                    break;
                case 4:
                    textBox1.Visible = true;
                    comboBox2.Visible = false;
                    column = "FullName";
                    break;

            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = clsDetainedLicense.GetallDataDetainedlicenses(column, textBox1.Text);
            label4.Text = dataGridView1.RowCount.ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string status = " ";
            switch (comboBox2.SelectedIndex)
            {

                case 0:

                    status = "";
                    break;
                case 1:
                    status = "1";
                    break;
                case 2:
                    status = "0";
                    break;
          
            }
                    dataGridView1.DataSource = clsDetainedLicense.GetallDataDetainedlicenses(column, status);
                    label4.Text = dataGridView1.RowCount.ToString();

        }






        private void button2_Click(object sender, EventArgs e)
        {
            DetainedLicenses frm = new DetainedLicenses();
            frm.ShowDialog();
            Loaddata();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RelasedLicenseForm FRM = new RelasedLicenseForm();
            FRM.ShowDialog();
            Loaddata();


        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ShowPersonDetiles frm = new ShowPersonDetiles((string)dataGridView1.CurrentRow.Cells[6].Value);
            frm.ShowDialog();
        }

        private void showLicenseDetalisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLicense frm = new ShowLicense((int)dataGridView1.CurrentRow.Cells[1].Value);
            frm .ShowDialog();
        }

        private void showPersonHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            license = clsLicense.Find((int)dataGridView1.CurrentRow.Cells[1].Value);
            clsApplications app = clsApplications.Find(license.ApplicationID);
            HistoryPersonform frm = new HistoryPersonform(app.ApplicantPersonID);
            frm.ShowDialog();
        }
        private void releasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RelasedLicenseForm frm = new RelasedLicenseForm((int)dataGridView1.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
            Loaddata();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if ((bool)dataGridView1.CurrentRow.Cells[3].Value == true)
            {
                releasToolStripMenuItem.Enabled = false;

            }
            else
            {
                releasToolStripMenuItem.Enabled = true;
            }
        }
    }
}
