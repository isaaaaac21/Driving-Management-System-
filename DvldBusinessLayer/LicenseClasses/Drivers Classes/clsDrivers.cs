using DVLDataAccessLayer.License_Data_Access_Classes;
using DvldBusinessLayer.Application_Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldBusinessLayer.LicenseClasses.Drivers_Classes
{
    public class clsDrivers
    {
        public int DriverID { get; set; } 
        public int PersonID { get; set; }
        public int CreatedByUserID { get; set; }
        public DateTime CreatedDate { get; set; }

        
        enum enMode { Add = 0, Update}
        enMode Mode; 

        
        public clsDrivers()
        {
            DriverID = -1;
            PersonID = -1;
            CreatedByUserID = -1;
            CreatedDate = DateTime.Now;
            Mode = enMode.Add; 
        }
        
        clsDrivers(int DrID, int PerID, int UserID, DateTime CreatDate )
        {
            DriverID = DrID;
            PersonID = PerID;
            CreatedByUserID = UserID;
            CreatedDate = CreatDate;
            Mode = enMode.Update; 
        }

        
        static public clsDrivers GetDriverByID(int ID)
        {
            int perID = -1, userID = -1;
            DateTime CreateDate = DateTime.Now;
            if (clsDriversDataAccess.GetDriverByID(ID, ref perID, ref userID, ref CreateDate))
            {
                return new clsDrivers(ID, perID, userID, CreateDate);
            }
            else return null; 
        }
        static public DataTable GetDriversList()
        {
            return clsDriversDataAccess.GetDriversList(); 
        }
        static public clsDrivers GetDriverByPersonID(int PerID)
        {
            int DriverID = -1, userID = -1;
            DateTime CreateDate = DateTime.Now;
            if (clsDriversDataAccess.GetDriverByPersonID(ref DriverID, PerID ,ref userID, ref CreateDate))
            {
                return new clsDrivers(DriverID, PerID, userID, CreateDate);
            }
            else return null;
        }


        static public int PersonExistsAsDriver(int PerID)
        {
            return clsDriversDataAccess.PersonExistsAsDriver(PerID); 
        }



        private bool _AddDriver()
        {
            this.DriverID = clsDriversDataAccess.AddNewDriver(this.PersonID, this.CreatedByUserID, this.CreatedDate);
            return (this.DriverID != -1); 
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.Add: 
                    if (_AddDriver())
                    {
                        Mode = enMode.Update;
                        return true; 
                    }
                    break; 
            }
            return false; 
        }



       

    }
}
