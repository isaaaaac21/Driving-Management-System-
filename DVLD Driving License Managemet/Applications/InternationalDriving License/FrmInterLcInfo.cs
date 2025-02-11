using DvldBusinessLayer.LicenseClasses;
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
    public partial class FrmInterLcInfo : Form
    {
        public FrmInterLcInfo(clsInterDL interDl)
        {
            InitializeComponent();
            clsDesign.ApplyRoundedCorners(this, 20); 
            ctrlInterDLInfo1._MapDataWithInterLicenseInfo(interDl); 
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void FrmInterLcInfo_Load(object sender, EventArgs e)
        {

        }
    }
}
