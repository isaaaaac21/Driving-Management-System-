using DvldBusinessLayer;
using DvldBusinessLayer.Application_Classes;
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

namespace DVLD_Driving_License_Managemet.Applications.Driving_License_Apps
{
    public partial class FrmIssueDL : Form
    {
        public FrmIssueDL(clsLocalDrivingLicenseApp LDLA)
        {
            InitializeComponent();
            clsDesign.ApplyRoundedCorners(this, 10);
            _CurrentLDLA = LDLA;
            ctrlDrivingeLicenseInfo1._MapDataWithLDLA(LDLA); 

        }
        private clsLocalDrivingLicenseApp _CurrentLDLA;
        private clsLicense _License = new clsLicense(); 




        private clsDrivers _CreateDriver()
        {
            clsDrivers NewDriver = new clsDrivers();
            NewDriver.PersonID = _CurrentLDLA._ApplicantPersonID;
            NewDriver.CreatedByUserID = clsCommonThings._MainUser.UserID;
            NewDriver.CreatedDate = DateTime.Now;

            return NewDriver; 
        }
        private int CreateDriverAndReturnItsID()
        {
            clsDrivers Driver = _CreateDriver(); 
            if (Driver.Save())
            {
                return Driver.DriverID; 
            }
            return -1; 
        }
        private int GetDriverID()
        {
            int DriverID = clsDrivers.PersonExistsAsDriver(_CurrentLDLA._ApplicantPersonID);

            if (DriverID != -1) return DriverID;

            return CreateDriverAndReturnItsID(); 
            
        }

        private void _CollectDataForNewLicense()
        {
            _License.ApplicationID = _CurrentLDLA._ApplicationID;
            _License.DriverID = GetDriverID();
            _License.LicenseClassID = _CurrentLDLA.LicenseClassID;
            _License.IssueDate = DateTime.Now;
            _License.ExpirationDate = _License.IssueDate.AddYears(clsLicenseClass.GetLicenseClassByID(_CurrentLDLA.LicenseClassID)._ValidityLength);
            _License.Notes = txtNotes.Text;
            _License.PaidFees = _CurrentLDLA._PaidFees;
            _License.isActive = true;
            _License.IssueReason = clsLicense.enIssueReasons.FirstTime;
            _License.CreatedByUserID = clsCommonThings._MainUser.UserID; 
        }

        //this function will update the LDLA Status to completed which indicated that a new license has been issued from this LDLA
        private void _UpdateLDLAStatusToCompletedAndSave()
        {
            _CurrentLDLA._ApplicationStatus = clsApplication.STATUS_COMPLETED;
            _CurrentLDLA._LastStatusDate = DateTime.Now;

            _CurrentLDLA.Save() ; 

        }
        private void _DisableButtonIssue()
        {
            btnIssue.Enabled = false; 
        }






        private void FrmIssueDL_Load(object sender, EventArgs e)
        {

        }

        private void btnClose1_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            _CollectDataForNewLicense(); 

            if (_License.Save())
            {
                clsDesign._ShowSuccessMessage("License Has been Added Succussfully :-)");
                _UpdateLDLAStatusToCompletedAndSave();
                _DisableButtonIssue(); 
            }
            else
            {
                clsDesign._ShowErrorMessage("License has not been Added XXXX "); 
            }
        }
    }
}
