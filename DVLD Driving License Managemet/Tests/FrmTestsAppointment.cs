using DVLD_Driving_License_Managemet.Properties;
using DVLD_Driving_License_Managemet.Tests;
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

namespace DVLD_Driving_License_Managemet.Applications.ManageTestTypes.TestTypes
{
    public partial class FrmTestsAppointment : Form
    {
        public FrmTestsAppointment(clsLocalDrivingLicenseApp LDLA,  int TestType)
        {
            InitializeComponent();
            clsDesign.ApplyRoundedCorners(this); 
            ctrlDrivingeLicenseInfo1._MapDataWithLDLA(LDLA); 
            _TestType = TestType;
            _CurrentApp = LDLA; 
        }
        private int _TestType;
        private clsLocalDrivingLicenseApp _CurrentApp = new clsLocalDrivingLicenseApp(); 

        private void _PopulateDataGrid()
        {
            dgvList.DataSource = clsTestAppointment.GetAppointmentsList(_CurrentApp.LDLA_ID, _TestType);
            lblRe.Text = dgvList.Rows.Count.ToString(); 
        }


        private clsTestAppointment _ReturnSelectedRow()
        {
            int ID = 0; 
            if (dgvList.CurrentRow.Index >= 0)
            {
                ID = Convert.ToInt32(dgvList.CurrentRow.Cells["AppointmentID"].Value); 
            }
            return clsTestAppointment.FindAnAppointmentByID(ID); 
        }
        private void _InitializeVisionTestForm()
        {
            lblTitle.Text = "Vision Test Appointments";
            pbTypePic.Image = Resources.Glasses; 

        }
        private void _InitializeFormAccordingToTestType()
        {
            if (_TestType == clsTestTypes.VISION)
            {
                _InitializeVisionTestForm(); 
            }

            _PopulateDataGrid(); 
        }

        private bool _TestAppointmentExists()
        {
            if (clsTestAppointment.TestAppExists(_CurrentApp.LDLA_ID, _TestType))
            {
                clsDesign._ShowErrorMessage("An active Appointment for this test  already exists !!!");
                return true; 
            }
            return false; 
        }

        private bool _TestAppointmentIsLocked(clsTestAppointment testApp)
        {
            if (testApp.isLocked)
            {
                clsDesign._ShowErrorMessage("The test of this appointment has been already taken XXXX");
                return false; 
            }

            return true; 
        }


        private bool TestPassed()
        {
           if (clsTests.TestPassed(_TestType, _CurrentApp.LDLA_ID))
            {
                clsDesign._ShowErrorMessage("This Type of test has already been passed"); 
                return true; 
            }

            return false; 
        }
        private void FrmTestsAppointment_Load(object sender, EventArgs e)
        {
            _InitializeFormAccordingToTestType(); 
        }

        private void btnClose1_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (_TestAppointmentExists() || TestPassed()) return; 
            FrmScheduleTest frmSchedTest = new FrmScheduleTest(_CurrentApp, _TestType);
            frmSchedTest.FormClosed += FrmSchedTest_FormClosed;
            frmSchedTest.ShowDialog(); 
        }

        private void FrmSchedTest_FormClosed(object sender, FormClosedEventArgs e)
        {
            _PopulateDataGrid();
        }

        private void tsmEdit_Click(object sender, EventArgs e)
        {
            clsTestAppointment TestApp = _ReturnSelectedRow();
            FrmScheduleTest FrmSchedule = new FrmScheduleTest(_CurrentApp, _TestType, TestApp);
            FrmSchedule.FormClosed += FrmSchedTest_FormClosed; 
            FrmSchedule.ShowDialog(); 
        }

        private void tsmTakeTest_Click(object sender, EventArgs e)
        {
            clsTestAppointment testApp = _ReturnSelectedRow();
            if (!_TestAppointmentIsLocked(testApp)) return;



            FrmTakeTest TakeTest = new FrmTakeTest(_CurrentApp, testApp);
            TakeTest.FormClosed += FrmSchedTest_FormClosed;
            TakeTest.ShowDialog(); 
        }
    }
}
