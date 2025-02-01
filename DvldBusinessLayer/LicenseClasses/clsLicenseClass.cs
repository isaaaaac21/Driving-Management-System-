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
        public short _MinAge { get; set; }
        public short _ValidityLength { get; set; }
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

        clsLicenseClass(int ID, string Name, string Desc, short MinAge, short Validity, decimal fees)
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








    }
}
