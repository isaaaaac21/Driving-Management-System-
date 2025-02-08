using DVLD_Driving_License_Managemet.Applications.ManageTestTypes.TestTypes;
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

namespace DVLD_Driving_License_Managemet.Applications.Driving_License_Apps
{
    public partial class FrmNDLA : Form
    {
        public FrmNDLA()
        {
            InitializeComponent();
            clsDesign.ApplyRoundedCorners(this); 
        }

        private DataTable Dt = clsLocalDrivingLicenseApp.GetAllDrivingLicenseApps();
        string _Filtering = ""; 


        public clsLocalDrivingLicenseApp _ReturnClickedRowLDLA()
        {
            int ID = 0;
            int PassedTests = 0; 
            if (dgvList.CurrentRow.Index != null)
            {

                ID = Convert.ToInt16(dgvList.CurrentRow.Cells["LDL_ID"].Value);
                
            }
            return clsLocalDrivingLicenseApp.FindLDLA(ID); 
        }

        private void _FilterDGVByComboBoxChoice()
        {
            DataView dv = Dt.DefaultView;
            if (cbStatus.SelectedItem != "All")
            {
                dv.RowFilter = $"{_Filtering} like '{cbStatus.SelectedItem.ToString()}' ";
            }
            else
            {
                dv.RowFilter = "";
            }
            dgvList.DataSource = dv;
            lblResult.Text = dv.Count.ToString();
        }


        private void _FilterDGVByTextBoxInput()
        {
            DataView DV = Dt.DefaultView;
            if (_Filtering == "LDL_ID")
            {
                if (txtFilter.Text != "") DV.RowFilter = $"{_Filtering} = {txtFilter.Text}";
                else DV.RowFilter = "";
            }

            else if (string.IsNullOrEmpty(_Filtering))
            {
                DV.RowFilter = "";
            }
            else
            {
                DV.RowFilter = $"{_Filtering} like '{txtFilter.Text}%'";
            }

            lblResult.Text = DV.Count.ToString();
            dgvList.DataSource = DV;
        }



        private void _ShowActiveComboBox()
        {
            cbStatus.Visible = true;
            cbStatus.SelectedIndex = 0;

        }
        private void _ControlsChangeByFilter()
        {
            txtFilter.Visible = false;
            cbStatus.Visible = false;
            _DebouncingTimer.Stop();

            if (cbFilter.SelectedIndex != 0 && cbFilter.SelectedIndex < 6)
            {
                txtFilter.Visible = true;
                cbStatus.Visible = false;
            }
            else if (cbFilter.SelectedIndex == 6)
            {
                txtFilter.Visible = false;
                _ShowActiveComboBox();
            }
            _Filtering = cbFilter.SelectedIndex == 0 ? "" : cbFilter.SelectedItem.ToString();
            _RefreshForm(); 
        }
        private void _UpdateFormAccordingToFilter()
        {
            txtFilter.Clear();
            _ControlsChangeByFilter();
        }
        private void _RefreshForm()
        {
            dgvList.DataSource = clsLocalDrivingLicenseApp.GetAllDrivingLicenseApps(); 
            lblResult.Text = dgvList.Rows.Count.ToString();
        }
        private void _PreventLettersInLDLAFilter(KeyPressEventArgs e)
        {
            if (cbFilter.Text == "LDL_ID")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private void _InitializeControls()
        {
            clsDesign.FillComboBox(ref cbFilter, Dt);
            _RefreshForm();
        }

        private void DisableAllTestsButtons()
        {
            tsmVisTest.Enabled = false;
            tsmWritingTest.Enabled = false;
            tsmStreetTest.Enabled = false;
        }

        private void EnableTestButtonAccordingToPassedTests(int PassedTests)
        {
            if (PassedTests == 0) tsmVisTest.Enabled = true;
            else if (PassedTests == 1) tsmWritingTest.Enabled = true;
            else if (PassedTests == 2) tsmStreetTest.Enabled = true;
        }
        private void TestsEnabling()
        {
            DisableAllTestsButtons();
            clsLocalDrivingLicenseApp app = _ReturnClickedRowLDLA();


            if (app._ApplicationStatus == 3) return; /*this mean that the application is canceled so there is no need to enable tests */ 
            int PassedTests = clsTests.GetTotalPassedTests(app.LDLA_ID);

            EnableTestButtonAccordingToPassedTests(PassedTests); 
            
        }

        private bool _LDLAIsAlreadyCanceled(clsLocalDrivingLicenseApp ldla)
        {
            if (ldla._ApplicationStatus == clsApplication.STATUS_CANCELED)
            {
                clsDesign._ShowErrorMessage("This Application is already canceled !!!");
                return true; 
            }
            return false; 
        }


        private void FrmNDLA_Load(object sender, EventArgs e)
        {
            _InitializeControls(); 
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _UpdateFormAccordingToFilter();
        }

        private void cbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            _FilterDGVByComboBoxChoice();
        }

        private void _DebouncingTimer_Tick(object sender, EventArgs e)
        {
            _FilterDGVByTextBoxInput(); 
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            _DebouncingTimer.Stop();
            _DebouncingTimer.Start(); 
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            _PreventLettersInLDLAFilter(e); 
        }

        private void cbFilter_SelectionChangeCommitted(object sender, EventArgs e)
        {

        }

        private void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmNewDrvLcs Nfrm = new FrmNewDrvLcs();
            Nfrm.FormClosed += Nfrm_FormClosed;
            Nfrm.ShowDialog(); 
        }

