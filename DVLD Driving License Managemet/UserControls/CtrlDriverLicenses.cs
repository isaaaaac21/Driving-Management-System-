using DvldBusinessLayer;
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
    public partial class CtrlDriverLicenses : UserControl
    {
        public CtrlDriverLicenses()
        {
            InitializeComponent();
           
        }

        public clsDrivers _Driver { get; set; }
        
        public void _InitializeCtrlsWithData(clsPersons person)
        {
            _Driver = clsDrivers.GetDriverByPersonID(person.PersonID); 
            dgvLocalList.DataSource = clsLicense.GetTotalLicensesOfDriver(_Driver.DriverID);
            lblResult.Text = dgvLocalList.Rows.Count.ToString(); 
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void CtrlDriverLicenses_Load(object sender, EventArgs e)
        {
            
        }
    }
}
