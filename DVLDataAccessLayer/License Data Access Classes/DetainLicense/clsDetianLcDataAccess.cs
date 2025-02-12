using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace DVLDataAccessLayer.License_Data_Access_Classes.DetainLicense
{
    public class clsDetianLcDataAccess
    {
        static public SqlConnection Connection = new SqlConnection(clsShared.ConnectionString);

        //int DetID, int lcID, DateTime DetTime, decimal fees, int userID, bool isRel, DateTime relDate, int RelUserID, int RelAppID




        static public DataTable GetDetainedLicensesList()
        {
            DataTable Dt = new DataTable();

            string Query = "select D.DetainID as DetID, D.LicenseID as LcID,  D.FineFees, D.ReleaseDate, NationalNo as NatNo, " +
                "P.FirstName + ' ' + P.SecondName + ' ' + P.LastName as FullName, D.ReleaseApplicationID as RelAppID,  " +
                " IIF (IsReleased = 1, 'Yes'," +
                " IIF (IsReleased = 0, 'No', 'Unknown')) as IsReleased " +
                "from DetainedLicenses D " +
                "Inner join Licenses L On D.LicenseID = L.LicenseID " +
                "Inner join Drivers Dr On L.DriverID = Dr.DriverID " +
                "Inner join People P On Dr.PersonID = P.PersonID ; ";

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




        static public bool GetDetainedLicenseByLcID(int LcID, ref int DetID, ref DateTime DetDate, ref decimal fees, ref int userID, ref bool isRelease,
            ref DateTime? ReleaseDate, ref int? RelUserID, ref int? RelAppID) 
        {
            bool isFound = false;

            string Query = "select * from DetainedLicenses " +
                "Where LicenseID = @LCID";

            SqlCommand cmd = new SqlCommand(Query, Connection);
            cmd.Parameters.AddWithValue("@LCID", LcID);

            try
            {
                Connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    DetID = Convert.ToInt32(reader["DetainID"]);
                    DetDate = Convert.ToDateTime(reader["DetainDate"]);
                    userID = Convert.ToInt32(reader["CreatedByUserID"]);
                    isRelease = Convert.ToBoolean(reader["IsReleased"]);
                    fees = Convert.ToDecimal(reader["FineFees"]);

                    if (reader["ReleasedByUserID"] == DBNull.Value) RelUserID = null;
                    else RelUserID = Convert.ToInt32(reader["ReleaseByUserID"]);

                    if (reader["ReleaseDate"] == DBNull.Value) ReleaseDate = null;
                    else ReleaseDate = Convert.ToDateTime(reader["ReleaseApplicationID"]);


                    if (reader["ReleaseApplicationID"] == DBNull.Value) RelAppID = null;
                    else RelAppID = Convert.ToInt32(reader["ReleaseApplicationID"]);


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



        static public int _AddDetainedLicense(int LcID, DateTime DetDate,  decimal fees,  int userID,  bool isRelease,
            DateTime? ReleaseDate,  int? RelUserID,  int? RelAppID)
        {
            int DetainID = -1;

            string Query = "Insert into DetainedLicenses " +
                "values(@LcID, @DetDate, @fees, @userID, @isRel, @rDate, @rUserID, @rAppID) ; " +
                "Select SCOPE_IDENTITY() ; ";

            SqlCommand cmd = new SqlCommand(Query, Connection); 


            cmd.Parameters.AddWithValue("@LcID", LcID);
            cmd.Parameters.AddWithValue("@DetDate", DetDate);
            cmd.Parameters.AddWithValue("@fees", fees);
            cmd.Parameters.AddWithValue("@userID", userID);
            cmd.Parameters.AddWithValue("@isRel", isRelease);
            cmd.Parameters.AddWithValue("@rDate", ReleaseDate?? (object)DBNull.Value) ;
            cmd.Parameters.AddWithValue("@rUserID", RelUserID ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@rAppID", RelAppID ?? (object)DBNull.Value);




            try
            {
                Connection.Open();

                object result = cmd.ExecuteScalar();
                if (result != null) DetainID = Convert.ToInt32(result); 

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }
            finally
            {
                Connection.Close();
            }

            return DetainID; 
        }





        static public bool _UpdateDetained(int DetainedID, bool isRelease, DateTime? ReleaseDate, int? RelUserID, int? RelAppID)
        {
            int rowAffected = 0;


            string Query = "Update DetainedLicenses " +
                "Set IsReleased = @isRel, " +
                "ReleaseDate = @relDate, " +
                "ReleasedByUserID = @relUser, " +
                "ReleaseApplicationID = @rAppID " +
                "Where DetainID = @DetID ; ";

            SqlCommand cmd = new SqlCommand(Query, Connection);


            cmd.Parameters.AddWithValue("@DetID", DetainedID);
            cmd.Parameters.AddWithValue("@isRel", isRelease);
            cmd.Parameters.AddWithValue("@relDate", ReleaseDate ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@relUser", RelUserID ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@rAppID", RelAppID ?? (object)DBNull.Value);


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




        static public bool LicenseIsDetained(int LcID)
        {
            bool isDetained = false;

            string Query = "Select found = 1 from DetainedLicenses Where LicenseID = @LCID and IsReleased = 0 ; ";

            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@LCID", LcID); 


            try
            {
                Connection.Open();

                object result = cmd.ExecuteScalar();
                if (result != null) isDetained = true; 


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }
            finally
            {
                Connection.Close();
            }

            return isDetained; 


        }

    }
}
