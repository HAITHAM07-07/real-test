using BuisnessDVLD;
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
    public  class clsApplicationtypes
    {

        public enum Emode { addmode = 1, updatamode = 0 };

        Emode EMode = Emode.addmode;
        public int    IDApplicationtypes   { get; set; }
        public string ApplicationtypesName { get; set; }
        public decimal Applicationtypesfees { get; set; }
 




        public clsApplicationtypes()
        {
            IDApplicationtypes = 0;
            ApplicationtypesName = "";
            Applicationtypesfees = 0;
            EMode = Emode.addmode;

        }




        public clsApplicationtypes(int id, string nameapp, decimal fees)
        {
            this.IDApplicationtypes = id;
            this.ApplicationtypesName = nameapp;
            this.Applicationtypesfees = fees;
         
            EMode = Emode.updatamode;


        }




        private bool _AddApplicationtypes()
        {



            this.IDApplicationtypes = DATAApplicationType.AddOneApplicationTypes(this.ApplicationtypesName, this.Applicationtypesfees);
            if (this.IDApplicationtypes > 0)
            {

                return true;

            }
            return false;

        }


        private bool _updateApplicationtypes()
        {



            return DATAApplicationType.UpdateOneApplicationTypes(this.IDApplicationtypes,this.ApplicationtypesName,this.Applicationtypesfees);
                   


        }


     
        public bool Save()
        {

            switch (EMode)
            {
                case Emode.addmode:
                    {
                        if (_AddApplicationtypes())
                        {
                            EMode = Emode.updatamode;
                            return true;

                        }
                        return false;
                    }
                case Emode.updatamode:
                    {
                        if (_updateApplicationtypes())
                        {
                            return true;

                        }
                        return false;

                    }



            }
            return false;
        }


        static public clsApplicationtypes Find(int id)
        {
            string title = "";
            decimal fees = 0;

            if(DATAApplicationType.GetApplicationtypesByID(id,ref title,ref fees) == true)
            {
                return new clsApplicationtypes(id,title,fees);  
            }
            return null;
        }


        static public DataTable GetallApplicationtypes()
        {
            return DATAApplicationType.GetAllDataFromApplicationTypes();
        }


     








    }
}
