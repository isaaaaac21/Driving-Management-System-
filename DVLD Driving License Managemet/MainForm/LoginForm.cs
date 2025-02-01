using DvldBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DVLD_Driving_License_Managemet
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            
            InitializeComponent();
            clsDesign.ApplyRoundedCorners(this);
        }
        private string _Path = @"D:\F things\Programming\Fundamentals\After course 13\c#\Projects\Driving License Management System project\DVLD Driving License Managemet\Users\Login.txt";
        private bool _CheckEmptyFields()
        {
            if (tbUseName.Text == "" || tbPassword.Text == "")
            {
                clsDesign._ShowErrorMessage("Please Fill the Empty Fields");
                return false; 
            }
            return true; 
        }
        private bool _CheckCorrectInfo()
        {
            clsUsers user = clsUsers.FindUserByUN(tbUseName.Text);
            if (user != null && tbPassword.Text == user.Password)
            {
                clsCommonThings._MainUser = user; 

                return true; 
            }
            return false; 
        }
        private string _ReturnFileInfo()
        {
            return File.ReadAllText(_Path); 
        }
        private void _PupolateTextBoxesWithInfo()
        {
            
            if (File.Exists(_Path) && !string.IsNullOrWhiteSpace(File.ReadAllText(_Path)) )
            {
                
                string[] infos = File.ReadAllText(_Path).Split('#');
                tbUseName.Text = infos[0];
                tbPassword.Text = infos[1];
                chkRemeberMe.Checked = true; 
            }
            
        }
        private string _ReturnInfoInOneString()
        {
            string result = ""; 
            result = clsCommonThings._MainUser.UserName + "#";
            result += clsCommonThings._MainUser.Password;

            return result; 
        }
        private void _SaveLoginInfoIfChecked()
        {
            
            if (chkRemeberMe.Checked)
            {
                File.WriteAllText(_Path, _ReturnInfoInOneString());     
            }
            //else
            //{
            //    File.WriteAllText(_Path, string.Empty); 
            //}
        }
        private void _ShowOrHidePassword()
        {
            if (tbPassword.PasswordChar == '*')
            {
                tbPassword.PasswordChar = '\0'; 
            }
            else
            {
                tbPassword.PasswordChar = '*'; 
            }
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            _PupolateTextBoxesWithInfo(); 
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!_CheckEmptyFields()) return;

            if (_CheckCorrectInfo())
            {
                frmMain frmMain = new frmMain();
                _SaveLoginInfoIfChecked(); 
                frmMain.ShowDialog();

            }
            else
                clsDesign._ShowErrorMessage("Invalid Username/Password"); 

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnShowPassword_Click(object sender, EventArgs e)
        {
            _ShowOrHidePassword(); 
        }

        private void pbWheel_Click(object sender, EventArgs e)
        {
        }

   

    }
}
