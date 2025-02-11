using DVLD_Driving_License_Managemet.Properties;
using DvldBusinessLayer;
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

namespace DVLD_Driving_License_Managemet.UserControls
{
    public partial class ctrlInterDLInfo : UserControl
    {
        public ctrlInterDLInfo()
        {
            InitializeComponent();

           
        }

        private clsInterDL _CurrInterDl; 
        private void _FillPersonData()
        {
            clsDrivers Driver = clsDrivers.GetDriverByID(_CurrInterDl.DriverID);
            clsPersons person = clsPersons.FindPerson(Driver.PersonID);

            lblName.Text = person.FirstName + " " + person.LastName;
            lblNo.Text = person.NationalID;
            lblGen.Text = person.Gender == 0 ? "Male" : "Female";
            lblBirthDate.Text = person.DateOfBirth.ToShortDateString();
            lblDriID.Text = Driver.DriverID.ToString();
            pbPerson.Image = person.ImagePath == "" ? Resources.MaleUser : Image.FromFile(person.ImagePath); 
         }

        private void _FillLicenseData()
        {
            lblInterLcID.Text = _CurrInterDl.InterLicenseID.ToString();
            lblLicenseID.Text = _CurrInterDl.IssuedLocalLicenseID.ToString();
            lblAppID.Text = _CurrInterDl.ApplicationID.ToString();
            lblIssDate.Text = _CurrInterDl.IssueDate.ToShortDateString();
            lblExpDate.Text = _CurrInterDl.ExpirationDate.ToShortDateString();
            lblIsAct.Text = _CurrInterDl.IsActive ? "Yes" : "No";
        }
        public void _MapDataWithInterLicenseInfo(clsInterDL InterDl)
        {
            _CurrInterDl = InterDl;
            _FillPersonData();
            _FillLicenseData(); 
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
