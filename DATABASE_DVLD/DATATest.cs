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
    public class DATATest
    {
 

        static public int AddOneTest(int TestAppointmentID, int TestResult, string Notes, int CreatedByUserID)
        {
            int IDTest = -1;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);



            string Query = "INSERT INTO [dbo].[Tests]([TestAppointmentID] ,[TestResult],[Notes],[CreatedByUserID])" +
                "VALUES (@TestAppointmentID,@TestResult,@Notes,@CreatedByUserID)" +
                "select SCOPE_IDENTITY();";




            SqlCommand command = new SqlCommand(Query, connection);


            command.Parameters.AddWithValue(@"TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue(@"TestResult", TestResult);
            command.Parameters.AddWithValue(@"CreatedByUserID", CreatedByUserID);
            
            if (string.IsNullOrEmpty(Notes))
            {
                command.Parameters.AddWithValue(@"Notes", DBNull.Value);
            }
            else
            {

                command.Parameters.AddWithValue(@"Notes", Notes);
            }





            try
            {

                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedid))
                {
                    IDTest = insertedid;


                }
                else
                {
                    IDTest = -1;
                }




            }
            catch { }
            finally { connection.Close(); }


            return IDTest;

        }




        
        public static int IsExists(int TestAppointmentID)
        {

            int testid = -1;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);
            string Query = @"

           SELECT        Tests.TestID
FROM            TestAppointments INNER JOIN
                         Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
						 where  TestAppointments.TestAppointmentID=@TestAppointmentID

                            ;";

            SqlCommand command = new SqlCommand(Query, connection);


            command.Parameters.AddWithValue(@"TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int id)) 
                {
                    testid = id;
                }
                else
                {

                     testid = -1;
                }

            }
            catch { }
            finally { connection.Close(); }



            return (testid);
        }

        



    }
}
