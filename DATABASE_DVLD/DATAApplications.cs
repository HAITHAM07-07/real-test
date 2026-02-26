using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseDVLD

{
    public class DATAApplications
    {




        static public DataTable GetAllDataFromApplications(string subQuery, string likeletters)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = @"select * from applications
                    WHERE (@likeletters = '' OR " + subQuery + @" LIKE @likeletters)";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@likeletters", likeletters + "%");

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


        static public int AddOneApplication(int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, short ApplicationStatus, DateTime LastStatusDate, float PaidFees,int CreatedByUserID)
        {
            int id = -1;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string query = @"INSERT INTO Applications ( 
                            ApplicantPersonID,ApplicationDate,ApplicationTypeID,
                            ApplicationStatus,LastStatusDate,
                            PaidFees,CreatedByUserID)
                             VALUES (@ApplicantPersonID,@ApplicationDate,@ApplicationTypeID,
                                      @ApplicationStatus,@LastStatusDate,
                                      @PaidFees,   @CreatedByUserID);
                             SELECT SCOPE_IDENTITY();";




            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue(@"ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue(@"ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue(@"ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue(@"ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue(@"LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue(@"PaidFees", PaidFees);
            command.Parameters.AddWithValue(@"CreatedByUserID", CreatedByUserID);



            connection.Close();
            try
            {

                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedid))
                {
                    id = insertedid;

                 
                }
                else
                {
                    id = -1;
                }




            }
            catch { }
            finally { connection.Close(); }


            return id;

        }


        static public bool UpdateOneApplication(int ApplicationID,int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, short ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            int row = 0;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = "UPDATE [dbo].[Applications]\r\n   " +
                "SET [ApplicantPersonID] = @ApplicantPersonID   \r\n" +
                "      ,[ApplicationDate] = @ApplicationDate    \r\n" +
                "      ,[ApplicationTypeID] = @ApplicationTypeID\r\n" +
                "      ,[ApplicationStatus] = @ApplicationStatus\r\n" +
                "      ,[LastStatusDate] = @LastStatusDate      \r\n" +
                "      ,[PaidFees] = @PaidFees                  \r\n" +
                "      ,[CreatedByUserID] = @CreatedByUserID    " +
                " WHERE   ApplicationID  = @ApplicationID ";


            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"ApplicationID", ApplicationID);
            command.Parameters.AddWithValue(@"ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue(@"ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue(@"ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue(@"ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue(@"LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue(@"PaidFees", PaidFees);
            command.Parameters.AddWithValue(@"CreatedByUserID", CreatedByUserID);



            try
            {

                connection.Open();

                row = command.ExecuteNonQuery();






            }
            catch { }
            finally { connection.Close(); }


            return (row > 0);

        }


        static public bool DeleteOneApplication(int IDApplication)
        {
            int row = 0;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = "DELETE FROM [dbo].[Applications]\r\n      WHERE  ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@ApplicationID", IDApplication);


            try
            {

                connection.Open();

                row = command.ExecuteNonQuery();






            }
            catch { }
            finally { connection.Close(); }


            return (row > 0);

        }


        public static bool GetApplicationByID(int ApplicationID,ref int ApplicantPersonID,ref DateTime ApplicationDate,ref int ApplicationTypeID,ref short ApplicationStatus,ref DateTime LastStatusDate,ref float PaidFees,ref int CreatedByUserID) 
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = @"select * from Applications where ApplicationID = @ApplicationID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    ApplicantPersonID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = Convert.ToInt16(reader["ApplicationStatus"]);
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = Convert.ToSingle((reader["PaidFees"]));
                    CreatedByUserID = (int)reader["CreatedByUserID"];

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