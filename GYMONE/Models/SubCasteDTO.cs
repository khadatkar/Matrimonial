using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GYMONE.Models
{
    [Table("tblSubCaste")]
    public class SubCasteDTO
    {
        [Key]
        public int Id { get; set; }

        [Remote("SubCastenameExists", "SubCaste", ErrorMessage = "SubCaste Name Already Exists ")]
        [Required(ErrorMessage = "Enter SubCaste Name")]
        public string SubCaste { get; set; }


        [Display(Name = "Caste")]
        [ValidateCaste_SubCaste(ErrorMessage = "Select Caste")]
        public int CasteId { get; set; }

        [Display(Name = "Religion")]
        [ValidateReligion_Caste(ErrorMessage = "Select Religion")]
        public int ReligionId { get; set; }

        [NotMapped]
        public string Caste { get; set; }

        [NotMapped]
        public string Religion { get; set; }

        [NotMapped]
        public IEnumerable<CasteDTO> ListCaste { get; set; }
        [NotMapped]
        public IEnumerable<ReligionDTO> ListReligion { get; set; }
    }

    public class ValidateCaste_SubCaste : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (Convert.ToInt32(value) == 0 || Convert.ToInt32(value) == null)
                return false;
            else
                return true;
        }
    }

    public class ValidateReligion_Caste : ValidationAttribute
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