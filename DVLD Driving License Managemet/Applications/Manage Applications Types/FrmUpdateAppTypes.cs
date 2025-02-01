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

namespace DVLD_Driving_License_Managemet.Applications.Manage_Applications_Types
{
    public partial class FrmUpdateAppTypes : Form
    {
        public FrmUpdateAppTypes(clsApplicationsTypes apptype)
        {
            InitializeComponent();
            clsDesign.ApplyRoundedCorners(this);
            _AppType = apptype; 
        }


        private clsApplicationsTypes _AppType = new clsApplicationsTypes();


        private void _InitializeControlsWithData()
        {
            lblID.Text = _AppType._AppTypeID.ToString();
            txtTitle.Text = _AppType._AppTypeTitle;
            txtFees.Text = _AppType._AppTypeFees.ToString();
        }

        private void _LoadInputsToAnApp()
        {
            _AppType._AppTypeTitle = txtTitle.Text;
            _AppType._AppTypeFees = Convert.ToDecimal(txtFees.Text); 
        }



        private void FrmUpdateAppTypecs_Load(object sender, EventArgs e)
        {
            _InitializeControlsWithData(); 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _LoadInputsToAnApp();
            if (_AppType.Save())
            {
                clsDesign._ShowSuccessMessage("Application Type Has Been Updated Succefully :-)");
            }
            else clsDesign._ShowErrorMessage("Application Has Not Been Updated XXX"); 
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
    }
}
