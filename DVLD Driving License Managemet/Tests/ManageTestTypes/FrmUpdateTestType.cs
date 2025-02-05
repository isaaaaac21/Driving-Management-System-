using DvldBusinessLayer.Application_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Driving_License_Managemet.Applications.ManageTestTypes
{
    public partial class FrmUpdateTestType : Form
    {
        public FrmUpdateTestType(clsTestTypes test)
        {
            InitializeComponent();
            clsDesign.ApplyRoundedCorners(this);
            _TestType = test;
        }
        private clsTestTypes _TestType = new clsTestTypes();


        private void _InitializeControlsWithData()
        {
            lblID.Text = _TestType._TestTypeID.ToString();
            txtTitle.Text = _TestType._TestTypeTitle;
            txtDesc.Text = _TestType._TestTypeDesc; 
            txtFees.Text = _TestType._TestTypeFees.ToString();
        }

        private void _LoadInputsToATest()
        {
            _TestType._TestTypeTitle = txtTitle.Text;
            _TestType._TestTypeDesc = txtDesc.Text; 
            _TestType._TestTypeFees = Convert.ToDecimal(txtFees.Text);
        }










        private void UpdateTestType_Load(object sender, EventArgs e)
        {
            _InitializeControlsWithData(); 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _LoadInputsToATest();
            if (_TestType.Save())
            {
                clsDesign._ShowSuccessMessage("Test Type Has been Updated Successfully :-)");
            }
            else clsDesign._ShowErrorMessage("Test Has Not Been Updated XXX"); 
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
    }
}
