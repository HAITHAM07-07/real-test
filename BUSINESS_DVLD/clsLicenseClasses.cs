using DATABASE_DVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUSINESS_DVLD
{
    public  class clsLicenseClasses
    {

        public  int   LicenseClassID        { get; set; }
        public string ClassName             { get; set; }
        public string ClassDescription      { get; set; }
        public short  MinimumAllowedAge     { get; set; }
        public short  DefaultValidityLength { get; set; }
        public float  ClassFees             { get; set; }

    
        clsLicenseClasses(int id , string name,string Description,short age,short ValidityLength,float fees)
        {
           this. LicenseClassID = id;
           this. ClassName = name;
           this. ClassDescription = Description;
           this. MinimumAllowedAge = age;
           this. DefaultValidityLength = ValidityLength;
           this. ClassFees = fees;
        }


        static public clsLicenseClasses Find(int id)
        {

            string name = "", Description = "";
            short age = 0, ValidityLength = 0;
            float fees = 0;

            if (DATALicenseClasses.GetLicenseClassesByID(id,ref name,ref Description,ref age,ref ValidityLength,ref fees) == true)

            {
                return new clsLicenseClasses(id,  name,  Description,  age,  ValidityLength,  fees);
            }
            return null;
        }



        static public DataTable GetallLicenseClasses()
        {
            return DATALicenseClasses.GetAllDataFromLicenseClasses();
        }


    }
}
