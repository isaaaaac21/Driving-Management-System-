using DVLD_Driving_License_Managemet.Applications.Driving_License_Apps;
using DvldBusinessLayer;
using DvldBusinessLayer.LicenseClasses;
using DvldBusinessLayer.LicenseClasses.Detain_License;
using DvldBusinessLayer.LicenseClasses.Drivers_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Driving_License_Managemet.Applications.DetainLicense
{
    public partial class FrmMngDetLcs : Form
    {
        public FrmMngDetLcs()
        {
            InitializeComponent();
            clsDesign.ApplyRoundedCorners(this, 20); 
        }

        DataTable _Dt = clsDetainLicense.GetDetainedLicenseList();
        string _Filtering = ""; 


        private clsLicense _ReturnClickedLicense()
        {
            int ID = -1;
            if (dgvList.CurrentRow.Index >= 0)
            {
                ID = Convert.ToInt32(dgvList.CurrentRow.Cells["LcID"].Value); 
            }
            return clsLicense.GetLicenseByID(ID); 
        }

        private bool LicenseIsReleased(clsLicense lc)
        {
            if (!clsDetainLicense.LicenseIsDetained(lc.LicenseID))
            {
                return true; 
            }
            return false; 
        }


        private clsPersons _ExtractPersonFromLicense()
        {
            clsLicense lc = _ReturnClickedLicense();

            return clsPersons.FindPerson(clsDrivers.GetDriverByID(lc.DriverID).PersonID); 

        }


        private void _RefreshForm()
        {
            dgvList.DataSource = clsDetainLicense.GetDetainedLicenseList();

            lblResult.Text = dgvList.Rows.Count.ToString();
        }
        private void _FilterDGVByComboBoxChoice()
        {
            DataView dv = _Dt.DefaultView;
            if (cbAct.SelectedItem != "All")
            {
                dv.RowFilter = $"{_Filtering} like '{cbAct.SelectedItem.ToString()}' ";
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
            DataView DV = _Dt.DefaultView;
            if (_Filtering == "DetID" || _Filtering == "LcID" )
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
            cbAct.Visible = true;
            cbAct.SelectedIndex = 0;

        }
        private void _ControlsChangeByFilter()
        {
            txtFilter.Visible = false;
            cbAct.Visible = false;
            _DebouncingTimer.Stop();

            if (cbFilter.SelectedIndex != 0 && cbFilter.SelectedIndex != 5)
            {
                txtFilter.Visible = true;
                cbAct.Visible = false;
            }
            else if (cbFilter.SelectedIndex == 5)
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
        private void _PopulateFilterComboBox()
        {
            foreach (DataColumn clmn in _Dt.Columns)
            {
                if (clmn.ColumnName != "FineFees" && clmn.ColumnName != "ReleaseDate" && clmn.ColumnName != "RelAppID")
                    cbFilter.Items.Add(clmn.ColumnName);
            }
            cbFilter.SelectedIndex = 0;
        }
        private void _InitializeControls()
        {
            _RefreshForm();
            _PopulateFilterComboBox();
        }
        private void _PreventLettersInPersonIDFilter(KeyPressEventArgs e)
        {
            if (_Filtering == "DetID" || _Filtering == "LcID" )
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }


        private void FrmMngDetLcs_Load(object sender, EventArgs e)
        {
            _InitializeControls();
        }
        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            _DebouncingTimer.Stop();
            _DebouncingTimer.Start(); 
        }

        private void _DebouncingTimer_Tick(object sender, EventArgs e)
        {
            _FilterDGVByTextBoxInput(); 
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            _PreventLettersInPersonIDFilter(e); 
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _UpdateFormAccordingToFilter();
        }

        private void cbAct_SelectedIndexChanged(object sender, EventArgs e)
        {
            _FilterDGVByComboBoxChoice(); 
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmDetainLc frmDetLc = new FrmDetainLc();
            frmDetLc.FormClosed += FrmDetLc_FormClosed;
            frmDetLc.ShowDialog(); 
        }

        private void FrmDetLc_FormClosed(object sender, FormClosedEventArgs e)
        {
            _RefreshForm(); 
        }

        private void tsmShowPerInfo_Click(object sender, EventArgs e)
        {
            clsPersons per = _ExtractPersonFromLicense();

            frmPersonalInfo perInfo = new frmPersonalInfo(per);
            perInfo.ShowDialog(); 


        }

        private void tsmLicenseInfo_Click(object sender, EventArgs e)
        {
            clsLicense lc = _ReturnClickedLicense();
            FrmLicenseInfo frmLcInf = new FrmLicenseInfo(lc);
            frmLcInf.ShowDialog(); 
        }

        private void tsmLicenseHis_Click(object sender, EventArgs e)
        {
            clsPersons per = _ExtractPersonFromLicense();
            FrmLicenseHitstory frmHist = new FrmLicenseHitstory(per);
            frmHist.ShowDialog(); 
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            FrmReleaseDetained frmReleDet = new FrmReleaseDetained();
            frmReleDet.ShowDialog(); 
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLicense lc = _ReturnClickedLicense();

            FrmReleaseDetained frmRelease = new FrmReleaseDetained(lc);
            frmRelease.FormClosed += FrmDetLc_FormClosed; 
            frmRelease.ShowDialog(); 
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            clsLicense lc = _ReturnClickedLicense();
            if (LicenseIsReleased(lc)) tsmReleaseDetLc.Enabled = false; 


        }
    }
}
