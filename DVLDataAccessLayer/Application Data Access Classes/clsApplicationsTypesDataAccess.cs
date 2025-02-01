using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDataAccessLayer
{
    public  class clsApplicationsTypesDataAccess
    {
        static string ConnectionString = "Server =.\\MSSQLSERVER1; Database = DVLD; user Id = sa ; password = sa12345 ; ";
        static SqlConnection Connection = new SqlConnection(ConnectionString); 


        static public  DataTable GetAppsTypesList()
        {
            DataTable Dt = new DataTable();

            SqlConnection Connection = new SqlConnection(ConnectionString);

            string Query = "Select * from ApplicationTypes";

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

        static public bool FindTypeApp(int ID, ref string Title, ref decimal Fees)
        {
            bool isFound = true;

            SqlConnection Connection = new SqlConnection(ConnectionString);

            string Query = "Select * from ApplicationTypes where ApplicationTypeID = @ID";

            SqlCommand cmd = new SqlCommand(Query, Connection);
            cmd.Parameters.AddWithValue("@ID", ID); 

            try
            {
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader(); 
                if (reader.Read())
                {
                    Title = (string)reader["ApplicationTypeTitle"];
                    Fees = Convert.ToDecimal(reader["ApplicationFees"]);
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


        static public bool UpdateApp(int ID, string Title, decimal Fees)
        {
            int RowAffected = 0;
            bool isUpdated = false;

            string Query = "Update ApplicationTypes " +
                "Set ApplicationTypeTitle = @Title, " +
                "ApplicationFees = @fe " +
                "Where ApplicationTypeID = @ID ";

            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@Title", Title);
            cmd.Parameters.AddWithValue("@fe", Fees);
            
            try
            {
                Connection.Open();

                RowAffected = cmd.ExecuteNonQuery();
                if (RowAffected > 0) isUpdated = true; 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }
            finally
            {
                Connection.Close();
            }

            return isUpdated;

        }
    }
}
