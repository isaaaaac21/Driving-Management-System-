using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Driving_License_Managemet.Properties;
using DvldBusinessLayer; 
namespace DVLD_Driving_License_Managemet
{
    public partial class FrmAddEdit : Form
    {
        public FrmAddEdit(clsPersons per = null, bool Add = true)
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            person = per == null ? new clsPersons() : per; 
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



        public bool IsPictureBoxImageDifferent(Image resourceImage, PictureBox pictureBox)
        {
            // Convert the PictureBox image to a byte array
            byte[] pictureBoxImageBytes = ImageToByteArray(pictureBox.Image);

            // Convert the resource image to a byte array
            byte[] resourceImageBytes = ImageToByteArray(resourceImage);

            // Compare the two byte arrays
            return !pictureBoxImageBytes.SequenceEqual(resourceImageBytes);
        }

        private byte[] ImageToByteArray(Image image)
        {
            if (image == null)
                return new byte[0];

            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }







        bool PictureChanged = false; 
        string ImgPath = "";
        string SelectedPath = ""; 
        clsPersons person;

        enum enFrmMode { Add = 0, Edit}; 

        public delegate void SendPersonToForm(clsPersons person);
        public event SendPersonToForm PersonAdded;


        private clsPersons _LoadInputsToAperson()
        {
           

            person.NationalID = txtNationalNo.Text; 
            person.FirstName = txtFn.Text;
            person.SecondName = txtSn.Text;
            person.ThirdName = txtTn.Text;
            person.LastName = txtLn.Text;
            person.DateOfBirth = dtpBirth.Value;
            person.Gender = rdMale.Checked == true ? 0 : 1;
            person.Address = txtAddress.Text; 
            person.CountryID = clsCountries.GetCountryIDByName(cbCountries.SelectedItem.ToString());
            person.Phone = txtPhone.Text;
            person.Email = txtEmail.Text;

            return person; 
        }


        private void _ShowErrorMessage(string Message)
        {
            MessageBox.Show(Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        private void _ShowSuccessMessage(string Message)
        {
            MessageBox.Show(Message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private bool _ValidateForm()
        {
            if (
                txtFn.Text == "" ||
                txtSn.Text == ""        || 
                txtTn.Text == ""        ||
                txtLn.Text == ""        ||
                txtPhone.Text == ""     || 
                txtAddress.Text == "")
            {
                _ShowErrorMessage("Please fill the empty fields"); 
                return false;
            }
            else if (!IsPictureBoxImageDifferent(Resources.MaleUser, pbPerson) ||!IsPictureBoxImageDifferent(Resources.AdduserFemale, pbPerson))
            {
                _ShowErrorMessage("Please upload a picture !!!"); 
                return false; 
            }

            return true; 
        }

        private void _UpdateFilePath()
        {
            if (SelectedPath != "")
            {
                try
                {
                    File.Copy(SelectedPath, person.ImagePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
        }


        //This function is for the Add mode of the form
        private void _FillFormWithAdding()
        {
            dtpBirth.MaxDate = DateTime.Now.AddYears(-18);
            rdMale.Checked = true;
            _FillCountriesInCb(); 
        }
        private void _LoadPersonDataToTheForm()
        {
            lblID.Text = person.PersonID.ToString();
            
            txtFn.Text = person.FirstName;
            txtSn.Text = person.SecondName;
            txtTn.Text = person.ThirdName;
            txtLn.Text = person.LastName;
            txtNationalNo.Text = person.NationalID;
            dtpBirth.Value = person.DateOfBirth;
            if (person.Gender == 0) rdMale.Checked = true;
            else rbFemale.Checked = true;

            txtAddress.Text = person.Address;
            txtPhone.Text = person.Phone;
            txtEmail.Text = person.Email;
            _FillCountriesInCb();
             if (person.ImagePath != "" ) pbPerson.Image = Image.FromFile(person.ImagePath); 
        }


        private void _FillFormWithEditing()
        {
            _LoadPersonDataToTheForm(); 
            lblFormMode.Text = "Edit Person"; 
        }

        private bool CheckIfTheNationalNoExists(string No)
        {
            return clsPersons.isExistsByNo(No); 
        }

        private void _SetTheErrorProviderAccordingToTheTxtBox(TextBox txt, CancelEventArgs e)
        {
            if(txt == txtNationalNo)
            {
                if (CheckIfTheNationalNoExists(txtNationalNo.Text))
                {
                    e.Cancel = true;
                    txtNationalNo.Focus();
                    errorProvider1.SetError(txtNationalNo, "The National No is already exists, please change it");
                }
                else
                {
                    errorProvider1.SetError(txtNationalNo, "");
                }
            }
            else if (txt == txtEmail)
            {
                if (txtEmail.Text.Length != 0 && !txtEmail.Text.Contains("@gmail.com"))
                {
                    e.Cancel = true;
                    txtEmail.Focus();
                    errorProvider1.SetError(txtEmail, "The email must contain (@gmail.com)");
                }
                else
                {
                    errorProvider1.SetError(txtEmail, "");
                }
            }
        }

        private void _HandleRadioPicChange(object sender, EventArgs e)
        {
            RadioButton rd = (RadioButton)sender; 
            if (rd == rbFemale) pbPerson.Image = Resources.AdduserFemale;
            else pbPerson.Image = Resources.MaleUser; 
        }


        private void _FillCountriesInCb()
        {
            DataTable countriesDt = clsCountries.GetCountries(); 
            
            foreach(DataRow row in countriesDt.Rows)
            {
                cbCountries.Items.Add(row["CountryName"]); 
            }

            //selecting the item according the to the person
            if (person == null) cbCountries.SelectedIndex = 0; 
            else
            {
               cbCountries.SelectedIndex = cbCountries.FindString(clsCountries.Find(person.CountryID).CountryName); 
            }

        }

        private void FrmAddEdit_Load(object sender, EventArgs e)
        {
            if (person.PersonID  == -1)
            {
                _FillFormWithAdding(); 
            }
            else
            {
                _FillFormWithEditing();
            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }

        private void txtBoxesValidation(object sender, CancelEventArgs e)
        {
            TextBox txt = (TextBox)sender;

            _SetTheErrorProviderAccordingToTheTxtBox(txt, e); 


        }

        private void lkAddPhoto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ofdImage.InitialDirectory = @"C:\";
            ofdImage.Title = "Choose an Image";
            ofdImage.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp";



            if (ofdImage.ShowDialog() == DialogResult.OK)
            {
                 SelectedPath = ofdImage.FileName;

                string TargetedDirectory = @"D:\F things\Programming\Fundamentals\After course 13\c#\Projects\Driving License Management System project\DVLD Driving License Managemet\PeoplePics"; 

                string fileExtension = Path.GetExtension(SelectedPath);
                string NewFileName = Guid.NewGuid().ToString() + fileExtension;
                person.ImagePath = SelectedPath == "" ? "" : Path.Combine(TargetedDirectory, NewFileName); 


                try
                {
                    // we must not save the person.imagepath cuz it stores the new path where the image will be copied
                    pbPerson.Image = Image.FromFile(SelectedPath);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Error While Copying the file !!", ex.Message); 
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!_ValidateForm()) return; 
            clsPersons person = _LoadInputsToAperson();

            if (person.Save())
            {
                _UpdateFilePath();
                _ShowSuccessMessage("Save operation has been succeded :-)");
                PersonAdded?.Invoke(person);
            }
            else
                _ShowErrorMessage("Failed to Save");
        }
    }
}
