using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GYMONE.Models
{
    [Table("tblState")]
    public class StateMasterDTO
    {
        [Key]
        public int Id { get; set; }

        [Remote("StatenameExists", "State", ErrorMessage = "State Name Already Exists ")]
        [Required(ErrorMessage = "Enter State Name")]
        public string State { get; set; }

        [NotMapped]
        [Display(Name = "Country")]
        [ValidateCountry_State1(ErrorMessage = "Select Country")]
        public int CountryID { get; set; }

        [NotMapped]
        public string Country { get; set; }

        [NotMapped]
        public IEnumerable<CountryMasterDTO> ListCountry { get; set; }

        public StateMasterDTO()
        {
        }
    }

    public class ValidateCountry_State1 : ValidationAttribute
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