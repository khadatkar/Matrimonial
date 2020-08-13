using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GYMONE.Models.ViewModels
{
    public class CityVM
    {
        public CityVM()
        {

        }
        public CityVM( CityMasterDTO row )
        {
            Id = row.Id;
            CityName = row.CityName;
        }

        
        public int Id { get; set; }

        
        public string CityName { get; set; }


        
        
        public int StateId { get; set; }

        
        public int CountryId { get; set; }

        
        public string State { get; set; }

        
        public string Country { get; set; }

        
        public IEnumerable<StateMasterDTO> ListState { get; set; }
        
        public IEnumerable<CountryMasterDTO> ListCountry { get; set; }

        
        public IEnumerable<MemberRegistrationDTO> members { get; set; }
    }
}