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
            cmd.Parameters.AddWithValue("@StatusLastDate", appStatus);
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













    }
}
