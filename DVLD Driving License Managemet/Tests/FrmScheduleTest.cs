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
        public FrmScheduleTest(clsLocalDrivingLicenseApp app, int typeTest, clsTestAppointment Testapp = null)
        {
            InitializeComponent();
            clsDesign.ApplyRoundedCorners(this);
            _TestType = typeTest;
            _CurrentApp = app;
            _NewTest = Testapp == null ? new clsTestAppointment() : Testapp;
        }

        private int _TestType;
        private clsLocalDrivingLicenseApp _CurrentApp = new clsLocalDrivingLicenseApp();
        private clsTestAppointment _NewTest = new clsTestAppointment(); 


        private void _SetImageAccordingToType()
        {
            if (_TestType == clsTestTypes.VISION) pbTypePic.Image = Resources.Glasses;
            else if (_TestType == clsTestTypes.WRITING) pbTypePic.Image = Resources.Writing;
            else pbTypePic.Image = Resources.Road; 
        }
        private void _InitializeFormWithData()
        {
            _SetImageAccordingToType(); 
            clsPersons per = clsPersons.FindPerson(_CurrentApp._ApplicantPersonID);

            lblLDLAID.Text = _CurrentApp.LDLA_ID.ToString();
            lblLcCls.Text = clsLicenseClass.GetLicenseClassByID(_CurrentApp.LicenseClassID)._Name;
            lblName.Text = per.FirstName + " " + per.LastName;
            dtpAppointment.MinDate = DateTime.Today.AddDays(1); 
            lblTestTypeFees.Text = clsTestTypes.FindTestTypeByID(_TestType)._TestTypeFees.ToString(); 

        }

        private void _CreateAppointment()
        {

            _NewTest.TestTypeID = _TestType;
            _NewTest.LDLAID = _CurrentApp.LDLA_ID;
            _NewTest.AppointmentDate = dtpAppointment.Value;
            _NewTest.PaidFees = clsTestTypes.FindTestTypeByID(_TestType)._TestTypeFees;
            _NewTest.CreatedByUserID = clsCommonThings._MainUser.UserID;
           
            
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
            if (_NewTest.TestAppointmentID != -1 && _NewTest.isLocked) DisableIfAppIsLocked(); 
            _InitializeFormWithData();
        }

        private void btnClose1_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _CreateAppointment(); 
            if(_NewTest.Save())
            {
                clsDesign._ShowSuccessMessage("Appointment has been saved successfully :-)"); 
            }
            else
            {
                clsDesign._ShowErrorMessage("The Appointment has not been saved XXX"); 
            }
        }
    }
}
