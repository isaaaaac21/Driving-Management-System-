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

namespace DVLD_Driving_License_Managemet.Applications.InternationalDriving_License
{
    public partial class FrmInterLicenseApp : Form
    {
        public FrmInterLicenseApp()
        {
            InitializeComponent();
            clsDesign.ApplyRoundedCorners(this, 20); 
            ctrlFindLicenseInfo1.objectFound += CtrlFindLicenseInfo1_objectFound;
        }

        private clsLicense _CurrLicense;
        private clsInterDL _CurrInterDl; 


        private void CtrlFindLicenseInfo1_objectFound(clsLicense license)
        {
            _CurrLicense = license;
            _FinishInitializing(); 
        }

        private void _FinishInitializing()
        {
            lblLocLcID.Text = _CurrLicense.LicenseID.ToString();
            lkLcHis.Enabled = true;
            btnIssue.Enabled = true; 
        }


        private clsApplication CreateApplication()
        {
            clsApplication app = new clsApplication();

            app._ApplicantPersonID = clsPersons.FindPerson(clsDrivers.GetDriverByID(_CurrLicense.DriverID).PersonID).PersonID; 
            app._ApplicationDate = DateTime.Now;
            app._ApplicationTypeID = Convert.ToInt32(clsApplicationsTypes.enAppTypes.NewInterDL);  
            app._ApplicationStatus = clsApplication.STATUS_COMPLETED;
            app._LastStatusDate = DateTime.Now;
            app._PaidFees = clsApplicationsTypes.FindAppType(Convert.ToInt32( clsApplicationsTypes.enAppTypes.NewInterDL))._AppTypeFees;
            app._UserCreatedID = clsCommonThings._MainUser.UserID;

            return app; 
        }
        private int  _CreateApplicationAndSaveItAndReturnItsID()
        {
            clsApplication _interApp = CreateApplication();
            _interApp.Save();
            return _interApp._ApplicationID; 

        }
        
    

        private clsInterDL _CreateInternationalLicense()
        {
            clsInterDL interDL = new clsInterDL();

            interDL.ApplicationID = _CreateApplicationAndSaveItAndReturnItsID();
            interDL.DriverID = _CurrLicense.DriverID;
            interDL.IssuedLocalLicenseID = _CurrLicense.LicenseID;
            interDL.IssueDate = DateTime.Today;
            interDL.ExpirationDate = interDL.IssueDate.AddYears(1);
            interDL.IsActive = true;
            interDL.CreatedByUserID = clsCommonThings._MainUser.UserID;
            return interDL; 
        }

        private void _DisableIssueAndEnableLicenseInfo()
        {
            btnIssue.Enabled = false;
            lkLcInfo.Enabled = true; 
          
        }



        private bool _ValidLicense()
        {
            if (!IsOrdinaryLicense()) return false;
            if (!LicenseIsActive()) return false;
            if (DriverHasInternationalLicense()) return false;
            if (_CurrLicenseIsDead()) return false;

            return true; 
        }


        private void _UpdateCtrls(clsInterDL inDL)
        {
            lblInterLcID.Text = inDL.InterLicenseID.ToString();
            lblAppID.Text = inDL.ApplicationID.ToString();
            _DisableIssueAndEnableLicenseInfo(); 
        }

        private bool LicenseIsActive()
        {
            if (!_CurrLicense.isActive)
            {
                clsDesign._ShowErrorMessage("You Cannot create an international license from this license since it is not active !! \n" +
                    "Please ReActivate it and try again :-(");

                return false;
            }
            return true;
        }
        private bool _CurrLicenseIsDead()
        {
            if (DateTime.Now > _CurrLicense.ExpirationDate)
            {
                clsDesign._ShowErrorMessage("This License is Dead !!!\nPlease renew it and try again.");
                return true; 
            }
            return false; 
        }
        private bool DriverHasInternationalLicense()
        {
            if (clsInterDL.DriverHasInternationalLicense(_CurrLicense.DriverID))
            {
                clsDesign._ShowErrorMessage("This Driver already has an international Driving License !!!");

                return true; 
            }
            return false; 
        }
        private bool IsOrdinaryLicense()
        {
            if (_CurrLicense.LicenseClassID != 3)
            {
                clsDesign._ShowErrorMessage("You Cannot create an international license from this license since it is not Ordinary License !!");
                return false; 
            }
            return true; 
        }

        private void _InitializeDefaultControls()
        {
            lblappDate.Text = DateTime.Now.ToShortDateString();
            lblIssDate.Text = DateTime.Now.ToShortDateString();
            lblFees.Text = clsApplicationsTypes.FindAppType(Convert.ToInt32(clsApplicationsTypes.enAppTypes.NewInterDL))._AppTypeFees.ToString();
            lblExpDate.Text = DateTime.Now.AddYears(1).ToShortDateString();
            lblUserID.Text = clsCommonThings._MainUser.UserName; 
        }
        private void FrmInterLicense_Load(object sender, EventArgs e)
        {
            _InitializeDefaultControls(); 
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void lkLcHis_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsDrivers Driver = clsDrivers.GetDriverByID(_CurrLicense.DriverID);
            clsPersons person = clsPersons.FindPerson(Driver.PersonID);

            FrmLicenseHitstory frmHis = new FrmLicenseHitstory(person);
            frmHis.ShowDialog(); 
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (!_ValidLicense()) return; 
 
            _CurrInterDl = _CreateInternationalLicense(); 

            if (_CurrInterDl.Save())
            {
                clsDesign._ShowSuccessMessage("New International License Has been added successfully :-)");
                _UpdateCtrls(_CurrInterDl); 
            }
            else
            {
                clsDesign._ShowErrorMessage("Failed to Save new InternationalLicense XXX "); 
            }
        }

        private void lkLcInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmInterLcInfo frmInterInfo = new FrmInterLcInfo(_CurrInterDl);
            frmInterInfo.ShowDialog(); 
        }
    }
}
