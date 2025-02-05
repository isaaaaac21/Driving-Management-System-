using DvldBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Driving_License_Managemet.ManageUsers
{
    public partial class frmChangePass : Form
    {
        public frmChangePass(clsUsers user)
        {
            InitializeComponent();
            _User = user;
            ctrlUserInfo1._MapUserControls(_User);
            clsDesign.ApplyRoundedCorners(this); 
        }
        clsUsers _User = new clsUsers(); 


        private bool _ValidateInputs()
        {
            if (txtCurrPass.Text == "" || txtNewPass.Text == "" || txtConfPass.Text == "")
            {
                clsDesign._ShowErrorMessage("Please Fill the empty fields !!!");
                return false; 
            }

            return true; 
        }
        private void _setNewPassWord()
        {
            _User.Password = txtConfPass.Text; 
        }
        private bool _CheckConfirmPassword()
        {
            return txtConfPass.Text == txtNewPass.Text; 
        }
        private bool _CheckPasswordMatch()
        {
            return txtCurrPass.Text == "" || txtCurrPass.Text == _User.Password; 
        }

        private void _SetError(TextBox txt, CancelEventArgs e, string msg)
        {
            e.Cancel = true;
            txt.Focus();
            errorProvider1.SetError(txt, msg);
        }




        private void _RemoveError(TextBox txt)
        {
            errorProvider1.SetError(txt, ""); 
        }
        private void _SetErrorForIncorrectValue(TextBox sender, CancelEventArgs e)
        {
            if (sender == txtCurrPass)
            {
                if (!_CheckPasswordMatch())
                    _SetError(sender, e, "Incorrect Password");
                else
                    _RemoveError(sender); 
            }
            else
            {
                if (!_CheckConfirmPassword())
                    _SetError(sender, e, "Passwords don't match");
                else
                    _RemoveError(sender); 
            }
           
        }
        private void frmChangePass_Load(object sender, EventArgs e)
        {

        }

        private void TextsVidatingValidating(object sender, CancelEventArgs e)
        {
            _SetErrorForIncorrectValue((TextBox) sender, e);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_ValidateInputs()) return; 
            _setNewPassWord(); 
            if(_User.Save())
            {
                clsDesign._ShowSuccessMessage("New Password has been Set succefully"); 
            }
            else
            {
                clsDesign._ShowErrorMessage("Failed to Save the New password"); 
            }
        }

        private void btnClose2_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
    }
}
