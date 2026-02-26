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
    static public class clsDataUsers
    {

        static public DataTable GetAllDataFromUsers(string subQuery, string likeletters)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = @"select Users.UserID,Users.PersonID ,FullName =
                          People.FirstName + ' ' + People.SecondName + ' ' +  People.ThirdName + ' ' + People.LastName,
                          Users.UserName,Users.IsActive from Users 
                          inner join People on People.PersonID = Users.PersonID
 
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

        static public int AddOneUser(int PersonID, string UserName, string Password, bool IsActive)
        {
            int iduser = -1;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = "INSERT INTO [Users] ([PersonID],[UserName],[Password],[IsActive])" +
                " VALUES(@PersonID,@UserName,@Password,@IsActive);" +
                "select SCOPE_IDENTITY();";




            SqlCommand command = new SqlCommand(Query, connection);


            command.Parameters.AddWithValue(@"PersonID", PersonID);
            command.Parameters.AddWithValue(@"UserName", UserName);
            command.Parameters.AddWithValue(@"Password", Password);
            command.Parameters.AddWithValue(@"IsActive", IsActive);






            try
            {

                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedid))
                {
                    iduser = insertedid;


                }
                else
                {
                    iduser = -1;
                }




            }
            catch { }
            finally { connection.Close(); }


            return iduser;

        }


        static public bool UpdateOneUser(int IDUser, string UserName, string Password, bool IsActive)
        {
            int row = 0;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = "UPDATE [dbo].[Users]\r\n" +
                "   SET [UserName] = @UserName\r\n" +
                "      ,[Password] = @Password\r\n " +
                "     ,[IsActive] = @IsActive\r\n " +

                " WHERE   UserID  = @UserID ";


            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@UserID", IDUser);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);





            try
            {

                connection.Open();

                row = command.ExecuteNonQuery();






            }
            catch { }
            finally { connection.Close(); }


            return (row > 0);

        }


        static public bool DeleteOneUser(int IDUser)
        {
            int row = 0;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = "DELETE FROM [dbo].[Users]\r\n      WHERE  UserID = @UserID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@UserID", IDUser);


            try
            {

                connection.Open();

                row = command.ExecuteNonQuery();






            }
            catch { }
            finally { connection.Close(); }


            return (row > 0);

        }


        public static bool GetUserByID(int UserID, ref int personid, ref string UserName, ref string Password, ref bool IsActive)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = @"select * from Users where UserID = @UserID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"UserID", UserID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    personid = (int)reader["PersonID"];
                    UserName = (string)reader["UserName"];
                    Password = (string)reader["Password"];
                    IsActive = (bool)reader["IsActive"];




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


        public static bool GetUserByUsernameAndPassword( ref int UserID, ref int personid,  string UserName,  string Password, ref bool IsActive)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = @"select * from Users Where Username = @Username and Password = @Password;";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"Username", UserName);
            command.Parameters.AddWithValue(@"Password", Password);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    UserID = (int)reader["UserID"];
                    personid = (int)reader["PersonID"];
                   
                    IsActive = (bool)reader["IsActive"];




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

        public static bool IsExists(int UserID)
        {

            bool isfound = false;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);
            string Query = @"

                              select found = 1  from Users
                              where Users.PersonID = @id

                            ;";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"id", UserID);

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



    }
}
