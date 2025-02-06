using DVLD_Driving_License_Managemet.Properties;
using DvldBusinessLayer;
using DvldBusinessLayer.Application_Classes;
using DvldBusinessLayer.Test_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Driving_License_Managemet.Tests
{
    public partial class FrmScheduleTest : Form
    {
        public FrmScheduleTest(clsLocalDrivingLicenseApp app, int typeTest, /*this object for the edit mode*/ clsTestAppointment Testapp = null, bool Retake = false)
        {
            InitializeComponent();
            clsDesign.ApplyRoundedCorners(this);
            _TestType = typeTest;
            _CurrentApp = app;
            _NewTestAppointment = Testapp == null ? new clsTestAppointment() : Testapp;
            _RetakeTest = Retake; 
        }

        private int _TestType;
        private clsLocalDrivingLicenseApp _CurrentApp = new clsLocalDrivingLicenseApp();
        private clsTestAppointment _NewTestAppointment = new clsTestAppointment();
        private bool _RetakeTest = false; 

        private void _SetImageAccordingToType()
        {
            if (_TestType == clsTestTypes.VISION) pbTypePic.Image = Resources.Glasses;
            else if (_TestType == clsTestTypes.WRITING) pbTypePic.Image = Resources.Writing;
            else pbTypePic.Image = Resources.Road; 
        }
        private void _InitializeForRetakeTest()
        {
            clsApplicationsTypes RetType = clsApplicationsTypes.FindAppType(7);
            gbRetake.Enabled = true;
            lblRetAppFees.Text = RetType._AppTypeFees.ToString();
            lblTotalFees.Text = (clsTestTypes.FindTestTypeByID(_TestType)._TestTypeFees + RetType._AppTypeFees).ToString(); 
        }
        private void CheckIfRetakeTest()
        {
            if (_RetakeTest)
            {
                _InitializeForRetakeTest(); 
            }
        }


        private void _InitializeFormWithData()
        {
             CheckIfRetakeTest();
            _SetImageAccordingToType(); 
            clsPersons per = clsPersons.FindPerson(_CurrentApp._ApplicantPersonID);

            lblLDLAID.Text = _CurrentApp.LDLA_ID.ToString();
            lblLcCls.Text = clsLicenseClass.GetLicenseClassByID(_CurrentApp.LicenseClassID)._Name;
            lblName.Text = per.FirstName + " " + per.LastName;
            dtpAppointment.MinDate = DateTime.Today.AddDays(1); 
            lblTestTypeFees.Text = clsTestTypes.FindTestTypeByID(_TestType)._TestTypeFees.ToString();
            lblTrial.Text = clsTests.GetTotalFialedTrialsOfTest(_CurrentApp.LDLA_ID, _TestType).ToString(); 

        }
        private void DisableButtonSave()
        {
            btnSave.Enabled = false;
        }
        private void _CreateAppointment()
        {

            _NewTestAppointment.TestTypeID = _TestType;
            _NewTestAppointment.LDLAID = _CurrentApp.LDLA_ID;
            _NewTestAppointment.AppointmentDate = dtpAppointment.Value;
            _NewTestAppointment.PaidFees = clsTestTypes.FindTestTypeByID(_TestType)._TestTypeFees;
            _NewTestAppointment.CreatedByUserID = clsCommonThings._MainUser.UserID;
            
        }
        private clsApplication _CreateRetakeApplication()
        {
            clsApplication app = new clsApplication();
            app._ApplicantPersonID = _CurrentApp._ApplicantPersonID;
            app._ApplicationDate = DateTime.Now;
            app._ApplicationTypeID = 7;
            app._ApplicationStatus = 1;
            app._LastStatusDate = DateTime.Now;
            app._PaidFees = clsApplicationsTypes.FindAppType(7)._AppTypeFees;
            app._UserCreatedID = clsCommonThings._MainUser.UserID;

            return app; 
        }
        private void DisableIfAppIsLocked()
        {
            foreach(Control ctrl in this.Controls)
            {
               if (ctrl != btnClose1) ctrl.Enabled = false; 
            }
        }

        private void FrmScheduleTest_Load(object sender, EventArgs e)
        {
            if (_NewTestAppointment.TestAppointmentID != -1 && _NewTestAppointment.isLocked) DisableIfAppIsLocked(); 
            _InitializeFormWithData();
        }

        private void btnClose1_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private bool _HandleRetakeApp()
        {
            clsApplication RetakeApp = _CreateRetakeApplication(); 

            if (!RetakeApp.Save())
            {
                clsDesign._ShowErrorMessage("Retake Test Has not been Saved X X X");
                return false; 
            }
            _NewTestAppointment.RetakeTestAppID = RetakeApp._ApplicationID;
            return true; 
        }

        private void _CreateAndSaveAppointmen()
        {
            _CreateAppointment();
            if (_NewTestAppointment.Save())
            {
                clsDesign._ShowSuccessMessage("Appointment has been saved successfully :-)");
                DisableButtonSave();
            }
            else
            {
                clsDesign._ShowErrorMessage("The Appointment has not been saved XXX");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if (_RetakeTest && !_HandleRetakeApp())
            {
                 return; 
            }

            _CreateAndSaveAppointmen(); 
        }
    }
}
