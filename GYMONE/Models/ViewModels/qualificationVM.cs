using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GYMONE.Models.ViewModels
{
    public class qualificationVM
    {
        public qualificationVM()
        {

        }

        public qualificationVM(qualificationDTO row)
        {
            Id = row.Id;
            Qulification = row.Qulification;
        }

        public int Id { get; set; }
        [Required]
        public string Qulification { get; set; }
    }
}