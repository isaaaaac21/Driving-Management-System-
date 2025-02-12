using DVLD_Driving_License_Managemet.Applications.Driving_License_Apps;
using DvldBusinessLayer;
using DvldBusinessLayer.Application_Classes;
using DvldBusinessLayer.LicenseClasses;
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

namespace DVLD_Driving_License_Managemet.Applications.Renew_Driving_License
{
    public partial class FrmRenewDLc : Form
    {
        public FrmRenewDLc()
        {
            InitializeComponent();
            ctrlFindLicenseInfo1.objectFound += CtrlFindLicenseInfo1_objectFound;
            clsDesign.ApplyRoundedCorners(this, 20); 
        }


        private clsLicense _OldLicense;
        private clsLicense _NewLicense = new clsLicense();
        private clsApplication CreateApplication()
        {
            clsApplication app = new clsApplication();

            app._ApplicantPersonID = clsPersons.FindPerson(clsDrivers.GetDriverByID(_OldLicense.DriverID).PersonID).PersonID;
            app._ApplicationDate = DateTime.Now;
            app._ApplicationTypeID = Convert.ToInt32(clsApplicationsTypes.enAppTypes.RenewDL);
            app._ApplicationStatus = clsApplication.STATUS_COMPLETED;
            app._LastStatusDate = DateTime.Now;
            app._PaidFees = clsApplicationsTypes.FindAppType(Convert.ToInt32(clsApplicationsTypes.enAppTypes.RenewDL))._AppTypeFees;
            app._UserCreatedID = clsCommonThings._MainUser.UserID;

            return app;
        }
        private int _CreateApplicationAndSaveItAndReturnItsID()
        {
            clsApplication _RnewApp = CreateApplication();
            _RnewApp.Save();
            return _RnewApp._ApplicationID;

        }

        private clsLicense _CreateRenewalLicense()
        {
            clsLicense RenewLicense = new clsLicense();

            RenewLicense.ApplicationID = _CreateApplicationAndSaveItAndReturnItsID();
            RenewLicense.DriverID = _OldLicense.DriverID;
            RenewLicense.LicenseClassID = _OldLicense.LicenseClassID;
            RenewLicense.Notes = txtNotes.Text;
            RenewLicense.PaidFees = clsLicenseClass.GetLicenseClassByID(_OldLicense.LicenseClassID)._ClassFess; 
            RenewLicense.IssueDate = DateTime.Today;
            RenewLicense.ExpirationDate = RenewLicense.IssueDate.AddYears(clsLicenseClass.GetLicenseClassByID(_OldLicense.LicenseClassID)._ValidityLength);
            RenewLicense.isActive = true;
            RenewLicense.IssueReason = clsLicense.enIssueReasons.Renew; 
            RenewLicense.CreatedByUserID = clsCommonThings._MainUser.UserID;
            return RenewLicense;
        }

        private void UpdateOldLcActStatus()
        {
            _OldLicense.isActive = false;
            _OldLicense.Save(); 
        }
        private void _UpdateCtrlsAndUpdateAcitivity(clsLicense RenLc)
        {
           
            lblRenewLcID.Text = RenLc.LicenseID.ToString();
            lblAppID.Text = RenLc.ApplicationID.ToString();
            lkLcInfo.Enabled = true;
            btnRenew.Enabled = false; 
            UpdateOldLcActStatus();
        }


        //this function will initialize fees and license class labels based on the found license
        private void _FinishInitilization()
        {
            clsLicenseClass lcCls = clsLicenseClass.GetLicenseClassByID(_OldLicense.LicenseClassID);
            lblLcFees.Text = lcCls._ClassFess.ToString();
            lblExpDate.Text = DateTime.Now.AddYears(lcCls._ValidityLength).ToShortDateString();
        }

        private void _LicenseIsValid(clsLicense lc)
        {
           
            if (lc.ExpirationDate > DateTime.Now)
            {
                clsDesign._ShowErrorMessage("This License is still valid\n you cannot renew it yet.");
                EnableOrDisableButtons(false); 
                return; 
            }
            // if license is not valid the issue button will be enabled since we need to renew it 

            EnableOrDisableButtons(true);
            _FinishInitilization(); 
        }

        //this function will disable the license info butoon and renew button if valid license is found
        private void EnableOrDisableButtons(bool Enable = true)
        {
            if (Enable)
            {
                btnRenew.Enabled = true;
            }
            else
            {
                btnRenew.Enabled = false;
                lkLcInfo.Enabled = false;

            }
        }

        private void _InitializeDefaultControls()
        {
           
            lblappDate.Text = DateTime.Now.ToShortDateString();
            lblIssDate.Text = DateTime.Now.ToShortDateString();
            lblAppFees.Text = clsApplicationsTypes.FindAppType(Convert.ToInt32(clsApplicationsTypes.enAppTypes.RenewDL))._AppTypeFees.ToString();
           
            lblUserID.Text = clsCommonThings._MainUser.UserName;
        }

        private void CtrlFindLicenseInfo1_objectFound(DvldBusinessLayer.clsLicense license)
        {
            _OldLicense = license;
            lkLcHis.Enabled = true;
            _LicenseIsValid(license);

          
        }

        private void FrmRenewDLc_Load(object sender, EventArgs e)
        {
            _InitializeDefaultControls(); 
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lkLcHis_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsDrivers Driver = clsDrivers.GetDriverByID(_OldLicense.DriverID);
            clsPersons person = clsPersons.FindPerson(Driver.PersonID);

            FrmLicenseHitstory frmHis = new FrmLicenseHitstory(person);
            frmHis.ShowDialog();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            _NewLicense = _CreateRenewalLicense(); 

            if (_NewLicense.Save())
            {
                clsDesign._ShowSuccessMessage("Renewed License Has been added successfully :-)");
                _UpdateCtrlsAndUpdateAcitivity(_NewLicense); 
            }
            else
            {
                clsDesign._ShowErrorMessage("Failed to Save The renewal license XXX ");
            }
        }

        private void lkLcInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmLicenseInfo LcInfo = new FrmLicenseInfo(_NewLicense);
            LcInfo.ShowDialog(); 
        }
    }
}
