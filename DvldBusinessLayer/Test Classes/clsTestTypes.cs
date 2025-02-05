using DVLDataAccessLayer;
using DVLDataAccessLayer.Application_Data_Access_Classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldBusinessLayer.Application_Classes
{
    public class clsTestTypes
    {
        public int _TestTypeID { get; set; }
        public string _TestTypeTitle { get; set; }
        public string _TestTypeDesc { get; set; }
        public decimal _TestTypeFees { get; set; }

        public clsTestTypes()
        {
            _TestTypeID = -1;
            _TestTypeTitle = "";
            _TestTypeDesc = "";
            _TestTypeFees = 0; 
        }

        clsTestTypes(int ID, string Title, string Desc, decimal fees)
        {
            _TestTypeID = ID;
            _TestTypeTitle = Title ;
            _TestTypeDesc = Desc;
            _TestTypeFees = fees;
        }

        static public int VISION = 1, WRITING = 2, STREET = 3;  
        
        static public clsTestTypes FindTestTypeByID(int ID)
        {
            string Title = "";
            string Desc = "";
            decimal Fees = 0;
            if (clsTestTypesDataAccess.FindTestType(ID, ref Title, ref Desc, ref Fees))
            {
                return new clsTestTypes(ID, Title, Desc, Fees);
            }
            else return null;
        }

        static public DataTable GetTestsTypesList()
        {
            return clsTestTypesDataAccess.GetTestsTypesList(); 
        }

        private bool _Update()
        {
            return clsTestTypesDataAccess.UpdateTest(this._TestTypeID, this._TestTypeTitle, this._TestTypeDesc, this._TestTypeFees);
        }



        public bool Save()
        {
            if (_Update())
            {
                return true;
            }
            return false;
        }
    }
}
