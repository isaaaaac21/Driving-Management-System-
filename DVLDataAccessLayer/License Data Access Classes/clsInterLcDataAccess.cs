using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDataAccessLayer.License_Data_Access_Classes
{
    public class clsInterLcDataAccess
    {

        static public SqlConnection Connection = new SqlConnection(clsShared.ConnectionString);



        static public bool GetInterLcByID(int interLcID, ref int appID, ref int DriverID, ref int issLocID, ref DateTime issDate, ref DateTime expDate,
            ref bool isAct, ref int userID)
        {
            bool isFound = false;

            string Query = "Select * from InternationalLicenses Where InternationalLicenseID = @interID";

            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@interID", interLcID);

            try
            {
                Connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    appID = Convert.ToInt32(reader["ApplicationID"]);
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    issLocID = Convert.ToInt32(reader["IssuedUsingLocalLicenseID"]);
                    issDate = Convert.ToDateTime(reader["IssueDate"]);
                    expDate = Convert.ToDateTime(reader["ExpirationDate"]);
                    isAct = Convert.ToBoolean(reader["IsActive"]);
                    userID = Convert.ToInt32(reader["CreatedByUserID"]);


                    isFound = true;
                }
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



        static public int _Add( int appID,  int DriverID,  int issLocID,  DateTime issDate,  DateTime expDate,
             bool isAct,  int userID)
        {
            int interID = -1;

            string Query = "Insert into InternationalLicenses " +
                "values (@appID, @DrID, @issLocID, @issDate, @expDate,  @isAct, @userID) ; " +
                "Select Scope_Identity() ; ";


            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@appID", appID);
            cmd.Parameters.AddWithValue("@DrID", DriverID);
            cmd.Parameters.AddWithValue("@issLocID", issLocID);
            cmd.Parameters.AddWithValue("@issDate", issDate);
            cmd.Parameters.AddWithValue("@expDate", expDate);
            cmd.Parameters.AddWithValue("@isAct", isAct);
            cmd.Parameters.AddWithValue("@userID", userID);

            try
            {
                Connection.Open();

                object result = cmd.ExecuteScalar();

                if (result != null) interID = Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }
            finally
            {
                Connection.Close();
            }

            return interID;
        }



        static public bool DriverHasInterLicense(int DriverID)
        {
            bool HasInterLc = false; 

            string Query = "Select found = 1 From InternationalLicenses Where DriverID = @DID";

            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@DID", DriverID); 

            try
            {
                Connection.Open();

                object result = cmd.ExecuteScalar();

                if (result != null) HasInterLc = true; 


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }
            finally
            {
                Connection.Close();
            }

            return HasInterLc; 
        }


        static public DataTable GetInterLicensesOfDriver(int DriverID)
        {
            DataTable Dt = new DataTable();

            string Query = "select InternationalLicenseID as IntLcID, ApplicationID, IssuedUsingLocalLicenseID as LocalLcID, IssueDate, ExpirationDate as expDate, IsActive " + 
                  "From InternationalLicenses "+
                  "Where DriverID = @DID; ";

            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@DID", DriverID);

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




        static public DataTable GetInterLicenses()
        {
            DataTable Dt = new DataTable();

            string Query = "select InternationalLicenseID as IntLcID, ApplicationID, IssuedUsingLocalLicenseID as LocalLcID, IssueDate, ExpirationDate as ExpDate," +
                "IIF (IsActive = 1, 'Yes', " +
                "IIF (IsActive = 0, 'No', 'Unknown')) as IsActive  " +
                "From InternationalLicenses ; "; 

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











    }
}
