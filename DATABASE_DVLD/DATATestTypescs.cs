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
    static public class DATATestTypes
    {



        static public DataTable GetAllDataFromTestTypes()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = @"select * from TestTypes";

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

        static public int AddOneTestTypes(string TestTypesTitle, string TestTypeDescription, decimal TestTypeFees)
        {
            int id = -1;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = "INSERT INTO [TestTypes] ([TestTypeTitle],[TestTypeDescription],[TestTypeFees]) " +
                " VALUES(@TestTypesTitle,@TestTypeDescription,@TestTypeFees);" +
                "select SCOPE_IDENTITY();";
            



            SqlCommand command = new SqlCommand(Query, connection);


            command.Parameters.AddWithValue(@"TestTypeTitle", TestTypesTitle);
            command.Parameters.AddWithValue(@"TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue(@"TestTypeFees", TestTypeFees);





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

        static public bool UpdateOneTestTypess(int IDTestTypess, string TestTypesTitle, string TestTypeDescription, decimal TestTypeFees)
        {
            int row = 0; ;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = "UPDATE [dbo].[TestTypes]" +
                "  SET [TestTypeTitle] = @TestTypeTitle    ,[TestTypeFees] = @TestTypeFees ,[TestTypeDescription] = @TestTypeDescription  " +
                " WHERE TestTypeID = @TestTypeID";




            SqlCommand command = new SqlCommand(Query, connection);


            command.Parameters.AddWithValue(@"TestTypeTitle", TestTypesTitle);
            command.Parameters.AddWithValue(@"TestTypeFees", TestTypeFees);
            command.Parameters.AddWithValue(@"TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue(@"TestTypeID", IDTestTypess);





            try
            {

                connection.Open();
                row = command.ExecuteNonQuery();


            }
            catch { }
            finally { connection.Close(); }


            return (row > 0);

        }


        public static bool GetTestTypessByID(int id, ref string title, ref decimal fees,ref string description)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = @"select * from TestTypes where TestTypeID = @TestTypeID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"TestTypeID", id);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;

                    title = (string)reader["TestTypeTitle"];
                    description = (string)reader["TestTypeDescription"];
                    fees = (decimal)reader["TestTypeFees"];





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
