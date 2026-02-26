using BiusnessDVLD;
using DVLD_Project.Global;
using DVLD_Project.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD_Project
{
    public partial class LogInScreen : Form
    {
        public LogInScreen()
        {
            InitializeComponent();
            checkBox1.Checked = true;
        }

        private void LogInScreen_Load(object sender, EventArgs e)
        {
          

            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            string filePath = Path.Combine(folderPath, "login.txt");

            if (File.Exists(filePath))
            {
                string data = File.ReadAllText(filePath);

                if (!string.IsNullOrWhiteSpace(data))
                {
                    string[] parts = data.Split(new string[] { "#//#" }, StringSplitOptions.None);

                    string username = parts[0];
                    string password = parts[1];

                  
                    textBox1.Text = username;
                    textBox2.Text = password;
                   
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = clsUser.Find(textBox1.Text, textBox2.Text);
            if (clsGlobal.CurrentUser == null||textBox1.Text==null|| textBox2.Text == null) 
            {

                MessageBox.Show("The UserName And Password are incorrect.","information",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            if (clsGlobal.CurrentUser.IsActive == false)
            {
                MessageBox.Show("Your account is inactive. Please contact your admin.", "information", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            
                Checking_checkBox(textBox1.Text, textBox2.Text);
            


            this.Hide();
            MainScreen frm = new MainScreen(this);
            frm.ShowDialog();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            if (textBox2.UseSystemPasswordChar == false)
            {
                textBox2.UseSystemPasswordChar = true;
                pictureBox7.Image = Resources.eye_crossed;
            }
            else
            {
                textBox2.UseSystemPasswordChar = false;
                pictureBox7.Image = Resources.eye;
            }
        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {
            Checking_checkBox(textBox1.Text, textBox2.Text);
            this.Close();
        }


        private void Checking_checkBox(string username,string password)
        {
            string datasave = username + "#//#" + password;


            // choose a safe folder (your app folder)
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");

            // make sure folder exists
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // full file path
            string filePath = Path.Combine(folderPath, "login.txt");

            // write data (creates file if not exists, overwrites if exists)
            if(username == null && password == null||checkBox1.Checked==false)
            {
                datasave = "#//#";
            }
            File.WriteAllText(filePath, datasave);

            
        }


    }
}
