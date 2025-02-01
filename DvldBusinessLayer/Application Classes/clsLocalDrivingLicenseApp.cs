using DVLDataAccessLayer.Application_Data_Access_Classes;
using DVLDataAccessLayer.License_Data_Access_Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldBusinessLayer.Application_Classes
{
    public class clsLocalDrivingLicenseApp : clsApplication
    {
        //LDLA_ID stands for : Local driving license application ID
        public int LDLA_ID { get; set; }
        public int LicenseClassID { get; set; }



        public clsLocalDrivingLicenseApp () : base() 
        {
            LDLA_ID = -1;
            LicenseClassID = -1;
        }
        clsLocalDrivingLicenseApp(int appID, int perID, DateTime AppDate, int typeID, short status, DateTime LastStatus, decimal Fees, int userID,
            int LDLAID, int licClsID) : base(appID, perID, AppDate, typeID, status, LastStatus, Fees, userID )
        {
            LDLA_ID = LDLAID;
            LicenseClassID = licClsID; 
        }

        static public DataTable GetAllDrivingLicenseApps()
        {
            return clsLDLADataAccess.GetAllNewLocalDrivingLicenseApps();
        }

         

        static public bool LDLAExists(clsLocalDrivingLicenseApp LDVLA)
        {
            return clsLDLADataAccess.LDLAExists(LDVLA._ApplicantPersonID, LDVLA.LicenseClassID); 
        }

        private bool _Add()
        {
            this._ApplicationID = clsApplicationDataAccess._AddApp(this._ApplicantPersonID, this._ApplicationDate, this._ApplicationTypeID,
                this._ApplicationStatus, this._LastStatusDate, this._PaidFees, this._UserCreatedID); 
            
            if (this._ApplicationID != -1)
            {
                this.LDLA_ID = clsLDLADataAccess.AddLDLApp(this._ApplicationID, this.LicenseClassID);
                return (this.LDLA_ID != -1); 
            }
            else
            {
                return false; 
            }
        }


        //static public int FindAppIDByClsAndNationalNo()
        //{
        // I need to find the ApplicationID by the selected row info, then I will make the status of that application cancel
        //}



        public bool Save()
        {
            return _Add(); 
        }
    }
}
