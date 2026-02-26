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
    public partial class TestTypesList : Form
    {
        public TestTypesList()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TestTypesList_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource=clsTestTypes.GetallTestTypes();
            dataGridView1.Columns["TestTypeDescription"].Width = 300;
            label3.Text = dataGridView1.RowCount.ToString();
        }

        private void editTestTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditTestTypes FRM = new EditTestTypes((int)dataGridView1.CurrentRow.Cells[0].Value);
            FRM.ShowDialog();
            TestTypesList_Load(null, null);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
