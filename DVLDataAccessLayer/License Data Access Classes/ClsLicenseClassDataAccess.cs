using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDataAccessLayer
{
    public class ClsLicenseClassDataAccess
    {
        static private SqlConnection Connection = new SqlConnection(clsShared.ConnectionString); 


        static public DataTable GetLicenseClassesList()
        {
            DataTable DT = new DataTable();

            string Query = "Select * from LicenseClasses ; ";

            SqlCommand cmd = new SqlCommand(Query, Connection); 

            try
            {
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                DT.Load(reader);
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

            return DT; 


        }

        static public bool GetLicenseClassByID(int ID, ref string _Name, ref string Description, ref int MinAge, ref int valid, ref decimal fees)
        {
            bool isFound = false;
            string Query = "Select * from LicenseClasses Where LicenseClassID = @ID";

            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@ID", ID);


            try
            {
                Connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    _Name = (string)reader["ClassName"];
                    Description = (string)reader["ClassDescription"];
                    MinAge = Convert.ToInt32(reader["MinimumAllowedAge"]);
                    valid = Convert.ToInt32(reader["DefaultValidityLength"]);
                    fees = Convert.ToDecimal(reader["ClassFees"]);

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

    }






    }

