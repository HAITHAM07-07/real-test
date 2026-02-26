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
    public partial class uctlShowPersonDetilesWithFilter : UserControl
    {
        public uctlShowPersonDetilesWithFilter()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;

        }

        public  bool isfound=false;
        public bool isEditMode;
        public int ID { get; set; }
        public string NationalNO {  get; set; }

        void form2_back(int id)
        {
            uctlShowDetilesPerson1.number = id;
            uctlShowDetilesPerson1.LOADDATA();
            textBox1.Text = id.ToString();
            ID = id;
            isfound = uctlShowDetilesPerson1.ISFOUND;
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void uctlShowDetilesPerson1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 1;
            if (isEditMode == true) {

                groupBox1.Enabled = false;
                textBox1.Text=ID.ToString();
                uctlShowDetilesPerson1.number = ID;
                uctlShowDetilesPerson1.LOADDATA();
            
            }

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text.Trim())) 
            {
                return;
            }

            if (comboBox1.SelectedIndex == 0)
            {
                uctlShowDetilesPerson1.number = Convert.ToInt32((textBox1.Text));
                uctlShowDetilesPerson1.LOADDATA();
                ID = uctlShowDetilesPerson1.number;
              
            }
            else
            {
                uctlShowDetilesPerson1.nationalNo = (textBox1.Text);
                uctlShowDetilesPerson1.LOADDATAnationalno();
                ID=uctlShowDetilesPerson1.number;
                NationalNO = uctlShowDetilesPerson1.nationalNo;
            }
                isfound = uctlShowDetilesPerson1.ISFOUND;

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            AddAndEditPersoncs frm = new AddAndEditPersoncs(-1);
            frm.databack1 += form2_back;
            frm.ShowDialog();

        }
    }
}
