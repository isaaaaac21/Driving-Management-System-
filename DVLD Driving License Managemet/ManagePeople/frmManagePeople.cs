using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DvldBusinessLayer; 

namespace DVLD_Driving_License_Managemet
{
    public partial class frmManagePeople : Form
    {
        public frmManagePeople()
        {
            InitializeComponent();
            clsDesign.ApplyRoundedCorners(this);
            this.DoubleBuffered = true;
        }


       

        //Func to allow dragging the form ; 
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


        bool PreventChars = false;
        DataTable dt = new DataTable();
        string Filtering = ""; 











        //private clsPersons _ReturnThedataOfClickedRow()
        //{
        //    /// Here I need to find the person that is being clicked
        //}

        private void _FilterTheDataGrid(string text)
        {

            DataView Dv = dt.DefaultView;
            // if the prevent chars is true this means that we will filter by the ID
            // so the filter must be with = operator (the datacolumn of type int) not like.
            if (PreventChars)
            {
                if (text != "") Dv.RowFilter = $"{Filtering} = {text}";
                else Dv.RowFilter = "";
            }
            else Dv.RowFilter = $"{Filtering} Like '{text}%'";

            lblResult.Text = Dv.Count.ToString(); 
            dgvList.DataSource = Dv; 
        }

        private void _FillTheComboBox()
        {
            foreach(DataColumn clmn in dt.Columns)
            {
                if (clmn.ColumnName != "DateOfBirth" ) cbFilter.Items.Add(clmn.ColumnName);
            }
            cbFilter.SelectedIndex = 0; 
        }
        private void _RefreshTheDataGrid()
        {
            dt = clsPersons.GetPersonsList();
            dgvList.DataSource = dt;
            lblResult.Text = Convert.ToString(dt.Rows.Count);

        }
        private void _InitializeTheFormWithTheData()
        {
            _RefreshTheDataGrid(); 
            _FillTheComboBox();
            txtFilter.Visible = false; 
        }
        
        private clsPersons _ReturnTheSelectedRow()
        {
            clsPersons person = new clsPersons(); 
            if (dgvList.CurrentRow.Index >= 0)
            {
                DataGridViewRow row = dgvList.Rows[dgvList.CurrentRow.Index];

                int Id = Convert.ToInt32(row.Cells["PersonID"].Value);
                person = clsPersons.FindPerson(Id); 
            }
            return person; 
        }


        private void frmManagePeople_Load(object sender, EventArgs e)
        {
            _InitializeTheFormWithTheData(); 
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Clear();
            if (cbFilter.SelectedIndex != 0)
            {
                if (!txtFilter.Visible) txtFilter.Visible = true;
                Filtering = cbFilter.SelectedItem.ToString();
            }
            else
            {
                txtFilter.Visible = false;
                Filtering = "";
            }


            if (cbFilter.SelectedIndex == 1) PreventChars = true;
            else PreventChars = false; 

        }

        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (PreventChars)
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true; 
                }
            }
        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            
            _FilterTheDataGrid(txtFilter.Text);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmAddEdit frmAdd = new FrmAddEdit();

            frmAdd.FormClosed += frmAdd_FormClosed; 

            frmAdd.ShowDialog(); 
        }

        private void frmAdd_FormClosed(object sender, FormClosedEventArgs e)
        {
            _RefreshTheDataGrid();
        }

        private void cmEdit_Click(object sender, EventArgs e)
        {
            clsPersons per = _ReturnTheSelectedRow();
            FrmAddEdit frm = new FrmAddEdit(per);

            frm.FormClosed += frmAdd_FormClosed;

            frm.ShowDialog(); 

        }

        private void cmDelete_Click(object sender, EventArgs e)
        {
            clsPersons DeletedPerson = _ReturnTheSelectedRow();

            if (MessageBox.Show("Are You sure to delete this person ? ", "Stop", MessageBoxButtons.OKCancel, MessageBoxIcon.Hand) == DialogResult.OK)
            {
                if (clsPersons.DeletePerson(DeletedPerson))
                {
                    MessageBox.Show("Delete Operation has been succeded", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _RefreshTheDataGrid();
                }
                else
                {
                    MessageBox.Show("Delete Operation has been Failed", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cmShowDet_Click(object sender, EventArgs e)
        {
            clsPersons person = _ReturnTheSelectedRow(); 
            frmPersonalInfo fpi = new frmPersonalInfo(person);
            fpi.FormClosed += Fpi_FormClosed;
            fpi.ShowDialog();
        }

        private void Fpi_FormClosed(object sender, FormClosedEventArgs e)
        {
            _RefreshTheDataGrid(); 
        }
    }
}
