using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GYMONE.Models
{
    [Table("tblCaste")]
    public class CasteDTO
    {
        [Key]
        public int Id { get; set; }

        [Remote("CastenameExists", "Caste", ErrorMessage = "Caste Name Already Exists ")]
        [Required(ErrorMessage = "Enter Caste")]
        public string Caste { get; set; }


        [Display(Name = "Religion")]
        [ValidateReligion_Caste1(ErrorMessage = "Select Religion")]
        public int ReligionId { get; set; }

        [NotMapped]
        public string Religion { get; set; }

        [NotMapped]
        public IEnumerable<ReligionDTO> ListReligion { get; set; }

        
    }
    public class ValidateReligion_Caste1 : ValidationAttribute
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