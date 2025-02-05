using DvldBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Driving_License_Managemet
{
    public partial class AddUserFrm : Form
    {
        public AddUserFrm(clsUsers user = null  )
        {
            InitializeComponent();
            clsDesign.ApplyRoundedCorners(this);
            ctrlFilter2.PersonInfoFilled += ctrlFilter2_PersonFilled;
            ctrlFilter2.NoPersonInfo += ctrlFilter2_NoPersonInfo;
         
            _User = user != null ? user : new clsUsers();
        }

        clsUsers _User = new clsUsers();


        private void _LoadUserDataToControls()
        {
            ctrlFilter2._MapControlsForEditing(_User.PersonID);
            _LoadLoginInfo(); 
        }

        private void _LoadLoginInfo()
        {
            txtUserName.Text = _User.UserName;
            txtPass.Text = _User.Password; 
            txtConfPass.Text = _User.Password;
            lblID.Text = _User.UserID.ToString(); 
            chkActive.Checked = _User.isActive ? true : false; 
        }

        private void _CleanLogin()
        {
            txtUserName.Text = "";
            txtPass.Text = "";
            txtConfPass.Text = "";
            chkActive.Checked = false; 
        }
        private void _EditingMode()
        {
            lblMode.Text = "Update a User"; 
            _LoadUserDataToControls();
            _EnableDisableLoginButtons();
        }
        private bool _checkMissingFields()
        {
            if (txtUserName.Text == "" ||
                txtPass.Text == "" || 
                txtConfPass.Text == "")
            {
                _ShowErrorMessage("Please fill the empty fields");
                return false; 
            }

            return true; 
        }


        private clsUsers _LoadInputToUser()
        {
           
            

            _User.PersonID = ctrlFilter2.per.PersonID;
            _User.UserName = txtUserName.Text;
            _User.Password = txtPass.Text;
            _User.isActive = (chkActive.Checked == true ? true : false ) ;

            return _User;
        }

        private void _UpdateIDlbl(clsUsers user)
        {
            lblID.Text = user.UserID.ToString(); 
        }

        private void _PasswordNotMatchError(CancelEventArgs e)
        {
            if (txtConfPass.Text != "" && txtConfPass.Text != txtPass.Text)
            {
                e.Cancel = true;
                txtConfPass.Focus();
                errorProvider1.SetError(txtConfPass, "Passwords don't match !!!");
            }
            else
            {

                errorProvider1.SetError(txtConfPass, "");
            }
        }
        private void _EnableDisableLoginButtons(bool Enable = true)
        {
            if (Enable)
            {
                txtUserName.Enabled = true;
                txtPass.Enabled = true;
                txtConfPass.Enabled = true;
                chkActive.Enabled = true;
                btnSave.Enabled = true; 
            }
            else
            {
                txtUserName.Enabled = false;
                txtPass.Enabled = false;
                txtConfPass.Enabled = false;
                chkActive.Enabled = false;
                btnSave.Enabled = false;
            }
        }
        public void ctrlFilter2_PersonFilled(object sender, EventArgs e)
        {

            _CleanLogin(); 
            btnNext.Enabled = true; 
        }
        public void ctrlFilter2_NoPersonInfo(object sender, EventArgs e)
        {
            _EnableDisableLoginButtons(false);
            _CleanLogin(); 
            btnNext.Enabled = false; 
        }
        
        

        private void _ShowSuccessMessage(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void _ShowErrorMessage(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        public bool _CheckIfPersonExistsAsUser()
        {
            return clsUsers.PersonExistsAsUser(ctrlFilter2.per.PersonID);
        }
        


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
          
            if (_CheckIfPersonExistsAsUser())
           {
                _EnableDisableLoginButtons(false);


                _ShowErrorMessage("The User is already exists");
            }
           else
            {
                _EnableDisableLoginButtons();
                tb1.SelectedIndex++; 
            }
          
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_checkMissingFields()) return;
            clsUsers user = _LoadInputToUser(); 

            if (user.Save())
            {
                _ShowSuccessMessage("User Has been Added successfully");
                _UpdateIDlbl(user); 
            }
            else
            {

                _ShowErrorMessage("User has not been added"); 
            }
        }

        private void txtConfPass_Validating(object sender, CancelEventArgs e)
        {
            _PasswordNotMatchError(e);
        }

        private void AddUserFrm_Load(object sender, EventArgs e)
        {
            if (_User.UserID != -1)
            {
                _EditingMode(); 
            }
        }
    }
}
