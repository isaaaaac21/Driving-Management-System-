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








    }
}
