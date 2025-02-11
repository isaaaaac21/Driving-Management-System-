namespace DVLD_Driving_License_Managemet
{
    partial class frmPersonalInfo
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
            this.lblFormMode = new System.Windows.Forms.Label();
            this.btnClose2 = new System.Windows.Forms.Button();
            this.ctrlInfo1 = new DVLD_Driving_License_Managemet.ctrlInfo();
            this.SuspendLayout();
            // 
            // lblFormMode
            // 
            this.lblFormMode.AutoSize = true;
            this.lblFormMode.Font = new System.Drawing.Font("Gadugi", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFormMode.ForeColor = System.Drawing.Color.FloralWhite;
            this.lblFormMode.Location = new System.Drawing.Point(211, 19);
            this.lblFormMode.Name = "lblFormMode";
            this.lblFormMode.Size = new System.Drawing.Size(227, 35);
            this.lblFormMode.TabIndex = 3;
            this.lblFormMode.Text = "PersonalDetails";
            // 
            // btnClose2
            // 
            this.btnClose2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose2.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose2.Location = new System.Drawing.Point(577, 354);
            this.btnClose2.Name = "btnClose2";
            this.btnClose2.Size = new System.Drawing.Size(77, 31);
            this.btnClose2.TabIndex = 66;
            this.btnClose2.Text = "Close";
            this.btnClose2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnClose2.UseVisualStyleBackColor = true;
            this.btnClose2.Click += new System.EventHandler(this.btnClose2_Click);
            // 
            // ctrlInfo1
            // 
            this.ctrlInfo1._person = null;
            this.ctrlInfo1.Location = new System.Drawing.Point(6, 57);
            this.ctrlInfo1.Name = "ctrlInfo1";
            this.ctrlInfo1.Size = new System.Drawing.Size(648, 291);
            this.ctrlInfo1.TabIndex = 67;
            // 
            // frmPersonalInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSalmon;
            this.ClientSize = new System.Drawing.Size(682, 393);
            this.Controls.Add(this.ctrlInfo1);
            this.Controls.Add(this.btnClose2);
            this.Controls.Add(this.lblFormMode);
            this.Name = "frmPersonalInfo";
            this.Text = "frmPersonalInfo";
            this.Load += new System.EventHandler(this.frmPersonalInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblFormMode;
        private System.Windows.Forms.Button btnClose2;
        private ctrlInfo ctrlInfo1;
    }
}