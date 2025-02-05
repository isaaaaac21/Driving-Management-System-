using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using DVLDataAccessLayer;
namespace DvldBusinessLayer
{
    public class clsPersons
    {
        public int PersonID { get; set; }
        public string NationalID { get; set; }
        public string FirstName {get; set;} 
        public string SecondName {get; set;}
        public string ThirdName {get; set;}
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Gender { get; set;  }
        public string Address { get; set; }
        public int CountryID { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; } 

        public string ImagePath { get; set; } 
        
        enum enMode { Add = 0, Update};
         enMode Mode = enMode.Add; 

        public clsPersons()
        {
            PersonID = -1;
            NationalID = "";
            FirstName = "";
            SecondName = "";
            ThirdName = "";
            LastName = "";
            DateOfBirth = DateTime.Now;
            Gender = -1;
            Address = ""; 
            CountryID = -1;
            Phone = "";
            Email = "";
            ImagePath = "";
            Mode = enMode.Add; 
        }

        clsPersons(int ID, string NationalityID, string fn, string sn, string tn, string ln, DateTime db, int gender,
           string ads, int cn, string ph, string Em, string Img)
        {
            PersonID = ID;
            NationalID = NationalityID;
            FirstName = fn;
            SecondName = sn;
            ThirdName = tn;
            LastName = ln;
            DateOfBirth = db;
            Gender = gender;
            Address = ads; 
            CountryID = cn;
            Phone = ph;
            Email = Em;
            ImagePath = Img;
            Mode = enMode.Update;
        }

        static public clsPersons FindPerson(int ID)
        {
            string NID = "";  int gen = -1, cn = -1;
            string fn = "", sn = "", tn = "", ln = "", ph = "", em = "", imgPath = "" , ads = "";
            DateTime db = DateTime.Now;


            if (clsPersonsDataAccess.FindPerson(ID, ref NID, ref fn, ref sn, ref tn, ref ln, ref db, ref gen,
              ref ads,   ref cn, ref ph, ref em, ref imgPath))
            {
                return new clsPersons(ID, NID, fn, sn, tn, ln, db, gen, ads,
                 cn, ph, em, imgPath);
            }
            else return null; 
        }

        private bool _Add()
        {
            this.PersonID = clsPersonsDataAccess.AddNewPerson(this.NationalID, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gender,
              this.Address,   this.CountryID, this.Phone, this.Email, this.ImagePath);
            return (this.PersonID != -1); 
        }

        private bool _Update()
        {
            return clsPersonsDataAccess.UpdatePerson(this.PersonID, this.NationalID, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.DateOfBirth, this.Gender,
              this.Address, this.CountryID, this.Phone, this.Email, this.ImagePath);
        }

        public bool Save()
        {
            switch (Mode)
            {
                case enMode.Add:
                    if (_Add())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    break;
                case enMode.Update: 
                    if (_Update())
                    {
                        return true; 
                    }
                    break; 
            }

            return false;
        }

        // I need to start working on the edit

        static public DataTable GetPersonsList()
        {

            return clsPersonsDataAccess.GetPeopleList(); 
        }


        static public bool isExistsByNo(string No)
        {
            return clsPersonsDataAccess.isExistsByNo(No); 
        }


        static public bool DeletePerson(clsPersons person)
        {
            if (clsPersonsDataAccess.DeletePer(person.PersonID))
            {
                if (!string.IsNullOrEmpty(person.ImagePath))
                {
                    if (File.Exists(person.ImagePath))
                    {
                        File.Delete(person.ImagePath);
                    }
                }
                return true;
            }
            else return false; 
        }

      static  public clsPersons FindByNo(string NO)
        {
            int ID = -1, gen = -1, cn = -1;
            string fn = "", sn = "", tn = "", ln = "", ph = "", em = "", imgPath = "", ads = "";
            DateTime db = DateTime.Now;


            if (clsPersonsDataAccess.FindByNo(ref ID, NO, ref fn, ref sn, ref tn, ref ln, ref db, ref gen,
              ref ads, ref cn, ref ph, ref em, ref imgPath))
            {
                return new clsPersons(ID, NO, fn, sn, tn, ln, db, gen, ads,
                 cn, ph, em, imgPath);
            }
            else return null;
        }
    }
}
