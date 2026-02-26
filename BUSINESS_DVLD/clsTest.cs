using BUSINESS_DVLD;
using DATABASE_DVLD;
using DatabaseDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessDVLD
{
    public class clsTest
    {



        public enum Emode { addmode = 1, updatamode = 0 };

        Emode EMode = Emode.addmode;
        public int TestID { get; set; }

        public int TestAppointmentID { get; set; }

        public int TestResult { get; set; }

        public string Notes { get; set; }

        public int CreatedByUserID { get; set; }


        public clsTest()
        {
            this.TestID = 1;
            this.TestResult = -1;
            this.TestAppointmentID = -1;
            this.CreatedByUserID = -1;
            this.Notes ="";

            EMode = Emode.addmode;
        }
     

   

        private bool _Addtest()
        {



            this.TestID = DATATest.AddOneTest(this.TestAppointmentID, this.TestResult, this.Notes, this.CreatedByUserID);
            if (this.TestID > 0)
            {

                return true;

            }
            return false;

        }


        private bool _Updatetest()
        {



            return false;


        }

        
        static public int IsExists(int appointmentid)
        {



            return DATATest.IsExists(appointmentid);


        }
 


        public bool Save()
        {

            switch (EMode)
            {
                case Emode.addmode:
                    {
                        if (_Addtest())
                        {
                            EMode = Emode.updatamode;
                            return true;

                        }
                        return false;
                    }
                case Emode.updatamode:
                    {
                        if (_Updatetest())
                        {
                            return true;

                        }
                        return false;

                    }



            }
            return false;
        }



    }
}

