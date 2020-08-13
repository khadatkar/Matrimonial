using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GYMONE.Models
{
    [Table("tblCity")]
    public class CityMasterDTO
    {
        [Key]
        public int Id { get; set; }

        [Remote("CitynameExists", "City", ErrorMessage = "City Name Already Exists ")]
        [Required(ErrorMessage = "Enter City Name")]
        public string CityName { get; set; }


        [Display(Name = "State")]
        [ValidateState_City(ErrorMessage = "Select State")]
        public int StateId { get; set; }

        [Display(Name = "Country")]
        [ValidateCountry_State(ErrorMessage = "Select Country")]
        public int CountryId { get; set; }

        [NotMapped]
        public string State { get; set; }

        [NotMapped]
        public string Country { get; set; }

        [NotMapped]
        public IEnumerable<StateMasterDTO> ListState { get; set; }
        [NotMapped]
        public IEnumerable<CountryMasterDTO> ListCountry { get; set; }

        [NotMapped]
        public IEnumerable<MemberRegistrationDTO> members { get; set; }
    }

    public class ValidateCountry_State : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (Convert.ToInt32(value) == 0 || Convert.ToInt32(value) == null)
                return false;
            else
                return true;
        }
    }
    public class ValidateState_City : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (Convert.ToInt32(value) == 0 || Convert.ToInt32(value) == null)
                return false;
            else
                return true;
        }
    }
}