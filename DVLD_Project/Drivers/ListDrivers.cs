using BUSINESS_DVLD;
using DVLD_Project.Licenses;
using DVLD_Project.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Drivers
{
    public partial class ListDrivers : Form
    {
        public ListDrivers()
        {
            InitializeComponent();
        }

      private string column;


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ListDrivers_Load(object sender, EventArgs e)
        {
            ListDrivers_Loaddata();
        }

        private void ListDrivers_Loaddata()
        {
            dgvDrivers.DataSource = clsDriver.GetAllDataBaseFromDrivers("DriverID", null);
            dgvDrivers.Columns["FullName"].Width = 237;
            lblRecordsCount.Text = dgvDrivers.RowCount.ToString();

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 0) 
            {
                txtFilterValue.Visible = false;
            }
            else
            {
                txtFilterValue.Visible = true;

                switch (cbFilterBy.SelectedIndex)
                {


                    case 0:

                        break;
                    case 1:
                        column = "DriverID";
                        break;
                    case 2:
                        column = "PersonID";
                        break;
                    case 3:
                        column = "NationalNO";
                        break;
                    case 4:
                        column = "FullName";
                        break;





                }
            }





        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {

            dgvDrivers.DataSource = clsDriver.GetAllDataBaseFromDrivers(column, txtFilterValue.Text.Trim());
            dgvDrivers.Columns["FullName"].Width = 237;

            lblRecordsCount.Text = dgvDrivers.RowCount.ToString();

        }

        private void personHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HistoryPersonform frm = new HistoryPersonform((int)dgvDrivers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }

        private void personInformationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowPersonDetiles frm = new ShowPersonDetiles((int)dgvDrivers.CurrentRow.Cells[1].Value);
            frm.ShowDialog();
        }
    }
}
