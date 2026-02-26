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
   static public class DATAApplicationType
    {



        static public DataTable GetAllDataFromApplicationTypes()
        {
            DataTable dt = new DataTable();

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = @"select * from ApplicationTypes";

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

        static public int AddOneApplicationTypes(string ApplicationTypeTitle, decimal ApplicationFees)
        {
            int id = -1;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = "INSERT INTO [ApplicationTypes] ([ApplicationTypeTitle],[ApplicationFees]) " +
                " VALUES(@ApplicationTypeTitle,@ApplicationFees);" +
                "select SCOPE_IDENTITY();";




            SqlCommand command = new SqlCommand(Query, connection);


            command.Parameters.AddWithValue(@"ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue(@"ApplicationFees", ApplicationFees);
          

            



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

        static public bool UpdateOneApplicationTypes(int IDApplicationTypes, string ApplicationTypeTitle, decimal ApplicationFees)
        {
            int row = 0; ;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = "UPDATE [dbo].[ApplicationTypes]" +
                "  SET [ApplicationTypeTitle] = @ApplicationTypeTitle    ,[ApplicationFees] = @ApplicationFees " +
                " WHERE ApplicationTypeID = @ApplicationTypeID";




            SqlCommand command = new SqlCommand(Query, connection);


            command.Parameters.AddWithValue(@"ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue(@"ApplicationFees", ApplicationFees);
            command.Parameters.AddWithValue(@"ApplicationTypeID", IDApplicationTypes);





            try
            {

                connection.Open();
                row = command.ExecuteNonQuery();
       

            }
            catch { }
            finally { connection.Close(); }


            return (row>0);

        }


        public static bool GetApplicationtypesByID(int id,ref string apptitle,ref decimal fees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = @"select * from ApplicationTypes where ApplicationTypeID = @ApplicationTypeID";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"ApplicationTypeID", id);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;


                    apptitle = (string)reader["ApplicationTypeTitle"];
                    fees = (decimal)reader["ApplicationFees"];
                




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
