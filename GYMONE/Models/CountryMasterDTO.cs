using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace GYMONE.Models
{
    public class CountryMasterDTO
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter Country")]
        [Remote("CountryNameExists", "Country", ErrorMessage = "Country Already Exists ")]
        public string Country { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}