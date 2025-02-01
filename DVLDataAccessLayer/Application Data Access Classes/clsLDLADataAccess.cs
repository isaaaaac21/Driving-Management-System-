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

            string Query = "\r\nSelect LDLA.ApplicationID as LDL_ID, " +
                "P.NationalNo as NationalNo, " +
                "LC.ClassName as DrivingClass, " +
                "A.ApplicationDate,  " +
                "concat(P.FirstName, ' ', P.SecondName, ' ', P.ThirdName, ' ', P.LastName) as FullName,  " +
                "Status =" +
                "  case " +
                "       WHEN A.ApplicationStatus = 1 Then 'New'" +
                "       WHEN A.ApplicationStatus = 2 Then 'Completed'" +
                "       WHEN A.ApplicationStatus = 3 Then 'Canceled'" +
                "  Else 'Unknown' End " +
                "from LocalDrivingLicenseApplications as LDLA " +
                "Inner Join Applications as A On LDLA.ApplicationID = A.ApplicationID " +
                "Join People as P On A.ApplicantPersonID = P.PersonID " +
                "Inner Join LicenseClasses as LC on LDLA.LicenseClassID = LC.LicenseClassID;";

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







    }
}
