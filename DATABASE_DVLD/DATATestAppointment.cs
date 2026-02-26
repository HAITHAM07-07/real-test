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
    public class DATATestAppointment
    {

        static public DataTable GetAllDataFromTestAppointment(int idlocalapp, int testtype)
        {
            DataTable dt = new DataTable();

            SqlConnection connection =
                new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string query =
                "SELECT  TestAppointments.TestAppointmentID , TestAppointments.AppointmentDate , TestAppointments.PaidFees,TestAppointments.IsLocked FROM TestAppointments " +
                "WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID " +
                "AND TestTypeID = @TestTypeID" +
                "  order by IsLocked asc ;";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", idlocalapp);
            command.Parameters.AddWithValue("@TestTypeID", testtype);

            SqlDataReader reader = null;

            try
            {
                connection.Open();

                reader = command.ExecuteReader();

               
                dt.Load(reader);
            }
            catch 
            {
            }
            finally
            {
                if (reader != null)
                    reader.Close();

                connection.Close();
            }

            return dt;
        }
          


        static public int isAppointmenthere(int idlocalapp, int TestTypeID)
        {
            int round = 0;

            SqlConnection con =  new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string query = "SELECT COUNT(*) AS NumberFound" +
                " FROM TestAppointments" +
                " WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID " +
                " AND TestTypeID = @TestTypeID" +
                "  AND IsLocked = 1;";

            SqlCommand cmd = new SqlCommand(query, con);

            cmd.Parameters.AddWithValue(@"LocalDrivingLicenseApplicationID", idlocalapp);

            cmd.Parameters.AddWithValue(@"TestTypeID", TestTypeID);

            try
            {
                con.Open();

               
                object result = cmd.ExecuteScalar();

                if (result != null) 
                {
                    int.TryParse(result.ToString(), out int number);
                    round = number;
                    
                    }


            }
            catch
            {

            }
            finally
            {
                con.Close();
            }





            return round;




        }


        static public bool GetAppointmentbyID(int IDAPPOINTMENT,ref int localappid, ref int testtypeid,ref DateTime appointmentdate, ref decimal fees,ref int createbyuser,ref bool islocked,ref string retakeid)
        {
            bool isfound =false;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);
            string query = "select * from TestAppointments " +
                "where TestAppointmentID = @TestAppointmentID;";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue(@"TestAppointmentID", IDAPPOINTMENT);
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()) 
                {
                    isfound = true;
                    appointmentdate = (DateTime)reader["AppointmentDate"];
                    fees = (decimal)reader["PaidFees"];
                    islocked = (bool)reader["IsLocked"];
                    testtypeid = (int)reader["TestTypeID"];
                    createbyuser = (int)reader["CreatedByUserID"];

                    localappid = (int)reader["LocalDrivingLicenseApplicationID"];

                    if (reader["RetakeTestApplicationID"] == DBNull.Value)
                    {
                        retakeid = "";

                    }
                    else
                    {
                        int retakeint;
                        retakeint = (int)reader["RetakeTestApplicationID"];
                        retakeid = retakeint.ToString();
                    }

                }
                else
                {
                    isfound = false;
                }


            }
            catch
            {

            }
            finally
            {
                connection.Close();
            }









            return isfound;
        }

        static public int AddOneTestAppointment(int testtypeid, int localappid, DateTime appointmentdate, decimal fees, int createbyuser, bool islocked, string retaketestappid)
        {
            int IDTESTAPPOINTMENT = -1;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);



            string Query = "INSERT INTO [dbo].[TestAppointments]([TestTypeID] ,[LocalDrivingLicenseApplicationID],[AppointmentDate],[PaidFees] ,[CreatedByUserID] ,[IsLocked] ,[RetakeTestApplicationID])" +
                "VALUES (@TestTypeID,@LocalDrivingLicenseApplicationID,@AppointmentDate,@PaidFees,@CreatedByUserID,@IsLocked,@RetakeTestApplicationID)" +
                "select SCOPE_IDENTITY();";




            SqlCommand command = new SqlCommand(Query, connection);


            command.Parameters.AddWithValue(@"TestTypeID", testtypeid);
            command.Parameters.AddWithValue(@"LocalDrivingLicenseApplicationID", localappid);
            command.Parameters.AddWithValue(@"AppointmentDate", appointmentdate);
            command.Parameters.AddWithValue(@"PaidFees", fees);
            command.Parameters.AddWithValue(@"CreatedByUserID", createbyuser);
            command.Parameters.AddWithValue(@"IsLocked", islocked);
            if (string.IsNullOrEmpty(retaketestappid))
            {
                command.Parameters.AddWithValue(@"RetakeTestApplicationID", DBNull.Value);
            }
            else
            {

            command.Parameters.AddWithValue(@"RetakeTestApplicationID", retaketestappid);
            }





            try
            {

                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedid))
                {
                    IDTESTAPPOINTMENT = insertedid;


                }
                else
                {
                    IDTESTAPPOINTMENT = -1;
                }




            }
            catch { }
            finally { connection.Close(); }


            return IDTESTAPPOINTMENT;

        }


        static public bool UpdateOneAppointment(int appointmentid, int testtypeid, int localappid, DateTime appointmentdate, decimal fees, int createbyuser, bool islocked, string retaketestappid)
        {
            int row = 0;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);



            string Query = "UPDATE [dbo].[TestAppointments] " +
                "SET [TestTypeID] = @TestTypeID" +
                "   ,[LocalDrivingLicenseApplicationID] = @LocalDrivingLicenseApplicationID" +
                "   ,[AppointmentDate] = @AppointmentDate " +
                " ,[PaidFees] = @PaidFees" +
                ",[CreatedByUserID] = @CreatedByUserID" +
                ",[IsLocked] = @IsLocked" +
                "  where TestAppointmentID = @TestAppointmentID ";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"TestAppointmentID", appointmentid);
            command.Parameters.AddWithValue(@"TestTypeID", testtypeid);
            command.Parameters.AddWithValue(@"LocalDrivingLicenseApplicationID", localappid);
            command.Parameters.AddWithValue(@"AppointmentDate", appointmentdate);
            command.Parameters.AddWithValue(@"PaidFees", fees);
            command.Parameters.AddWithValue(@"CreatedByUserID", createbyuser);
            command.Parameters.AddWithValue(@"IsLocked", islocked);
            //if (retaketestappid != null) 
            //{

            //command.Parameters.AddWithValue(@"RetakeTestApplicationID", retaketestappid);
            //}
            //else
            //{
            //    command.Parameters.AddWithValue(@"RetakeTestApplicationID", DBNull.Value);

            //}

            // for app table i dont want to edit the retake appid



            try
                {

                    connection.Open();

                    row = command.ExecuteNonQuery();






                }
                catch { }
                finally { connection.Close(); }


            return (row > 0);

        }




        public static bool IsExists(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {

            bool isfound = false;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);
            string Query = @"

                            
           SELECT  true=1 FROM TestAppointments 
           WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
           AND TestTypeID = @TestTypeID and islocked = 0;

                            ;";

            SqlCommand command = new SqlCommand(Query, connection);


            command.Parameters.AddWithValue(@"LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue(@"TestTypeID", TestTypeID);

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

        public static bool IsExistsexam(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {

            int isfound = 0;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);
            string Query = @"

                            
       
     (SELECT        COUNT(*) AS PassedTest  
  FROM            dbo.Tests INNER JOIN  
                            dbo.TestAppointments ON dbo.Tests.TestAppointmentID = dbo.TestAppointments.TestAppointmentID  
  WHERE        (dbo.TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) AND 
  (dbo.Tests.TestResult = 1and dbo.TestAppointments.TestTypeID = @TestTypeID)) ;

                            ;";

            SqlCommand command = new SqlCommand(Query, connection);


            command.Parameters.AddWithValue(@"LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue(@"TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null&& int.TryParse(result.ToString(),out int number))
                
                { isfound = number; }

            }
            catch { }
            finally { connection.Close(); }



            return (isfound>0);
        }





    }
}
