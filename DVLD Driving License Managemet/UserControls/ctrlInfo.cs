using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Driving_License_Managemet.Properties;
using DvldBusinessLayer; 
namespace DVLD_Driving_License_Managemet
{
    public partial class ctrlInfo : UserControl
    {
        public ctrlInfo()
        {
            InitializeComponent();
            
        }
        public clsPersons _person { get; set; }

        private void _RefreshData()
        {
            _person = clsPersons.FindPerson(_person.PersonID);
            _MapControlsWithData(_person); 
        }

        private void _SetControlsToDefault()
        {
            lblID.Text = lblID.Tag.ToString();
            lblName.Text = lblName.Tag.ToString();
            lblNo.Text = lblNo.Tag.ToString();
            lblGen.Text = lblGen.Tag.ToString();
            lblEm.Text = lblEm.Tag.ToString();
            lblAds.Text = lblAds.Tag.ToString();
            lblBirth.Text = lblBirth.Tag.ToString();
            lblPhone.Text = lblPhone.Tag.ToString();
            lblCountry.Text = lblCountry.Tag.ToString();
            pbPerson.Image = Resources.MaleUser;
            btnEdit.Enabled = false;

        }

        public void _MapControlsWithData(clsPersons person)
        {
            _person = person;
            if (person == null)
            {
                _SetControlsToDefault();
                return;
            }
            lblID.Text = person.PersonID.ToString();
            lblNo.Text = person.NationalID;
            lblName.Text = person.FirstName + " " + person.SecondName + " " + person.ThirdName + " " + person.LastName;
            lblNo.Text = person.NationalID;
            lblGen.Text = person.Gender == 0 ? "Male" : "Female";
            lblEm.Text = person.Email;
            lblAds.Text = person.Address;
            lblBirth.Text = person.DateOfBirth.ToShortDateString();
            lblPhone.Text = person.Phone;
            lblCountry.Text = clsCountries.Find(person.CountryID).CountryName;
            if (person.ImagePath != "") pbPerson.Image = Image.FromFile(person.ImagePath);

            btnEdit.Enabled = true; 
        }


        private void gbPersonalInfo_Enter(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
          
                FrmAddEdit frm = new FrmAddEdit(_person, false);

                frm.FormClosed += Frm_FormClosed;
                frm.ShowDialog();
            
        }

        private void Frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _RefreshData();
        }
    }
}
