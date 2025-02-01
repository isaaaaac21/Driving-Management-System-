using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDataAccessLayer.Application_Data_Access_Classes
{
    public class clsTestTypesDataAccess
    {
        static string ConnectionString = "Server =.\\MSSQLSERVER1; Database = DVLD; user Id = sa ; password = sa12345 ; ";
        static SqlConnection Connection = new SqlConnection(ConnectionString);
        static public bool FindTestType(int ID, ref string Title, ref string Desc,  ref decimal Fees)
        {
            bool isFound = true;

            string Query = "Select * from TestTypes where TestTypeID = @ID";

            SqlCommand cmd = new SqlCommand(Query, Connection);
            cmd.Parameters.AddWithValue("@ID", ID);

            try
            {
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    Title = (string)reader["TestTypeTitle"];
                    Desc = (string)reader["TestTypeDescription"];

                    Fees = Convert.ToDecimal(reader["TestTypeFees"]);
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

        static public DataTable GetTestsTypesList()
        {
            DataTable Dt = new DataTable();


            string Query = "Select * from TestTypes";

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

        static public bool UpdateTest(int ID, string Title, string Desc, decimal Fees)
        {
            int RowAffected = 0;
            bool isUpdated = false;

            string Query = "Update TestTypes " +
                "Set TestTypeTitle = @Title, " +
                "TestTypeDescription = @Desc, " +
                "TestTypeFees = @fe " +
                "Where TestTypeID = @ID ";

            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@Title", Title);
            cmd.Parameters.AddWithValue("@Desc", Desc);

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
