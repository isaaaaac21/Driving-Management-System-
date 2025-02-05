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
            bool isExists = false;

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
                if (result != null) isExists = true;  


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }
            finally
            {
                Connection.Close();
            }

            return isExists;

        }
    }
}
