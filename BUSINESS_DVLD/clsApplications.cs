using BuisnessDVLD;
using DATABASE_DVLD;
using DatabaseDVLD;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BUSINESS_DVLD
{
    public class clsApplications
    {



        public enum Emode { addmode = 1, updatamode = 0 };


        Emode EMode = Emode.addmode;


      public int        ApplicationID      {  get; set; }
      public int        ApplicantPersonID { get; set; }
      public  DateTime  ApplicationDate      {  get; set; }
      public int        ApplicationTypeID         {  get; set; }
      public short      ApplicationStatus       {  get; set; }
      public DateTime   LastStatusDate       {  get; set; }
      public float      PaidFees                {  get; set; }
      public int        CreatedByUserID           {  get; set; }


      public clsBuisnessPeople Personinfo
        {
            get
            {
                return clsBuisnessPeople.Find(ApplicantPersonID);

            }
        }

        public clsApplications()
        {
            ApplicationID     = 0;
            ApplicantPersonID = 0;
            ApplicationTypeID = 0;
            ApplicationStatus = 0;
            LastStatusDate    = DateTime.Now;
            PaidFees          = 0;
            ApplicationDate   = DateTime.Now;
            CreatedByUserID   = 0;
            EMode = Emode.addmode;
        }
        clsApplications(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, short ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationTypeID = ApplicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.ApplicationDate = ApplicationDate;
            this.CreatedByUserID = CreatedByUserID;
            EMode = Emode.updatamode;


        }




        private bool _AddApplications()
        {



            this.ApplicationID = DATAApplications.AddOneApplication(this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID, this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
            if (this.ApplicationID > 0)
            {

                return true;

            }
            return false;

        }


        private bool _updateApplications()
        {



            return DATAApplications.UpdateOneApplication(this.ApplicationID,this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID, this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);



        }



        public bool Save()
        {

            switch (EMode)
            {
                case Emode.addmode:
                    {
                        if (_AddApplications())
                        {
                            EMode = Emode.updatamode;
                            return true;

                        }
                        return false;
                    }
                case Emode.updatamode:
                    {
                        if (_updateApplications())
                        {
                            return true;

                        }
                        return false;

                    }



            }
            return false;
        }


        static public clsApplications Find(int id)
        {
            int  personid = 0, typeapp = 0, createduserid = 0;
            short statusid = 0;
            float fees = 0;
            DateTime appdate = DateTime.Now,laststatusdate=DateTime.Now;

            if (DATAApplications.GetApplicationByID(id, ref personid, ref appdate , ref typeapp, ref statusid, ref laststatusdate, ref fees, ref createduserid) == true)
            {
                return new clsApplications(id,  personid,  appdate,  typeapp,  statusid,  laststatusdate,  fees,  createduserid);
            }
            return null;
        }



        static public bool DeleteApplication(int id)
        {

            return DATAApplications.DeleteOneApplication(id);

        }


    }
}
