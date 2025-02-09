using DvldBusinessLayer;
using DvldBusinessLayer.Application_Classes;
using DvldBusinessLayer.Test_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Driving_License_Managemet.UserControls
{
    public partial class ctrlLDLAInfo : UserControl
    {
        public ctrlLDLAInfo()
        {
            InitializeComponent();

        }
        clsLocalDrivingLicenseApp _CurrentLDLA = new clsLocalDrivingLicenseApp(); 
        


        public void _MapDataWithLDLA(clsLocalDrivingLicenseApp LDLA)
        {
            _CurrentLDLA = LDLA; 
            lblID.Text = _CurrentLDLA.LDLA_ID.ToString();
            lblLcName.Text = clsLicenseClass.GetLicenseClassByID(_CurrentLDLA.LicenseClassID)._Name;


            // application 
            lblAppID.Text = _CurrentLDLA._ApplicationID.ToString();
            lblAppStatus.Text = _CurrentLDLA._ApplicationStatus == 1 ? "New" : _CurrentLDLA._ApplicationStatus == 2 ? "Completed" : "Canceled"; 
            lblFees.Text = _CurrentLDLA._PaidFees.ToString();
            lblType.Text = clsApplicationsTypes.FindAppType(_CurrentLDLA._ApplicationTypeID)._AppTypeTitle; 
            lblApplicant.Text = clsPersons.FindPerson(_CurrentLDLA._ApplicantPersonID).FirstName;
            lblDate.Text = _CurrentLDLA._ApplicationDate.ToShortDateString();
            lblStattusDate.Text = _CurrentLDLA._LastStatusDate.ToShortDateString();
            lblUser.Text = clsUsers.Find(_CurrentLDLA._UserCreatedID).UserName;

            lblTests.Text = clsTests.GetTotalPassedTests(_CurrentLDLA.LDLA_ID).ToString() + "/3"; 
        }








        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void ctrlDrivingeLicenseInfo_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void linkPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            clsPersons person = clsPersons.FindPerson(_CurrentLDLA._ApplicantPersonID);
            frmPersonalInfo perInfo = new frmPersonalInfo(person);
            perInfo.ShowDialog(); 
        }
    }
}
