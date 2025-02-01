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
        public short _ApplicationStatus { get; set; }
        public DateTime _LastStatusDate { get; set; }
        public decimal _PaidFees { get; set; }

        public int _UserCreatedID { get; set; }


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

        }

        protected clsApplication(int ID, int perID, DateTime AppDate, int typeID, short status, DateTime LastStatus, decimal Fees, int userID)
        {
            _ApplicationID = ID;
            _ApplicantPersonID = perID;
            _ApplicationDate = AppDate;
            _ApplicationTypeID = typeID;
            _ApplicationStatus = status;
            _LastStatusDate = LastStatus;
            _PaidFees = Fees;
            _UserCreatedID = userID;
        }


        static public bool CheckIfPersonHasSameApp(clsApplication App)
        {
            return false; 
        }
    }




}