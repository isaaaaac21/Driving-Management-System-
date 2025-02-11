using DVLD_Driving_License_Managemet.Applications.Driving_License_Apps;
using DVLD_Driving_License_Managemet.Applications.InternationalDriving_License;
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
    public partial class CtrlDriverLicenses : UserControl
    {
        public CtrlDriverLicenses()
        {
            InitializeComponent();
           
        }

        public clsDrivers _Driver { get; set; }
        


        private clsLicense _ReturnLocalClickedRow()
        {
            int ID = -1; 
            if (dgvLocalList.CurrentRow.Index >= 0)
            {
                ID = Convert.ToInt32(dgvLocalList.CurrentRow.Cells["LicenseID"].Value); 
            }
            return clsLicense.GetLicenseByID(ID); 
        }

        private clsInterDL _ReturnInternlClickedRow()
        {
            int ID = -1;
            if (dgvInterList.CurrentRow.Index >= 0)
            {
                ID = Convert.ToInt32(dgvLocalList.CurrentRow.Cells["IntLcID"].Value);
            }
            return clsInterDL.GetInterLcByID(ID); 
        }


        private void _InitializeInterTab()
        {
            dgvInterList.DataSource = clsInterDL.GetInterLicensesOfDriver(_Driver.DriverID);
            lblResult2.Text = dgvInterList.Rows.Count.ToString(); 
        }
        private void _InitializeLocalTab()
        {
            dgvLocalList.DataSource = clsLicense.GetTotalLicensesOfDriver(_Driver.DriverID);
            lblResult.Text = dgvLocalList.Rows.Count.ToString();
        }

        public void _InitializeCtrlsWithData(clsPersons person)
        {
            _Driver = clsDrivers.GetDriverByPersonID(person.PersonID);
            _InitializeLocalTab();
            _InitializeInterTab();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void CtrlDriverLicenses_Load(object sender, EventArgs e)
        {
            
        }

        private void tsmLocalInfo_Click(object sender, EventArgs e)
        {
            clsLicense LocalLc = _ReturnLocalClickedRow();

            FrmLicenseInfo LcInfo = new FrmLicenseInfo(LocalLc);
            LcInfo.ShowDialog(); 
        }

        private void tsmInternInfo_Click(object sender, EventArgs e)
        {
            clsInterDL interDl = _ReturnInternlClickedRow();

            FrmInterLcInfo frmInterInfo = new FrmInterLcInfo(interDl);
            frmInterInfo.ShowDialog(); 

        }
    }
}
