using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DVLDataAccessLayer.License_Data_Access_Classes
{
    public class clsDriversDataAccess
    {
        static SqlConnection Connection = new SqlConnection(clsShared.ConnectionString); 



        static public bool GetDriverByID(int DriverID, ref int perID, ref int UserID, ref DateTime CreateDate)
        {
            bool isFound = false;

            string Query = "Select * from Drivers Where DriverID = @DID";

            SqlCommand cmd = new SqlCommand(Query, Connection);
            cmd.Parameters.AddWithValue("@DID", DriverID);
            try
            {
                Connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    perID = Convert.ToInt32(reader["PersonID"]);
                    UserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    CreateDate = Convert.ToDateTime(reader["CreatedDate"]);
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
        static public bool GetDriverByPersonID(ref int DriverID, int perID, ref int UserID, ref DateTime CreateDate)
        {
            bool isFound = false;

            string Query = "Select * from Drivers Where PersonID = @PID";

            SqlCommand cmd = new SqlCommand(Query, Connection);
            cmd.Parameters.AddWithValue("@PID", perID);
            try
            {
                Connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    UserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    CreateDate = Convert.ToDateTime(reader["CreatedDate"]);
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

        //this function will check if a person exists as driver, if yes it will return its ID, else it will return -1 ; 
        static public int PersonExistsAsDriver(int PersonID)
        {
            
            int DriverID = -1; 
            string Query = "Select DriverID From Drivers Where PersonID = @perID";

            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@perID", PersonID);

            try
            {
                Connection.Open();

                object result = cmd.ExecuteScalar();
                if (result != null) DriverID = Convert.ToInt32(result); 

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }
            finally
            {
                Connection.Close();
            }
            return DriverID; 

        }
        static public int AddNewDriver(int perID, int userID, DateTime createdDate)
        {
            int DriverID = -1;


            string Query = "Insert Into Drivers " +
                "values (@perID, @userID, @CreateDate) ; " +
                "Select Scope_IDENTITY() ;  ";

            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@perID", perID);
            cmd.Parameters.AddWithValue("@userID", userID);
            cmd.Parameters.AddWithValue("@CreateDate", createdDate);


            try
            {
                Connection.Open();
                object result = cmd.ExecuteScalar();

                if (result != null) DriverID = Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }
            finally
            {
                Connection.Close();
            }

            return DriverID;

        }


        static public DataTable GetDriversList()
        {
            DataTable Dt = new DataTable();

            string Query = "select DriverID, P.PersonID, P.FirstName + ' ' + P.SecondName + ' ' + P.LastName as FullName, CreatedDate as Date, " +
                "(select count(*) as ActiveLicenses From Licenses Where DriverID = 11 and IsActive = 1) as ActiveLicenses  " +
                "from drivers d " +
                "Inner Join People as P on d.PersonID = P.PersonID ; "; 


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
