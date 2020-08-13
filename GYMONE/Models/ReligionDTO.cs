using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GYMONE.Models
{
    [Table("tblReligion")]
    public class ReligionDTO
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter Religion")]
        [Remote("ReligionNameExists", "Religion", ErrorMessage = "Religion Already Exists ")]
        public string Religion { get; set; }
    }
}