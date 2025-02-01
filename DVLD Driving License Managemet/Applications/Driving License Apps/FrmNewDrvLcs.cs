using DvldBusinessLayer;
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

namespace DVLD_Driving_License_Managemet.Applications
{
    public partial class FrmNewDrvLcs : Form
    {
        public FrmNewDrvLcs()
        {
            InitializeComponent();
            clsDesign.ApplyRoundedCorners(this);
            ctrlFilter1.PersonInfoFilled += CtrlFilter1_PersonInfoFilled;
            ctrlFilter1.NoPersonInfo += CtrlFilter1_NoPersonInfo;
        }

          decimal Fees = 20 ; 
         clsLocalDrivingLicenseApp _CurrentApplication = new clsLocalDrivingLicenseApp(); 
        enum enMode { New = 1, Completed, Canceled};
        string NEWDRIVINGLICENSEAPPFEES = "20,00"; 
        private void _PopulateComboBox()
        {
            DataTable Dt = clsLicenseClass.GetClassesList();
            
            foreach(DataRow row in Dt.Rows)
            {
                cbLsClass.Items.Add(row["ClassName"]); 
            }
            cbLsClass.SelectedIndex = 0; 

        }
        private void _ReturnDefaultValues()
        {
            lblDate.Text = "YYYY-MM-DD";
            lblFees.Text = "00,00";
            lblID.Text = "N/A";
            lblUserName.Text = "N/A"; 
          
        }

        private void _LoadInputsToAnApp()
        {
            _CurrentApplication._ApplicantPersonID = ctrlFilter1.per.PersonID;
            _CurrentApplication._ApplicationDate = DateTime.Now;
            _CurrentApplication._ApplicationTypeID = 1;
            _CurrentApplication._ApplicationStatus = Convert.ToInt16( enMode.New);
            _CurrentApplication._LastStatusDate = DateTime.Now;
            _CurrentApplication._PaidFees = Fees;
            _CurrentApplication._UserCreatedID = clsCommonThings._MainUser.UserID;

            _CurrentApplication.LicenseClassID = cbLsClass.SelectedIndex + 1 ; 

        }

        private void _InitializeAppInfo()
        {
            lblDate.Text = DateTime.Now.ToShortDateString();
            lblUserName.Text = clsCommonThings._MainUser.UserName;  ;
            lblFees.Text = NEWDRIVINGLICENSEAPPFEES; 
            _CurrentApplication._ApplicantPersonID = ctrlFilter1.per.PersonID;
        }

        private bool _LDVLAExists()
        {
            if( clsLocalDrivingLicenseApp.LDLAExists(_CurrentApplication))
            {
                clsDesign._ShowErrorMessage("Choose another license class, the selected person already has an active application for the selected class");
                return false; 
            }
            return true; 
        }
        private void CtrlFilter1_NoPersonInfo(object sender, EventArgs e)
        {
            btnNext.Enabled = false;
            cbLsClass.Enabled = false;
            btnSave.Enabled = false;
            _ReturnDefaultValues(); 
        }

        private void CtrlFilter1_PersonInfoFilled(object sender, EventArgs e)
        {
            btnNext.Enabled = true;
            cbLsClass.Enabled = true;
            btnSave.Enabled = true;

        }

        private void FrmNewDrvLcs_Load(object sender, EventArgs e)
        {
            _PopulateComboBox(); 
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            tb1.SelectedIndex++;
            _InitializeAppInfo(); 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            _LoadInputsToAnApp();
            if(! _LDVLAExists()) return;
            if (_CurrentApplication.Save())
            {
                clsDesign._ShowSuccessMessage("New Driving License Application Has Been Added succeffully :-) ");
            }
            else
            {
                clsDesign._ShowErrorMessage("Adding operation Has Been Failed"); 
            }
        }
    }
}
