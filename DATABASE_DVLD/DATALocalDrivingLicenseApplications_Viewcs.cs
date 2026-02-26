using DatabaseDVLD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATABASE_DVLD
{
    public class DATALocalDrivingLicenseApplications_Viewcs
    {

        static public bool Find (int localappid,ref int passtestcount )
        {
            bool isfound = false;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string query = "select * from LocalDrivingLicenseApplications_View " +
                "where LocalDrivingLicenseApplicationID=@LocalDrivingLicenseApplicationID";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue(@"LocalDrivingLicenseApplicationID", localappid);
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) 
                {
                    isfound = true;

                    passtestcount = (int)reader["passedtestcount"];
                
                
                }
                else
                {
                    isfound = false;
                }


            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }



            return isfound;

        }

    }
}
