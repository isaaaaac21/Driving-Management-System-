using DVLDataAccessLayer.License_Data_Access_Classes.DetainLicense;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldBusinessLayer.LicenseClasses.Detain_License
{
    public  class clsDetainLicense
    {
        public int DetainID { get; set; }
        public int LicenseID { get; set; }

        public DateTime DetainDate { get; set; }
        public decimal FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        public bool isReleased { get; set; }
        public DateTime? ReleasedDate { get; set; }
        public int? ReleaseByUserID { get; set; } 
        
        public int? ReleaseApplicationID { get; set; }

        enum enMode { Add = 0, update}
        enMode Mode; 

        public clsDetainLicense()
        {
            DetainID = -1;
            LicenseID = -1;
            DetainDate = DateTime.Today;
            FineFees = 0;
            CreatedByUserID = -1;
            isReleased = false;
            ReleasedDate = null;
            ReleaseByUserID = null;
            ReleaseApplicationID = null;

            Mode = enMode.Add; 
        }


        clsDetainLicense(int DetID, int lcID, DateTime DetTime, decimal fees, int userID, bool isRel, DateTime? relDate, int? RelUserID, int? RelAppID )
        {
            DetainID = DetID;
            LicenseID = lcID;
            DetainDate = DetTime;
            FineFees = fees;
            CreatedByUserID = userID;
            isReleased = isRel;
            ReleasedDate = relDate;
            ReleaseByUserID = RelUserID;
            ReleaseApplicationID = RelAppID;

            Mode = enMode.update; 
         }


        static public clsDetainLicense GetDetainedLicenseByLcID(int LcID)
        {

            int DetID = -1, userID = -1; 
            int? RelUserID = -1, relAppID = -1;
            DateTime DetDate = DateTime.Now; 
            DateTime? relDate = DateTime.Now;
            decimal fees = -1;
            bool isRel = false;

            if (clsDetianLcDataAccess.GetDetainedLicenseByLcID(LcID, ref DetID, ref DetDate, ref fees, ref userID, ref isRel, ref relDate, ref RelUserID, ref relAppID))
            {
                return new clsDetainLicense(DetID, LcID, DetDate, fees, userID, isRel, relDate, RelUserID, relAppID);
            }
            else return null;
        }

         public bool _Add()
        {
            this.DetainID = clsDetianLcDataAccess._AddDetainedLicense(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID, this.isReleased,
                this.ReleasedDate, this.ReleaseByUserID, this.ReleaseApplicationID);

            return (this.DetainID != -1); 
        }


        static public bool LicenseIsDetained(int LcID)
        {
            return clsDetianLcDataAccess.LicenseIsDetained(LcID);
        }




        public bool _Update()
        {
            return clsDetianLcDataAccess._UpdateDetained(this.DetainID, this.isReleased, this.ReleasedDate, this.ReleaseByUserID,
                this.ReleaseApplicationID); 
        }

        public bool Save()
        {
            switch(Mode)
            {
                case enMode.Add: 
                    if (_Add())
                    {
                        Mode = enMode.update;
                        return true; 
                    }
                    break;
                case enMode.update:
                    return _Update(); 
            }
            return false; 
        }




    }
}
