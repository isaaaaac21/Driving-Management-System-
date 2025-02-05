using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DVLDataAccessLayer
{
    public class clsTestAppointmentsDataAccess
    {
       static  SqlConnection Connection = new SqlConnection(clsShared.ConnectionString); 


        static public DataTable GetTestAppointmentsList(int LDLAID, int TestTypeID)
        {
            DataTable dt = new DataTable();

            string Query = "select AppointmentID = TestAppointments.TestAppointmentID, Date = TestAppointments.AppointmentDate, " +
                "PaidFees, isLocked from TestAppointments " +
                "Where LocalDrivingLicenseApplicationID = @LDLAID and TestTypeID = @TTID ";

            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@LDLAID", LDLAID);
            cmd.Parameters.AddWithValue("@TTID", TestTypeID);

            try
            {
                Connection.Open(); 
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
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

            return dt;    
        }

        static public int _AddTestApp(int TestTypeID, int LDLAID, DateTime appDate, decimal fees, int userID, bool isLocked,  int RetTestID )
        {
            int appID = -1; 

            string Query = "INSERT INTO TestAppointments " +
                "values (@TTID, @ldlaid, @appDate, @fees, @userID, @isLocked, @retID) ; " +
                "Select SCOPE_IDENTITY() ; ";

            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@TTID", TestTypeID);
            cmd.Parameters.AddWithValue("@ldlaid", LDLAID);
            cmd.Parameters.AddWithValue("@appDate", appDate);
            cmd.Parameters.AddWithValue("@fees", fees);
            cmd.Parameters.AddWithValue("@userID", userID);
            cmd.Parameters.AddWithValue("@isLocked", isLocked);
            cmd.Parameters.AddWithValue("@retID", RetTestID == -1 ? (object)DBNull.Value : RetTestID);


            try
            {
                Connection.Open();
                object result = cmd.ExecuteScalar();
                if (result != null) appID = Convert.ToInt32(result); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }
            finally
            {
                Connection.Close();
            }

            return appID; 
        }

        static public bool FindTestAppointmentByID(int ID, ref int testTypeID, ref int LDLAID, ref DateTime appDate, ref decimal fees, ref int userID, ref bool isLocked,
            ref int retID) 
        {

            bool isFound = false; 
            string Query = "Select * from TestAppointments Where TestAppointmentID = @ID";

            SqlCommand cmd = new SqlCommand(Query, Connection);
            cmd.Parameters.AddWithValue("@ID", ID); 


            try
            {
                Connection.Open();

                SqlDataReader reader = cmd.ExecuteReader(); 
                if (reader.Read())
                {
                    testTypeID = Convert.ToInt16(reader["TestTypeID"]);
                    LDLAID = Convert.ToInt16(reader["LocalDrivingLicenseApplicationID"]);
                    appDate = Convert.ToDateTime(reader["AppointmentDate"]);
                    fees = Convert.ToDecimal(reader["PaidFees"]);
                    userID = Convert.ToInt32(reader["CreatedByUserID"]);
                    isLocked = Convert.ToBoolean(reader["IsLocked"]);
                    retID = reader["RetakeTestApplicationID"] == DBNull.Value ? -1 : Convert.ToInt16(reader["RetakeTestApplicationID"]);

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

        static public bool TestAppExists(int LDLAID, int TestTypeID)
        {
            bool isExist = false; 
            string Query = "select found = 1 from TestAppointments Where LocalDrivingLicenseApplicationID = @LDLAID and TestTypeID = @TTID and IsLocked = 0";
            
             
            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@LDLAID", LDLAID);
            cmd.Parameters.AddWithValue("@TTID", TestTypeID);


            try
            {
                Connection.Open();

                object result = cmd.ExecuteScalar();
                if (result != null) isExist = true;  
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }
            finally
            {
                Connection.Close();
            }
            return isExist; 
            
        }


        static public bool UpdateAnAppointment(int ID, DateTime AppDate, bool isLocked)
        {
            int rowAffected = 0;

            string Query = "Update TestAppointments " +
                "Set AppointmentDate = @appDate, " +
                "IsLocked = @isLocked " +
                "Where TestAppointmentID = @ID";

            SqlCommand cmd = new SqlCommand(Query, Connection);
            cmd.Parameters.AddWithValue("@appDate", AppDate); 
            cmd.Parameters.AddWithValue("@isLocked", isLocked); 

            cmd.Parameters.AddWithValue("@ID", ID);
            

            try
            {
                Connection.Open();

                rowAffected = cmd.ExecuteNonQuery(); 
                

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }
            finally
            {
                Connection.Close();
            }

            return (rowAffected > 0);
        }

    }
}
