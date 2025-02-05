﻿using DVLD_Driving_License_Managemet.Applications.ManageTestTypes.TestTypes;
using DvldBusinessLayer;
using DvldBusinessLayer.Application_Classes;
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
            if (clsLocalDrivingLicenseApp.CancelAnLDLAByID(LDLA.LDLA_ID))
            {
                if (clsDesign.ConfirmMessage("Are u sure you want to cancel this application ??"))
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
            if (clsDesign.ConfirmMessage("Are you sure you want to delete this application ? "))
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

        private void tsmVisTest_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApp app = _ReturnClickedRowLDLA();

            FrmTestsAppointment FrmTest = new FrmTestsAppointment(app, clsTestTypes.VISION);
            FrmTest.ShowDialog();

        }

        private void tsmTestSch_MouseEnter(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApp app = _ReturnClickedRowLDLA();
            if (app._ApplicationStatus == 3) tsmVisTest.Enabled = false;
            else tsmVisTest.Enabled = true; 
        }
    }
}
