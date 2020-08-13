using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GYMONE.Models;

namespace GYMONE.Repository
{
    interface ICountryMaster
    {
        void InsertCountry(CountryMasterDTO Country); // C
        IEnumerable<CountryMasterDTO> GetCountries(); // R
        CountryMasterDTO GetCountryByID(string CountryID); // R
        void UpdateCountry(CountryMasterDTO Country); //U
        void DeleteCountry(string CountryId); //D
        //void Save();
        bool CountryNameExists(string CountryName);
    }
}
