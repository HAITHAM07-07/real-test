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
    public partial class uctlShowDetailesUser : UserControl
    {
        public uctlShowDetailesUser()
        {
            InitializeComponent();
        }

        public int Userid { get; set; }
        public int Personid { get; set; }

        public  clsUser _User;



        public void LoadData()
        {
            _User = clsUser.Find(Userid);
            if (_User != null)
            {
                uctlShowDetilesPerson1.number = Personid;
                uctlShowDetilesPerson1.LOADDATA();
                label5.Text = Userid.ToString();
                label2.Text = _User.Username;
                if (_User.IsActive == true)
                {
                    label3.Text = "Yes";
                }
                else
                {
                    label3.Text = "No";
                }
            }
        }
    }
}

