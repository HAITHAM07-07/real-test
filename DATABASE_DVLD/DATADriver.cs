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
    public class DATADriver
    {




        static public DataTable GetAllDataFromDriver(string subQuery, string likeletters)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = @"	select * from Drivers_View


           
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

        static public int AddOneDriver(int PersonID, int CreateByUserID ,DateTime CreatedDate)
        {
            int iddriver = -1;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = "INSERT INTO [dbo].[Drivers]([PersonID],[CreatedByUserID],[CreatedDate])" +
                " VALUES(@PersonID,@CreatedByUserID,@CreatedDate)" +
                 "select SCOPE_IDENTITY(); ";




            SqlCommand command = new SqlCommand(Query, connection);


            command.Parameters.AddWithValue(@"PersonID", PersonID);
            command.Parameters.AddWithValue(@"CreatedByUserID", CreateByUserID);
            command.Parameters.AddWithValue(@"CreatedDate", CreatedDate);
       


            try
            {

                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedid))
                {
                    iddriver = insertedid;


                }
                else
                {
                    iddriver = -1;
                }




            }
            catch { }
            finally { connection.Close(); }


            return iddriver;

        }


        static public bool UpdateOneDriver(int DriverID ,int PersonID, int CreateByUserID, DateTime Createdate)
        {
            int row = 0;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = "UPDATE [dbo].[Drivers]\r\n  " +
                " SET [PersonID] = @PersonID\r\n" +
                "      ,[CreateByUserID] = @CreateByUserID\r\n" +
    
                "     ,[Createdate] = @Createdate\r\n " +

                "WHERE  DriverID = @DriverID;\r\n ";



            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreateByUserID", CreateByUserID);
            command.Parameters.AddWithValue("@Createdate", Createdate);
 





            try
            {

                connection.Open();

                row = command.ExecuteNonQuery();






            }
            catch { }
            finally { connection.Close(); }


            return (row > 0);

        }

        public static int IsExistsByApplicationID(int PSERSONID)
        {

            int numberiddriver = 0;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);
            string Query = @"

                            
 select Drivers.DriverID from Drivers 
               where Drivers.PersonID = @personid;";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"personid", PSERSONID);


            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int id) )
                { numberiddriver = id; };

            }
            catch { }
            finally { connection.Close(); }



            return (numberiddriver);
        }




        public static bool GetDriverByIDperson(ref int iddriver,int personid ,ref int createedbyuserid,ref DateTime datecreated)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = @"

              select * from Drivers 
               where Drivers.PersonID = @personid;

";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"personid", personid);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    iddriver = (int)reader["DriverID"];
              

                    createedbyuserid = (int)reader["CreatedByUserID"];

                    datecreated = (DateTime)reader["CreatedDate"];




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
