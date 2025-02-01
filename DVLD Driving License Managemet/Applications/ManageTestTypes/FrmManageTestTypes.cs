using DvldBusinessLayer;
using DvldBusinessLayer.Application_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Driving_License_Managemet.Applications.ManageTestTypes
{
    public partial class FrmManageTestTypes : Form
    {
        public FrmManageTestTypes()
        {
            InitializeComponent();
            clsDesign.ApplyRoundedCorners(this); 
        }



        DataTable _Dt = new DataTable();
        private void _RefreshForm()
        {
            dgvList.DataSource = clsTestTypes.GetTestsTypesList();
            lblResult.Text = dgvList.Rows.Count.ToString();
        }
        private clsTestTypes _ReturnSelectedRow()
        {
            int ID = 0;
            if (dgvList.CurrentRow != null)
            {
                ID = Convert.ToInt32(dgvList.CurrentRow.Cells["TestTypeID"].Value);
            }
            return clsTestTypes.FindTestTypeByID(ID);
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void FrmManageTestTypes_Load(object sender, EventArgs e)
        {
            _RefreshForm(); 
        }

        private void cmEditAppType_Click(object sender, EventArgs e)
        {
            clsTestTypes Test = _ReturnSelectedRow();

            FrmUpdateTestType upTest = new FrmUpdateTestType(Test);
            upTest.FormClosed += UpTest_FormClosed;
            upTest.ShowDialog();

        }

        private void UpTest_FormClosed(object sender, FormClosedEventArgs e)
        {
            _RefreshForm(); 
        }
    }
}
