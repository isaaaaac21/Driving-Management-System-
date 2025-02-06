namespace DVLD_Driving_License_Managemet.Applications.Driving_License_Apps
{
    partial class FrmNDLA
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
            this.components = new System.ComponentModel.Container();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.cbFilter = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this._DebouncingTimer = new System.Windows.Forms.Timer(this.components);
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmShowDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmTestSch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmVisTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmWritingTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmStreetTest = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmIssueNewLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmShowLicense = new System.Windows.Forms.ToolStripMenuItem();
            this.btnClose = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnClose1 = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbStatus
            // 
            this.cbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Items.AddRange(new object[] {
            "All",
            "Completed",
            "Canceled",
            "New"});
            this.cbStatus.Location = new System.Drawing.Point(245, 196);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(75, 21);
            this.cbStatus.TabIndex = 54;
            this.cbStatus.Visible = false;
            this.cbStatus.SelectedIndexChanged += new System.EventHandler(this.cbStatus_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Haettenschweiler", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.LightSkyBlue;
            this.label1.Location = new System.Drawing.Point(262, 142);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(366, 37);
            this.label1.TabIndex = 53;
            this.label1.Text = "New Driving License Applications";
            // 
            // txtFilter
            // 
            this.txtFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilter.Location = new System.Drawing.Point(245, 196);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(123, 21);
            this.txtFilter.TabIndex = 51;
            this.txtFilter.TextChanged += new System.EventHandler(this.txtFilter_TextChanged);
            this.txtFilter.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilter_KeyPress);
            // 
            // cbFilter
            // 
            this.cbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilter.FormattingEnabled = true;
            this.cbFilter.Items.AddRange(new object[] {
            "None"});
            this.cbFilter.Location = new System.Drawing.Point(87, 196);
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(152, 21);
            this.cbFilter.TabIndex = 49;
            this.cbFilter.SelectedIndexChanged += new System.EventHandler(this.cbFilter_SelectedIndexChanged);
            this.cbFilter.SelectionChangeCommitted += new System.EventHandler(this.cbFilter_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Gill Sans MT", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 18);
            this.label3.TabIndex = 48;
            this.label3.Text = "Filter By : ";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Gill Sans MT", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 485);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 18);
            this.label4.TabIndex = 56;
            this.label4.Text = "#Result : ";
            // 
            // lblResult
            // 
            this.lblResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Gill Sans MT", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.Location = new System.Drawing.Point(76, 486);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(15, 18);
            this.lblResult.TabIndex = 57;
            this.lblResult.Text = "0";
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvList.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvList.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvList.GridColor = System.Drawing.Color.CadetBlue;
            this.dgvList.Location = new System.Drawing.Point(10, 233);
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(953, 242);
            this.dgvList.TabIndex = 58;
            this.dgvList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellContentClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmShowDetails,
            this.toolStripSeparator1,
            this.tsmEdit,
            this.tsmDelete,
            this.toolStripSeparator2,
            this.tsmCancel,
            this.toolStripSeparator3,
            this.tsmTestSch,
            this.toolStripSeparator4,
            this.tsmIssueNewLicense,
            this.tsmShowLicense});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(273, 316);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(269, 6);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(269, 6);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(269, 6);
            // 
            // _DebouncingTimer
            // 
            this._DebouncingTimer.Interval = 500;
            this._DebouncingTimer.Tick += new System.EventHandler(this._DebouncingTimer_Tick);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(269, 6);
            // 
            // tsmShowDetails
            // 
            this.tsmShowDetails.Font = new System.Drawing.Font("Franklin Gothic Medium", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmShowDetails.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.Details;
            this.tsmShowDetails.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmShowDetails.Name = "tsmShowDetails";
            this.tsmShowDetails.Size = new System.Drawing.Size(272, 38);
            this.tsmShowDetails.Text = "Show Application Details";
            // 
            // tsmEdit
            // 
            this.tsmEdit.Enabled = false;
            this.tsmEdit.Font = new System.Drawing.Font("Franklin Gothic Medium", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmEdit.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.Edit;
            this.tsmEdit.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmEdit.Name = "tsmEdit";
            this.tsmEdit.Size = new System.Drawing.Size(272, 38);
            this.tsmEdit.Text = "Edit Application";
            // 
            // tsmDelete
            // 
            this.tsmDelete.Enabled = false;
            this.tsmDelete.Font = new System.Drawing.Font("Franklin Gothic Medium", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmDelete.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.Delete;
            this.tsmDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmDelete.Name = "tsmDelete";
            this.tsmDelete.Size = new System.Drawing.Size(272, 38);
            this.tsmDelete.Text = "Delete Application";
            this.tsmDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // tsmCancel
            // 
            this.tsmCancel.Enabled = false;
            this.tsmCancel.Font = new System.Drawing.Font("Franklin Gothic Medium", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmCancel.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.Cancel;
            this.tsmCancel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmCancel.Name = "tsmCancel";
            this.tsmCancel.Size = new System.Drawing.Size(272, 38);
            this.tsmCancel.Text = "Cancel Application";
            this.tsmCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tsmTestSch
            // 
            this.tsmTestSch.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmVisTest,
            this.tsmWritingTest,
            this.tsmStreetTest});
            this.tsmTestSch.Enabled = false;
            this.tsmTestSch.Font = new System.Drawing.Font("Franklin Gothic Medium", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmTestSch.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.Test;
            this.tsmTestSch.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmTestSch.Name = "tsmTestSch";
            this.tsmTestSch.Size = new System.Drawing.Size(272, 38);
            this.tsmTestSch.Text = "Schedule Test";
            this.tsmTestSch.MouseEnter += new System.EventHandler(this.tsmTestSch_MouseEnter);
            // 
            // tsmVisTest
            // 
            this.tsmVisTest.Enabled = false;
            this.tsmVisTest.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.eye;
            this.tsmVisTest.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmVisTest.Name = "tsmVisTest";
            this.tsmVisTest.Size = new System.Drawing.Size(203, 30);
            this.tsmVisTest.Text = "Schedule Vision Test";
            this.tsmVisTest.Click += new System.EventHandler(this.HandleTestsButtons);
            // 
            // tsmWritingTest
            // 
            this.tsmWritingTest.Enabled = false;
            this.tsmWritingTest.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.Pen;
            this.tsmWritingTest.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmWritingTest.Name = "tsmWritingTest";
            this.tsmWritingTest.Size = new System.Drawing.Size(203, 30);
            this.tsmWritingTest.Text = "Schedule Writing Test";
            this.tsmWritingTest.Click += new System.EventHandler(this.HandleTestsButtons);
            // 
            // tsmStreetTest
            // 
            this.tsmStreetTest.Enabled = false;
            this.tsmStreetTest.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.Road2;
            this.tsmStreetTest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsmStreetTest.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmStreetTest.Name = "tsmStreetTest";
            this.tsmStreetTest.Size = new System.Drawing.Size(203, 30);
            this.tsmStreetTest.Text = "Schedule Street Test";
            this.tsmStreetTest.Click += new System.EventHandler(this.HandleTestsButtons);
            // 
            // tsmIssueNewLicense
            // 
            this.tsmIssueNewLicense.Enabled = false;
            this.tsmIssueNewLicense.Font = new System.Drawing.Font("Franklin Gothic Medium", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmIssueNewLicense.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.DriverLicenseSmall;
            this.tsmIssueNewLicense.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmIssueNewLicense.Name = "tsmIssueNewLicense";
            this.tsmIssueNewLicense.Size = new System.Drawing.Size(272, 38);
            this.tsmIssueNewLicense.Text = "Issue Driving License (First Time)";
            this.tsmIssueNewLicense.Click += new System.EventHandler(this.issueDrivingToolStripMenuItem_Click);
            // 
            // tsmShowLicense
            // 
            this.tsmShowLicense.Enabled = false;
            this.tsmShowLicense.Font = new System.Drawing.Font("Franklin Gothic Medium", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsmShowLicense.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.DriverImg;
            this.tsmShowLicense.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsmShowLicense.Name = "tsmShowLicense";
            this.tsmShowLicense.Size = new System.Drawing.Size(272, 38);
            this.tsmShowLicense.Text = "Show License";
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.BackgroundImage = global::DVLD_Driving_License_Managemet.Properties.Resources.Closing;
            this.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Location = new System.Drawing.Point(931, 7);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(32, 30);
            this.btnClose.TabIndex = 55;
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.Driver__1_;
            this.pictureBox1.Location = new System.Drawing.Point(255, -10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(373, 166);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 52;
            this.pictureBox1.TabStop = false;
            // 
            // btnClose1
            // 
            this.btnClose1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnClose1.Image = global::DVLD_Driving_License_Managemet.Properties.Resources.RedClose;
            this.btnClose1.Location = new System.Drawing.Point(899, 481);
            this.btnClose1.Name = "btnClose1";
            this.btnClose1.Size = new System.Drawing.Size(64, 26);
            this.btnClose1.TabIndex = 50;
            this.btnClose1.Text = "Close";
            this.btnClose1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose1.UseVisualStyleBackColor = true;
            this.btnClose1.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackgroundImage = global::DVLD_Driving_License_Managemet.Properties.Resources.Adding;
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAdd.Location = new System.Drawing.Point(923, 175);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(40, 39);
            this.btnAdd.TabIndex = 46;
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // FrmNDLA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(975, 514);
            this.Controls.Add(this.dgvList);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtFilter);
            this.Controls.Add(this.btnClose1);
            this.Controls.Add(this.cbFilter);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnAdd);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "FrmNDLA";
            this.Text = "FrmNDLA";
            this.Load += new System.EventHandler(this.FrmNDLA_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Button btnClose1;
        private System.Windows.Forms.ComboBox cbFilter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.Timer _DebouncingTimer;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmShowDetails;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem tsmEdit;
        private System.Windows.Forms.ToolStripMenuItem tsmDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem tsmTestSch;
        private System.Windows.Forms.ToolStripMenuItem tsmVisTest;
        private System.Windows.Forms.ToolStripMenuItem tsmWritingTest;
        private System.Windows.Forms.ToolStripMenuItem tsmStreetTest;
        private System.Windows.Forms.ToolStripMenuItem tsmCancel;
        private System.Windows.Forms.ToolStripMenuItem tsmIssueNewLicense;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem tsmShowLicense;
    }
}