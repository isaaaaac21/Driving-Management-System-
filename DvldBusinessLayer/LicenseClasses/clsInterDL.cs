using DVLDataAccessLayer.License_Data_Access_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldBusinessLayer.LicenseClasses
{
    public class clsInterDL
    {

        public int InterLicenseID { get; set; }
        public int ApplicationID { get; set; }
        public int DriverID { get; set;  }
        public int IssuedLocalLicenseID { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public bool IsActive { get; set; }
        public int CreatedByUserID { get; set; } 

        //I need to collect the data and save the international license
        enum enMode { Add = 1, Update }
        enMode Mode; 


        public clsInterDL()
        {
            InterLicenseID = -1;
            ApplicationID = -1;
            DriverID = -1;
            IssuedLocalLicenseID = -1;
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;
            IsActive = false;
            CreatedByUserID = -1;
            Mode = enMode.Add; 
        }
        clsInterDL(int interLID, int appID, int DriID, int LocID, DateTime IssDate, DateTime ExpDate, bool isAct, int userID)
        {
            InterLicenseID = interLID;
            ApplicationID = appID;
            DriverID = DriID;
            IssuedLocalLicenseID = LocID;
            IssueDate = IssDate;
            ExpirationDate = ExpDate;
            IsActive = isAct;
            CreatedByUserID = userID;
            Mode = enMode.Update;
        }

        static public clsInterDL GetInterLcByID(int interLcID)
        {
            int AppID = -1, DrID = -1, issLocID = -1, UserID = -1;
            DateTime issDate = DateTime.Now, expDate = DateTime.Now;
   
            bool isAct = false;
           

            if (clsInterLcDataAccess.GetInterLcByID (interLcID, ref AppID, ref DrID, ref issLocID, ref issDate, ref expDate,
                ref isAct,  ref UserID))
            {
                return new clsInterDL(interLcID, AppID, DrID, issLocID, issDate, expDate, isAct, UserID);
            }
            else return null;
        }


        static public bool DriverHasInternationalLicense(int DriverID)
        {
            return clsInterLcDataAccess.DriverHasInterLicense(DriverID);
        }


        static public DataTable GetInterLicensesOfDriver(int DriverID)
        {
            return clsInterLcDataAccess.GetInterLicensesOfDriver(DriverID);
        }



        static public DataTable GetInterLicenses()
        {
            return clsInterLcDataAccess.GetInterLicenses(); 
        }





        private bool _Add()
        {
            this.InterLicenseID = clsInterLcDataAccess._Add(this.ApplicationID, this.DriverID, this.IssuedLocalLicenseID, this.IssueDate, this.ExpirationDate,
                this.IsActive, this.CreatedByUserID);

            return (this.InterLicenseID != -1); 
        }


        public bool Save()
        {
            switch(Mode)
            {
                case enMode.Add: 
                    if (_Add())
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
