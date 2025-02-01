using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDataAccessLayer; 
namespace DvldBusinessLayer
{
    public class clsUsers 
    {
        public int PersonID { get; set;  }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set;  }

        public bool isActive { get; set; }

        enum enMode { Add = 0, Update};

        enMode Mode;

       public clsUsers() 
        {
            PersonID = -1; 
            UserID = -1;
            UserName = "";
            Password = "";
            isActive = false;
            Mode = enMode.Add; 
        }

         clsUsers(int perID, int uID, string username, string pass, bool active)
        {
            PersonID = perID;
            UserID = uID;
            UserName = username;
            Password = pass;
            isActive = active;
            Mode = enMode.Update; 
        }

        static public DataTable UsersList()
        {
            return clsUsersDataAccess.GetUsersList(); 
        }

        static public clsUsers Find(int ID)
        {
            int perID = -1; 
            string userName = "", password = "";
            bool isActive = false;
            if (clsUsersDataAccess.FindUser(ID, ref perID, ref userName, ref password, ref isActive))
            {
                return new clsUsers(perID, ID, userName, password, isActive);
            }
            else return null; 
        }
        static public bool PersonExistsAsUser(int personID)
        {
            return clsUsersDataAccess.PersonExistsAsUser(personID); 
        }

        private bool _Add()
        {
            this.UserID = clsUsersDataAccess.AddNewUser(this.PersonID, this.UserName, this.Password, this.isActive);

            return (this.UserID != -1); 
        }

        private bool _Update()
        {
            return clsUsersDataAccess.UpdateUser(this.UserID, this.UserName, this.Password, this.isActive); 
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.Add:
                    if(_Add())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    break;
                case enMode.Update:
                    return _Update(); 
            }
            return false; 
        }


        static public bool DeleteUser(int UserID)
        {
            return clsUsersDataAccess.DeleteUser(UserID); 
        }

        static public clsUsers FindUserByUN(string UserName)
        {
            int ID = -1;
            int perID = -1;
            string PassWord = "";
            bool isActive = false;

            if (clsUsersDataAccess.FindUserByUN(UserName, ref ID, ref perID, ref PassWord, ref isActive))
            {
                return new clsUsers(perID, ID, UserName, PassWord, isActive);
            }
            else return null; 
        }

    }
}
