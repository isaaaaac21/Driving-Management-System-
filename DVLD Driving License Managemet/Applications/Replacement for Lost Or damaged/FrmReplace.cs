using DVLD_Driving_License_Managemet.Applications.Driving_License_Apps;
using DvldBusinessLayer.Application_Classes;
using DvldBusinessLayer.LicenseClasses.Drivers_Classes;
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

namespace DVLD_Driving_License_Managemet.Applications.Replacement_for_Lost_Or_damaged
{
    public partial class FrmReplace : Form
    {
        public FrmReplace()
        {
            InitializeComponent();
            ctrlFindLicenseInfo1.objectFound += CtrlFindLicenseInfo1_objectFound;
            clsDesign.ApplyRoundedCorners(this, 20);
        }



        private clsLicense _OldLicense;
        private clsLicense _NewLicense = new clsLicense();






        private void _UpdateLabelsForDamaged()
        {
            lblTitle.Text = "Replace for Damaged License";
            lblAppFees.Text = clsApplicationsTypes.FindAppType(Convert.ToInt32(clsApplicationsTypes.enAppTypes.ReplaceForDmg))._AppTypeFees.ToString(); 
        }
        private void _UpdateLabelsForLost()
        {
            lblTitle.Text = "Replace for Lost License";
            lblAppFees.Text = clsApplicationsTypes.FindAppType(Convert.ToInt32(clsApplicationsTypes.enAppTypes.ReplacForLost))._AppTypeFees.ToString();
        }

        private clsApplication CreateApplication()
        {
            clsApplication app = new clsApplication();

            app._ApplicantPersonID = clsPersons.FindPerson(clsDrivers.GetDriverByID(_OldLicense.DriverID).PersonID).PersonID;
            app._ApplicationDate = DateTime.Now;
            app._ApplicationTypeID = Convert.ToInt32(clsApplicationsTypes.enAppTypes.RenewDL);
            app._ApplicationStatus = clsApplication.STATUS_COMPLETED;
            app._LastStatusDate = DateTime.Now;
            app._PaidFees = clsApplicationsTypes.FindAppType(Convert.ToInt32(
                rbDamaged.Checked? clsApplicationsTypes.enAppTypes.ReplaceForDmg : clsApplicationsTypes.enAppTypes.ReplacForLost))._AppTypeFees;
            app._UserCreatedID = clsCommonThings._MainUser.UserID;

            return app;
        }
        private int _CreateApplicationAndSaveItAndReturnItsID()
        {
            clsApplication _ReplaceApp = CreateApplication();
            _ReplaceApp.Save();
            return _ReplaceApp._ApplicationID;

        }


        private clsLicense _CreateReplacelLicense()
        {
            clsLicense ReplacedLicense = new clsLicense();

            ReplacedLicense.ApplicationID = _CreateApplicationAndSaveItAndReturnItsID();
            ReplacedLicense.DriverID = _OldLicense.DriverID;
            ReplacedLicense.LicenseClassID = _OldLicense.LicenseClassID;
            ReplacedLicense.Notes = txtNotes.Text;
            ReplacedLicense.PaidFees = clsLicenseClass.GetLicenseClassByID(_OldLicense.LicenseClassID)._ClassFess;
            ReplacedLicense.IssueDate = DateTime.Today;
            ReplacedLicense.ExpirationDate = ReplacedLicense.IssueDate.AddYears(clsLicenseClass.GetLicenseClassByID(_OldLicense.LicenseClassID)._ValidityLength);
            ReplacedLicense.isActive = true;
            ReplacedLicense.IssueReason = rbDamaged.Checked ? clsLicense.enIssueReasons.ReplacementForDamaged : clsLicense.enIssueReasons.ReplacementForLost;
            ReplacedLicense.CreatedByUserID = clsCommonThings._MainUser.UserID;
            return ReplacedLicense;
        }
        private void UpdateOldLcActStatus()
        {
            _OldLicense.isActive = false;
            _OldLicense.Save();
        }


        private void _UpdateCtrlsAndUpdateAcitivity(clsLicense RenLc)
        {
            lblRepLcID.Text = RenLc.LicenseID.ToString();
            lblAppID.Text = RenLc.ApplicationID.ToString();
            lkLcInfo.Enabled = true;
            btnReplace.Enabled = false;
            UpdateOldLcActStatus();
        }

        private void _InitilizeRemaininDataIfLicenseFound()
        {
            clsLicenseClass lcCls = clsLicenseClass.GetLicenseClassByID(_OldLicense.LicenseClassID);

            lblLcFees.Text = lcCls._ClassFess.ToString();
            lblExpDate.Text = DateTime.Now.AddYears(lcCls._ValidityLength).ToShortDateString();
        }


        private bool _LicenseIsAcitved()
        {

            if (!_OldLicense.isActive)
            {
                clsDesign._ShowErrorMessage("This License is not Active\nYou cannot replace it.");

                return false;
            }

            return true;
        }

        private bool _LicenseIsValid()
        {

            if (_OldLicense.ExpirationDate < DateTime.Now)
            {
                clsDesign._ShowErrorMessage("This License is not valid\n you need to renew it instead.");

                return false;
            }

            return true; 
        }


        //this function will disable the license info butoon and renew button if valid license is found
        private void EnableOrDisableButtons(bool Enable = true)
        {
            if (Enable)
            {
                btnReplace.Enabled = true;
            }
            else
            {
                btnReplace.Enabled = false;
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
            //here I need to check if valid or active
            if (!_LicenseIsValid() || !_LicenseIsAcitved())
            {
                EnableOrDisableButtons(false);
                return; 
            }

            EnableOrDisableButtons();
            _InitilizeRemaininDataIfLicenseFound(); 


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




        private void btnClose1_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }


      
     

        private void rbDamaged_CheckedChanged(object sender, EventArgs e)
        {
            _UpdateLabelsForDamaged();
        }

        private void rbLost_CheckedChanged(object sender, EventArgs e)
        {
            _UpdateLabelsForLost();
        }

        private void FrmReplace_Load(object sender, EventArgs e)
        {
            _InitializeDefaultControls(); 
        }

        private void lkLcHis_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {

            clsDrivers Driver = clsDrivers.GetDriverByID(_OldLicense.DriverID);
            clsPersons person = clsPersons.FindPerson(Driver.PersonID);

            FrmLicenseHitstory frmHis = new FrmLicenseHitstory(person);
            frmHis.ShowDialog();
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            _NewLicense = _CreateReplacelLicense();

            if (_NewLicense.Save())
            {
                clsDesign._ShowSuccessMessage("Replaced License Has been added successfully :-)");
                _UpdateCtrlsAndUpdateAcitivity(_NewLicense);
            }
            else
            {
                clsDesign._ShowErrorMessage("Failed to Save The renewal license XXX ");
            }
        }

        private void lkLcInfo_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmLicenseInfo LcInfo = new FrmLicenseInfo(_NewLicense);
            LcInfo.ShowDialog();
        }
    }
}
