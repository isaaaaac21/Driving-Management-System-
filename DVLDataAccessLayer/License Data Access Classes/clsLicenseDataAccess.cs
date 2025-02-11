using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDataAccessLayer.License_Data_Access_Classes
{
    public class clsLicenseDataAccess
    {
        static SqlConnection Connection = new SqlConnection(clsShared.ConnectionString);


        static public bool GetLicenseByID(int ID, ref int appID, ref int DriverID, ref int lcID, ref DateTime issDate,ref  DateTime expDate, ref string notes, 
            ref decimal fees, ref bool isAct, ref byte issReason, ref int userID)
        {
            bool isFound = false;

            string Query = "Select * from Licenses Where LicenseID = @ID";

            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@ID", ID);

            try
            {
                Connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    appID = Convert.ToInt32(reader["ApplicationID"]);
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    lcID = Convert.ToInt32(reader["LicenseClass"]);
                    userID = Convert.ToInt32(reader["CreatedByUserID"]);
                    issDate = Convert.ToDateTime(reader["IssueDate"]);
                    expDate = Convert.ToDateTime(reader["ExpirationDate"]);
                    notes = reader["Notes"] == DBNull.Value ? "" : (string)reader["Notes"];
                    fees = Convert.ToDecimal(reader["PaidFees"]);
                    isAct = Convert.ToBoolean(reader["IsActive"]);
                    issReason = Convert.ToByte(reader["IssueReason"]);

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

        static public int AddLicense(int appID, int DriverID, int lcID, DateTime issDate, DateTime expDate, string notes, decimal fees, bool isAct,
            byte issReason, int CreatedByUserID)
        {
            int LicenseID = -1;

            string Query = "Insert into Licenses " +
                "values (@appID, @DrID, @LcID, @issDate, @expDate, @notes, @fees, @isAct, @issReason, @userID) ; " +
                "Select Scope_Identity() ; ";


            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@appID", appID); 
            cmd.Parameters.AddWithValue("@DrID", DriverID ) ;
            cmd.Parameters.AddWithValue("@LcID",lcID ) ;
            cmd.Parameters.AddWithValue("@issDate",issDate ) ;
            cmd.Parameters.AddWithValue("@expDate",expDate ) ;
            cmd.Parameters.AddWithValue("@notes", notes) ;
            cmd.Parameters.AddWithValue("@fees", fees) ;
            cmd.Parameters.AddWithValue("@isAct", isAct);
            cmd.Parameters.AddWithValue("@issReason", issReason);
            cmd.Parameters.AddWithValue("@userID", CreatedByUserID);

            try
            {
                Connection.Open();

                object result = cmd.ExecuteScalar();

                if (result != null) LicenseID = Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }
            finally
            {
                Connection.Close();
            }

            return LicenseID; 


        }

        static public bool UpdateLicense(int LicenseID, int appID, int DriverID, int lcID, DateTime issDate, DateTime expDate, string notes, decimal fees, bool isAct,
            byte issReason, int CreatedByUserID)
        {
            int rowAffected = 0;

            string Query = "Update Licenses " +
                "Set ApplicationID = @appID, " +
                "DriverID = @DrID, " +
                "LicenseClass = @LcID, " +
                "IssueDate = @issDate, " +
                "ExpirationDate = @expDate, " +
                "Notes = @notes, " +
                "PaidFees = @fees, " +
                "IsActive = @isAct, " +
                "IssueReason = @issReason, " +
                "CreatedByUserID = @userID " +
                "Where LicenseID = @ID";

            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@ID", LicenseID);
            cmd.Parameters.AddWithValue("@appID", appID);
            cmd.Parameters.AddWithValue("@DrID", DriverID);
            cmd.Parameters.AddWithValue("@LcID", lcID);
            cmd.Parameters.AddWithValue("@issDate", issDate);
            cmd.Parameters.AddWithValue("@expDate", expDate);
            cmd.Parameters.AddWithValue("@notes", notes);
            cmd.Parameters.AddWithValue("@fees", fees);
            cmd.Parameters.AddWithValue("@isAct", isAct);
            cmd.Parameters.AddWithValue("@issReason", issReason);
            cmd.Parameters.AddWithValue("@userID", CreatedByUserID);

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


        static public bool LicenseExists(int PersonID, int LcID)
        {
            bool isExists = false;
            string Query = "select found = 1 from licenses " +
                "inner join Drivers on Licenses.DriverID = Drivers.DriverID " +
                "Where Drivers.PersonID = @perID and LicenseClass = @lcID ;";

            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@perID", PersonID); 
            cmd.Parameters.AddWithValue("@lcID", LcID);
            
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


        static public bool GetLicenseByPerLcID(int perID, int LcID, ref int LicenseID, ref int appID, ref int DriverID, ref DateTime issDate, ref DateTime expDate, 
            ref string notes, ref decimal fees, ref bool isAct, ref byte issReason, ref int userID)
        {
            bool isFound = false;

            string Query = "select * from licenses " +
                "inner join Drivers on Licenses.DriverID = Drivers.DriverID " +
                "Where Drivers.PersonID = @perID and LicenseClass = @lcID ;";

            SqlCommand cmd = new SqlCommand(Query, Connection);
            cmd.Parameters.AddWithValue("@perID", perID);
            cmd.Parameters.AddWithValue("@lcID", LcID);

            try
            {
                Connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    LicenseID = Convert.ToInt32(reader["LicenseID"]); 
                    appID = Convert.ToInt32(reader["ApplicationID"]);
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    userID = Convert.ToInt32(reader["CreatedByUserID"]);
                    issDate = Convert.ToDateTime(reader["IssueDate"]);
                    expDate = Convert.ToDateTime(reader["ExpirationDate"]);
                    notes = (string)reader["Notes"];
                    fees = Convert.ToDecimal(reader["PaidFees"]);
                    isAct = Convert.ToBoolean(reader["IsActive"]);
                    issReason = Convert.ToByte(reader["IssueReason"]);

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



        static public DataTable GetTotalLicensesOfDriver(int DriverID)
        {
            DataTable Dt = new DataTable();

            string Query = "select LicenseID, LicenseClasses.ClassName as ClassName, IssueDate, ExpirationDate, IsActive from Licenses " +
                "Inner join LicenseClasses On LicenseClass = LicenseClasses.LicenseClassID " +
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


    }



    




 }

