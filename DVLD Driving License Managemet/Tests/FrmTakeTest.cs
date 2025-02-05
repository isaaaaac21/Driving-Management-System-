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
    public partial class FrmTakeTest : Form
    {
        public FrmTakeTest(clsLocalDrivingLicenseApp app, clsTestAppointment testapp)
        {
            InitializeComponent();
            _CurrentApp = app;
            _CurrAppointment = testapp; 

        }

        clsLocalDrivingLicenseApp _CurrentApp = new clsLocalDrivingLicenseApp();
        clsTestAppointment _CurrAppointment = new clsTestAppointment();
        clsTests _CurrTest = new clsTests();



        private void _LoadDataToTest()
        {
            _CurrTest.TestAppointmentID = _CurrAppointment.TestAppointmentID;
            _CurrTest.Result = rbPass.Checked ? true : false;
            _CurrTest.Notes = txtNotes.Text;
            _CurrTest.CreatedByUser = clsCommonThings._MainUser.UserID; 
        }

        private void _SetImageAccordingToType()
        {
            if (_CurrAppointment.TestTypeID == clsTestTypes.VISION) pbTypePic.Image = Resources.Glasses;
            else if (_CurrAppointment.TestTypeID == clsTestTypes.WRITING) pbTypePic.Image = Resources.Writing;
            else pbTypePic.Image = Resources.Road;
        }
        private void _InitializeFormWithData()
        {
            _SetImageAccordingToType();
            clsPersons per = clsPersons.FindPerson(_CurrentApp._ApplicantPersonID);
            clsTestTypes testType = clsTestTypes.FindTestTypeByID(_CurrAppointment.TestTypeID);


            groupBox1.Text = testType._TestTypeTitle;  
            lblLDLAID.Text = _CurrentApp.LDLA_ID.ToString();
            lblLcCls.Text = clsLicenseClass.GetLicenseClassByID(_CurrentApp.LicenseClassID)._Name;
            lblName.Text = per.FirstName + " " + per.LastName;
            dtpAppointment.Value = _CurrAppointment.AppointmentDate;
            lblTestTypeFees.Text = testType._TestTypeFees.ToString();
            rbPass.Checked = true; 
        }

        private void btnClose1_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
        private bool LockAndSaveTestAppointment()
        {
            _CurrAppointment.isLocked = true;
            if (_CurrAppointment.Save()) return true;

            return false; 
        }
        private void FrmTakeTest_Load(object sender, EventArgs e)
        {
            _InitializeFormWithData(); 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _LoadDataToTest();

            if (clsDesign.ConfirmMessage("Are you sure you want to save ? \nAfter that You cannot Change the Pass\\Fail results !!"))
            {
                if (_CurrTest.Save())
                {
                    if(LockAndSaveTestAppointment())
                    {
                        clsDesign._ShowSuccessMessage("Test Has been Taken successully !!!"); 
                    }
                }
                else
                {
                    clsDesign._ShowErrorMessage("Test Data has not been saved !!!");
                }
            }
        }
    }
}
