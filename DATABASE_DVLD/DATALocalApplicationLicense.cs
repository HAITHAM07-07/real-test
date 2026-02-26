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
    public class DATALocalApplicationLicense
    {

        static public DataTable GetAllDataFromLocalApplications(string subQuery, string likeletters)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = @"select *  from LocalDrivingLicenseApplications_View
                    WHERE (@likeletters = '' OR " + subQuery + @" LIKE @likeletters)";

            SqlCommand command = new SqlCommand(Query, connection);
            command.Parameters.AddWithValue("@likeletters","%"+ likeletters + "%");

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


        static public int AddOneLocalApplication( int ApplicationID, int LicenseClassID)
        {
            int id = -1;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);


            string Query = @"INSERT INTO LocalDrivingLicenseApplications ( 
                            ApplicationID,LicenseClassID)
                             VALUES (@ApplicationID,@LicenseClassID);
                             SELECT SCOPE_IDENTITY();";



            SqlCommand command = new SqlCommand(Query, connection);


            command.Parameters.AddWithValue(@"ApplicationID", ApplicationID);
            command.Parameters.AddWithValue(@"LicenseClassID", LicenseClassID);
          



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


        static public bool UpdateOneLocalApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
        {
            int row = 0;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = "UPDATE [dbo].[LocalLocalApplications]\r\n   " +
                "SET [ApplicationID] = <@ApplicationID   >\r\n" +
                "      ,[LicenseClassID] = <@LicenseClassID    >\r\n" +
                " WHERE   LocalDrivingLicenseApplicationID  = @LocalDrivingLicenseApplicationID ";


            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue(@"ApplicationID", ApplicationID);
            command.Parameters.AddWithValue(@"LicenseClassID", LicenseClassID);
            




            try
            {

                connection.Open();

                row = command.ExecuteNonQuery();






            }
            catch { }
            finally { connection.Close(); }


            return (row > 0);

        }

        
        static public bool DeleteOneLocalApplication(int IDLocalApplication)
        {
            int row = 0;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = "DELETE FROM [dbo].[LocalDrivingLicenseApplications]\r\n      WHERE  LocalDrivingLicenseApplicationID = @LocalApplicationID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@LocalApplicationID", IDLocalApplication);


            try
            {

                connection.Open();

                row = command.ExecuteNonQuery();






            }
            catch { }
            finally { connection.Close(); }


            return (row > 0);

        }


        public static bool GetLocalApplicationByID(int LocalDrivingLicenseApplicationID, ref  int ApplicationID, ref int LicenseClassID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = @"select * from LocalDrivingLicenseApplications where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
            
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

        public static int IsExists(int idperson, int idclasstype)
        {

            int isfound = -1;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);
            string Query = @"

                            
SELECT        Applications.ApplicationID
FROM            Applications INNER JOIN
                         LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID INNER JOIN
                         LicenseClasses ON LocalDrivingLicenseApplications.LicenseClassID = LicenseClasses.LicenseClassID
						 where  Applications.ApplicationStatus = 1 and Applications.ApplicantPersonID = @id and LocalDrivingLicenseApplications.LicenseClassID=@idclass

                            ;";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"id", idperson);
            command.Parameters.AddWithValue(@"idclass", idclasstype);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null) { isfound =Convert.ToInt32( result.ToString()); }

            }
            catch { }
            finally { connection.Close(); }



            return (isfound);
        }

        public static int IsExistslicense(int idperson, int idclasstype)
        {

            int isfound = -1;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);
            string Query = @"

                            
SELECT     true=1   
FROM            Applications INNER JOIN
                         LocalDrivingLicenseApplications ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID INNER JOIN
                         People ON Applications.ApplicantPersonID = People.PersonID INNER JOIN
                         Licenses AS Licenses_1 ON Applications.ApplicationID = Licenses_1.ApplicationID
						 where People.PersonID = @id and Licenses_1.LicenseClass = @idclass;

                            ;";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"id", idperson);
            command.Parameters.AddWithValue(@"idclass", idclasstype);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null) { isfound = Convert.ToInt32(result.ToString()); }

            }
            catch { }
            finally { connection.Close(); }



            return (isfound);
        }


    }
}
