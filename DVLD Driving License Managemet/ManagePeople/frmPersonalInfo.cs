using DvldBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Driving_License_Managemet
{
    public partial class frmPersonalInfo : Form
    {
        public frmPersonalInfo(clsPersons per)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            ctrlInfo1._MapControlsWithData(per);
            clsDesign.ApplyRoundedCorners(this); 

        }
        protected override void WndProc(ref Message m)
        {
            const int WM_NCHITTEST = 0x84;
            const int HTCLIENT = 0x1;
            const int HTCAPTION = 0x2;

            base.WndProc(ref m);

            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)
            {
                m.Result = (IntPtr)HTCAPTION; // Allow dragging
            }
        }
        private void frmPersonalInfo_Load(object sender, EventArgs e)
        {
        }

        private void btnClose2_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void ctrlInfo2_Load(object sender, EventArgs e)
        {

        }
    }
}
