using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GYMONE.Models;
using System.Web;

namespace GYMONE.Repository
{
    interface ICityMaster
    {
        void InsertCity(CityMasterDTO City); // C
        IEnumerable<CityMasterDTO> GetCity(); // R
        CityMasterDTO GetCityByID(string CityID); // R
        void UpdateCity(CityMasterDTO City); //U
        void DeleteCity(string CityID); //D
        bool CitynameExists(string Cityname);
        IEnumerable<CityMasterDTO> GetCityByStateID(string StateID); //R
        IEnumerable<StateMasterDTO> GetStateByCountryID(string CountryID); // R
    }
}
