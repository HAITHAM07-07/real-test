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
    public class clsTestTypes
    {

        public enum Emode { addmode = 1, updatamode = 0 };

        public enum Etest {visioTest=1,writtenTest=2,streetTest=3 };


        Emode EMode = Emode.addmode;

        public clsTestTypes.Etest TestTypeID { get; set; }

        public string TestTypesName { get; set; }
        public decimal TestTypesfees { get; set; }
        public string TestTypeDescription { get; set; }




        public clsTestTypes(clsTestTypes.Etest id, string nameapp, string TestTypeDescription, decimal fees)
        {
            this.TestTypeID = id;
            this.TestTypesName = nameapp;
            this.TestTypesfees = fees;
            this.TestTypeDescription = TestTypeDescription;
            EMode = Emode.updatamode;


        }




        private bool _AddTestTypes()
        {



            this.TestTypeID =(clsTestTypes.Etest) DATATestTypes.AddOneTestTypes(this.TestTypesName, this.TestTypeDescription,this.TestTypesfees);
            if (this.TestTypeID > 0)
            {

                return true;

            }
            return false;

        }


        private bool _updateTestTypes()
        {



            return DATATestTypes.UpdateOneTestTypess((int)this.TestTypeID, this.TestTypesName,this.TestTypeDescription, this.TestTypesfees);



        }



        public bool Save()
        {

            switch (EMode)
            {
                case Emode.addmode:
                    {
                        if (_AddTestTypes())
                        {
                            EMode = Emode.updatamode;
                            return true;

                        }
                        return false;
                    }
                case Emode.updatamode:
                    {
                        if (_updateTestTypes())
                        {
                            return true;

                        }
                        return false;

                    }



            }
            return false;
        }


        static public clsTestTypes Find(clsTestTypes.Etest id)
        {
            string title = "", TestTypeDescription ="";
            decimal fees = 0;

            if (DATATestTypes.GetTestTypessByID((int)id, ref title ,ref fees, ref TestTypeDescription) == true)
            {
                return new clsTestTypes( id, title,  TestTypeDescription, fees);
            }
            return null;
        }


        static public DataTable GetallTestTypes()
        {
            return DATATestTypes.GetAllDataFromTestTypes();
        }











    }
}
