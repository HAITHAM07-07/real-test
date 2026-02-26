using DATABASE_DVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUSINESS_DVLD
{
    public class clsDetainedLicense
    {
        public enum Emode { addmode = 1, updatamode = 0 };

        Emode EMode = Emode.addmode;
        public int DetainedID { get; set; }
        public int LicenseID { get; set; }
        public DateTime DetainedDate { get; set; }
        public decimal PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool IsReleased { get; set; }
        public DateTime? ReleasDate { get; set; }
        public int ReleasByUserID { get; set; }
        public int ReleasApplication { get; set; }



        static public clsDetainedLicense FindByLicenseID(int licenseid)
        {

            int DetainedID = 1, ReleasByUserID = 1, ReleasApplication = 1, CreatedByUserID = 1;

            DateTime DetainedDate = DateTime.Now, ReleasDate = DateTime.Now;
            decimal PaidFees = 0;
            bool IsReleased = false;



            if (DATADetainedLicense.GetDetainedByLicenseID(ref DetainedID, licenseid, ref PaidFees, ref DetainedDate, ref CreatedByUserID, ref IsReleased, ref ReleasDate, ref ReleasByUserID, ref ReleasApplication))
            {
                return new clsDetainedLicense( DetainedID, licenseid,  PaidFees,  DetainedDate,  CreatedByUserID,  IsReleased, ReleasDate,  ReleasByUserID, ReleasApplication);
            }
            else
            {
                return null;
            }


        }

        static public clsDetainedLicense FindByLicenseIDandisdetained(int licenseid)
        {

            int DetainedID = 1, ReleasByUserID = 1, ReleasApplication = 1, CreatedByUserID = 1;

            DateTime DetainedDate = DateTime.Now, ReleasDate = DateTime.Now;
            decimal PaidFees = 0;
            bool IsReleased = false;



            if (DATADetainedLicense.GetDetainedByLicenseIDandisdetaind(ref DetainedID, licenseid, ref PaidFees, ref DetainedDate, ref CreatedByUserID, ref IsReleased, ref ReleasDate, ref ReleasByUserID, ref ReleasApplication))
            {
                return new clsDetainedLicense(DetainedID, licenseid, PaidFees, DetainedDate, CreatedByUserID, IsReleased, ReleasDate, ReleasByUserID, ReleasApplication);
            }
            else
            {
                return null;
            }


        }


        public clsDetainedLicense()
        {


            DetainedID = 0;
            LicenseID = 0;
            DetainedDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = 0;
            IsReleased = false;
            ReleasDate = DateTime.Now;
            ReleasByUserID = 0;
            ReleasApplication = 0;
            EMode = Emode.addmode;



        }




        public clsDetainedLicense(int DetainedID, int LicenseID, decimal PaidFees, DateTime DetainedDate, int CreatedByUserID, bool IsReleased, DateTime ReleasedDate, int ReleasByUserID, int ReleasApplicationID)
        {
            this.DetainedID = DetainedID;

            this.LicenseID = LicenseID;
            this.DetainedDate = DetainedDate;
            this.PaidFees = PaidFees;

            this.CreatedByUserID = CreatedByUserID;

            this.IsReleased = IsReleased;
            this.ReleasDate = ReleasedDate;
            this.ReleasByUserID = ReleasByUserID;
            this.ReleasApplication = ReleasApplicationID;

            this.EMode = Emode.updatamode;

        }

        

        private bool _AddDetainedlicense()
        {



            this.DetainedID = DATADetainedLicense.AddOneDetained( this. LicenseID, this. PaidFees, this. DetainedDate, this. CreatedByUserID, this. IsReleased, this. ReleasDate, this. ReleasByUserID, this. ReleasApplication);
            if (this.LicenseID > 0)
            {

                return true;

            }
            return false;

        }

        
        static public bool IsExists(int licenseid)
        {
            return DATADetainedLicense.IsExistsByLicenseID(licenseid);
        }
        

        private bool _updateDetainedlicense()
        {



            return DATADetainedLicense.UpdateOneDetained(this.DetainedID,this.LicenseID, this.PaidFees, this.DetainedDate, this.CreatedByUserID, this.IsReleased, this.ReleasDate, this.ReleasByUserID, this.ReleasApplication);


        }


        public bool Save()
        {

            switch (EMode)
            {
                case Emode.addmode:
                    {
                        if (_AddDetainedlicense())
                        {
                            EMode = Emode.updatamode;
                            return true;

                        }
                        return false;
                    }
                case Emode.updatamode:
                    {
                        if (_updateDetainedlicense())
                        {
                            return true;

                        }
                        return false;

                    }



            }
            return false;
        }


      



        static public DataTable GetallDataDetainedlicenses(string subquery, string letters)
        {
            return DATADetainedLicense.GetAllDataFromDetaineds(subquery, letters);
        }
        
    }
}
