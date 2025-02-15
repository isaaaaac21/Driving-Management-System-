﻿using DVLDataAccessLayer.Application_Data_Access_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldBusinessLayer.Application_Classes
{
    public class clsApplication
    {

        public int _ApplicationID { get; set; }
        public int _ApplicantPersonID { get; set; }
        public DateTime _ApplicationDate { get; set; }

        public int _ApplicationTypeID { get; set; }
        public int _ApplicationStatus { get; set; }
        public DateTime _LastStatusDate { get; set; }
        public decimal _PaidFees { get; set; }

        public int _UserCreatedID { get; set; }

        protected enum enMode { Add = 0, Update }
        protected enMode Mode = enMode.Add;

        static public int STATUS_NEW = 1, STATUS_COMPLETED = 2, STATUS_CANCELED = 3; 



        public clsApplication()
        {
            _ApplicationID = -1;
            _ApplicantPersonID = -1;
            _ApplicationDate = DateTime.Now;
            _ApplicationTypeID = -1;
            _ApplicationStatus = -1;
            _LastStatusDate = DateTime.Now;
            _PaidFees = 0;
            _UserCreatedID = -1;
            Mode = enMode.Add; 

        }

        protected clsApplication(int ID, int perID, DateTime AppDate, int typeID, int status, DateTime LastStatus, decimal Fees, int userID)
        {
            _ApplicationID = ID;
            _ApplicantPersonID = perID;
            _ApplicationDate = AppDate;
            _ApplicationTypeID = typeID;
            _ApplicationStatus = status;
            _LastStatusDate = LastStatus;
            _PaidFees = Fees;
            _UserCreatedID = userID;
            Mode = enMode.Update; 
        }

        static public bool CheckIfPersonHasSameApp(clsApplication App)
        {
            return false;
        }



        static public clsApplication FindAnAppByID(int ID)
        {
            int perID = -1;
            DateTime dt = DateTime.Now;
            int typeID = -1;
            int appStatus = -1;
            DateTime lastStatus = DateTime.Now;
            decimal fees = 0;
            int userID = -1;

            if (clsApplicationDataAccess.FindApp(ID, ref perID, ref dt, ref typeID, ref appStatus, ref lastStatus, ref fees, ref userID))
            {
                return new clsApplication(ID, perID, dt, typeID, appStatus, lastStatus, fees, userID);
            }
            else return null; 
        }

        protected bool _Add()
        {
            this._ApplicationID = clsApplicationDataAccess._AddApp(this._ApplicantPersonID, this._ApplicationDate, this._ApplicationTypeID, this._ApplicationStatus,
                this._LastStatusDate, this._PaidFees, this._UserCreatedID);

            return (this._ApplicationID != -1); 
        }

        protected bool _Update()
        {
            return clsApplicationDataAccess.UpdateApp(this._ApplicationID, this._ApplicantPersonID, this._ApplicationDate, this._ApplicationTypeID, this._ApplicationStatus,
                this._LastStatusDate, this._PaidFees, this._UserCreatedID); 
        }

        static public bool DeleteAppByID(int appID)
        {
            return clsApplicationDataAccess.DeleteApp(appID);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:
                    if (_Add())
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

    }
}