using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDataAccessLayer.Application_Data_Access_Classes
{


    public class clsApplicationDataAccess
    {

        static private SqlConnection Connection = new SqlConnection(clsShared.ConnectionString);


        static public int _AddApp(int perID, DateTime dt, int appType, int appStatus, DateTime lastDate, decimal Fees,
            int UserCreatedID)
        {

            int appID = -1;
            string query = "Insert into Applications " +
                "Values (@PerID, @Date, @type, @status, @StatusLastDate, @fees, @userID ); " +
                "Select SCOPE_IDENTITY()";

            SqlCommand cmd = new SqlCommand(query, Connection);

            cmd.Parameters.AddWithValue("@PerID", perID);
            cmd.Parameters.AddWithValue("@Date", dt);
            cmd.Parameters.AddWithValue("@type", appType);
            cmd.Parameters.AddWithValue("@status", appStatus);
            cmd.Parameters.AddWithValue("@StatusLastDate", lastDate);
            cmd.Parameters.AddWithValue("@Fees", Fees);
            cmd.Parameters.AddWithValue("@UserID", UserCreatedID);

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

        static public bool FindApp(int ID, ref int perID, ref DateTime dt, ref int apptype, ref int status, ref DateTime lastStatus, ref decimal fees,
            ref int UserID)
        {
            bool isFound = false;

            string Query = "Select * from Applications Where ApplicationID = @ID";

            SqlCommand cmd = new SqlCommand(Query, Connection);
            cmd.Parameters.AddWithValue("@ID", ID);

            try
            {
                Connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    perID = Convert.ToInt32(reader["ApplicantPersonID"]);
                    dt = Convert.ToDateTime(reader["ApplicationDate"]);
                    apptype = Convert.ToUInt16(reader["ApplicationTypeID"]);
                    status = Convert.ToInt32(reader["ApplicationStatus"]);
                    lastStatus = Convert.ToDateTime(reader["LastStatusDate"]);
                    fees = Convert.ToDecimal(reader["PaidFees"]);
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


        static public bool DeleteApp(int ID)
        {
            bool isDeleted = false;
            int rowAffected =-1;

            string Query = "Delete from Applications Where ApplicationID = @ID";
            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@ID", ID);


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

        static public bool UpdateApp(int appID, int perID, DateTime dt, int appType, int appStatus, DateTime lastDate, decimal Fees, int UserCreatedID)
        {
            int rowAffected = 0;

            string Query = "Update Applications " +
                "Set ApplicantPersonID = @perID, " +
                "ApplicationDate = @dt, " +
                "ApplicationTypeID = @TypeID, " +
                "ApplicationStatus = @status, " +
                "LastStatusDate = @lastDate, " +
                "PaidFees = @fees, " +
                "CreatedByUserID = @userID " +
                "Where ApplicationID = @appID ;";

            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@appID", appID);
            cmd.Parameters.AddWithValue("@perID", perID);
            cmd.Parameters.AddWithValue("@dt", dt);
            cmd.Parameters.AddWithValue("@TypeID", appType);
            cmd.Parameters.AddWithValue("@status", appStatus);
            cmd.Parameters.AddWithValue("@lastDate", lastDate);
            cmd.Parameters.AddWithValue("@fees", Fees);
            cmd.Parameters.AddWithValue("@userID", UserCreatedID);

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
