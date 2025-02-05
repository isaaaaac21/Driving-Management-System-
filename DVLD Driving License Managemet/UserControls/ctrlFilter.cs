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

namespace DVLD_Driving_License_Managemet
{
    public partial class ctrlFilter : UserControl
    {
        public ctrlFilter( bool Edit = false  ) 
        {
            InitializeComponent();
            cbFilter.SelectedIndex = 0;
            _EditMode = Edit;
            
        }
        public ctrlFilter() : this(false)
        {

        }

        public event EventHandler PersonInfoFilled;
        public event EventHandler NoPersonInfo;

        public clsPersons per = new clsPersons();
        private bool _EditMode = false ; 
        public void OnPersonInfoFilled()
        {
            PersonInfoFilled?.Invoke(this, EventArgs.Empty);
        }
        public void OnNoPersonInfo()
        {
            NoPersonInfo?.Invoke(this, EventArgs.Empty); 
        }


        private void _ShowErrorMessage(string msg)
        {
            MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
        }
        private void _PreventLetters(KeyPressEventArgs e)
        {
            if (cbFilter.SelectedItem == "PersonID")
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }
        private clsPersons FindByID(int ID)
        {

            return clsPersons.FindPerson(ID); 
            
        }
        private clsPersons FindByNo(string No)
        {
            return clsPersons.FindByNo(No.ToUpper()); 
        }
        private void _InitializeCtrlInfoIfPersonExists()
        {
            if (per == null) {
                _ShowErrorMessage("Person Not found");
                OnNoPersonInfo();
            }

            else
                OnPersonInfoFilled();
        
            ctrlInfo1._MapControlsWithData(per);

        }
        public bool _EmptyTextBox()
        {
            return (txtFilter.Text == "");
        }
        private void _SearchForAPerson()
        {
            if (_EmptyTextBox())
            {
                _ShowErrorMessage("Please Write something"); 
                return;
            }


           
            if (cbFilter.SelectedItem == "PersonID") 
                per = FindByID(Convert.ToInt32(txtFilter.Text));
            else 
                per = FindByNo(txtFilter.Text); 

            _InitializeCtrlInfoIfPersonExists(); 
        }

        private void EditGroupBoxFilter()
        {
            txtFilter.Text = per.PersonID.ToString();
            groupBox1.Enabled = false; 
        }
        public void _MapControlsForEditing(int ID)
        {
            per = clsPersons.FindPerson(ID);
            ctrlInfo1._MapControlsWithData(per); 
            EditGroupBoxFilter();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            _SearchForAPerson(); 
        }


        private void txtFilter_KeyPress(object sender, KeyPressEventArgs e)
        {
            _PreventLetters(e);
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilter.Clear(); 
        }

        private void ctrlFilter_Load(object sender, EventArgs e)
        {
            
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmAddEdit frmAdd = new FrmAddEdit();

            frmAdd.PersonAdded += FrmAdd_PersonAdded;
            frmAdd.ShowDialog();

        }

        private void FrmAdd_PersonAdded(clsPersons person)
        {
            per = person;
            _InitializeCtrlInfoIfPersonExists();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
