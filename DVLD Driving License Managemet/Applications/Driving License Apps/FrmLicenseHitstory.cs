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

namespace DVLD_Driving_License_Managemet.Applications.Driving_License_Apps
{
    public partial class FrmLicenseHitstory : Form
    {

        //I passed person instead of the license in order to initilize person Info and to search for license by personID
        public FrmLicenseHitstory(clsPersons person)
        {
            InitializeComponent();
            clsDesign.ApplyRoundedCorners(this, 20);
            ctrlInfo1._MapControlsWithData(person);
            ctrlDriverLicenses1._InitializeCtrlsWithData(person); 
        }

        private void FrmLicenseHitstory_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

       
        private void showLicenseInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
