using DatabaseDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDVLD
{
    public class clsCountryData
    {




        static public DataTable GetAllDataFromcountry()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = "select CountryName from Countries";

            SqlCommand command = new SqlCommand(Query, connection);

            try
            {

                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {

                    dt.Load(reader);



                }

                reader.Close();



            }
            catch { }
            finally { connection.Close(); }


            return dt;

        }


        static public string GetCountryNamebyID(int IDCOUNTRY)
        {
            string namecountry = "";

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);
            string query = " \t\t  select CountryName from countries where countryid = @countryid;\r\n";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue(@"countryid", IDCOUNTRY);
            try
            {
                connection.Open();
                object result = cmd.ExecuteScalar();
                namecountry = result.ToString();
            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }









            return namecountry;
        }



    }
}
