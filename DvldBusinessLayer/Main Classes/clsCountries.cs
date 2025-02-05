using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLDataAccessLayer; 
namespace DvldBusinessLayer
{
    public class clsCountries
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }


        clsCountries()
        {
            CountryID = -1;
            CountryName = ""; 
        }
        clsCountries(int ID, string cn)
        {
            CountryID = ID;
            CountryName = cn; 
        }

       static public DataTable GetCountries()
        {
            return clsCountriesDataAccess.GetCountriesList(); 
        }
        static public clsCountries Find(int ID)
        {
            string countryName = "";
            
            if (clsCountriesDataAccess.FindCountry(ID, ref countryName))
            {
                return new clsCountries(ID, countryName);
            }
            else
            {
                return null;
            }
        }

        static public int GetCountryIDByName(string Name)
        {
            return clsCountriesDataAccess.GetCountryIDByName(Name);
        }

    }
}
