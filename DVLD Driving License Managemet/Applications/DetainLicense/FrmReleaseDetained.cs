using DvldBusinessLayer.LicenseClasses.Detain_License;
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
using DVLD_Driving_License_Managemet.Applications.Driving_License_Apps;
using DvldBusinessLayer.LicenseClasses.Drivers_Classes;
using DvldBusinessLayer.Application_Classes;

namespace DVLD_Driving_License_Managemet.Applications.DetainLicense
{
    public partial class FrmReleaseDetained : Form
    {
        public FrmReleaseDetained()
        {
            InitializeComponent();
            clsDesign.ApplyRoundedCorners(this, 20); 
            ctrlFindLicenseInfo1.objectFound += CtrlFindLicenseInfo1_objectFound;
        }

        public FrmReleaseDetained(clsLicense license)
        {
            InitializeComponent();
            clsDesign.ApplyRoundedCorners(this, 20);
            _License = license;
            ctrlFindLicenseInfo1.RecievePreparedLicense(license); 
        }



        private clsLicense _License;
        private clsDetainLicense _Detained = new clsDetainLicense();



        private clsApplication CreateApplication()
        {
            clsApplication app = new clsApplication();

            app._ApplicantPersonID = clsPersons.FindPerson(clsDrivers.GetDriverByID(_License.DriverID).PersonID).PersonID;
            app._ApplicationDate = DateTime.Today;
            app._ApplicationTypeID = Convert.ToInt32(clsApplicationsTypes.enAppTypes.ReleaseDetained);
            app._ApplicationStatus = clsApplication.STATUS_COMPLETED;
            app._LastStatusDate = DateTime.Today;
            app._PaidFees = clsApplicationsTypes.FindAppType(Convert.ToInt32(clsApplicationsTypes.enAppTypes.ReleaseDetained))._AppTypeFees;
            app._UserCreatedID = clsCommonThings._MainUser.UserID;

            return app;
        }
        private int _CreateApplicationAndSaveItAndReturnItsID()
        {
            clsApplication _relApp = CreateApplication();
            _relApp.Save();
            return _relApp._ApplicationID;

        }
        private void _ReleaseDetainedLicense()
        {
            _Detained.isReleased = true;
            _Detained.ReleasedDate = DateTime.Today;
            _Detained.ReleaseByUserID = clsCommonThings._MainUser.UserID;
            _Detained.ReleaseApplicationID = _CreateApplicationAndSaveItAndReturnItsID(); 
        }


        private bool _LicenseIsAcitved()
        {

            if (!_License.isActive)
            {
                clsDesign._ShowErrorMessage("This License is no long Active\nYou cannot Release it.");

                return false;
            }

            return true;
        }
        private bool LicenseIsNotDetained()
        {
            if (!clsDetainLicense.LicenseIsDetained(_License.LicenseID))
            {
                clsDesign._ShowErrorMessage("This License is Not detained.");
                return true;
            }
            return false;
        }


        // this function will update the form according to the found license (if it is detained some actions will  happen, if no some other actions will happen
        private void _HandleLicenseStatus()
        {
            _EnableLinkLabels();

            if (LicenseIsNotDetained() || !_LicenseIsAcitved())
            {
                EnableOrDisableReleaseButton(false); 
                return;
            }
            _Detained = clsDetainLicense.GetDetainedLicenseByLcID(_License.LicenseID);
            EnableOrDisableReleaseButton();
            _FinishInitilizing();
        }
        private clsPersons _ExtractPersonFromLicense()
        {
           

            return clsPersons.FindPerson(clsDrivers.GetDriverByID(_License.DriverID).PersonID);

        }
        private void _EnableLinkLabels()
        {
            lkLcHis.Enabled = true;
            lkLcInfo.Enabled = true; 
        }

        private void _UpdateAfterSave()
        {
            lkLcInfo.Enabled = true;
            btnRelease.Enabled = false;
        }
        private void _InitializeDefaultControls()
        {

            lblAppFees.Text = clsApplicationsTypes.FindAppType(Convert.ToInt32(clsApplicationsTypes.enAppTypes.ReleaseDetained))._AppTypeFees.ToString(); 

            lblUserID.Text = clsCommonThings._MainUser.UserName;
        }


        //this function will initialize the detain labels according to the detained license when it is found 
        private void _FinishInitilizing()
        {
            lblLcID.Text = _License.LicenseID.ToString();
            lblDetID.Text = _Detained.DetainID.ToString();
            lblFineFees.Text = _Detained.FineFees.ToString();
            lblDetDate.Text = _Detained.DetainDate.ToShortDateString();
            lblUserID.Text = clsUsers.Find(_Detained.CreatedByUserID).UserName; 
        }
        //if enable is true it means that 
        private void EnableOrDisableReleaseButton(bool Enable = true)
        {

            if (Enable)
            {
                btnRelease.Enabled = true;
            }
            else
            {
                btnRelease.Enabled = false;
            }
        }
        private void CtrlFindLicenseInfo1_objectFound(clsLicense license)
        {
            _License = license; 
            
            _HandleLicenseStatus(); 
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void FrmReleaseDetained_Load(object sender, EventArgs e)
        {
            if (_License == null) // this mean that the _license has not been initialized through constructor so we need to search for it by filter
                _InitializeDefaultControls();
            else // this mean that the second constructor has passed a prepared license so we need to directly handle it  
            {
                _InitializeDefaultControls();
                _HandleLicenseStatus();
            }
        }

        private void lkLcInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmLicenseInfo lcInfo = new FrmLicenseInfo(_License);
            lcInfo.ShowDialog(); 
        }

        private void lkLcHis_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsPersons per = _ExtractPersonFromLicense();
            FrmLicenseHitstory frmHis = new FrmLicenseHitstory(per);
            frmHis.ShowDialog();
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            _ReleaseDetainedLicense(); 
            if (_Detained.Save())
            {
                clsDesign._ShowSuccessMessage("Detained License has been released successfully :-)");
                btnRelease.Enabled = false; 
            }
            else
            {
                clsDesign._ShowErrorMessage("Detained License Has not been released :-("); 
            }
        }
    }
}
