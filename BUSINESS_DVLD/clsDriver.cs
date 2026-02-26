using DATABASE_DVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUSINESS_DVLD
{
    public class clsDriver
    {

        public enum Emode { addmode = 1, updatamode = 0 };

        Emode EMode = Emode.addmode;
        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public int CreateByUserID { get; set; }
        public DateTime CreateDate { get; set; }
 


    

        public clsDriver()
        {


            this.DriverID = 0;
            this.PersonID = 0;
            this.CreateByUserID = 0;
            this.CreateDate = DateTime.Now;
            EMode = Emode.addmode;



        }




        public clsDriver(int Driverid, int Personid, int CreateByUserID, DateTime CREATEDATE)
        {
         
            this.DriverID = Driverid;   
            this.PersonID = Personid;   
            this.CreateByUserID = CreateByUserID;   
            this.CreateDate= CREATEDATE;
            EMode = Emode.updatamode;


        }


        public static clsDriver Find(int personid)
        {
            int driverid = 1, createdbyuserid = 1;
            DateTime date = DateTime.Now;
            if (DATADriver.GetDriverByIDperson(ref driverid, personid, ref createdbyuserid, ref date) )
               {return new clsDriver(driverid, personid, createdbyuserid, date); 
            }
            else
            {
                return null;
            }

        }


       private bool _AddDriver()
        {



            this.DriverID = DATADriver.AddOneDriver(this.PersonID,this.CreateByUserID,this.CreateDate);
            if (this.DriverID > 0)
            {

                return true;

            }
            return false;

        }


        private bool _updatelicense()
        {



            return DATADriver.UpdateOneDriver(this.DriverID,this.PersonID, this.CreateByUserID, this.CreateDate);


        } 

        static public int IsExists(int appID)
        {
            return DATADriver.IsExistsByApplicationID(appID);
        }



        public bool Save()
        {

            switch (EMode)
            {
                case Emode.addmode:
                    {
                        if (_AddDriver())
                        {
                            EMode = Emode.updatamode;
                            return true;

                        }
                        return false;
                    }
                case Emode.updatamode:
                    {
                        if (_updatelicense())
                        {
                            return true;

                        }
                        return false;

                    }



            }
            return false;
        }


      static  public DataTable GetAllDataBaseFromDrivers(string column,string letters)
        {



            return DATADriver.GetAllDataFromDriver(column,letters);

        }



    }
}
