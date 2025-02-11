using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDataAccessLayer.License_Data_Access_Classes
{

    //This class is the New Local Driving License Applications class
    public class clsLDLADataAccess
    {
        static private SqlConnection Connection = new SqlConnection(clsShared.ConnectionString); 



        static public DataTable GetAllNewLocalDrivingLicenseApps()
        {
            DataTable Dt = new DataTable();

            string Query = "Select * from LocalDrivingLicenseApplication_View";

            SqlCommand cmd = new SqlCommand(Query, Connection); 

            try
            {
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                Dt.Load(reader);
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


            return Dt; 

        }


        static public bool LDLAExists(int PerID, int clsID)
        {
            bool isExists = false;

            string Query = "select found = 1  From LocalDrivingLicenseApplications as LDLDA " +
                "Inner join Applications as A on LDLDA.ApplicationID = A.ApplicationID " +
                "Where A.ApplicantPersonID = @ID and LicenseClassID = @clsID and " +
                "(A.ApplicationStatus = 1 or A.ApplicationStatus = 2)";

            SqlCommand cmd = new SqlCommand(Query, Connection);
            cmd.Parameters.AddWithValue("@ID", PerID); 
            cmd.Parameters.AddWithValue("@clsID", clsID); 

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

        static public int AddLDLApp(int appID, int LSclassID)
        {
            int LDLAppID = -1;

            string Query = "Insert into LocalDrivingLicenseApplications " +
                "values(@appID, @lsID) ; " +
                "Select  Scope_Identity() ; ";

            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@appID", appID);
            cmd.Parameters.AddWithValue("@lsID", LSclassID);

            try
            {
                Connection.Open();

                object result = cmd.ExecuteScalar();

                if (result != null) LDLAppID = Convert.ToInt32(result); 


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }
            finally
            {
                Connection.Close();
            }

            return LDLAppID; 


        }


        static public bool CancelAnLDLAByID(int ID)
        {
            bool isCanceled = false;
            int rowAffected = 0; 
            string Query = "update Applications " +
                "set ApplicationStatus = 3 from Applications " +
                "Inner join LocalDrivingLicenseApplications on LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID " +
                "where LocalDrivingLicenseApplicationID = @ID;";

            SqlCommand cmd = new SqlCommand(Query, Connection);
            cmd.Parameters.AddWithValue("@ID", ID); 

            try
            {
                Connection.Open();

                rowAffected = cmd.ExecuteNonQuery();

                if (rowAffected > 0) isCanceled = true; 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }
            finally
            {
                Connection.Close();
            }

            return isCanceled; 
        }

        static public bool FindByID(int LDLAID, ref int appID, ref int perID, ref DateTime dt, ref int apptype, ref int status, ref DateTime lastStatus, ref decimal fees,
            ref int UserID, ref int lcID)
        {
            bool isFound = false;

            string Query = "select * from LocalDrivingLicenseApplications " +
                "Inner join Applications on LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID " +
                "Where LocalDrivingLicenseApplicationID = @ID ;";

            SqlCommand cmd = new SqlCommand(Query, Connection);
            cmd.Parameters.AddWithValue("@ID", LDLAID); 

            try
            {
                Connection.Open();

                SqlDataReader reader = cmd.ExecuteReader(); 

                if (reader.Read())
                {
                    appID = Convert.ToInt32(reader["ApplicationID"]); 
                    perID = Convert.ToInt32(reader["ApplicantPersonID"]);
                    dt = Convert.ToDateTime(reader["ApplicationDate"]);
                    apptype = Convert.ToUInt16(reader["ApplicationTypeID"]);
                    status = Convert.ToInt32(reader["ApplicationStatus"]);
                    lastStatus = Convert.ToDateTime(reader["LastStatusDate"]);
                    fees = Convert.ToDecimal(reader["PaidFees"]);
                    UserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    lcID = Convert.ToInt32(reader["LicenseClassID"]);

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

        static public bool DeleteLDLA(int LDLAID)
        {
            bool isDeleted = false;
            int rowAffected = -1;

            string Query = "Delete from LocalDrivingLicenseApplications Where LocalDrivingLicenseApplicationID = @ID";

            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@ID", LDLAID);

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
