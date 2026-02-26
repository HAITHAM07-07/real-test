using DATABASE_DVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUSINESS_DVLD
{
    public class clsInternationalLicense
    {

        enum Emode { add,edit }
        Emode mode = Emode.add;

        public int      Internationid    { get; set; }
        public int       APPID            { get; set; }
        public int      DriverID         { get; set; }
        public int      IssuedLocalID     { get; set; }
        public int       CreatedByUserID { get; set; }
        public bool     IsActive         { get; set; }
        public DateTime IssuedDate        { get; set; }
        public DateTime ExpirationDate   { get; set; }


        public  clsInternationalLicense()
        {
            Internationid = 0;
            APPID = 0;
            DriverID = 0;
            IssuedLocalID = 0;
            CreatedByUserID = 0;
            IsActive =false;
            IssuedDate = DateTime.Now;
            ExpirationDate = DateTime.Now;
            mode = Emode.add;

        }


        public clsInternationalLicense(int internationLicenseID,int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {

          this. Internationid = internationLicenseID;
          this. APPID = ApplicationID;
          this. DriverID = DriverID;
          this. IssuedLocalID = IssuedUsingLocalLicenseID;
          this. CreatedByUserID = CreatedByUserID;
          this. IsActive = IsActive;
          this. IssuedDate = IssueDate;
          this. ExpirationDate = ExpirationDate;
            mode = Emode.edit;

        }


        static public DataTable GetallDatalicensesByPersonID(int id)
        {
            return DATAInternationalLicense.GetAllDataFromLicensesByPersonID(id);
        }

        static public DataTable GetAllDataBaseFromInternational(string column, string letters)
        {



            return DATAInternationalLicense.GetAllDataFromInternational(column, letters);

        }

        static public int IsExists(int idlocalLicense)
        {



            return DATAInternationalLicense.IsExistsByLocalLicenseID(idlocalLicense);

        }



        static public clsInternationalLicense Find(int internationallicenseid)
        {
            int app = 0, driver = 0, local = 0, user = 0;
            bool isactive = false;
            DateTime issuedate = DateTime.Now,expirationdate = DateTime.Now;
            if(DATAInternationalLicense.GetInternationalByID(internationallicenseid,ref app, ref driver, ref local, ref issuedate, ref expirationdate,ref isactive,ref user))
            {
                return new clsInternationalLicense(internationallicenseid, app, driver, local, issuedate, expirationdate, isactive, user);
            
            }
            else
            {
                return null;
            }



          
        }


        private bool _AddLicense()
        {



            this.Internationid = DATAInternationalLicense.AddOneInternational(this.APPID,this.DriverID,this.IssuedLocalID,this.IssuedDate,this.ExpirationDate,this.IsActive,this.CreatedByUserID);
            if (this.Internationid > 0)
            {

                return true;

            }
            return false;

        }
        public bool _updatelicense(int localid)
        {

            //update international license make it isactive = false using local one 

            return DATAInternationalLicense.UpdateOneInternationalbylocallicense(localid); 
        }

        public bool Save()
        {

            switch (mode)
            {
                case Emode.add:
                    {
                        if (_AddLicense())
                        {
                            mode = Emode.edit;
                            return true;

                        }
                        return false;
                    }
                case Emode.edit:
                    {
                        if (5==5)
                        {
                            return true;

                        }

                    }



            }
            return false;
        }

    }
}
