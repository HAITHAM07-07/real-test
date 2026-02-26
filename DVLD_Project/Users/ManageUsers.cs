using BiusnessDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Users
{
    public partial class ManageUsers : Form
    {
        public ManageUsers()
        {
            InitializeComponent();
            
        }

        string _column;
        string _likeletter;

        private void ManageUsers_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            textBox1.Visible = false;
            comboBox2.Visible = false;
            ManageUsers_DataLoad();
        }
        private void ManageUsers_DataLoad()
        {
          dataGridView1.DataSource =  clsUser.GetallDataUsers("username", "");
            dataGridView1.Columns["FullName"].Width = 237;
            label5.Text = dataGridView1.RowCount.ToString();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

               textBox1.Clear();
              if (comboBox1.SelectedIndex == 0)
                {
                comboBox2.Visible = false;
                textBox1.Visible = false;
                ManageUsers_DataLoad(); // عرض كل البيانات
                return;
                }
              if(comboBox1.SelectedItem == "IsActive")
                {
                _column = "Users.IsActive";
                comboBox2.Visible = true;
                textBox1.Visible = false;
                ManageUsers_DataLoad();

               }
               else
               {
                ManageUsers_DataLoad();
                textBox1.Visible = true;
                comboBox2.Visible = false;
                textBox1.Focus();

                switch (comboBox1.SelectedIndex)
                {
                    case 1: _column = "UserID"  ; break;
                    case 2: _column = "Users.PersonID"; break;
                    case 3: _column = "People.FirstName + ' ' + People.SecondName + ' ' +  People.ThirdName + ' ' + People.LastName"; break;
                    case 4: _column = "UserName"; break;

                    default:
                        _column = "";
                        break;
                }






               }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            if (textBox1.Text != "")
            {
                _likeletter = textBox1.Text;
                dataGridView1.DataSource = clsUser.GetallDataUsers(_column, _likeletter);
                label5.Text = dataGridView1.RowCount.ToString();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            string staticc = "";
            switch (comboBox2.SelectedIndex)
            {
                case 0:
                    staticc = "";
                    break;
                case 1: 
                    staticc = "1";
                    break;
                case 2: 
                    staticc = "0";
                    break;

                 
                default:
                    staticc = "";
                    break;
            }
            dataGridView1.DataSource = clsUser.GetallDataUsers(_column, staticc);
            label5.Text = dataGridView1.RowCount.ToString();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddAndEditUser frm = new AddAndEditUser(-1);
            frm.ShowDialog();
            ManageUsers_DataLoad();
        }

        private void editToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            AddAndEditUser frm = new AddAndEditUser((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            ManageUsers_DataLoad();

        }

        private void addNewPeopleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddAndEditUser frm = new AddAndEditUser(-1);
            frm.ShowDialog();
            ManageUsers_DataLoad();

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (clsUser.DeleteUser((int)dataGridView1.CurrentRow.Cells[0].Value)) 
            {
                MessageBox.Show("the data is delete now");
            }
            ;
            ManageUsers_DataLoad();

        }

        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowUserInformation frm = new ShowUserInformation((int)dataGridView1.CurrentRow.Cells[1].Value, (int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
        }

        private void toolStripSeparator1_Click(object sender, EventArgs e)
        {
            ChangePasswordForm frm = new ChangePasswordForm((int)dataGridView1.CurrentRow.Cells[1].Value, (int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            ManageUsers_DataLoad();
        }
    }
}
