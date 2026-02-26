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
    public class clsTestAppointment
    {



        public enum Emode { addmode = 1, updatamode = 0 };

        Emode EMode = Emode.addmode;
        public int AppointmentID { get; set; }

        public int Testtypeid { get; set; }

        public int localappid { get; set; }

        public int createbyuser { get; set; }

        public DateTime AppointmentDate { get; set; }

        public decimal Fees { get; set; }

        public bool IsLocked { get; set; }

        public string RetakeID { get; set; }


        public clsTestAppointment()
        {
            this.AppointmentID = 1;
            this.Testtypeid = -1;
            this.localappid = -1;
            this.createbyuser = -1;
            this.AppointmentDate = DateTime.Now;
            this.Fees = 1;
            this.IsLocked = true;
            this.RetakeID = "";
            EMode = Emode.addmode;
        }
         clsTestAppointment(int id,int testtype,int localappid , DateTime dateappointment, decimal fees,int createbyuser ,bool islocked,string retakeid)
        {
            this.AppointmentID = id;
            this.AppointmentDate = dateappointment;
            this.Fees = fees;
            this.RetakeID = retakeid;
            this.IsLocked = islocked;
            this.Testtypeid = testtype;
            this.localappid = localappid;
            this.createbyuser = createbyuser;
            EMode = Emode.updatamode;

        }

        static public DataTable getalldataAppointment(int idlocal,clsTestTypes.Etest typetest)
        {
           int typeteseint= Convert.ToInt32(typetest);

            return DATATestAppointment.GetAllDataFromTestAppointment(idlocal, typeteseint);
        }


        static public int isappointmenthere(int idlocal, clsTestTypes.Etest typetest)
        {
            int typeteseint = Convert.ToInt32(typetest);

            return DATATestAppointment.isAppointmenthere(idlocal, typeteseint);
        }


        static public clsTestAppointment Find(int id)
        {
            DateTime date = DateTime.Now;
            decimal fees = 1;
            bool islocked = false;
            int  testtype = -1, localappid = -1,createbyuser=-1 ;
            string retakeid = "";
            if (DATATestAppointment.GetAppointmentbyID(id,ref localappid,ref testtype, ref date, ref fees,ref createbyuser, ref islocked,ref retakeid)) 
            {
                return new clsTestAppointment(id, testtype, localappid, date, fees, createbyuser, islocked, retakeid);
            }
            else
            {
                return null;
            }


        }



        private bool _AddAppointment()
        {



            this.AppointmentID = DATATestAppointment.AddOneTestAppointment(this.Testtypeid, this.localappid, this.AppointmentDate, this.Fees, this.createbyuser, this.IsLocked, this.RetakeID);
            if (this.AppointmentID > 0)
            {

                return true;

            }
            return false;

        }


        private bool _UpdateAppointment()
        {



            return DATATestAppointment.UpdateOneAppointment(this.AppointmentID,this.Testtypeid, this.localappid, this.AppointmentDate, this.Fees, this.createbyuser, this.IsLocked, this.RetakeID);


        }


        static public  bool IsExists(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {



            return DATATestAppointment.IsExists(LocalDrivingLicenseApplicationID, TestTypeID);


        }


        static public bool IsExistsexam(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {



            return DATATestAppointment.IsExistsexam(LocalDrivingLicenseApplicationID, TestTypeID);


        }

        public bool Save()
        {

            switch (EMode)
            {
                case Emode.addmode:
                    {
                        if (_AddAppointment())
                        {
                            EMode = Emode.updatamode;
                            return true;

                        }
                        return false;
                    }
                case Emode.updatamode:
                    {
                        if (_UpdateAppointment())
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

