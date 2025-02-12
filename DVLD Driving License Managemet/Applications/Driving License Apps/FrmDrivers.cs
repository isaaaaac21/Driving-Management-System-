using DvldBusinessLayer;
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

namespace DVLD_Driving_License_Managemet.Applications.Driving_License_Apps
{
    public partial class FrmDrivers : Form
    {
        public FrmDrivers()
        {
            InitializeComponent();
            clsDesign.ApplyRoundedCorners(this, 20); 
        }

        string _Filtering = "";

        DataTable _Dt = clsDrivers.GetDriversList(); 


        private void _FilterByTextInput()
        {
            DataView Dv = _Dt.DefaultView; 

            if (string.IsNullOrEmpty(txtFilter.Text))
            {
                Dv.RowFilter = ""; 
            }
            else
            {
                switch(_Filtering)
                {
                    case "DriverID":
                    case "PersonID":
                    case "AcitveLicenses":
                        if (int.TryParse(txtFilter.Text, out _)) Dv.RowFilter = $"{_Filtering} = {txtFilter.Text}";
                        else
                            Dv.RowFilter = "1 = 0";
                        break;


                    default:
                        Dv.RowFilter = $"{_Filtering} LIKE '{txtFilter.Text}%'";
                        break; 

                }
            }

            lblResult.Text = Dv.Count.ToString();
            dgvList.DataSource = Dv; 
        }


        private void _RefreshForm()
        {
            dgvList.DataSource = clsDrivers.GetDriversList();
            lblResult.Text = dgvList.Rows.Count.ToString(); 
        }

        private void _PopulateComboBox()
        {
            foreach(DataColumn clmn in _Dt.Columns)
            {
                if (clmn.ColumnName != "Date") cbFilter.Items.Add(clmn.ColumnName); 
            }
            cbFilter.SelectedIndex = 0; 
        }

        private void _HandleTxtVisible()
        {
            if (cbFilter.SelectedIndex != 0) txtFilter.Visible = true;
            else txtFilter.Visible = false; 
        }
        private void ChangeFilterAccordingToCbItem()
        {
            txtFilter.Clear(); 
            _DebouncingTimer.Stop();
            _HandleTxtVisible(); 




            _Filtering = cbFilter.SelectedItem.ToString() == "None" ? "" : cbFilter.SelectedItem.ToString();
            _RefreshForm(); 
        }


        private void _PreventLettersInPersonIDFilter(KeyPressEventArgs e)
        {
            if (_Filtering == "PersonID" || _Filtering == "DriverID" || _Filtering == "AcitveLicenses")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }



        private clsDrivers _ReturnClickedDriver()
        {
            int ID = -1; 
            if (dgvList.CurrentRow.Index >= 0)
            {
                ID = Convert.ToInt32(dgvList.CurrentRow.Cells["DriverID"].Value); 
            }
            return clsDrivers.GetDriverByID(ID); 
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void FrmDrivers_Load(object sender, EventArgs e)
        {
            _PopulateComboBox(); 
            _RefreshForm(); 
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            _PreventLettersInPersonIDFilter(e); 
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeFilterAccordingToCbItem(); 
        }

        private void _DebouncingTimer_Tick(object sender, EventArgs e)
        {
            _FilterByTextInput(); 
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            _DebouncingTimer.Stop();
            _DebouncingTimer.Start(); 
        }

        private void showDriverLicensesHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsDrivers Dr = _ReturnClickedDriver();
            clsPersons Per = clsPersons.FindPerson(Dr.PersonID);

            FrmLicenseHitstory lcHis = new FrmLicenseHitstory(Per);
            lcHis.ShowDialog(); 
        }
    }
}
