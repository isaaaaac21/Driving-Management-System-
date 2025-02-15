﻿using DVLD_Driving_License_Managemet.Applications;
using DVLD_Driving_License_Managemet.Applications.DetainLicense;
using DVLD_Driving_License_Managemet.Applications.Driving_License_Apps;
using DVLD_Driving_License_Managemet.Applications.InternationalDriving_License;
using DVLD_Driving_License_Managemet.Applications.Manage_Applications_Types;
using DVLD_Driving_License_Managemet.Applications.ManageTestTypes;
using DVLD_Driving_License_Managemet.Applications.Renew_Driving_License;
using DVLD_Driving_License_Managemet.Applications.Replacement_for_Lost_Or_damaged;
using DVLD_Driving_License_Managemet.ManageUsers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Driving_License_Managemet
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }


        private void tsbManagePeople_Click(object sender, EventArgs e)
        {
            frmManagePeople frm = new frmManagePeople();

            frm.ShowDialog();
        }

        private void tspUsers_Click(object sender, EventArgs e)
        {
            Form userForm = new frmUsers();
            userForm.ShowDialog();
        }



        private void tmChangePass_Click(object sender, EventArgs e)
        {
            ManageUsers.frmChangePass ChangePass = new ManageUsers.frmChangePass(clsCommonThings._MainUser);
            ChangePass.ShowDialog(); 
        }

        private void tmCurrentUser_Click(object sender, EventArgs e)
        {
            frmUserInfo frmUser = new ManageUsers.frmUserInfo(clsCommonThings._MainUser);
            frmUser.ShowDialog(); 
        }

        private void tmSignOut_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsmMngAppsTypes_Click(object sender, EventArgs e)
        {
            FrmMngAppsTypes mngAppsTypes = new FrmMngAppsTypes();
            mngAppsTypes.ShowDialog(); 
        }

        private void tsmAppTypes_Click(object sender, EventArgs e)
        {
            FrmManageTestTypes TestTypes = new FrmManageTestTypes();
            TestTypes.ShowDialog(); 
        }

        private void localDrivingLicenseApplicationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmNDLA frmNew = new FrmNDLA();
            frmNew.ShowDialog();
        }

        private void btnLDL_Click(object sender, EventArgs e)
        {
            FrmNewDrvLcs ndla = new FrmNewDrvLcs();
            ndla.ShowDialog(); 
        }

        private void tsbDrivers_Click(object sender, EventArgs e)
        {
            FrmDrivers frmDrvs = new FrmDrivers();
            frmDrvs.ShowDialog(); 
        }

        private void btnInterDL_Click(object sender, EventArgs e)
        {
            FrmInterLicenseApp frmInterLc = new FrmInterLicenseApp();
            frmInterLc.ShowDialog(); 
        }

        private void btnInterDLA_Click(object sender, EventArgs e)
        {
            FrmInterLicenses frmInterLicenses = new FrmInterLicenses();
            frmInterLicenses.ShowDialog(); 
        }

        private void toolStripDropDownButton2_Click(object sender, EventArgs e)
        {

        }

        private void renewDrivingLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRenewDLc RenewLc = new FrmRenewDLc();
            RenewLc.ShowDialog(); 
        }

        private void tsmReplace_Click(object sender, EventArgs e)
        {
            FrmReplace frmReplace = new FrmReplace();
            frmReplace.ShowDialog();
        }

        private void tsmDetainLc_Click(object sender, EventArgs e)
        {
            FrmDetainLc FrmDetLc = new FrmDetainLc();
            FrmDetLc.ShowDialog(); 
        }

        private void tsmMngDetLcs_Click(object sender, EventArgs e)
        {
            FrmMngDetLcs frmMngDetains = new FrmMngDetLcs();
            frmMngDetains.ShowDialog();
        }

        private void tsmReleaseLc_Click(object sender, EventArgs e)
        {
            FrmReleaseDetained frmRel = new FrmReleaseDetained();
            frmRel.ShowDialog(); 
        }

        private void tsmReleaseDet_Click(object sender, EventArgs e)
        {
            FrmReleaseDetained frmRelDet = new FrmReleaseDetained();
            frmRelDet.ShowDialog();
        }
    }
}
