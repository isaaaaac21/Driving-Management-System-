namespace DVLD_Driving_License_Managemet
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.label1 = new System.Windows.Forms.Label();
            this.btnApp = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripDropDownButton2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsmDLServices = new System.Windows.Forms.ToolStripMenuItem();
            this.btnNewDrivingLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLDL = new System.Windows.Forms.ToolStripMenuItem();
            this.btnInterDL = new System.Windows.Forms.ToolStripMenuItem();
            this.renewDrivingLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmManageApps = new System.Windows.Forms.ToolStripMenuItem();
            this.btnLDLA = new System.Windows.Forms.ToolStripMenuItem();
            this.btnInterDLA = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmDetain = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmMngAppsTypes = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmAppTypes = new System.Windows.Forms.ToolStripMenuItem();
            this.tsbManagePeople = new System.Windows.Forms.ToolStripButton();
            this.tsbDrivers = new System.Windows.Forms.ToolStripButton();
            this.tspUsers = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.tmCurrentUser = new System.Windows.Forms.ToolStripMenuItem();
            this.tmChangePass = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tmSignOut = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Cooper Black", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FloralWhite;
            this.label1.Location = new System.Drawing.Point(125, 278);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(542, 84);
            this.label1.TabIndex = 2;
            this.label1.Text = " Driver and Vehicle Licenses\r\n                Department";
            // 
            // btnApp
            // 
            this.btnApp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnApp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnApp.Name = "btnApp";
            this.btnApp.Size = new System.Drawing.Size(130, 76);
            this.btnApp.Text = "APPLICATION ";
            this.btnApp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnApp.ToolTipText = "HELEEPP625";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(15, 15);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripDropDownButton2,
            this.tsbManagePeople,
            this.tsbDrivers,
            this.tspUsers,
            this.toolStripDropDownButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(831, 76);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripDropDownButton2
            // 
            this.toolStripDropDownButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmDLServices,
            this.toolStripSeparator3,
            this.tsmManageApps,
            this.toolStripSeparator2,
            this.tsmDetain,
            this.toolStripSeparator4,
            this.tsmMngAppsTypes,
            this.toolStripSeparator5,
            this.tsmAppTypes});
            this.toolStripDropDownButton2.Font = new System.Drawing.Font("Franklin Gothic Demi Cond", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripDropDownButton2.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.index;
            this.toolStripDropDownButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripDropDownButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton2.Name = "toolStripDropDownButton2";
            this.toolStripDropDownButton2.Size = new System.Drawing.Size(147, 73);
            this.toolStripDropDownButton2.Text = "  Applications     ";
            this.toolStripDropDownButton2.Click += new System.EventHandler(this.toolStripDropDownButton2_Click);
            // 
            // tsmDLServices
            // 
            this.tsmDLServices.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnNewDrivingLicense,
            this.renewDrivingLicenseToolStripMenuItem,
            this.toolStripSeparator6,
            this.tsmReplace});
            this.tsmDLServices.Font = new System.Drawing.Font("Franklin Gothic Medium", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmDLServices.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.DriverLicense;
            this.tsmDLServices.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmDLServices.Name = "tsmDLServices";
            this.tsmDLServices.Size = new System.Drawing.Size(312, 86);
            this.tsmDLServices.Text = "Driving License Services";
            // 
            // btnNewDrivingLicense
            // 
            this.btnNewDrivingLicense.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLDL,
            this.btnInterDL});
            this.btnNewDrivingLicense.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.Adding__2_;
            this.btnNewDrivingLicense.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNewDrivingLicense.Name = "btnNewDrivingLicense";
            this.btnNewDrivingLicense.Size = new System.Drawing.Size(305, 30);
            this.btnNewDrivingLicense.Text = "New Driving License";
            // 
            // btnLDL
            // 
            this.btnLDL.ForeColor = System.Drawing.Color.Sienna;
            this.btnLDL.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.House2;
            this.btnLDL.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnLDL.Name = "btnLDL";
            this.btnLDL.Size = new System.Drawing.Size(296, 54);
            this.btnLDL.Text = "Local Driving License";
            this.btnLDL.Click += new System.EventHandler(this.btnLDL_Click);
            // 
            // btnInterDL
            // 
            this.btnInterDL.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.btnInterDL.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.earth;
            this.btnInterDL.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnInterDL.Name = "btnInterDL";
            this.btnInterDL.Size = new System.Drawing.Size(296, 54);
            this.btnInterDL.Text = "International Driving License";
            this.btnInterDL.Click += new System.EventHandler(this.btnInterDL_Click);
            // 
            // renewDrivingLicenseToolStripMenuItem
            // 
            this.renewDrivingLicenseToolStripMenuItem.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.RenewLc;
            this.renewDrivingLicenseToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.renewDrivingLicenseToolStripMenuItem.Name = "renewDrivingLicenseToolStripMenuItem";
            this.renewDrivingLicenseToolStripMenuItem.Size = new System.Drawing.Size(305, 30);
            this.renewDrivingLicenseToolStripMenuItem.Text = "Renew Driving License";
            this.renewDrivingLicenseToolStripMenuItem.Click += new System.EventHandler(this.renewDrivingLicenseToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(302, 6);
            // 
            // tsmReplace
            // 
            this.tsmReplace.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.replace;
            this.tsmReplace.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmReplace.Name = "tsmReplace";
            this.tsmReplace.Size = new System.Drawing.Size(305, 30);
            this.tsmReplace.Text = "Replacement for lost or Damaged";
            this.tsmReplace.Click += new System.EventHandler(this.tsmReplace_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(309, 6);
            // 
            // tsmManageApps
            // 
            this.tsmManageApps.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnLDLA,
            this.btnInterDLA});
            this.tsmManageApps.Font = new System.Drawing.Font("Franklin Gothic Medium", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmManageApps.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.mngApps;
            this.tsmManageApps.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmManageApps.Name = "tsmManageApps";
            this.tsmManageApps.Size = new System.Drawing.Size(312, 86);
            this.tsmManageApps.Text = "Manage Applications";
            // 
            // btnLDLA
            // 
            this.btnLDLA.Font = new System.Drawing.Font("Franklin Gothic Medium", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLDLA.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.LocalLc;
            this.btnLDLA.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnLDLA.Name = "btnLDLA";
            this.btnLDLA.Size = new System.Drawing.Size(422, 30);
            this.btnLDLA.Text = "Local Driving License applications";
            this.btnLDLA.Click += new System.EventHandler(this.localDrivingLicenseApplicationsToolStripMenuItem_Click);
            // 
            // btnInterDLA
            // 
            this.btnInterDLA.Font = new System.Drawing.Font("Franklin Gothic Medium", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInterDLA.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.InterDL;
            this.btnInterDLA.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnInterDLA.Name = "btnInterDLA";
            this.btnInterDLA.Size = new System.Drawing.Size(422, 30);
            this.btnInterDLA.Text = "International Driving Licnese applications";
            this.btnInterDLA.Click += new System.EventHandler(this.btnInterDLA_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(309, 6);
            // 
            // tsmDetain
            // 
            this.tsmDetain.Font = new System.Drawing.Font("Franklin Gothic Medium", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmDetain.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tsmDetain.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.Detain;
            this.tsmDetain.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmDetain.Name = "tsmDetain";
            this.tsmDetain.Size = new System.Drawing.Size(312, 86);
            this.tsmDetain.Text = "Detain License";
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(309, 6);
            // 
            // tsmMngAppsTypes
            // 
            this.tsmMngAppsTypes.Font = new System.Drawing.Font("Franklin Gothic Medium", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmMngAppsTypes.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.ManageApp;
            this.tsmMngAppsTypes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmMngAppsTypes.Name = "tsmMngAppsTypes";
            this.tsmMngAppsTypes.Size = new System.Drawing.Size(312, 86);
            this.tsmMngAppsTypes.Text = "Manage Application Types";
            this.tsmMngAppsTypes.Click += new System.EventHandler(this.tsmMngAppsTypes_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(309, 6);
            // 
            // tsmAppTypes
            // 
            this.tsmAppTypes.Font = new System.Drawing.Font("Franklin Gothic Medium", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmAppTypes.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.TestManage;
            this.tsmAppTypes.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmAppTypes.Name = "tsmAppTypes";
            this.tsmAppTypes.Size = new System.Drawing.Size(312, 86);
            this.tsmAppTypes.Text = "Manage Test Types";
            this.tsmAppTypes.Click += new System.EventHandler(this.tsmAppTypes_Click);
            // 
            // tsbManagePeople
            // 
            this.tsbManagePeople.Font = new System.Drawing.Font("Franklin Gothic Heavy", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbManagePeople.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.ppl3;
            this.tsbManagePeople.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbManagePeople.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbManagePeople.Name = "tsbManagePeople";
            this.tsbManagePeople.Size = new System.Drawing.Size(182, 73);
            this.tsbManagePeople.Text = "  Manage People     ";
            this.tsbManagePeople.Click += new System.EventHandler(this.tsbManagePeople_Click);
            // 
            // tsbDrivers
            // 
            this.tsbDrivers.Font = new System.Drawing.Font("Franklin Gothic Demi Cond", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsbDrivers.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.Driver;
            this.tsbDrivers.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbDrivers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDrivers.Name = "tsbDrivers";
            this.tsbDrivers.Size = new System.Drawing.Size(132, 73);
            this.tsbDrivers.Text = "    Drivers             ";
            this.tsbDrivers.Click += new System.EventHandler(this.tsbDrivers_Click);
            // 
            // tspUsers
            // 
            this.tspUsers.Font = new System.Drawing.Font("Franklin Gothic Demi Cond", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tspUsers.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.Admins;
            this.tspUsers.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tspUsers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tspUsers.Name = "tspUsers";
            this.tspUsers.Size = new System.Drawing.Size(121, 73);
            this.tspUsers.Text = "   Users             ";
            this.tspUsers.Click += new System.EventHandler(this.tspUsers_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tmCurrentUser,
            this.tmChangePass,
            this.toolStripSeparator1,
            this.tmSignOut});
            this.toolStripDropDownButton1.Font = new System.Drawing.Font("Franklin Gothic Demi Cond", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripDropDownButton1.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.Settings;
            this.toolStripDropDownButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(164, 73);
            this.toolStripDropDownButton1.Text = "  Account Settings  ";
            // 
            // tmCurrentUser
            // 
            this.tmCurrentUser.Font = new System.Drawing.Font("Franklin Gothic Medium", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tmCurrentUser.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.ID;
            this.tmCurrentUser.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tmCurrentUser.Name = "tmCurrentUser";
            this.tmCurrentUser.Size = new System.Drawing.Size(202, 30);
            this.tmCurrentUser.Text = "Current User";
            this.tmCurrentUser.Click += new System.EventHandler(this.tmCurrentUser_Click);
            // 
            // tmChangePass
            // 
            this.tmChangePass.Font = new System.Drawing.Font("Franklin Gothic Medium", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tmChangePass.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.password;
            this.tmChangePass.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tmChangePass.Name = "tmChangePass";
            this.tmChangePass.Size = new System.Drawing.Size(202, 30);
            this.tmChangePass.Text = "Change Password";
            this.tmChangePass.Click += new System.EventHandler(this.tmChangePass_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(199, 6);
            // 
            // tmSignOut
            // 
            this.tmSignOut.Font = new System.Drawing.Font("Franklin Gothic Medium", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tmSignOut.ForeColor = System.Drawing.Color.Tomato;
            this.tmSignOut.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.LogOut;
            this.tmSignOut.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tmSignOut.Name = "tmSignOut";
            this.tmSignOut.Size = new System.Drawing.Size(202, 30);
            this.tmSignOut.Text = "Sign Out";
            this.tmSignOut.Click += new System.EventHandler(this.tmSignOut_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(246, 79);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(300, 196);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(831, 425);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "frmMain";
            this.Text = "Main";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton btnApp;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton tsbManagePeople;
        private System.Windows.Forms.ToolStripButton tspUsers;
        private System.Windows.Forms.ToolStripButton tsbDrivers;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem tmCurrentUser;
        private System.Windows.Forms.ToolStripMenuItem tmChangePass;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tmSignOut;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton2;
        private System.Windows.Forms.ToolStripMenuItem tsmDLServices;
        private System.Windows.Forms.ToolStripMenuItem tsmManageApps;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem tsmDetain;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsmAppTypes;
        private System.Windows.Forms.ToolStripMenuItem tsmMngAppsTypes;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem btnLDLA;
        private System.Windows.Forms.ToolStripMenuItem btnNewDrivingLicense;
        private System.Windows.Forms.ToolStripMenuItem btnLDL;
        private System.Windows.Forms.ToolStripMenuItem btnInterDL;
        private System.Windows.Forms.ToolStripMenuItem btnInterDLA;
        private System.Windows.Forms.ToolStripMenuItem renewDrivingLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem tsmReplace;
    }
}

