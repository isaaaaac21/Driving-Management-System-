using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Driving_License_Managemet.ManageUsers;
using DvldBusinessLayer;
namespace DVLD_Driving_License_Managemet
{
    public partial class frmUsers : Form
    {
        public frmUsers()
        {
            InitializeComponent();
            clsDesign.ApplyRoundedCorners(this, 20);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen; 
        }

        public delegate void SendUserInfo(clsUsers user);
        public event SendUserInfo ShowUser; 

        private DataTable _dt = clsUsers.UsersList();
        private string _Filtering = "";
 
        

        private void _PopulateFilterComboBox()
        {
            foreach (DataColumn clmn in _dt.Columns)
            {
                if (clmn.ColumnName != "Password") cbFilter.Items.Add(clmn.ColumnName); 
            }
            cbFilter.SelectedIndex = 0; 
        }
        private void _RefreshForm()
        {
            dgvList.DataSource = clsUsers.UsersList();
            lblResult.Text = dgvList.Rows.Count.ToString(); 
        }
        private void _InitializeControls()
        {
            _RefreshForm();
            _PopulateFilterComboBox(); 
        }
        
        private void _FilterDGVByComboBoxChoice()
        {
            DataView dv = _dt.DefaultView; 
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
            DataView DV = _dt.DefaultView;
            if (_Filtering == "PersonID" || _Filtering == "UserID")
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
        private void _PreventLettersInPersonIDFilter(KeyPressEventArgs e)
        {
            if (_Filtering == "PersonID" || _Filtering == "UserID")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true; 
                }
            }
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
            DebouncingTimer.Stop(); 

            if (cbFilter.SelectedIndex != 0 && cbFilter.SelectedIndex < 5)
            {
                 txtFilter.Visible = true;
                 cbAct.Visible = false; 
            }
            else if (cbFilter.SelectedIndex == 5)
            {
                txtFilter.Visible = false;
                _ShowActiveComboBox();
            }
           
            _Filtering = cbFilter.SelectedIndex == 0 ?  "" : cbFilter.SelectedItem.ToString();
            _RefreshForm(); 
        }
        private void _UpdateFormAccordingToFilter()
        {
            txtFilter.Clear();
            _ControlsChangeByFilter(); 
        }
        private clsUsers _ReturnTheSelectedRow()
        {
            clsUsers user = new clsUsers();
            if (dgvList.CurrentRow.Index >= 0)
            {
                DataGridViewRow row = dgvList.Rows[dgvList.CurrentRow.Index];

                int Id = Convert.ToInt32(row.Cells["UserID"].Value);
                user = clsUsers.Find(Id); 
            }
            return user;
        }
        private void frmUsers_Load(object sender, EventArgs e)
        {

            _InitializeControls();
            
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            _UpdateFormAccordingToFilter(); 
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            _PreventLettersInPersonIDFilter(e); 
        }

        private void btnClose1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            
            DebouncingTimer.Stop();
            DebouncingTimer.Start();
        }

        private void cbAct_SelectedIndexChanged(object sender, EventArgs e)
        {
            _FilterDGVByComboBoxChoice(); 
        }

        private void DebouncingTimer_Tick(object sender, EventArgs e)
        {
            _FilterDGVByTextBoxInput(); 
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Form userFrm = new AddUserFrm();
            userFrm.FormClosed += UserFrm_FormClosed;
            userFrm.ShowDialog();
        }

        private void UserFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _RefreshForm();
        }

        private void cmShowDet_Click(object sender, EventArgs e)
        {
            clsUsers user = _ReturnTheSelectedRow();

            frmUserInfo userInfo = new frmUserInfo(user);
            userInfo.ShowDialog(); 
        }

        private void cmChangePass_Click(object sender, EventArgs e)
        {
            clsUsers user = _ReturnTheSelectedRow(); 
            frmChangePass chPass = new frmChangePass(user);

            chPass.ShowDialog();
        }

        private void cmEdit_Click(object sender, EventArgs e)
        {
            clsUsers user = _ReturnTheSelectedRow();

            AddUserFrm addfrm = new AddUserFrm(user);
            addfrm.FormClosed += UserFrm_FormClosed; 
            addfrm.ShowDialog(); 
        }

        private void cmDelete_Click(object sender, EventArgs e)
        {
            clsUsers user = _ReturnTheSelectedRow();

            if (clsDesign.ConfirmMessage("Are You Sure You Want to Delete This User ??"))
            {
                if (clsUsers.DeleteUser(user.UserID))
                {
                    clsDesign._ShowSuccessMessage("User Has Been Deleted Successfully :-)");
                    _RefreshForm();
                }
                else
                {
                    clsDesign._ShowErrorMessage("User Has Not Been Deleted X X X");
                }
            }
            else
            {
                clsDesign._ShowErrorMessage("Operation Has been Canceled");
            }
        }
    }
}
