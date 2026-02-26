using DatabaseDVLD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATABASE_DVLD
{
    public  class DATALicenses
    {

        public static bool IsExistsByApplicationID(int ApplicationID)
        {

            bool isfound = false;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);
            string Query = @"

                            
select * from Licenses
where ApplicationID = @id
                            ;";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"id", ApplicationID);
          

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null) { isfound = true; }
                
            }
            catch { }
            finally { connection.Close(); }



            return (isfound);
        }


        static public DataTable GetAllDataFromLicenses(string subQuery, string likeletters)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = @"select * from Licenses


           
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

        static public int AddOnelicense(int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate,DateTime ExpirationDate,string Notes,decimal PaidFees,bool IsActive,int IssueReason,int CreatedByUserID)
        {
            int idlicense = -1;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = "INSERT INTO [dbo].[Licenses]([ApplicationID],[DriverID],[LicenseClass],[IssueDate],[ExpirationDate],[Notes],[PaidFees],[IsActive],[IssueReason],[CreatedByUserID])" +
                " VALUES(@ApplicationID,@DriverID,@LicenseClass,@IssueDate,@ExpirationDate,@Notes,@PaidFees,@IsActive,@IssueReason,@CreatedByUserID)" +
                 "select SCOPE_IDENTITY(); ";
       



            SqlCommand command = new SqlCommand(Query, connection);


            command.Parameters.AddWithValue(@"ApplicationID", ApplicationID);
            command.Parameters.AddWithValue(@"DriverID", DriverID);
            command.Parameters.AddWithValue(@"LicenseClass", LicenseClass);
            command.Parameters.AddWithValue(@"IssueDate", IssueDate);
            command.Parameters.AddWithValue(@"ExpirationDate", ExpirationDate);
            if (string.IsNullOrEmpty(Notes))
            {
                command.Parameters.AddWithValue(@"Notes", DBNull.Value);

            }
            else
            {

                command.Parameters.AddWithValue(@"Notes", Notes);
            }
            command.Parameters.AddWithValue(@"PaidFees", PaidFees);
            command.Parameters.AddWithValue(@"IsActive", IsActive);
            command.Parameters.AddWithValue(@"IssueReason", IssueReason);
            command.Parameters.AddWithValue(@"CreatedByUserID", CreatedByUserID);
     







            try
            {

                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedid))
                {
                    idlicense = insertedid;


                }
                else
                {
                    idlicense = -1;
                }




            }
            catch { }
            finally { connection.Close(); }


            return idlicense;

        }


        static public bool UpdateOnelicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, decimal PaidFees, bool IsActive, int IssueReason, int CreatedByUserID)
        {
            int row = 0;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = "UPDATE [dbo].[Licenses]\r\n  " +
                " SET [ApplicationID] = @ApplicationID\r\n" +
                "      ,[DriverID] = @DriverID\r\n" +
                "      ,[LicenseClass] = @LicenseClass\r\n " +
                "     ,[IssueDate] = @IssueDate\r\n " +
                "     ,[ExpirationDate] = @ExpirationDate\r\n" +
                "      ,[Notes] = @Notes\r\n" +
                "      ,[PaidFees] = @PaidFees\r\n " +
                "     ,[IsActive] = @IsActive\r\n  " +
                "    ,[IssueReason] =@IssueReason\r\n " +
                "     ,[CreatedByUserID] = @CreatedByUserID\r\n " +
                "WHERE  LicenseID = @LicenseID;\r\n ";
           


            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);





            try
            {

                connection.Open();

                row = command.ExecuteNonQuery();






            }
            catch { }
            finally { connection.Close(); }


            return (row > 0);

        }


        static public bool DeleteOnelicense(int IDlicense)
        {
            int row = 0;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = "DELETE FROM [dbo].[Licenses]\r\n      WHERE  licenseID = @licenseID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@licenseID", IDlicense);


            try
            {

                connection.Open();

                row = command.ExecuteNonQuery();






            }
            catch { }
            finally { connection.Close(); }


            return (row > 0);

        }


        public static bool GetlicenseByID( ref int LicenseID, ref int ApplicationID, ref int DriverID, ref int LicenseClass, ref DateTime IssueDate, ref DateTime  ExpirationDate, ref string Notes, ref decimal PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = @"select * from Licenses where licenseID = @licenseID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"licenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    if(reader["Notes"] == DBNull.Value)
                    {
                        Notes = "";
                    }
                    else
                    {
                        Notes = (string)reader["Notes"];

                    }
                    PaidFees = (decimal)reader["PaidFees"];
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = (byte)reader["IssueReason"];
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

        public static bool GetlicenseByPersonID(int personid,ref int LicenseID, ref int ApplicationID, ref int DriverID, ref int LicenseClass, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref decimal PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = @"SELECT        People.PersonID, Drivers.DriverID AS Expr1, Licenses.*
FROM            Drivers INNER JOIN
                         People ON Drivers.PersonID = People.PersonID INNER JOIN
                         Licenses ON Drivers.DriverID = Licenses.DriverID
						 where People.PersonID= @personid;";

          

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"personid", personid);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    LicenseID = (int)reader["LicenseID"];
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    if (reader["Notes"] == DBNull.Value)
                    {
                        Notes = "";
                    }
                    else
                    {
                        Notes = (string)reader["Notes"];

                    }
                    PaidFees = (decimal)reader["PaidFees"];
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = (byte)reader["IssueReason"];
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

        public static bool GetlicenseByappID(ref int personid, ref int LicenseID,  int ApplicationID, ref int DriverID, ref int LicenseClass, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref decimal PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);


            string Query = "         SELECT        People.PersonID, Licenses.*\r\n" +
                "FROM            Drivers INNER JOIN\r\n                        " +
                " People ON Drivers.PersonID = People.PersonID INNER JOIN\r\n  " +
                "                       Licenses ON Drivers.DriverID = Licenses.DriverID\r\n\t\t\t\t\t\t" +
                " INNER JOIN Applications ON Applications.ApplicationID = Licenses.ApplicationID\r\n\t\t\t\t\t\t" +
                " where Applications.ApplicationID =@ApplicationID ";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    LicenseID = (int)reader["LicenseID"];
                    personid = (int)reader["PersonID"];
                    DriverID = (int)reader["DriverID"];
                    LicenseClass = (int)reader["LicenseClass"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    if (reader["Notes"] == DBNull.Value)
                    {
                        Notes = "";
                    }
                    else
                    {
                        Notes = (string)reader["Notes"];

                    }
                    PaidFees = (decimal)reader["PaidFees"];
                    IsActive = (bool)reader["IsActive"];
                    IssueReason = (byte)reader["IssueReason"];
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



        static public DataTable GetAllDataFromLicensesByPersonID(int personid)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = @"SELECT        Licenses.LicenseID, Licenses.ApplicationID, LicenseClasses.ClassName, Licenses.IssueDate, Licenses.ExpirationDate, Licenses.IsActive
FROM            Applications INNER JOIN
                         Licenses ON Applications.ApplicationID = Licenses.ApplicationID INNER JOIN
                         LicenseClasses ON Licenses.LicenseClass = LicenseClasses.LicenseClassID INNER JOIN
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

    }
}
