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
    public class clsDatabasePeople
    {



        static public DataTable GetAllDataFromPeople(string subQuery, string likeletters)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = @"SELECT People.PersonID, People.NationalNo, People.FirstName, People.SecondName, 
                    People.ThirdName, People.LastName,
                    Gendor = case 
                        when People.Gendor = 0 then 'male'
                        when People.Gendor = 1 then 'female'
                        else 'no info'
                    end,
                    People.DateOfBirth, People.Address, People.Phone, People.Email,
                    Countries.CountryName
                    FROM People 
                    INNER JOIN Countries ON People.NationalityCountryID + 2 = Countries.CountryID 
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

        static public int AddOnePerson(string NationalNo, string First, string second, string third, string last, int gendor,
         string address, string phone, string email, int NationalityCountryID, string Image, DateTime Dateofbirth)
        {
            int id = -1;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = "INSERT INTO [People] ([NationalNo],[FirstName],[SecondName],[ThirdName],[LastName],[DateOfBirth],[Gendor],[Address],[Phone],[Email],[NationalityCountryID],[ImagePath]) " +
                " VALUES(@NationalNo,@FirstName,@SecondName,@ThirdName,@LastName,@DateOfBirth,@Gendor,@Address,@Phone,@Email,@NationalityCountryID,@ImagePath);" +
                "select SCOPE_IDENTITY();";




            SqlCommand command = new SqlCommand(Query, connection);


            command.Parameters.AddWithValue(@"NationalNo", NationalNo);
            command.Parameters.AddWithValue(@"FirstName", First);
            command.Parameters.AddWithValue(@"SecondName", second);

            if (third != null)
            {

                command.Parameters.AddWithValue(@"ThirdName", third);
            }
            else
            {
                command.Parameters.AddWithValue(@"ThirdName", DBNull.Value);
            }
            command.Parameters.AddWithValue(@"LastName", last);
            command.Parameters.AddWithValue(@"DateOfBirth", Dateofbirth);
            command.Parameters.AddWithValue(@"Gendor", gendor);
            command.Parameters.AddWithValue(@"Address", address);
            command.Parameters.AddWithValue(@"Phone", phone);

            if (email != null)
            {

                command.Parameters.AddWithValue(@"Email", email);
            }
            else
            {
                command.Parameters.AddWithValue(@"Email", DBNull.Value);
            }

            command.Parameters.AddWithValue(@"NationalityCountryID", NationalityCountryID);

            if (Image != null)
            {

                command.Parameters.AddWithValue(@"ImagePath", Image);
            }
            else
            {
                command.Parameters.AddWithValue(@"ImagePath", DBNull.Value);
            }





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


        static public bool UpdateOnePerson(int IDPerson, string NationalNo, string First, string second, string third, string last, int gendor,
       string address, string phone, string email, int NationalityCountryID, string Image, DateTime Dateofbirth)
        {
            int row = 0;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = "UPDATE [dbo].[People]\r\n" +
                "   SET [NationalNo] = @NationalNo\r\n" +
                "      ,[FirstName] = @FirstName\r\n " +
                "     ,[SecondName] = @SecondName\r\n " +
                "     ,[ThirdName] = @ThirdName\r\n  " +
                "    ,[LastName] = @LastName\r\n    " +
                "  ,[DateOfBirth] = @DateOfBirth\r\n     " +
                " ,[Gendor] = @Gendor\r\n      " +
                ",[Address] = @Address\r\n     " +
                " ,[Phone] = @Phone\r\n      " +
                ",[Email] =  @Email\r\n      " +
                ",[NationalityCountryID] = @NationalityCountryID\r\n     " +
                " ,[ImagePath] = @ImagePath\r\n" +
                " WHERE   PersonID  = @PersonID ";


            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@PersonID", IDPerson);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", First);
            command.Parameters.AddWithValue("@SecondName", second);

            if (third != null)
            {

                command.Parameters.AddWithValue("@ThirdName", third);
            }
            else
            {
                command.Parameters.AddWithValue("@ThirdName", DBNull.Value);
            }
            command.Parameters.AddWithValue("@LastName", last);
            command.Parameters.AddWithValue("@DateOfBirth", Dateofbirth);
            command.Parameters.AddWithValue("@Gendor", gendor);
            command.Parameters.AddWithValue("@Address", address);
            command.Parameters.AddWithValue("@Phone", phone);

            if (email != null)
            {

                command.Parameters.AddWithValue("@Email", email);
            }
            else
            {
                command.Parameters.AddWithValue("@Email", DBNull.Value);
            }

            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);

            if (Image != null)
            {

                command.Parameters.AddWithValue("@ImagePath", Image);
            }
            else
            {
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            }





            try
            {

                connection.Open();

                row = command.ExecuteNonQuery();






            }
            catch { }
            finally { connection.Close(); }


            return (row > 0);

        }


        static public bool DeleteOnePerson(int IDPerson)
        {
            int row = 0;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = "DELETE FROM [dbo].[People]\r\n      WHERE  PersonID = @PersonID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@PersonID", IDPerson);


            try
            {

                connection.Open();

                row = command.ExecuteNonQuery();






            }
            catch { }
            finally { connection.Close(); }


            return (row > 0);

        }


        public static bool GetPersonByID(int PersonID, ref string NationalNo, ref string FirstName, ref string second, ref string third, ref string LastName, ref int gendor, ref string address, ref string phone,
                          ref string email, ref int NationalityCountryID, ref string image, ref DateTime Dateofbirth)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = @"select * from People where PersonID = @PersonID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    NationalNo = (string)reader["NationalNo"];
                    FirstName = (string)reader["FirstName"];
                    second = (string)reader["SecondName"];



                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        third = (string)reader["ThirdName"];

                    }
                    else
                    {
                        third = " ";
                    }

                    LastName = (string)reader["LastName"];
                    Dateofbirth = (DateTime)reader["DateOfBirth"];
                    gendor = Convert.ToInt32(reader["Gendor"]);
                    address = (string)reader["Address"];
                    phone = (string)reader["Phone"];





                    if (reader["Email"] != DBNull.Value)
                    {
                        email = (string)reader["Email"];

                    }
                    else
                    {
                        email = " ";
                    }



                    NationalityCountryID = (int)reader["NationalityCountryID"];








                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        image = (string)reader["ImagePath"];

                    }
                    else
                    {
                        image = " ";
                    }








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

        public static bool GetPersonByNationalNo(ref int PersonID, string NationalNo, ref string FirstName, ref string second, ref string third, ref string LastName, ref int gendor, ref string address, ref string phone,
                      ref string email, ref int NationalityCountryID, ref string image, ref DateTime Dateofbirth)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = @"select * from People where NationalNo = @nationalno";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"nationalno", NationalNo);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    PersonID = (int)reader["PersonID"];
                    FirstName = (string)reader["FirstName"];
                    second = (string)reader["SecondName"];



                    if (reader["ThirdName"] != DBNull.Value)
                    {
                        third = (string)reader["ThirdName"];

                    }
                    else
                    {
                        third = " ";
                    }

                    LastName = (string)reader["LastName"];
                    Dateofbirth = (DateTime)reader["DateOfBirth"];
                    gendor = Convert.ToInt32(reader["Gendor"]);
                    address = (string)reader["Address"];
                    phone = (string)reader["Phone"];





                    if (reader["Email"] != DBNull.Value)
                    {
                        email = (string)reader["Email"];

                    }
                    else
                    {
                        email = " ";
                    }



                    NationalityCountryID = (int)reader["NationalityCountryID"];








                    if (reader["ImagePath"] != DBNull.Value)
                    {
                        image = (string)reader["ImagePath"];

                    }
                    else
                    {
                        image = " ";
                    }








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




        public static bool IsExists(string name)
        {

            bool isfound = false;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);
            string Query = @"

                             select found = 1  from People
                              where NationalNo = @NationalNo

                            ;";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"NationalNo", name);

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

        public static bool IsExists(int id)
        {

            bool isfound = false;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);
            string Query = @"

                             select found = 1  from People
                              where PersonID = @id

                            ;";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"id", id);

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