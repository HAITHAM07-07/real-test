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
    public class DATADetainedLicense
    {
        
        public static bool IsExistsByLicenseID(int LicenseID)
        {

            bool isfound = false;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);
            string Query = @"

select true=1 from DetainedLicenses
where LicenseID = @LicenseID and IsReleased = 0;
                            ;";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"LicenseID", LicenseID);


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
        

        static public DataTable GetAllDataFromDetaineds(string subQuery, string likeletters)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = @"select * from DetainedLicenses_View


           
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

        
        static public int AddOneDetained(  int LicenseID,  decimal FineFees,  DateTime DetainedDate,  int CreatedByUserID,  bool IsReleased,  DateTime? ReleasedDate,  int ReleasByUserID,  int ReleasApplicationID)
        {
            int idDetained = -1;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = "INSERT INTO [dbo].[DetainedLicenses]([LicenseID],[FineFees],[DetainDate],[CreatedByUserID],[IsReleased],[ReleaseDate],[ReleasedByUserID],[ReleaseApplicationID])" +
                " VALUES(@LicenseID,@FineFees,@DetainDate,@CreatedByUserID,@IsReleased,@ReleaseDate,@ReleasedByUserID,@ReleaseApplicationID)" +
                 "select SCOPE_IDENTITY(); ";




            SqlCommand command = new SqlCommand(Query, connection);


            command.Parameters.AddWithValue(@"LicenseID", LicenseID);
            command.Parameters.AddWithValue(@"FineFees", FineFees);
            command.Parameters.AddWithValue(@"DetainDate", DetainedDate);
            command.Parameters.AddWithValue(@"CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue(@"IsReleased", IsReleased);
            if (!ReleasedDate.HasValue)
            {
                command.Parameters.AddWithValue(@"ReleaseDate", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue(@"ReleaseDate", ReleasedDate);

            }


            if(ReleasByUserID == 0)//mean no any one
            {
                command.Parameters.AddWithValue(@"ReleasedByUserID", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue(@"ReleasedByUserID", ReleasByUserID);
            }

            if (ReleasApplicationID == 0)//mean no any one
            {
                command.Parameters.AddWithValue(@"ReleaseApplicationID", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue(@"ReleaseApplicationID", ReleasApplicationID);
            }










            try
            {

                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedid))
                {
                    idDetained = insertedid;


                }
                else
                {
                    idDetained = -1;
                }




            }
            catch { }
            finally { connection.Close(); }


            return idDetained;

        }

  
        static public bool UpdateOneDetained(int DetainedID,  int LicenseID, decimal FineFees, DateTime DetainedDate, int CreatedByUserID, bool IsReleased, DateTime? ReleasedDate, int ReleasByUserID, int ReleasApplicationID)
        {
            int row = 0;

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = "UPDATE [dbo].[DetainedLicenses]\r\n  " +
                " SET [LicenseID] = @LicenseID\r\n" +
                "      ,[FineFees] = @FineFees\r\n" +
                "      ,[DetainDate] = @DetainDate\r\n " +
                "     ,[CreatedByUserID] = @CreatedByUserID\r\n " +
                "     ,[IsReleased] = @IsReleased\r\n" +
                "      ,[ReleaseDate] = @ReleaseDate\r\n" +
                "      ,[ReleasedByUserID] = @ReleasedByUserID\r\n " +
                "     ,[ReleaseApplicationID] = @ReleaseApplicationID\r\n  " +
              
                "WHERE  DetainID = @DetainID;\r\n ";



            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@DetainID", DetainedID);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@DetainDate", DetainedDate);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", IsReleased);
            if (!ReleasedDate.HasValue)
            {
                command.Parameters.AddWithValue(@"ReleaseDate", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue(@"ReleaseDate", ReleasedDate);

            }


            if (ReleasByUserID == 0)//mean no any one
            {
                command.Parameters.AddWithValue(@"ReleasedByUserID", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue(@"ReleasedByUserID", ReleasByUserID);
            }

            if (ReleasApplicationID == 0)//mean no any one
            {
                command.Parameters.AddWithValue(@"ReleaseApplicationID", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue(@"ReleaseApplicationID", ReleasApplicationID);
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



        public static bool GetDetainedByLicenseID(ref int DetainedID, int LicenseID, ref decimal PaidFees, ref DateTime DetainedDate, ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleasedDate, ref int ReleasByUserID, ref int ReleasApplicationID)
        {
            bool isFound = false;

         

            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = @"select * from DetainedLicenses where LicenseID = @LicenseID and IsReleased=0";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    DetainedID = (int)reader["DetainID"];
                    PaidFees = (decimal)reader["FineFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];
                    DetainedDate = (DateTime)reader["DetainDate"];
                 
                    if (reader["ReleaseDate"] == DBNull.Value)
                    {
                        ReleasedDate =Convert.ToDateTime(null);
                    }
                    else
                    {
                        ReleasedDate = (DateTime)reader["ReleaseDate"];

                    }

                    if (reader["ReleasedByUserID"] == DBNull.Value)
                    {
                        ReleasByUserID = 0;
                    }
                    else
                    {
                        ReleasByUserID = (int)reader["ReleasedByUserID"];

                    }

                    if (reader["ReleaseApplicationID"] == DBNull.Value)
                    {
                        ReleasApplicationID = 0;
                    }
                    else
                    {
                        ReleasApplicationID = (int)reader["ReleaseApplicationID"];

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

        public static bool GetDetainedByLicenseIDandisdetaind(ref int DetainedID, int LicenseID, ref decimal PaidFees, ref DateTime DetainedDate, ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleasedDate, ref int ReleasByUserID, ref int ReleasApplicationID)
        {
            bool isFound = false;



            SqlConnection connection = new SqlConnection(clsDatabaseAccess.DataBaseAccess);

            string Query = @"select * from DetainedLicenses where LicenseID = @LicenseID and IsReleased = 0";

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue(@"LicenseID", LicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isFound = true;
                    DetainedID = (int)reader["DetainID"];
                    PaidFees = (decimal)reader["FineFees"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];
                    DetainedDate = (DateTime)reader["DetainDate"];

                    if (reader["ReleaseDate"] == DBNull.Value)
                    {
                        ReleasedDate = Convert.ToDateTime(null);
                    }
                    else
                    {
                        ReleasedDate = (DateTime)reader["ReleaseDate"];

                    }

                    if (reader["ReleasedByUserID"] == DBNull.Value)
                    {
                        ReleasByUserID = 0;
                    }
                    else
                    {
                        ReleasByUserID = (int)reader["ReleasedByUserID"];

                    }

                    if (reader["ReleaseApplicationID"] == DBNull.Value)
                    {
                        ReleasApplicationID = 0;
                    }
                    else
                    {
                        ReleasApplicationID = (int)reader["ReleaseApplicationID"];

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


    }
}
