using BiusnessDVLD;
using BUSINESS_DVLD;
using DVLD_Project.Licenses;
using DVLD_Project.Test.TestTries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD_Project.Applications.LocalApplication
{
    public partial class ListLocalApplicationlicense : Form
    {
        public ListLocalApplicationlicense()
        {
            InitializeComponent();
        }

        clsLocalDrivingLicenseApplications_View localappview;
        clsLocalDrivingLicenseApplication localapp;
        clsApplications app;
        string _column;
        string _likeletter;

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }






        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowLocalApplicationInfo frm = new ShowLocalApplicationInfo(((int)dataGridView1.CurrentRow.Cells[0].Value));
            frm.ShowDialog();
        }









        private void ListLocalApplicationlicense_LoadData()
        {
            dataGridView1.DataSource = clsLocalDrivingLicenseApplication.GetallLocalApplications("FullName", "");
            dataGridView1.Columns["FullName"].Width = 243;
            dataGridView1.Columns["ClassName"].Width = 243;
            label3.Text = dataGridView1.RowCount.ToString();
        }
        private void ListLocalApplicationlicense_Load(object sender, EventArgs e)
        {
            textBox1.Visible = false;
            ListLocalApplicationlicense_LoadData();
        }





        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
            if (comboBox1.SelectedIndex == 0)
            {
                ListLocalApplicationlicense_LoadData();
                textBox1.Visible = false;
                return;
            }

            else
            {
                ListLocalApplicationlicense_LoadData();
                textBox1.Visible = true;
                textBox1.Focus();


                switch (comboBox1.SelectedIndex)
                {
                    case 1: _column = "LocalDrivingLicenseApplicationID"; break;
                    case 2: _column = "NationalNo"; break;
                    case 3: _column = "FullName"; break;
                    case 4: _column = "Status"; break;
                    case 5: _column = "ClassName"; break;
                    default:
                        _column = "";
                        break;
                }

            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
         
                _likeletter = textBox1.Text;
                dataGridView1.DataSource = clsLocalDrivingLicenseApplication.GetallLocalApplications(_column, _likeletter);
                label3.Text = dataGridView1.RowCount.ToString();
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddAndEditApplicationLocal frm = new AddAndEditApplicationLocal(-1);
            frm.ShowDialog();
            ListLocalApplicationlicense_LoadData();

        }

        private void addNewPeopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            localapp = clsLocalDrivingLicenseApplication.Find((int)dataGridView1.CurrentRow.Cells[0].Value);
            app = clsApplications.Find(localapp.ApplicationID);
            if (app != null)
            {
                app.LastStatusDate = DateTime.Now;
                app.ApplicationStatus = 2;
                app.Save();
            }
            ListLocalApplicationlicense_LoadData();
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddAndEditApplicationLocal frm = new AddAndEditApplicationLocal((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            ListLocalApplicationlicense_LoadData();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            localapp = clsLocalDrivingLicenseApplication.Find((int)dataGridView1.CurrentRow.Cells[0].Value);
            if (clsLocalDrivingLicenseApplication.DeleteLocalApplication(localapp.LocalDrivingLicenseApplicationID))
            {
                if (clsApplications.DeleteApplication(localapp.ApplicationID))
                {
                    MessageBox.Show("The Data Delete Successfully", "information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ListLocalApplicationlicense_LoadData();
                }
            }
            else
            {
                MessageBox.Show("The Data not Delete because it contained related information.", "information", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }









        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            localapp = clsLocalDrivingLicenseApplication.Find(((int)dataGridView1.CurrentRow.Cells[0].Value));
            localappview = clsLocalDrivingLicenseApplications_View.Find((int)dataGridView1.CurrentRow.Cells[0].Value);
            app = clsApplications.Find(localapp.ApplicationID);
            HandelStatusOfMenuStrip(app.ApplicationStatus, localappview.passedtestcount);


        }
        private void HandelStatusOfMenuStrip(short status, int passedtests)
        {
            if (status == 1&&passedtests!=3)
            {
                showToolStripMenuItem.Enabled = true;
                editToolStripMenuItem1.Enabled = true;
                deleteToolStripMenuItem.Enabled = true;
                cancelToolStripMenuItem.Enabled = true;
                SechduleoolStripMenuItem.Enabled = true;
                issueToolStripMenuItem.Enabled = false;
                showLicenseToolStripMenuItem.Enabled = false;
                showPersonLicenseHistoryToolStripMenuItem.Enabled = true;
                if (passedtests == 0)
                {
                    scheduleVisionTestToolStripMenuItem.Enabled = true;
                    scheduleWrittenTestToolStripMenuItem.Enabled = false;
                    scheduleStreetTestToolStripMenuItem.Enabled=false;
                }
                else if (passedtests == 1)
                {
                    scheduleVisionTestToolStripMenuItem.Enabled = false;
                    scheduleWrittenTestToolStripMenuItem.Enabled = true;
                    scheduleStreetTestToolStripMenuItem.Enabled = false;
                }
                else
                {
                    scheduleVisionTestToolStripMenuItem.Enabled = false;
                    scheduleWrittenTestToolStripMenuItem.Enabled = false;
                    scheduleStreetTestToolStripMenuItem.Enabled = true;
                }

            }
            else if (status == 1 && passedtests == 3)
            {
                showToolStripMenuItem.Enabled = true;
                editToolStripMenuItem1.Enabled = true;
                deleteToolStripMenuItem.Enabled = true;
                cancelToolStripMenuItem.Enabled = true;
                SechduleoolStripMenuItem.Enabled = false;
                issueToolStripMenuItem.Enabled = true;
                showLicenseToolStripMenuItem.Enabled = false;
                showPersonLicenseHistoryToolStripMenuItem.Enabled = true;
            }
            else if (status == 2)
            {
                showToolStripMenuItem.Enabled = true;
                editToolStripMenuItem1.Enabled = false;
                deleteToolStripMenuItem.Enabled = false;
                cancelToolStripMenuItem.Enabled = false;
                SechduleoolStripMenuItem.Enabled = false;
                issueToolStripMenuItem.Enabled = false;
                showLicenseToolStripMenuItem.Enabled = false;
                showPersonLicenseHistoryToolStripMenuItem.Enabled = true;
            }
            else if (status == 3)
            {
                {
                    showToolStripMenuItem.Enabled = true;
                    editToolStripMenuItem1.Enabled = false;
                    deleteToolStripMenuItem.Enabled = false;
                    cancelToolStripMenuItem.Enabled = false;
                    SechduleoolStripMenuItem.Enabled = false;
                    issueToolStripMenuItem.Enabled = false;
                    showLicenseToolStripMenuItem.Enabled = true;
                    showPersonLicenseHistoryToolStripMenuItem.Enabled = true;
                }





            }




        }




        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestAppointment frm = new TestAppointment((int)dataGridView1.CurrentRow.Cells[0].Value, clsTestTypes.Etest.visioTest);
            frm.ShowDialog();
            ListLocalApplicationlicense_LoadData();


        }
        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestAppointment frm = new TestAppointment((int)dataGridView1.CurrentRow.Cells[0].Value, clsTestTypes.Etest.writtenTest);
            frm.ShowDialog();
            ListLocalApplicationlicense_LoadData();
        }
        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestAppointment frm = new TestAppointment((int)dataGridView1.CurrentRow.Cells[0].Value, clsTestTypes.Etest.streetTest);
            frm.ShowDialog();
            ListLocalApplicationlicense_LoadData();
        }

        private void issueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IssueLicenseFirstTimeLocal frm = new IssueLicenseFirstTimeLocal((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            ListLocalApplicationlicense_LoadData();

        }

        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            clsLocalDrivingLicenseApplication applocal =  clsLocalDrivingLicenseApplication.Find((int)dataGridView1.CurrentRow.Cells[0].Value);

          

            clsLicense license = clsLicense.FindByappID(applocal.ApplicationID);



            ShowLicense frm = new ShowLicense(license.LicenseID);
            frm.ShowDialog();


        }

        private void showPersonLicenseHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int number =(int) dataGridView1.CurrentRow.Cells[0].Value;

            HistoryPersonform frm = new HistoryPersonform((float)number);
            frm.ShowDialog();
        }
    }
}

