using DVLDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldBusinessLayer.Test_Classes
{
    public class clsTestAppointment
    {
        public int TestAppointmentID { get; set; } 
        public int TestTypeID { get; set; }
        public int LDLAID { get; set; } 

        public DateTime AppointmentDate { get; set; }
        public decimal PaidFees { get; set; }

        public int CreatedByUserID { get; set; }

        public bool isLocked { get; set; }

        public int RetakeTestAppID { get; set; }

        enum enMode {Add = 0, Edit}
        enMode Mode = enMode.Add; 

        public clsTestAppointment()
        {
            TestAppointmentID = -1;
            TestTypeID = -1;
            LDLAID = -1;
            AppointmentDate = DateTime.Now;
            PaidFees = 0;
            CreatedByUserID = -1;
            isLocked = false;
            RetakeTestAppID = -1;
            Mode = enMode.Add; 
        }

        clsTestAppointment(int TappID, int TtypeID, int lDLAID, DateTime AppDate, decimal fees, int user, bool locked, int retakeID)
        {
            TestAppointmentID = TappID;
            TestTypeID = TtypeID;
            LDLAID = lDLAID;
            AppointmentDate = AppDate;
            PaidFees = fees;
            CreatedByUserID = user;
            isLocked = locked;
            RetakeTestAppID = retakeID;
            Mode = enMode.Edit;
        }

       static public DataTable GetAppointmentsList(int LDLAID, int tTypeID)
        {
            return clsTestAppointmentsDataAccess.GetTestAppointmentsList(LDLAID, tTypeID); 
        }

       static public bool TestAppIsActive(int LDLAID, int TypeID)
        {
            return clsTestAppointmentsDataAccess.TestAppIsActive(LDLAID, TypeID); 
        }



       static public clsTestAppointment FindAnAppointmentByID(int AppID)
       {
            int TestTypeID = -1, LDLAID = -1, userID = -1, RetID = -1;
            DateTime appDate = DateTime.Now;
            decimal fees = 0;
            bool isLocked = false;

            if (clsTestAppointmentsDataAccess.FindTestAppointmentByID(AppID, ref TestTypeID, ref LDLAID, ref appDate, ref fees, ref userID, ref isLocked, ref RetID))
            {
                return new clsTestAppointment(AppID, TestTypeID, LDLAID, appDate, fees, userID, isLocked, RetID);
            }
            else return null; 

       }

      private bool _Add()
      {
            this.TestAppointmentID = clsTestAppointmentsDataAccess._AddTestApp(this.TestTypeID, this.LDLAID, this.AppointmentDate, this.PaidFees,
                this.CreatedByUserID, this.isLocked, this.RetakeTestAppID);

            return (this.TestAppointmentID != -1); 
      }

      private bool _Update()
        {
            return clsTestAppointmentsDataAccess.UpdateAnAppointment(this.TestAppointmentID, this.AppointmentDate, this.isLocked); 
        }

        static public bool DeleteAllAppointmentsOfLDLA(int LDLAID)
        {
           return clsTestAppointmentsDataAccess.DeleteAllAppointmentsOfLDLA(LDLAID); 
        }

      public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add: 
                    if (_Add())
                    {
                        Mode = enMode.Edit;
                        return true; 
                    }
                    break;
                case enMode.Edit:
                    return _Update();
                  
            }
            return false; 
        }


     

    }
}
