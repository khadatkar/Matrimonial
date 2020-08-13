using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GYMONE.Models.ViewModels
{
    [Table("MemberRegistration")]
    public class MemberRegDTO
    {


        [ForeignKey("cityid")]
        public virtual CityMasterDTO Citys { get; set; }
        [ForeignKey("stateid")]
        public virtual StateMasterDTO States { get; set; }


        ////////////////////////////
        public string AnyDisability { get; set; }
        public string fatherstatus { get; set; }
        public string mothername { get; set; }
        public string motherstatus { get; set; }
        public string noofbrothers { get; set; }
        public string noofsisters { get; set; }
        public string marriedbrother { get; set; }
        public string marriedsisters { get; set; }
        public int? highestqualification { get; set; }
        public string otherqualification { get; set; }
        public string diet { get; set; }
        ///////////////////////////



        ////////////////////////////////////////


        public string profilecreatedby { get; set; }

        public int? forgenderid { get; set; }


        ///////////////////// Member Registration //////////////////////////



        //Height Weight color

        [ValidFeetAttribute(ErrorMessage = "Select Feet")]
        public int? Feet { get; set; }
        [ValidInchesAttribute(ErrorMessage = "Select Inches")]
        public int? Inches { get; set; }
        [ValidcolorAttribute(ErrorMessage = "Select Color")]
        public string color { get; set; }
        [ValidWeightAttribute(ErrorMessage = "Select Weight")]
        public long? Weight { get; set; }

        // Marrital Status
        [ValidmarritalmemberstatusAttribute(ErrorMessage = "Select Marrital Status")]
        public string marritalmemberstatus { get; set; }

        [ValidformaritalmemberstatusAttribute(ErrorMessage = "Select For Marrital Status")]
        public string formaritalmemberstatus { get; set; }




        [Key]
        public long MemID { get; set; }
        public string MemberNo { get; set; }


        public int? countryid { get; set; }

        public int? stateid { get; set; }
        public int? cityid { get; set; }
        public int? religionid { get; set; }
        public int? casteid { get; set; }
        public int? subcasteid { get; set; }

        public int? userid { get; set; }


        //public string religionname { get; set; }
        [DisplayName("First Name")]
        [Required(ErrorMessage = "Please enter First Name")]
        public string MemberFName { get; set; }
        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Please enter Last Name")]
        public string MemberLName { get; set; }
        [DisplayName("Middle Name")]
        [Required(ErrorMessage = "Please enter Middle Name")]
        public string MemberMName { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Birth Date")]
        [Required(ErrorMessage = "Please select Birth Date")]
        public DateTime? DOB { get; set; }


        [Required(ErrorMessage = "Please select Birth Date")]
        public string Age { get; set; }

        [Required(ErrorMessage = "Please enter Contactno")]
        public string Contactno { get; set; }

        [Required(ErrorMessage = "Please enter EmailID")]
        public string EmailID { get; set; }

        [validateGender(ErrorMessage = "Please select Gender")]
        public string Gender { get; set; }

        [DisplayName("Plan")]
        [ValidPlanAttribute(ErrorMessage = "Please select Plantype")]
        public int? PlantypeID { get; set; }

        [DisplayName("Workouttype")]
        [ValidWorkouttypeAttribute(ErrorMessage = "Please select Workouttype")]
        public int? WorkouttypeID { get; set; }


        public long Createdby { get; set; }

        public long ModifiedBy { get; set; }

        public string MemImagename { get; set; }

        public string MemImagePath { get; set; }

        [DisplayName("Joining Date")]
        [Required(ErrorMessage = "Please select Joining Date")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? JoiningDate { get; set; }

        [Required(ErrorMessage = "Please enter Address")]
        public string Address { get; set; }
        public long MainMemberID { get; set; }

        //[Required(ErrorMessage = "Amount Cannot be Empty")]
        //public Decimal? Amount { get; set; }

        [NotMapped]
        public IEnumerable<SchemeMasterDTO> ListScheme { get; set; }
        [NotMapped]
        public IEnumerable<PlanMasterDTO> ListPlan { get; set; }
        [NotMapped]
        public IEnumerable<SelectListItem> Listgender { get; set; }

        [NotMapped]
        public IEnumerable<CountryMasterDTO> ListCountry { get; set; }

        [NotMapped]
        public IEnumerable<StateMasterDTO> ListState { get; set; }

        [NotMapped]
        public IEnumerable<CityMasterDTO> ListCity { get; set; }

        [NotMapped]
        public IEnumerable<ReligionDTO> ListReligion { get; set; }

        [NotMapped]
        public IEnumerable<CasteDTO> ListCaste { get; set; }

        [NotMapped]
        public IEnumerable<SubCasteDTO> ListSubCaste { get; set; }


        public IEnumerable<string> GalleryImages { get; set; }

        [NotMapped]
        public string FullName { get; set; }

        [NotMapped]
        public string PaymentID { get; set; }

        public class ValidWorkouttypeAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (Convert.ToInt32(value) == 0)
                    return false;
                else
                    return true;
            }


        }


        public class ValidPlanAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (Convert.ToInt32(value) == 0 || Convert.ToInt32(value) == null)
                    return false;
                else
                    return true;
            }
        }

        public class validateGender : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (Convert.ToInt32(value) == 0 || Convert.ToInt32(value) == null)
                    return false;
                else
                    return true;
            }
        }

        //for feet

        public class ValidFeetAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (Convert.ToInt32(value) == 0 || Convert.ToInt32(value) == null)
                    return false;
                else
                    return true;
            }
        }

        //for inches
        public class ValidInchesAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (Convert.ToInt32(value) == 0 || Convert.ToInt32(value) == null)
                    return false;
                else
                    return true;
            }
        }

        // for color
        public class ValidcolorAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (Convert.ToInt32(value) == 0 || Convert.ToInt32(value) == null)
                    return false;
                else
                    return true;
            }
        }

        // for weight
        public class ValidWeightAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (Convert.ToInt32(value) == 0 || Convert.ToInt32(value) == null)
                    return false;
                else
                    return true;
            }
        }

        // for marital status
        public class ValidmarritalmemberstatusAttribute : ValidationAttribute
        {
            public override bool IsValid(object value)
            {
                if (Convert.ToInt32(value) == 0 || Convert.ToInt32(value) == null)
                    return false;
                else
                    return true;
            }
        }

        // for marital status

        public class ValidformaritalmemberstatusAttribute : ValidationAttribute
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
}