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
    public partial class frmUserInfo : Form
    {
        public frmUserInfo(clsUsers user)
        {
            InitializeComponent();
            clsDesign.ApplyRoundedCorners(this);
            _InitializeCtrls(user); 
            
        }
        public void _InitializeCtrls(clsUsers user)
        {
            ctrlUserInfo1._MapUserControls(user); 
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmUserInfo_Load(object sender, EventArgs e)
        {

        }

        private void ctrlInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
