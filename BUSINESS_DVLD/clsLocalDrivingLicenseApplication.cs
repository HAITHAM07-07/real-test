using DATABASE_DVLD;
using DatabaseDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUSINESS_DVLD
{
    public class clsLocalDrivingLicenseApplication
    {


        public enum Emode { addmode = 1, updatamode = 0 };


        Emode EMode = Emode.addmode;


        public int LocalDrivingLicenseApplicationID { get; set; }
        public int ApplicationID { get; set; }
        public int LicenseClassID { get; set; }

        public clsLicenseClasses ClassInfo
        {
            get
            {
                return clsLicenseClasses.Find(LicenseClassID);
            }
        }

        public clsLocalDrivingLicenseApplication()
        {
            ApplicationID = 0;
            LocalDrivingLicenseApplicationID = 0;
            LicenseClassID = 0; 
            EMode = Emode.addmode;
        }
        clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
        {
            this.ApplicationID = ApplicationID;
            this.LocalDrivingLicenseApplicationID= LocalDrivingLicenseApplicationID;
            this.LicenseClassID= LicenseClassID;
            EMode = Emode.updatamode;


        }




        private bool _AddLocalApplications()
        {



            this.LocalDrivingLicenseApplicationID = DATALocalApplicationLicense.AddOneLocalApplication(this.ApplicationID,this.LicenseClassID);
            if (this.LocalDrivingLicenseApplicationID > 0)
            {

                return true;

            }
            return false;

        }


        private bool _updateLocalApplications()
        {



            return DATALocalApplicationLicense.UpdateOneLocalApplication(this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID);


        }



        public bool Save()
        {

            switch (EMode)
            {
                case Emode.addmode:
                    {
                        if (_AddLocalApplications())
                        {
                            EMode = Emode.updatamode;
                            return true;

                        }
                        return false;
                    }
                case Emode.updatamode:
                    {
                        if (_updateLocalApplications())
                        {
                            return true;

                        }
                        return false;

                    }



            }
            return false;
        }


        static public clsLocalDrivingLicenseApplication Find(int idlocal)
        {
            int idapp = 0, licenseid = 0;
    

            if (DATALocalApplicationLicense.GetLocalApplicationByID(idlocal,ref idapp, ref licenseid) == true)
            {
                
                return new clsLocalDrivingLicenseApplication(idlocal, idapp, licenseid);
            }
            return null;
        }


        static public DataTable GetallLocalApplications(string colum, string letter)
        {
            return DATALocalApplicationLicense.GetAllDataFromLocalApplications(colum, letter);
        }


       static public int IsExists(int idperson, int idclasstype)
        {
            return DATALocalApplicationLicense.IsExists(idperson, idclasstype);
        }

        static public int IsExistslicense(int idperson, int idclasstype)
        {
            return DATALocalApplicationLicense.IsExistslicense(idperson, idclasstype);
        }



        static public bool DeleteLocalApplication(int id)
        {

            return DATALocalApplicationLicense.DeleteOneLocalApplication(id);

        }
    }
}
