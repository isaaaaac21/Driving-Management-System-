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

namespace DVLD_Driving_License_Managemet.UserControls
{
    public partial class ctrlUserInfo : UserControl
    {
        public ctrlUserInfo()
        {
            InitializeComponent();
         
        }

        clsUsers _user = new clsUsers(); 

        public void _InitializePersonalInfo()
        {
            clsPersons per = clsPersons.FindPerson(_user.PersonID);
            ctrlInfo1._MapControlsWithData(per); 
        }
        public void _InitializeUserInfo()
        {
            lblID.Text = _user.UserID.ToString();
            lblUserName.Text = _user.UserName;
            lblActive.Text = _user.isActive ? "Yes" : "No";
        }
        public void _MapUserControls(clsUsers user)
        {
            _user = user;
            _InitializePersonalInfo();
            _InitializeUserInfo();

            
        }



        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
