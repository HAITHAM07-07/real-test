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

namespace DVLD_Project.Applications.Application_Type
{
    public partial class ApplicationTypeList : Form
    {
        public ApplicationTypeList()
        {
            InitializeComponent();
            loaddata();
        }


       void loaddata()
        {
            dataGridView1.DataSource = clsApplicationtypes.GetallApplicationtypes();
            dataGridView1.Columns["ApplicationTypeTitle"].Width = 277;
            label3.Text = dataGridView1.RowCount.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editAppToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditApplicationtypes frm = new EditApplicationtypes((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            loaddata();

        }
    }
}
