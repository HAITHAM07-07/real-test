using BuisnessDVLD;
using BUSINESS_DVLD;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Test.TestTries
{
    public partial class TestAppointment : Form
    {

       private int idlocal;
       private clsTestTypes.Etest typetest;


        public TestAppointment(int IDLocalapp,clsTestTypes.Etest typetestname)
        {
            idlocal = IDLocalapp;
            typetest = typetestname;    
            InitializeComponent();
            ChangeStatusOfForm(typetest);
            uctlShowLocalApplicationInfo1.LocalappID = IDLocalapp;
            uctlShowLocalApplicationInfo1.LoadData();




        }





        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ChangeStatusOfForm(clsTestTypes.Etest type)
        {
            
            switch (type) 
            {

                case clsTestTypes.Etest.visioTest:
                    pictureBox1.Image = Resources.Vision_512;
                    label1.Text = "Vision Test ";
                    break;
                case clsTestTypes.Etest.writtenTest:
                    pictureBox1.Image = Resources.Written_Test_512;
                    label1.Text = "Written Test ";
                    break;
                case clsTestTypes.Etest.streetTest:
                    pictureBox1.Image = Resources.driving_test_512;
                    label1.Text = "Street Test ";
                    break;


            }




        }




        private void LoadDataGridView()
        {

        dataGridView1.DataSource=clsTestAppointment.getalldataAppointment(idlocal, typetest); 
        label3.Text=dataGridView1.RowCount.ToString(); 



        }
        private void TestAppointment_Load(object sender, EventArgs e)
        {
            LoadDataGridView();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (clsTestAppointment.IsExists(idlocal, (int)typetest))
            {
                MessageBox.Show("Person Already have an active appointment for this test, you cannot add new appointment.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (clsTestAppointment.IsExistsexam(idlocal, (int)typetest))
            {
                MessageBox.Show("This person already passed this test before, you can only retake faild test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            AddEditAppointments frm = new AddEditAppointments(idlocal, typetest, -1,false);
            frm.ShowDialog();
            LoadDataGridView();
        }

        private void etidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool islocked = (bool)dataGridView1.CurrentRow.Cells[3].Value; 

            AddEditAppointments frm = new AddEditAppointments(idlocal, typetest,(int) dataGridView1.CurrentRow.Cells[0].Value, islocked);
            frm.ShowDialog();
            LoadDataGridView();
        }

        private void takeTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TakeTest frm = new TakeTest(typetest, idlocal, (int)dataGridView1.CurrentRow.Cells[0].Value, (bool)dataGridView1.CurrentRow.Cells[3].Value);
            frm.ShowDialog();
            LoadDataGridView();
        }
    }
}
