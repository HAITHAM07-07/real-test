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
    public class DATAInternationalLicense
    {
        static public DataTable GetAllDataFromLicensesByPersonID(int personid)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = @"
SELECT        InternationalLicenses.InternationalLicenseID, InternationalLicenses.ApplicationID, InternationalLicenses.IssuedUsingLocalLicenseID, InternationalLicenses.IssueDate, InternationalLicenses.ExpirationDate, 
                         InternationalLicenses.IsActive
FROM            Applications INNER JOIN
                         InternationalLicenses ON Applications.ApplicationID = InternationalLicenses.ApplicationID INNER JOIN
                         People ON Applications.ApplicantPersonID = People.PersonID
						  where People.PersonID= @id";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue(@"id", personid);

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


        static public DataTable GetAllDataFromInternational(string subQuery, string likeletters)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = @"	select * from InternationalLicenses



           
                    WHERE (@likeletters = '' OR " + subQuery + @" LIKE @likeletters)";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue(@"likeletters", likeletters + "%");

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


        static public int AddOneInternational(int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID,DateTime IssueDate,DateTime ExpirationDate,bool IsActive,int CreatedByUserID)
        {
            int idInternational = -1;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);



            string Query =
       "INSERT INTO [dbo].[InternationalLicenses] " +
       "([ApplicationID],[DriverID],[IssuedUsingLocalLicenseID],[IssueDate],[ExpirationDate],[IsActive],[CreatedByUserID]) " +
       "VALUES (@ApplicationID,@DriverID,@IssuedUsingLocalLicenseID,@IssueDate,@ExpirationDate,@IsActive,@CreatedByUserID); " +
       "SELECT SCOPE_IDENTITY();";





            SqlCommand command = new SqlCommand(Query, connection);


            command.Parameters.AddWithValue(@"ApplicationID", ApplicationID);
            command.Parameters.AddWithValue(@"DriverID", DriverID);
            command.Parameters.AddWithValue(@"IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue(@"IssueDate", IssueDate);
            command.Parameters.AddWithValue(@"ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue(@"IsActive", IsActive);
            command.Parameters.AddWithValue(@"CreatedByUserID", CreatedByUserID);



            try
            {

                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedid))
                {
                    idInternational = insertedid;


                }
                else
                {
                    idInternational = -1;
                }




            }
            catch { }
            finally { connection.Close(); }


            return idInternational;

        }

        public static int IsExistsByLocalLicenseID(int locallicenseid)
        {

            int numberidInternational = 0;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);
            string Query = @"

                            
   select InternationalLicenses.InternationalLicenseID from InternationalLicenses
   where IssuedUsingLocalLicenseID = @locallicenseid and IsActive=1;";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"locallicenseid", locallicenseid);


            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int id))
                { numberidInternational = id; }
                ;

            }
            catch { }
            finally { connection.Close(); }



            return (numberidInternational);
        }


        static public bool UpdateOneInternationalbylocallicense(int IssuedUsingLocalLicenseID)
        {
            int row = 0;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = "UPDATE [dbo].[InternationalLicenses]\r\n  " +
                " SET [IsActive] = 0\r\n" +


                "WHERE  IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID;\r\n ";



            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
        






            try
            {

                connection.Open();

                row = command.ExecuteNonQuery();






            }
            catch { }
            finally { connection.Close(); }


            return (row > 0);

        }
        

        public static bool GetInternationalByID(int internationallicenseid,ref int ApplicationID, ref int DriverID, ref int IssuedUsingLocalLicenseID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = @"

               select * from InternationalLicenses
						  where InternationalLicenseID = @InternationalLicenseID;

";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"InternationalLicenseID", internationallicenseid);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsActive = (bool)reader["IsActive"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];



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
