using BUSINESS_DVLD;
using DVLD_Project.Applications.InternationalApplication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Licenses
{
    public partial class HistoryPersonform : Form
    {
        int personid;
        float localappid;

        public HistoryPersonform(float localappid )
        {
            this.localappid = localappid;
            InitializeComponent();
            HistoryPersonform_Load();
            HistoryPersonform_Load2();



        }

        public HistoryPersonform( int personid)
        {
            this.personid = personid;
            InitializeComponent();
            HistoryPersonform_Load();
            HistoryPersonform_Load2();

        }



        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

         void HistoryPersonform_Load()
        {
            clsLocalDrivingLicenseApplication localapp = clsLocalDrivingLicenseApplication.Find((int)localappid);
            if (localapp != null) 
            {
                clsApplications app = clsApplications.Find(localapp.ApplicationID);
                this.personid = app.Personinfo.PersonID;
            }
            uctlShowPersonDetilesWithFilter1.ID = personid;
            uctlShowPersonDetilesWithFilter1.isEditMode = true;
          


        }
         void HistoryPersonform_Load2()
        {
           

        
            dataGridView1.DataSource = clsLicense.GetallDatalicensesByPersonID(personid);
            dataGridView2.DataSource = clsInternationalLicense.GetallDatalicensesByPersonID(personid);
            dataGridView1.Columns["ClassName"].Width = 190;
            dataGridView1.Columns["IssueDate"].Width = 140;
            dataGridView2.Columns["IssueDate"].Width = 140;
            dataGridView1.Columns["ExpirationDate"].Width = 160;
            dataGridView2.Columns["ExpirationDate"].Width = 160;
            label3.Text = dataGridView1.RowCount.ToString();



        }


        private void updatelabel3()
        {
            if (tabControl1.SelectedTab == tabPage1)
            {
                label3.Text = dataGridView1.RowCount.ToString();
            }
            else if (tabControl1.SelectedTab == tabPage2)
            {
                label3.Text = dataGridView2.RowCount.ToString();

            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            updatelabel3();
        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
           ShowLicense frm = new ShowLicense((int)dataGridView1.CurrentRow.Cells[0].Value);
           frm.ShowDialog();
        }

   
        private void showLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ShowInternationalLicense frm = new ShowInternationalLicense((int)dataGridView2.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }
    }
}
