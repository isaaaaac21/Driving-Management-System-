using DVLDataAccessLayer.Tests_Data_Access;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldBusinessLayer.Test_Classes
{
    public class clsTests
    {
        public int TestID { get; set; }
        public int TestAppointmentID { get; set; }
        public bool Result { get; set; }
        public string Notes { get; set; }
        
        public int CreatedByUser { get; set; }


        public clsTests()
        {
            TestID = -1;
            TestAppointmentID = -1;
            Result = false;
            Notes = "";
            CreatedByUser = -1; 
        }

         clsTests(int id, int testappID, bool result, string Note, int user)
        {
            TestID = id;
            TestAppointmentID = testappID;
            Result = result;
            Notes = Note;
            CreatedByUser = user; 
        }

        private bool _Add()
        {
            this.TestID = clsTestDataAccess._AddTest(this.TestAppointmentID, this.Result, this.Notes, this.CreatedByUser);

            return (this.TestID != -1); 
        }

        static public bool TestPassed(int TestTypeID, int LDLAID)
        {
            return clsTestDataAccess.TestHasBeenPassed(TestTypeID, LDLAID); 
        }

        public bool Save()
        {
            return _Add(); 
        }

    }
}
