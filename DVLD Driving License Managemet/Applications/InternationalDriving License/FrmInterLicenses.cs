using DVLD_Driving_License_Managemet.Applications.Driving_License_Apps;
using DvldBusinessLayer;
using DvldBusinessLayer.LicenseClasses;
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

namespace DVLD_Driving_License_Managemet.Applications.InternationalDriving_License
{
    public partial class FrmInterLicenses : Form
    {
        public FrmInterLicenses()
        {
            InitializeComponent();
            clsDesign.ApplyRoundedCorners(this, 20); 
        }

        DataTable _Dt = clsInterDL.GetInterLicenses();
        string _Filtering = "";









        private clsInterDL _ReturnClickedRow()
        {
            int InterID = -1; 
            if (dgvList.CurrentRow.Index >= 0)
            {
                InterID = Convert.ToInt32(dgvList.CurrentRow.Cells["IntLcID"].Value); 
            }
            return clsInterDL.GetInterLcByID(InterID); 
        }

        private clsPersons _ExtractPersonFromInterLc()
        {
            clsInterDL interLc = _ReturnClickedRow();
            clsDrivers Driver = clsDrivers.GetDriverByID(interLc.DriverID);

            return clsPersons.FindPerson(Driver.PersonID); 
        }



        private void _RefreshForm()
        {
            dgvList.DataSource = clsInterDL.GetInterLicenses();
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
            if (_Filtering == "IntLcID" || _Filtering == "ApplicationID" ||_Filtering == "LocalLcID")
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

            if (cbFilter.SelectedIndex != 0 && cbFilter.SelectedIndex < 4)
            {
                txtFilter.Visible = true;
                cbAct.Visible = false;
            }
            else if (cbFilter.SelectedIndex == 4)
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
                if (clmn.ColumnName != "IssueDate" && clmn.ColumnName != "ExpDate") cbFilter.Items.Add(clmn.ColumnName);
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
            if (_Filtering == "IntLcID" || _Filtering == "ApplicationID" || _Filtering == "LocalLcID")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void FrmInterLicenses_Load(object sender, EventArgs e)
        {
            _InitializeControls();
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            _PreventLettersInPersonIDFilter(e);
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _UpdateFormAccordingToFilter();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmInterLicenseApp frmInterLicenseApp = new FrmInterLicenseApp();

            frmInterLicenseApp.FormClosed += FrmInterLicenseApp_FormClosed;

            frmInterLicenseApp.ShowDialog(); 
        }

        private void FrmInterLicenseApp_FormClosed(object sender, FormClosedEventArgs e)
        {
            _RefreshForm(); 
        }

        private void cbAct_SelectedIndexChanged(object sender, EventArgs e)
        {
            _FilterDGVByComboBoxChoice(); 
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

        private void tsmShowPerInfo_Click(object sender, EventArgs e)
        {
            clsPersons person = _ExtractPersonFromInterLc();

            frmPersonalInfo frmPersonInfo = new frmPersonalInfo(person);
            frmPersonInfo.ShowDialog(); 

        }

        private void tsmLicenseInfo_Click(object sender, EventArgs e)
        {
            clsInterDL interLc = _ReturnClickedRow();
            FrmInterLcInfo frmInterInfo = new FrmInterLcInfo(interLc);
            frmInterInfo.ShowDialog(); 
        }

        private void tsmLicenseHis_Click(object sender, EventArgs e)
        {
            clsPersons person = _ExtractPersonFromInterLc();
            FrmLicenseHitstory frmLcHis = new FrmLicenseHitstory(person);

            frmLcHis.ShowDialog(); 
        }
    }
}
