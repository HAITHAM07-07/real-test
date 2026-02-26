using DatabaseDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnessDVLD
{
    public class clsBuisnessCountry
    {

        public string namecountry { get; set; }




        static public DataTable getalldatacountry()
        {

            return clsCountryData.GetAllDataFromcountry();
        }


        static public string getnamecountrybyid(int id)
        {
            return clsCountryData.GetCountryNamebyID(id);
        }

    }
}
