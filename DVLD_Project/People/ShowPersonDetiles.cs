using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.People
{
    public partial class ShowPersonDetiles : Form
    {
        public ShowPersonDetiles(int number)
        {
            InitializeComponent();
            uctlShowDetilesPerson1.number = number;
            uctlShowDetilesPerson1.LOADDATA();

        }
        public ShowPersonDetiles(string nationalnumber)
        {
            InitializeComponent();
            uctlShowDetilesPerson1.nationalNo = nationalnumber;
            uctlShowDetilesPerson1.LOADDATAnationalno();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
