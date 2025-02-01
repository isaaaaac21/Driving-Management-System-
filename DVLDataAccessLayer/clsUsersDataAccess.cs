using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDataAccessLayer
{
    public class clsUsersDataAccess
    {

        static string ConnectionString = "Server =.\\MSSQLSERVER1; Database = DVLD; user Id = sa ; password = sa12345 ; ";



        static public bool FindUser(int UID, ref int perID, ref string userName, ref string password, ref bool isActive)
        {
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(ConnectionString);
            string Query = "Select * from Users Where userID = @UID";

            SqlCommand cmd = new SqlCommand(Query, Connection);
            cmd.Parameters.AddWithValue("@UID", UID);
            try
            {
                Connection.Open();

                SqlDataReader reader = cmd.ExecuteReader(); 

                if (reader.Read())
                {
                    isFound = true;
                    perID = Convert.ToInt32( reader["PersonID"]);
                    userName = (string)reader["UserName"];
                    password = (string)reader["Password"];
                    isActive = Convert.ToBoolean(reader["IsActive"]); 

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
        static public bool FindUserByUN(string un, ref int UserID, ref int PerID, ref string pass, ref bool isAct)
        {
            bool isFound = false;

            SqlConnection Connection = new SqlConnection(ConnectionString);

            string Query = "Select * from users Where UserName = @un";

            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@un", un);
            try
            {
                Connection.Open();

                SqlDataReader reader = cmd.ExecuteReader(); 

                if (reader.Read())
                {
                    UserID = Convert.ToInt32(reader["UserID"]); 
                    PerID = Convert.ToInt32(reader["PersonID"]);
                    pass = (string)reader["Password"];
                    isAct = Convert.ToBoolean(reader["IsActive"]);
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
        static public DataTable GetUsersList()
        {
            DataTable dt = new DataTable();

            SqlConnection Connection = new SqlConnection(ConnectionString);

            string Query = "select UserID, users.PersonID," +
                " FullName = People.FirstName + ' ' + " +
                "People.SecondName + ' ' + " +
                "People.ThirdName + ' ' + " +
                "People.LastName, " +
                "UserName,  " +
                "IIF (IsActive = 1, 'Yes', " +
                "IIF (IsActive = 0, 'No', 'Unknown')) as IsActive  " +
                "from users " +
                "INNER JOIN People On Users.PersonID = People.PersonID ; ";
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

         static public bool PersonExistsAsUser(int perID)
        {
            bool isExist = false;
            SqlConnection Connection = new SqlConnection(ConnectionString);

            string Query = "Select isFound = 1 from Users Where PersonID = @PerID";
            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@PerID", perID); 
            
            try
            {
                Connection.Open();
                object result = cmd.ExecuteScalar();
                if (result != null) isExist = true; 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }
            finally
            {
                Connection.Close();
            }

            return isExist; 
        }


        static public int AddNewUser(int perID, string userName, string password, bool isActive)
        {
            int UserID = -1;

            SqlConnection Connection = new SqlConnection(ConnectionString);

            string Query = "Insert Into users " +
                "values (@perID, @un, @pass, @Act) ; " +
                "Select Scope_IDENTITY() ;  ";

            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@perID", perID);
            cmd.Parameters.AddWithValue("@un", userName);
            cmd.Parameters.AddWithValue("@pass", password);
            cmd.Parameters.AddWithValue("@Act", isActive);


            try
            {
                Connection.Open();
                object result = cmd.ExecuteScalar();

                if (result != null) UserID = Convert.ToInt32(result); 
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error : " + ex);
            }
            finally
            {
                Connection.Close();
            }

            return UserID; 

        }




        static public bool UpdateUser(int userID, string UserName, string Password, bool isAct)
        {
            int rowsAffected = 0;
            bool hasBeenUp = false;

            SqlConnection Connection = new SqlConnection(ConnectionString);

            string Query = "Update  Users " +
                "Set UserName = @un,  " +
                "Password = @pass, " +
                "IsActive = @act " +
                "Where UserID = @ID ; ";

            SqlCommand cmd = new SqlCommand(Query, Connection);
            cmd.Parameters.AddWithValue("@ID", userID); 
            cmd.Parameters.AddWithValue("@un", UserName);
            cmd.Parameters.AddWithValue("@pass", Password);
            cmd.Parameters.AddWithValue("@act", isAct ? 1 : 0);

            try
            {
                Connection.Open();

                rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0) hasBeenUp = true; 
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



        static public bool DeleteUser(int UserID)
        {
            int rowAffected = 0;
            bool isDeleted = false;

            SqlConnection Connection = new SqlConnection(ConnectionString);

            string Query = "Delete From users Where UserID = @ID";

            SqlCommand cmd = new SqlCommand(Query, Connection);

            cmd.Parameters.AddWithValue("@ID", UserID); 

            try
            {
                Connection.Open();

                rowAffected = cmd.ExecuteNonQuery();

                if (rowAffected > 0) isDeleted = true; 
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

    }
}
