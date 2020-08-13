using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GYMONE.Models
{
    public class Mystring : DbContext 
    {
        public DbSet<MemberRegistrationDTO> members { get; set; }
    }
}