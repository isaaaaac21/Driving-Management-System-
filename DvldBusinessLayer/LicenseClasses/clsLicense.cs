using DVLDataAccessLayer.License_Data_Access_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldBusinessLayer
{
    public class clsLicense
    {
        public int LicenseID { get; set; } 
        public int ApplicationID { get; set; }
        public int DriverID { get; set; }
        public int LicenseClassID { get; set; }
        public DateTime IssueDate { get; set; } 
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public decimal PaidFees { get; set; }
        public bool isActive { get; set; }
        public enIssueReasons IssueReason { get; set; }
        public int CreatedByUserID { get; set; }
        
        
        public enum enIssueReasons
        {

            FirstTime = 1, 
            Renew = 2, 
            ReplacementForDamaged = 3, 
            ReplacementForLost = 4
        }

        enum enMode { Add = 0, Update};
        enMode Mode; 
        public clsLicense()
        {
            LicenseID = -1;
            ApplicationID = -1;
            DriverID = -1;
            LicenseClassID = -1;
            IssueDate = DateTime.Now;
            ExpirationDate = DateTime.Now;
            Notes = "";
            PaidFees = 0;
            isActive = false;
            IssueReason = enIssueReasons.FirstTime;
            CreatedByUserID = -1;
            Mode = enMode.Add; 

        }

        clsLicense(int LID, int appID, int DriID, int LcID, DateTime IssDate, DateTime expDate, string notes, decimal fees, bool isAct, byte issReason, int userID)
        {
            LicenseID = LID;
            ApplicationID = appID;
            DriverID = DriID;
            LicenseClassID = LcID;
            IssueDate = IssDate;
            ExpirationDate = expDate;
            Notes = notes;
            PaidFees = fees;
            isActive = isAct;
            IssueReason = (enIssueReasons) issReason;
            CreatedByUserID = userID;
            Mode = enMode.Update; 
        }



        static public DataTable GetTotalLicensesOfDriver(int DriverID)
        {
            return clsLicenseDataAccess.GetTotalLicensesOfDriver(DriverID); 
        }


        public clsLicense GetLicenseByID(int LicenseID)
        {
            int AppID = -1, DrID = -1, LClassID = -1, UserID = -1;
            DateTime issDate = DateTime.Now, expDate = DateTime.Now;
            string notes = "";
            decimal fees = 0;
            bool isAct = false;
            byte issReason = 0;

            if (clsLicenseDataAccess.GetLicenseByID(LicenseID, ref AppID, ref DrID, ref LClassID, ref issDate, ref expDate, ref notes, ref fees,
                ref isAct, ref issReason, ref UserID))
            {
                return new clsLicense(LicenseID, AppID, DrID, LClassID, issDate, expDate, notes, fees, isAct, issReason, UserID);
            }
            else return null; 
        }

        static public clsLicense GetLicenseByInfoID(int personID, int LcID)
        {
            int LicenseID = -1, AppID = -1, DrID = -1, UserID = -1;
            DateTime issDate = DateTime.Now, expDate = DateTime.Now;
            string notes = "";
            decimal fees = 0;
            bool isAct = false;
            byte issReason = 0;

            if (clsLicenseDataAccess.GetLicenseByPerLcID(personID, LcID,  ref LicenseID, ref AppID, ref DrID, ref issDate, ref expDate, ref notes, ref fees,
                ref isAct, ref issReason, ref UserID))
            {
                return new clsLicense(LicenseID, AppID, DrID, LcID, issDate, expDate, notes, fees, isAct, issReason, UserID);
            }
            else return null;
        }

        //this function will check if the applicant already has a license of the giving LicenseClass
        static public bool LicenseExists(int PersonID, int LcID )
        {
            return clsLicenseDataAccess.LicenseExists(PersonID, LcID); 
        }


        private bool _AddLicense()
        {
            this.LicenseID = clsLicenseDataAccess.AddLicense(this.ApplicationID, this.DriverID, this.LicenseClassID, this.IssueDate, this.ExpirationDate,
                this.Notes, this.PaidFees, this.isActive, (byte)this.IssueReason, this.CreatedByUserID);

            return (this.LicenseID != -1);
        }

        private bool _UpdateLicense()
        {
            return clsLicenseDataAccess.UpdateLicense(this.LicenseID, this.ApplicationID, this.DriverID, this.LicenseClassID, this.IssueDate, this.ExpirationDate,
                this.Notes, this.PaidFees, this.isActive, (byte)this.IssueReason, this.CreatedByUserID);
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.Add: 
                    if (_AddLicense())
                    {
                        Mode = enMode.Update;
                        return true; 
                    }
                    break;
                case enMode.Update:
                    return _UpdateLicense(); 
            }
            return false; 
        }


    }
}
