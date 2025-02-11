using DVLD_Driving_License_Managemet.Properties;
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
    public partial class FrmLicenseInfo : Form
    {
        public FrmLicenseInfo(clsLocalDrivingLicenseApp LDLA)
        {
            InitializeComponent();
            clsDesign.ApplyRoundedCorners(this, 5);
            _CurrLicense = clsLicense.GetLicenseByInfoID(LDLA._ApplicantPersonID, LDLA.LicenseClassID);
        }
        public FrmLicenseInfo(clsLicense lc)
        {
            InitializeComponent();
            clsDesign.ApplyRoundedCorners(this, 5);
            _CurrLicense = lc; 
        }













        clsLicense _CurrLicense = null; 


        private clsPersons GetPersonByLicense()
        {
            clsDrivers Driver = clsDrivers.GetDriverByID(_CurrLicense.DriverID);

            return clsPersons.FindPerson(Driver.PersonID); 
        }

        private void _InitializePersonData()
        {
            clsPersons person = GetPersonByLicense(); 
            lblName.Text = person.FirstName + " " + person.SecondName + " " + person.LastName;
            lblNo.Text = person.NationalID.ToString();
            lblGen.Text = person.Gender == 0 ? "Male" : "Female";
            lblBirthDate.Text = person.DateOfBirth.ToShortDateString();
            pbPerson.Image = person.ImagePath == "" ? Resources.MaleUser:   Image.FromFile( person.ImagePath); 

        }
        private void _InitializeLicenseData()
        {
            lblLicenseClass.Text = clsLicenseClass.GetLicenseClassByID(_CurrLicense.LicenseClassID)._Name;

            lblLicenseID.Text = _CurrLicense.LicenseID.ToString();


            lblIssDate.Text = _CurrLicense.IssueDate.ToShortDateString();
            lblExpDate.Text = _CurrLicense.ExpirationDate.ToShortDateString();
            lblIssReason.Text = _CurrLicense.IssueReason.ToString();
            lblIsAct.Text = _CurrLicense.isActive ? "Yes" : "No";

            lblDriID.Text = _CurrLicense.DriverID.ToString();
            lblNotes.Text = _CurrLicense.Notes; 
        }
        private void _InitializeCtrlsWithData()
        {
            _InitializePersonData();
            _InitializeLicenseData(); 
             
        }



        private void FrmLicenseInfo_Load(object sender, EventArgs e)
        {
            _InitializeCtrlsWithData(); 
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
    }
}
