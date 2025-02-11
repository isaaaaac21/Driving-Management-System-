using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDataAccessLayer.Tests_Data_Access
{
    public class clsTestDataAccess
    {

        static SqlConnection Connection = new SqlConnection(clsShared.ConnectionString); 

        static public int _AddTest(int TestAppID, bool Result, string Note, int userID)
        {
            int TestID = -1;

            string Query = "INSERT INTO Tests " +
                "values(@TAppID, @Result, @note, @userID) ; " +
                "Select SCOPE_IDENTITY() ; ";

            SqlCommand cmd = new SqlCommand(Query, Connection);
            cmd.Parameters.AddWithValue("@TAppID", TestAppID); 
            cmd.Parameters.AddWithValue("@Result", Result); 
            cmd.Parameters.AddWithValue("@note", Note); 
            cmd.Parameters.AddWithValue("@userID", userID);
            

            try
            {
                Connection.Open();

                object result = cmd.ExecuteScalar();

                if (result != null) TestID = Convert.ToInt16(result); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }
            finally
            {
                Connection.Close();
            }

            return TestID; 
        }
        static public bool FindTestByAppID(int AppointmentID, ref int TestID, ref bool Result, ref string Notes, ref int UserID)
        {
            bool isFound = false;

            string Query = "Select * from Tests Where TestAppointmentID = @appID ";

            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@appID", AppointmentID); 

            try
            {
                Connection.Open();

                SqlDataReader reader = cmd.ExecuteReader(); 

                if (reader.Read())
                {
                    TestID = Convert.ToInt32(reader["TestID"]);
                    Result = Convert.ToBoolean(reader["TestResult"]);
                    Notes = reader["Notes"] == DBNull.Value ? "" : (string)reader["Notes"];
                    UserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    isFound = true; 
                }
                reader.Close(); 


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }
            finally
            {
                Connection.Close();
            }

            return isFound; 

        }

        static public bool TestHasBeenPassed(int TestTypeID, int LDLAID)
        {
            bool isPassed = false;

            string Query = "select * from tests " +
                "Inner join TestAppointments On Tests.TestAppointmentID = TestAppointments.TestAppointmentID " +
                "Where TestAppointments.LocalDrivingLicenseApplicationID = @LDLAID and TestTypeID = @TestTypeID and TestResult = 1 ;";

            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@LDLAID", LDLAID);
            cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                Connection.Open();

                object result = cmd.ExecuteScalar();
                if (result != null) isPassed = true;  


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }
            finally
            {
                Connection.Close();
            }

            return isPassed;

        }

        static public bool GetLastTest(int LDLAID, int TestTypeID, ref int TestID, ref int AppointmentID, ref bool Result, ref string Notes, ref int userID)
        {
            bool isFound = false;
            string Query = "select top 1 * from tests " +
                "Inner join TestAppointments on Tests.TestAppointmentID = TestAppointments.TestAppointmentID " +
                "Inner join LocalDrivingLicenseApplications on TestAppointments.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID " +
                "Where LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LDLAID and TestTypeID = @TTID " +
                "order by TestID desc ;  ";

            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@LDLAID", LDLAID);
            cmd.Parameters.AddWithValue("@TTID", TestTypeID); 

            try
            {
                Connection.Open();

                SqlDataReader reader = cmd.ExecuteReader(); 

                if (reader.Read())
                {
                    TestID = Convert.ToInt32(reader["TestID"]); 
                    AppointmentID = Convert.ToInt32(reader["TestAppointmentID"]);
                    Result = Convert.ToBoolean(reader["TestResult"]);
                    Notes = reader["Notes"] == DBNull.Value ? "" : (string) reader["Notes"];
                    userID = Convert.ToInt32(reader["CreatedByUserID"]);
                    isFound = true; 
                }
                reader.Close(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }
            finally
            {
                Connection.Close();
            }

            return isFound; 


        }

        static public int GetTotalPassedTests(int LDLAID)
        {
            int TotalPassedTests = -1;

            string Query = "select count(*) as passedTests from Tests " +
                "Inner join TestAppointments On Tests.TestAppointmentID = TestAppointments.TestAppointmentID " +
                "where TestAppointments.LocalDrivingLicenseApplicationID = @LDLAID and TestResult = 1 ;";

            SqlCommand cmd = new SqlCommand(Query, Connection);
            cmd.Parameters.AddWithValue("@LDLAID", LDLAID);

            try
            {
                Connection.Open();
                object result = cmd.ExecuteScalar();

                if (result != null) TotalPassedTests = Convert.ToInt32(result);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }
            finally
            {
                Connection.Close();
            }

            return TotalPassedTests;


        }

        //this function will return how many times did the applicant fail in the giving testType
        static public int GetTotalTestFailTrialsOfType(int LDLAID, int TestTypeID)
        {
            int totalFails = 0; 

            string Query = "select count(*) as failedTrials from  tests" +
                " Inner join TestAppointments On Tests.TestAppointmentID = TestAppointments.TestAppointmentID " +
                "Where TestAppointments.LocalDrivingLicenseApplicationID = @LDLAID and TestTypeID = @TTID and TestResult = 0;" ;

            SqlCommand cmd = new SqlCommand(Query, Connection);
            cmd.Parameters.AddWithValue("@LDLAID", LDLAID);
            cmd.Parameters.AddWithValue("@TTID", TestTypeID);


            try
            {
                Connection.Open();
                object result = cmd.ExecuteScalar();

                if (result != null) totalFails = Convert.ToInt32(result);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }
            finally
            {
                Connection.Close();
            }

            return totalFails;
        }


        static public bool DeleteAllTestsByLDLAID(int LDLAID)
        {
            bool isDeleted = false;
            int rowAffected = -1;

            string Query = "Delete T from tests T " +
                "Inner join TestAppointments Ta on Ta.TestAppointmentID  = T.TestAppointmentID " +
                "Inner join LocalDrivingLicenseApplications LDLA on LDLA.LocalDrivingLicenseApplicationID = Ta.LocalDrivingLicenseApplicationID " +
                "where ta.LocalDrivingLicenseApplicationID = @LDLAID ;";


            
            SqlCommand cmd = new SqlCommand(Query, Connection);
            cmd.Parameters.AddWithValue("@LDLAID", LDLAID);

            try
            {
                Connection.Open();

                rowAffected = cmd.ExecuteNonQuery();
                if (rowAffected >= 0) isDeleted = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }
            finally
            {
                Connection.Close();
            }

            return isDeleted;

        }


    }
}
