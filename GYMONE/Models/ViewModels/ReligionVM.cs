using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GYMONE.Models.ViewModels
{
    public class ReligionVM
    {
        public ReligionVM()
        {
                
        }

        public ReligionVM(ReligionDTO row)
        {
            Id = row.Id;
            Religion = row.Religion;

        }
        
        public int Id { get; set; }
        public string Religion { get; set; }
    }
}