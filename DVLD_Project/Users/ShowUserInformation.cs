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
    public partial class ShowUserInformation : Form
    {
        public ShowUserInformation(int idperson,int iduser)
        {
            InitializeComponent();
            uctlShowDetailesUser1.Userid = iduser;
            uctlShowDetailesUser1.Personid = idperson;
            uctlShowDetailesUser1.LoadData();
        }
    }
}