        private void Nfrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _RefreshForm(); 
        }

        private void btnShowDetails_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApp LDLA = _ReturnClickedRowLDLA();
            if (_LDLAIsAlreadyCanceled(LDLA)) return;


            if (clsDesign.ConfirmMessage("Are u sure you want to cancel this application ??"))
            {
                if (clsLocalDrivingLicenseApp.CancelAnLDLAByID(LDLA.LDLA_ID))
                {
                    clsDesign._ShowSuccessMessage("The Application has been canceled successfully :-)");
                    _RefreshForm();
                } 
            }
            else
            {
                clsDesign._ShowErrorMessage("The Application has not been canceled !!!"); 
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApp LDLA = _ReturnClickedRowLDLA(); 
            if (clsDesign.ConfirmMessage("Are you sure you want to delete this application ? \nThis will Delete all tests and appointments of this Application."))
            {
                if (clsLocalDrivingLicenseApp.DeleteLDLA(LDLA))
                {
                    clsDesign._ShowSuccessMessage("The application has been deleted successfully :-)");
                    _RefreshForm(); 
                
                }
                else 
                {
                    clsDesign._ShowErrorMessage("The Delete operation has been failed XXX"); 
                }
                
            }
            
        }


        private void tsmTestSch_MouseEnter(object sender, EventArgs e)
        {
            TestsEnabling(); 
            
        }

  
        private FrmTestsAppointment _SetTestAppointmentFormAccordingToTestButton(clsLocalDrivingLicenseApp app, ToolStripMenuItem tsm)
        {
          
            if (tsm == tsmVisTest) return  new FrmTestsAppointment(app, clsTestTypes.VISION);
            else if (tsm == tsmWritingTest) return  new FrmTestsAppointment(app, clsTestTypes.WRITING); 
            else return new FrmTestsAppointment(app, clsTestTypes.STREET);
        }

        private void HandleTestsButtons(object sender, EventArgs e)
        {
            ToolStripMenuItem Tsm = (ToolStripMenuItem)sender; 
            clsLocalDrivingLicenseApp app = _ReturnClickedRowLDLA();

            FrmTestsAppointment frmTestAppointment = _SetTestAppointmentFormAccordingToTestButton(app, Tsm);
            frmTestAppointment.FormClosed += Nfrm_FormClosed;
            frmTestAppointment.ShowDialog(); 

        }


        private void DisableAllTsms()
        {
            foreach(ToolStripItem tsm in contextMenuStrip1.Items)
            {
               if (tsm is ToolStripMenuItem item) tsm.Enabled = false; 
            }
        }
        private void _EnableItemsForNewApp()
        {
            tsmShowDetails.Enabled = true; 
            tsmEdit.Enabled = true;
            tsmDelete.Enabled = true;
            tsmCancel.Enabled = true;
            tsmTestSch.Enabled = true;

        }


        private void _EnableForIssueLicense()
        {
            tsmShowDetails.Enabled = true;
            tsmEdit.Enabled = true;
            tsmDelete.Enabled = true;
            tsmCancel.Enabled = true;
            tsmIssueNewLicense.Enabled = true; 
        }

        private void _EnableForCancelApp()
        {
            tsmShowDetails.Enabled = true; 
            tsmDelete.Enabled = true; 
        }

        private void _EnableForShowLicenseInfo()
        {
            tsmShowDetails.Enabled = true;
            tsmShowLicense.Enabled = true;
            tsmShowLcHis.Enabled = true; 
        }


        //this function will decide which context menu items will be shown based on the selectedApp
        private void _HandleShowedItems(clsLocalDrivingLicenseApp app)
        {
            int TotalTestPassed = clsTests.GetTotalPassedTests(app.LDLA_ID);
            DisableAllTsms(); 
            if (app._ApplicationStatus  == clsApplication.STATUS_CANCELED) // if the application is Canceled
            {
                _EnableForCancelApp();
            }
            else if (TotalTestPassed != 3 && app._ApplicationStatus == clsApplication.STATUS_NEW) //this mean that the applicant has not completed all tests
            {
                _EnableItemsForNewApp();
            }
            else if (TotalTestPassed == 3 && app._ApplicationStatus == clsApplication.STATUS_NEW) // this mean that the applicant has finished all tests and ready for taking a driving license
            {
                _EnableForIssueLicense(); 
            }
            else if (app._ApplicationStatus == clsApplication.STATUS_COMPLETED)
            {
                _EnableForShowLicenseInfo(); 
            }
            

        }


        


        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            clsLocalDrivingLicenseApp app = _ReturnClickedRowLDLA();
            _HandleShowedItems(app); 
        }

        private void tsmIssueNewLicense_Click(object sender, EventArgs e)
        {
            FrmIssueDL frmIssueDL = new FrmIssueDL(_ReturnClickedRowLDLA());
            frmIssueDL.FormClosed += Nfrm_FormClosed;
            frmIssueDL.ShowDialog(); 
        }

        private void tsmShowLicense_Click(object sender, EventArgs e)
        {
            FrmLicenseInfo frmLcInfo = new FrmLicenseInfo(_ReturnClickedRowLDLA());
            frmLcInfo.ShowDialog(); 
        }

        private void tsmShowLcHis_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApp app = _ReturnClickedRowLDLA();
            clsPersons person = clsPersons.FindPerson(app._ApplicantPersonID);

            FrmLicenseHitstory frmLcHis = new FrmLicenseHitstory(person);
            frmLcHis.ShowDialog();
        }
    }
}
