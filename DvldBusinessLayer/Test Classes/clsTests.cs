﻿using DVLDataAccessLayer.License_Data_Access_Classes;
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

        //this function will return the last test info of an LocalDrivingLicense Application of a giving type. 
        static public clsTests GetTheLastTestOfApplication(int LDLAID, int TestType)
        {
            int testID = -1, TestAppointmentID = -1, userID = -1;
            bool result = false;
            string Note = "";
            if (clsTestDataAccess.GetLastTest(LDLAID, TestType, ref testID, ref TestAppointmentID, ref result, ref Note, ref userID))
            {
                return new clsTests(testID, TestAppointmentID, result, Note, userID);
            }
            else return null; 
        }

        static public int GetTotalPassedTests(int LDLAID)
        {
            return clsTestDataAccess.GetTotalPassedTests(LDLAID);
        }

        static public int GetTotalFialedTrialsOfTest(int LDLAID, int TestTypeID)
        {
            return clsTestDataAccess.GetTotalTestFailTrialsOfType(LDLAID, TestTypeID); 
        }

        static public bool DeleteAllTestsByLDLAID(int LDLAID )
        {
            return clsTestDataAccess.DeleteAllTestsByLDLAID(LDLAID); 
        }

        public bool Save()
        {
            return _Add(); 
        }

    }
}
