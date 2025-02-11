using DVLD_Driving_License_Managemet.Applications.Driving_License_Apps;
using DvldBusinessLayer;
using DvldBusinessLayer.LicenseClasses.Detain_License;
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

namespace DVLD_Driving_License_Managemet.Applications.DetainLicense
{
    public partial class FrmDetainLc : Form
    {
        public FrmDetainLc()
        {
            InitializeComponent();

            clsDesign.ApplyRoundedCorners(this, 20); 

            ctrlFindLicenseInfo1.objectFound += CtrlFindLicenseInfo1_objectFound;
        }

        private clsLicense _License;
        private clsDetainLicense Detained = new clsDetainLicense(); 



        private bool FeesEmpty()
        {
            if (string.IsNullOrWhiteSpace(txtFineFees.Text))
            {
                clsDesign._ShowErrorMessage("Please insert fine fees !!!"); return true; 
            }
            return false; 
        }

        private clsDetainLicense _CreateADetainedLicense()
        {
            clsDetainLicense DetLc = new clsDetainLicense();

            DetLc.LicenseID = _License.LicenseID;
            DetLc.DetainDate = DateTime.Today;
            DetLc.FineFees = Convert.ToDecimal(txtFineFees.Text);
            DetLc.CreatedByUserID = clsCommonThings._MainUser.UserID;

            return DetLc; 
        }

        private void _UpdateAfterSave()
        {
            lblDetID.Text = Detained.DetainID.ToString();
            lkLcInfo.Enabled = true;
            btnDetain.Enabled = false; 
        }
        private void _InitializeDefaultControls()
        {

            lblDetDate.Text = DateTime.Now.ToShortDateString();
            lblUserID.Text = clsCommonThings._MainUser.UserName;
        }

        private void _FinishInitilizing()
        {
            lblLcID.Text = _License.LicenseID.ToString(); 
        }
        //if enable is true it means that 
        private void EnableOrDisableButtons(bool Enable = true)
        {

            if (Enable)
            {
                btnDetain.Enabled = true;
            }
            else
            {
                btnDetain.Enabled = false;
            }
        }

        private bool _LicenseIsAcitved()
        {

            if (!_License.isActive)
            {
                clsDesign._ShowErrorMessage("This License is not Active\nYou cannot Detain it.");

                return false;
            }

            return true;
        }
        private bool LicenseIsDetained()
        {
            if (clsDetainLicense.LicenseIsDetained(_License.LicenseID))
            {
                clsDesign._ShowErrorMessage("This License is already detained...");
                return true; 
            }
            return false; 
        }


        //this function will check if the found license is active or detained and will do certain actions
        private void _HandleLicenseStatus()
        {
            if (LicenseIsDetained() || !_LicenseIsAcitved())
            {
                EnableOrDisableButtons(false); return;
            }
            EnableOrDisableButtons();
            _FinishInitilizing(); 
        }

        private void CtrlFindLicenseInfo1_objectFound(DvldBusinessLayer.clsLicense license)
        {
            _License = license;
            lkLcHis.Enabled = true;
            lkLcInfo.Enabled = true; 
            _HandleLicenseStatus(); 


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void FrmDetainLc_Load(object sender, EventArgs e)
        {
            _InitializeDefaultControls(); 
        }

        private void btnDetain_Click(object sender, EventArgs e)
        {
            if (FeesEmpty()) return;
            Detained = _CreateADetainedLicense(); 


            if (Detained.Save())
            {
                clsDesign._ShowSuccessMessage("Detained License has been added successfully :-)");
                _UpdateAfterSave(); 

            }
            else
            {
                clsDesign._ShowErrorMessage("Failed to add detained license"); 
            }
        }

        private void lkLcInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmLicenseInfo frmLcInfo = new FrmLicenseInfo(_License);
            frmLcInfo.ShowDialog();
        }

        private void lkLcHis_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsDrivers Driver = clsDrivers.GetDriverByID(_License.DriverID);
            clsPersons person = clsPersons.FindPerson(Driver.PersonID);

            FrmLicenseHitstory frmHis = new FrmLicenseHitstory(person);
            frmHis.ShowDialog();
        }
    }
}
