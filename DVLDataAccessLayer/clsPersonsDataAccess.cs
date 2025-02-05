using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace DVLDataAccessLayer
{
    public class clsPersonsDataAccess
    {

        static string ConnectionString = "Server =.\\MSSQLSERVER1; Database = DVLD; user Id = sa ; password = sa12345 ; ";


        static public bool FindPerson( int ID, ref string NationalID, ref string fn, ref string sn, ref string tn, ref string ln, ref DateTime db, ref int gender,
           ref string ads,  ref int cn, ref string Phone, ref string Em, ref string Img )
        {
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(ConnectionString);

            string Query = "Select  * from people Where PersonID = @ID";
            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@ID", ID); 

            try
            {
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader(); 

                if (reader.Read())
                {
                    isFound = true; 
                    NationalID = (string)reader["NationalNo"]; 
                    fn = (string)reader["FirstName"];
                    sn = (string)reader["SecondName"];
                    tn = (string)reader["ThirdName"];
                    ln = (string)reader["LastName"];
                    db = Convert.ToDateTime(reader["DateOfBirth"]);
                    gender = Convert.ToInt32(reader["Gendor"]);
                    ads = (string)reader["Address"]; 
                    cn = (int)reader["NationalityCountryID"]; 
                    Phone = (string) reader["Phone"];
                    Em = (string)reader["Email"];
                    if (reader["ImagePath"] == DBNull.Value)
                    {
                        Img = "";
                    }
                    else Img = (string)reader["ImagePath"]; 

                }

                reader.Close(); 
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error : " + ex );
            }
            finally
            {
                Connection.Close(); 
            }

            return isFound; 

        }

        
        static public int AddNewPerson(string NationalNo, string fn, string sn, string tn, string ln,  
            DateTime db, int gender, string Address, int cn, string ph, string em, string img)
        {
            int PerID = -1;

            SqlConnection Connection = new SqlConnection(ConnectionString);

            string Query = "Insert into people " +
                "values (@no, @fn, @sn, @tn, @ln, @db, @gen, @ad, @ph,  @em, @cn, @img); \n" +
                "Select Scope_IDENTITY() ; ";


            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@fn", fn);
            cmd.Parameters.AddWithValue("@no", NationalNo);
            cmd.Parameters.AddWithValue("@sn", sn);
            cmd.Parameters.AddWithValue("@tn", tn);
            cmd.Parameters.AddWithValue("@ln", ln);
            cmd.Parameters.AddWithValue("@db", db);
            cmd.Parameters.AddWithValue("@gen", gender);
            cmd.Parameters.AddWithValue("@ad", Address);
            cmd.Parameters.AddWithValue("@cn", cn);
            cmd.Parameters.AddWithValue("@ph", ph);
            cmd.Parameters.AddWithValue("@em", em);
            cmd.Parameters.AddWithValue("@img", img == "" ? (object)DBNull.Value : img);

            try
            {
                Connection.Open();
                object result = cmd.ExecuteScalar(); 
                if(result != null)
                {
                    PerID = Convert.ToInt32(result); 
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

            return PerID; 


        }

        static public bool UpdatePerson(int ID, string NationalNo, string fn, string sn, string tn, string ln,
            DateTime db, int gender, string Address, int cn, string ph, string em, string img)
        {
            int rowAffected = 0;
            bool hasBeenUp = false;

            SqlConnection Connection = new SqlConnection(ConnectionString);
            string Query = "Update People " +
                "Set NationalNo = @no, " +
                "FirstName = @fn, " +
                "SecondName = @sn, " +
                "ThirdName = @Tn, " +
                "LastName = @Ln, " +
                "DateOfBirth = @db, " +
                "Gendor = @gen, " +
                "Address = @ads, " +
                "NationalityCountryID = @cn, " +
                "Phone = @ph, " +
                "Email = @em, " +
                "ImagePath = @img " +
                "Where PersonID = @ID ;";

            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@no", NationalNo);
            cmd.Parameters.AddWithValue("@fn", fn);
            cmd.Parameters.AddWithValue("@sn", sn);
            cmd.Parameters.AddWithValue("@Tn", tn);
            cmd.Parameters.AddWithValue("@Ln", ln);
            cmd.Parameters.AddWithValue("@db", db);
            cmd.Parameters.AddWithValue("@gen", gender);
            cmd.Parameters.AddWithValue("@ads", Address);
            cmd.Parameters.AddWithValue("@cn", cn);
            cmd.Parameters.AddWithValue("@ph", ph);
            cmd.Parameters.AddWithValue("@em", em);
            cmd.Parameters.AddWithValue("@img", img);
            

            try
            {
                Connection.Open();
                rowAffected = cmd.ExecuteNonQuery();

                if (rowAffected > 0) hasBeenUp = true; 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }
            finally
            {
                Connection.Close();
            }

            return hasBeenUp; 
        }
        static public DataTable GetPeopleList()
        {
            DataTable dt = new DataTable(); 

            SqlConnection Connection = new SqlConnection(ConnectionString);

            string Query = "\r\nselect PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName, DateOfBirth," +
                " Gender =" +
                " Case When Gendor = 0 Then 'Male' " +
                " When Gendor = 1 Then 'Female'" +
                " Else 'Unknown' End , " +
                " Nationality = CountryName, Phone, Email" +
                " from people Inner join Countries On" +
                " People.NationalityCountryID = Countries.CountryID";

            SqlCommand cmd = new SqlCommand(Query, Connection);

            try
            {
                Connection.Open();

                SqlDataReader reader = cmd.ExecuteReader(); 

                dt.Load(reader); 
                
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


        static public bool isExistsByNo(string No)
        {
            bool isExists = false; 
            SqlConnection Connection = new SqlConnection(ConnectionString);
            string Query = "Select found = 1 from People where NationalNo = @No";

            SqlCommand cmd = new SqlCommand(Query, Connection);
            cmd.Parameters.AddWithValue("@No", No); 

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





        static public bool DeletePer(int ID)
        {
            bool isDeleted = false;
            int rowAffected = 0; 
            SqlConnection conn = new SqlConnection(ConnectionString);

            string Query = "Delete from people Where PersonID = @ID";

            SqlCommand cmd = new SqlCommand(Query, conn);

            cmd.Parameters.AddWithValue("@ID", ID); 

            try
            {
                conn.Open();

                rowAffected = cmd.ExecuteNonQuery();

                if (rowAffected > 0) isDeleted = true; 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }
            finally
            {
                conn.Close();
            }
            return isDeleted; 

        }


        static public bool FindByNo(ref int ID, string NationalID, ref string fn, ref string sn, ref string tn, ref string ln, ref DateTime db, ref int gender,
           ref string ads, ref int cn, ref string Phone, ref string Em, ref string Img)
        {
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(ConnectionString);

            string Query = "Select  * from people Where NationalNo = @No";
            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@No", NationalID);

            try
            {
                Connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    ID = (int)reader["PersonID"];
                    fn = (string)reader["FirstName"];
                    sn = (string)reader["SecondName"];
                    tn = (string)reader["ThirdName"];
                    ln = (string)reader["LastName"];
                    db = Convert.ToDateTime(reader["DateOfBirth"]);
                    gender = Convert.ToInt32(reader["Gendor"]);
                    ads = (string)reader["Address"];
                    cn = (int)reader["NationalityCountryID"];
                    Phone = (string)reader["Phone"];
                    Em = (string)reader["Email"];
                    if (reader["ImagePath"] == DBNull.Value)
                    {
                        Img = "";
                    }
                    else Img = (string)reader["ImagePath"];

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
    }
}
