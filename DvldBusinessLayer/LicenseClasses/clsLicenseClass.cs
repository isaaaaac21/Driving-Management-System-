using DVLDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldBusinessLayer.Application_Classes
{
    public  class clsLicenseClass
    {
        public int _LicenseClsID { get; set; }
        public string _Name { get; set; }
        public string _Description { get; set; }
        public int _MinAge { get; set; }
        public int _ValidityLength { get; set; }
        public decimal _ClassFess { get; set; }


        

        clsLicenseClass()
        {
            _LicenseClsID = -1;
            _Name = "";
            _Description = "";
            _MinAge = 0;
            _ValidityLength = 0;
            _ClassFess = 0;

        }

        clsLicenseClass(int ID, string Name, string Desc, int MinAge, int Validity, decimal fees)
        {

            _LicenseClsID = ID;
            _Name = Name;
            _Description = Desc;
            _MinAge = MinAge;
            _ValidityLength = Validity;
            _ClassFess = fees;
        }


        public static DataTable GetClassesList()
        {
            return ClsLicenseClassDataAccess.GetLicenseClassesList(); 
        }

        

        public static clsLicenseClass GetLicenseClassByID(int ID)
        {
            int MinAge = 0, Validity = -1;
            string name = "", desc = "";
            decimal classFees = 0;
            if (ClsLicenseClassDataAccess.GetLicenseClassByID(ID, ref name, ref desc, ref MinAge, ref Validity, ref classFees))
            {
                return new clsLicenseClass(ID, name, desc, MinAge, Validity, classFees);

            }
            else return null;

            
        }




    }
}
