using GYMONE.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GYMONE.Models
{
    public class Db : DbContext
    {
        public DbSet<UserDTO> Users { get; set; }
        public DbSet<RoleDTO> Roles { get; set; }
        public DbSet<UserRoleDTO> UserRoles { get; set; }
        public DbSet<ReligionDTO> Religions { get; set; }

        public DbSet<CasteDTO> Castes { get; set; }
        public DbSet<SubCasteDTO> SubCastes { get; set; }
        public DbSet<CityMasterDTO> Cities { get; set; }
        public DbSet<StateMasterDTO> States { get; set; }
        public DbSet<CountryMasterDTO> Countries { get; set; }
        
        public DbSet<MemberRegDTO> Members { get; set; }
        public DbSet<qualificationDTO> qualifications { get; set; }

        public DbSet<MessageDTO> Messages { get; set; }
    }
}