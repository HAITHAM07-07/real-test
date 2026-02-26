using DatabaseDVLD;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATABASE_DVLD
{
    static public class DATALicenseClasses
    {


        static public DataTable GetAllDataFromLicenseClasses()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = "select * from LicenseClasses";

            SqlCommand command = new SqlCommand(Query, connection);
    

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // Load the whole reader once (this is the correct way)
                dt.Load(reader);

                reader.Close();
            }
            catch { }
            finally { connection.Close(); }

            return dt;
        }

        public static bool GetLicenseClassesByID(int id,ref string name,ref string Description,ref short age,ref short ValidityLength,ref float fees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = @"select * from LicenseClasses where LicenseClassID=@LicenseClassID;";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"LicenseClassID", id);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;


                    name = (string)reader["ClassName"];
                    Description = (string)reader["ClassDescription"];
                    age = Convert.ToInt16(reader["MinimumAllowedAge"]);
                    ValidityLength = Convert.ToInt16(reader["DefaultValidityLength"]);
                    fees = Convert.ToSingle(reader["ClassFees"]);





                }
                else
                {
                    isFound = false;
                }


                reader.Close();

            }
            catch
            {
                //erorr messag
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;

        }



    }
}
