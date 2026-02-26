using BiusnessDVLD;
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
    public class clsLicense
    {
        public enum Emode { addmode = 1, updatamode = 0 };

        Emode EMode = Emode.addmode;
     public   int LicenseID { get; set; }
     public   int ApplicationID { get; set; }
     public   int DriverID { get; set; }
     public   int LicenseClass {  get; set; }
     public   DateTime IssueDate       {  get; set; }
     public   DateTime ExpirationDate  {  get; set; }
     public   string Notes            {  get; set; }
     public   decimal PaidFees           {  get; set; }
     public   bool IsActive           {  get; set; }
     public   byte IssueReason           {  get; set; }
     public   int CreatedByUserID { get; set; }

     public clsLicenseClasses classinfo
        {

            get
            {
                return clsLicenseClasses.Find(LicenseClass);
            }
        }



      static   public bool IsExists(int appID)
        {
            return DATALicenses.IsExistsByApplicationID(appID);
        }


        public clsLicense()
        {


            this.LicenseID =   1;
            this.ApplicationID = 1;
            this.DriverID = 1;
            this.LicenseClass = 1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = " ";
            this.IsActive = false;
            this.IssueReason = 1;
            this.CreatedByUserID = 1;
            this.PaidFees = 1;
            EMode = Emode.addmode;



        }




        public clsLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClass = LicenseClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;
            this.PaidFees = PaidFees;
            EMode = Emode.updatamode;


        }




        private bool _Addlicense()
        {



            this.LicenseID = DATALicenses.AddOnelicense(this. ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, this.IssueReason, this.CreatedByUserID);
            if (this.LicenseID > 0)
            {

                return true;

            }
            return false;

        }


        private bool _updatelicense()
        {



            return DATALicenses.UpdateOnelicense(this.LicenseID,this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, this.IssueReason, this.CreatedByUserID);


        }


   

        public bool Save()
        {

            switch (EMode)
            {
                case Emode.addmode:
                    {
                        if (_Addlicense())
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


        static public clsLicense Find(int licenseid)
        {

            int  ApplicationID = 1, DriverID = 1, LicenseClass = 1,   CreatedByUserID = 1;

            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = " ";
            decimal PaidFees = 0;
            bool IsActive = false;
            byte  IssueReason = 0;



            if (DATALicenses.GetlicenseByID(ref licenseid, ref  ApplicationID, ref  DriverID, ref  LicenseClass, ref  IssueDate, ref  ExpirationDate, ref  Notes, ref  PaidFees, ref  IsActive, ref IssueReason, ref  CreatedByUserID))
            {
                return new clsLicense(licenseid,  ApplicationID,  DriverID,  LicenseClass,  IssueDate,  ExpirationDate,  Notes,  PaidFees,  IsActive, IssueReason,  CreatedByUserID);
            }
            else
            {
                return null;
            }


        }


        static public clsLicense FindBypersonID(int PERSONID)
        {

            int LicenseID = 1, DriverID = 1, LicenseClass = 1, CreatedByUserID = 1, APPID = 1;

            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = " ";
            decimal PaidFees = 0;
            bool IsActive = false;
            byte issuereason = 0;



            if (DATALicenses.GetlicenseByPersonID(PERSONID, ref LicenseID, ref APPID, ref DriverID, ref LicenseClass, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref issuereason, ref CreatedByUserID))
            {
                return new clsLicense(LicenseID, APPID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, issuereason, CreatedByUserID);
            }
            else
            {
                return null;
            }


        }

        static public clsLicense FindByappID(int app)
        {

            int LicenseID = 1, DriverID = 1, LicenseClass = 1, CreatedByUserID = 1, personID = 1;

            DateTime IssueDate = DateTime.Now, ExpirationDate = DateTime.Now;
            string Notes = " ";
            decimal PaidFees = 0;
            bool IsActive = false;
            byte issuereason = 0;



            if (DATALicenses.GetlicenseByappID(ref personID, ref LicenseID, app, ref DriverID, ref LicenseClass, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref issuereason, ref CreatedByUserID))
            {
                return new clsLicense(LicenseID, app, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, issuereason, CreatedByUserID);
            }
            else
            {
                return null;
            }


        }




        static public DataTable GetallDatalicensesByPersonID(int id)
        {
            return DATALicenses.GetAllDataFromLicensesByPersonID(id);
        }





    }
}
