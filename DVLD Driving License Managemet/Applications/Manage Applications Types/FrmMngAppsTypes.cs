using DvldBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Driving_License_Managemet.Applications.Manage_Applications_Types
{
    public partial class FrmMngAppsTypes : Form
    {
        public FrmMngAppsTypes()
        {
            InitializeComponent();
            clsDesign.ApplyRoundedCorners(this); 
            _RefreshForm(); 
        }
        private DataTable _Dt = new DataTable(); 

        private void _RefreshForm()
        {
            dgvList.DataSource = clsApplicationsTypes.GetAppsTypesList();
            lblResult.Text = dgvList.Rows.Count.ToString(); 
        }

        private clsApplicationsTypes _ReturnSelectedRow()
        {
            clsApplicationsTypes appType = new clsApplicationsTypes();
            int ID = 0; 
            if (dgvList.CurrentRow != null)
            {
                 ID = Convert.ToInt32(dgvList.CurrentRow.Cells["ApplicationTypeID"].Value);
            }
            return clsApplicationsTypes.FindAppType(ID);
        }

        private void FrmMngAppsTypes_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmEditAppType_Click(object sender, EventArgs e)
        {
            clsApplicationsTypes app = _ReturnSelectedRow(); 
            FrmUpdateAppTypes UpdateAppFrm = new FrmUpdateAppTypes (app);

            UpdateAppFrm.FormClosed += UpdateAppFrm_FormClosed;
            UpdateAppFrm.ShowDialog();
        }

        private void UpdateAppFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _RefreshForm(); 
        }
    }
}
