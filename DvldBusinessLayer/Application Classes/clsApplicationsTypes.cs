using DVLDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DvldBusinessLayer
{
    public class clsApplicationsTypes
    {
       public  int _AppTypeID { get; set; } 
       public string _AppTypeTitle { get; set; } 
       public  decimal _AppTypeFees { get; set; }
       
  

        public clsApplicationsTypes()
        {
            _AppTypeID = -1;
            _AppTypeTitle = "";
            _AppTypeFees = 1;
  
        }

        public clsApplicationsTypes(int ID, string Title, decimal Fees)
        {
            _AppTypeID = ID;
            _AppTypeTitle = Title;
            _AppTypeFees = Fees;
        }


        static public DataTable GetAppsTypesList()
        {
            return clsApplicationsTypesDataAccess.GetAppsTypesList(); 
        }

        static public clsApplicationsTypes FindAppType(int ID)
        {
            string title = "";
            decimal fees = 0;
            if (clsApplicationsTypesDataAccess.FindTypeApp(ID, ref title, ref fees))
            {
                return new clsApplicationsTypes(ID, title, fees);
            }
            else return null; 
        }


        private bool _Update()
        {
            return clsApplicationsTypesDataAccess.UpdateApp(this._AppTypeID, this._AppTypeTitle, this._AppTypeFees); 
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
