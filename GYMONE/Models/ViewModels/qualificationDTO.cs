using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GYMONE.Models.ViewModels
{
    [Table("tblqualificationmaster")]
    public class qualificationDTO
    {
        [Key]
        public int Id { get; set; }
        public string Qulification { get; set; }
    }
}