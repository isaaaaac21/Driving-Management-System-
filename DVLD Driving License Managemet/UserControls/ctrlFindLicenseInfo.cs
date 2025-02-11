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

namespace DVLD_Driving_License_Managemet.UserControls
{
    public partial class ctrlFindLicenseInfo : UserControl
    {
        public ctrlFindLicenseInfo()
        {
            InitializeComponent();
        }

        public delegate void ObjectFoundHandler(clsLicense license);

        public event ObjectFoundHandler objectFound; 

        private clsLicense _CurrLicense  ; 

        private bool _ValidInput()
        {
            if (string.IsNullOrEmpty(txtFilter.Text))
            {
                clsDesign._ShowErrorMessage("Input required: Please enter text in the box");
                return false;
            }
            return true; 
        }
        private void _AllowOnlyDigits(KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void SendLicneseBackToForm(clsLicense License)
        {
            objectFound?.Invoke(License); 
        }
        private void _HandleLicenseObject(clsLicense Lc)
        {
            if (Lc != null)
            {
                _CurrLicense = Lc;
                _InitializeCtrlsWithData(); 
                SendLicneseBackToForm(Lc);
            }
            else

            {
                clsDesign._ShowErrorMessage("License Not found !!!\nPlease Input another ID");
            }
        }
        private void FindLicenseByInput()
        {
            int LicenseID = Convert.ToInt32(txtFilter.Text);
            clsLicense License = clsLicense.GetLicenseByID(LicenseID);

            _HandleLicenseObject(License); 
        }


        private void _InitializePersonData()
        {
            clsDrivers Driver = clsDrivers.GetDriverByID(_CurrLicense.DriverID); 
            clsPersons person = clsPersons.FindPerson(Driver.PersonID);
            lblName.Text = person.FirstName + " " + person.SecondName + " " + person.LastName;
            lblNo.Text = person.NationalID.ToString();
            lblGen.Text = person.Gender == 0 ? "Male" : "Female";
            lblBirthDate.Text = person.DateOfBirth.ToShortDateString();
            pbPerson.Image = person.ImagePath != "" ?  Image.FromFile(person.ImagePath) : Resources.MaleUser; 

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




        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!_ValidInput()) return;
            FindLicenseByInput(); 
        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            _AllowOnlyDigits(e); 
        }

        private void ctrlFindLicenseInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
