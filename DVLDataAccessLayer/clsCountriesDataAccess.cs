using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDataAccessLayer
{
     public class clsCountriesDataAccess
    {
        static string ConnectionString = "Server =.\\MSSQLSERVER1; Database = DVLD; user Id = sa ; password = sa12345 ; ";




        static public DataTable GetCountriesList()
        {
            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(ConnectionString);

            string Query = "Select * from countries";

            SqlCommand cmd = new SqlCommand(Query, Connection); 

            try
            {
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader(); 

                if (reader.Read())
                {
                    dt.Load(reader); 
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

            return dt; 
        }


        static public bool FindCountry(int ID, ref string CountryName)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(ConnectionString);

            string Query = "Select * from countries Where CountryID = @ID";

            SqlCommand cmd = new SqlCommand(Query, connection);

            cmd.Parameters.AddWithValue("@ID", ID);

            try
            {
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    CountryName = (string)reader["CountryName"];
                    
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }
            finally
            {
                connection.Close();
            }
            return true;
        }

        static public int GetCountryIDByName(string Name)
        {
            int CountryID = -1;

            SqlConnection Connection = new SqlConnection(ConnectionString);

            string Query = "Select CountryID from Countries Where CountryName = @Name";

            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@Name", Name); 

            try
            {
                Connection.Open();

                object result = cmd.ExecuteScalar();
                if (result != null) CountryID = Convert.ToInt32(result); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }
            finally
            {
                Connection.Close();
            }

            return CountryID;


        }
    }
}
