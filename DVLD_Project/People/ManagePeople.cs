using BuisnessDVLD;
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

namespace DVLD_Project
{
    public partial class ManagePeople : Form
    {
        public ManagePeople()
        {
            InitializeComponent();

        }



        private string _likeletter;
        string _column;


        private void ManagePeople_Load_1(object sender, EventArgs e)
        {

            comboBox1.SelectedIndex = 0;
            textBox1.Visible = false;
            ManagePeople_Refresh();
        }
        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

            this.Close();


        }
        private void ManagePeople_Refresh()
        {
            dataGridView1.DataSource = clsBuisnessPeople.GetallDataPeople("FirstName", "");
            label4.Text = dataGridView1.RowCount.ToString();

        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (comboBox1.SelectedIndex == 0)
            {
                textBox1.Visible = false;
                ManagePeople_Refresh(); // عرض كل البيانات
                return;
            }
            else
            {
                textBox1.Visible = true;



                switch (comboBox1.SelectedIndex)
                {
                    case 1: _column = "NationalNo"; break;
                    case 2: _column = "FirstName"; break;
                    case 3: _column = "SecondName"; break;
                    case 4: _column = "ThirdName"; break;
                    case 5: _column = "LastName"; break;
                    case 6: _column = "Gendor"; break;
                    case 7: _column = "Address"; break;
                    case 8: _column = "Phone"; break;
                    case 9: _column = "Email"; break;
                    case 10: _column = "CountryName"; break;

                    default:
                        _column = "";
                        break;
                }

                // تحميل البيانات بناءً على الكولوم المختار

            }

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            _likeletter = textBox1.Text;
            if(_column == "Gendor")
            {
                if (textBox1.Text.ToLower() == "m")
                {

                dataGridView1.DataSource = clsBuisnessPeople.GetallDataPeople(_column, "0");
                label4.Text = dataGridView1.RowCount.ToString();
                }
                else if(textBox1.Text.ToLower() == "f")
                {
                    dataGridView1.DataSource = clsBuisnessPeople.GetallDataPeople(_column, "1");
                    label4.Text = dataGridView1.RowCount.ToString();
                }

            }
           else if (_column != "")
            {
                dataGridView1.DataSource = clsBuisnessPeople.GetallDataPeople(_column, _likeletter);
                label4.Text = dataGridView1.RowCount.ToString();
            }
        }




       






        private void showToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            ShowPersonDetiles frm = new ShowPersonDetiles((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            ManagePeople_Refresh();
        }

        private void addNewPeopleToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            AddAndEditPersoncs frm = new AddAndEditPersoncs(-1);
            frm.ShowDialog();
     
            ManagePeople_Refresh();
        }

        private void editToolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            AddAndEditPersoncs frm = new AddAndEditPersoncs((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            ManagePeople_Refresh();
        }

        private void deleteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (clsBuisnessPeople.DeletePerson((int)dataGridView1.CurrentRow.Cells[0].Value) == false)
            {
                MessageBox.Show("Something is wrong Delete is not Successful !!! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            ManagePeople_Refresh();
        }

        private void sendEmailToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("This Method Will Be Ready Soon", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void phoneCallToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("This Method Will Be Ready Soon", "info", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (clsBuisnessPeople.DeletePerson((int)dataGridView1.CurrentRow.Cells[0].Value) == false)
            {
                MessageBox.Show("Something is wrong Delete is not Successful !!! ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            ManagePeople_Refresh();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
              ShowPersonDetiles frm = new ShowPersonDetiles((int)dataGridView1.CurrentRow.Cells[0].Value);
              frm.ShowDialog();
            ManagePeople_Refresh();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            AddAndEditPersoncs frm = new AddAndEditPersoncs((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();

            ManagePeople_Refresh();
        }

        private void BTNADD_Click_1(object sender, EventArgs e)
        {
             AddAndEditPersoncs frm = new AddAndEditPersoncs(-1);
             frm.ShowDialog();

            ManagePeople_Refresh();
        }
    }
}
