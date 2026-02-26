using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Applications.LocalApplication
{
    public partial class ShowLocalApplicationInfo : Form
    {
        public ShowLocalApplicationInfo(int localappid)
        {
            InitializeComponent();
            uctlShowLocalApplicationInfo1.LocalappID = localappid;
            uctlShowLocalApplicationInfo1.LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
