using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GYMONE.Models.ViewModels
{
    public class MemberRegistrationVM
    {

        [ForeignKey("cityid")]
        public virtual CityMasterDTO Citys { get; set; }
        [ForeignKey("stateid")]
        public virtual StateMasterDTO States { get; set; }

        public MemberRegistrationVM()
        {

        }

        public MemberRegistrationVM(MemberRegDTO row)
        {
            MemID = row.MemID;
            MemberNo = row.MemberNo;
            countryid = row.countryid;
            stateid = row.stateid;
            cityid = row.cityid;
            religionid = row.religionid;
            casteid = row.casteid;
            subcasteid = row.subcasteid;
            //religionname = row.religionname;
            MemberFName = row.MemberFName;
            MemberLName = row.MemberLName;
            MemberMName = row.MemberMName;
            DOB = row.DOB;
            Age = row.Age;
            Contactno = row.Contactno;
            EmailID = row.EmailID;
            Gender = row.Gender;
            PlantypeID = row.PlantypeID;
            WorkouttypeID = row.WorkouttypeID;
            Createdby = row.Createdby;
            ModifiedBy = row.ModifiedBy;
            MemImagename = row.MemImagename;
            MemImagePath = row.MemImagePath;
            JoiningDate = row.JoiningDate;
            Address = row.Address;
            MainMemberID = row.MainMemberID;

            userid = row.userid;
            forgenderid = row.forgenderid;
            //Amount = row.Amount;




            AnyDisability = row.AnyDisability;
            fatherstatus = row.fatherstatus;
            mothername = row.mothername;
            motherstatus = row.motherstatus;
            noofbrothers = row.noofbrothers;
            noofsisters = row.noofsisters;
            marriedbrother = row.marriedbrother;
            marriedsisters = row.marriedsisters;
            highestqualification = row.highestqualification;
            otherqualification = row.otherqualification;
            diet = row.diet;

            Feet = row.Feet;
            Inches = row.Inches;
            color = row.color;
            Weight = row.Weight;
            marritalmemberstatus = row.marritalmemberstatus;
            formaritalmemberstatus = row.formaritalmemberstatus;
            

        }

        public string state { get; set; }

        public string caste { get; set; }
        public string religion { get; set; }

        public string City { get; set; }
        

        #region Personal Details
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
        #endregion

        public int? Feet { get; set; }
        
        public int? Inches { get; set; }
        
        public string color { get; set; }
        
        public long? Weight { get; set; }

        // Marrital Status
        
        public string marritalmemberstatus { get; set; }

        
        public string formaritalmemberstatus { get; set; }

        


        public long MemID { get; set; }

        public string MemberNo { get; set; }

        public int? countryid { get; set; }
        public int? stateid { get; set; }
        public int? cityid { get; set; }

        public int? religionid { get; set; }
        public int? casteid { get; set; }
        public int? subcasteid { get; set; }

        //public string religionname { get; set; }

        public string MemberFName { get; set; }

        public string MemberLName { get; set; }

        public string MemberMName { get; set; }


        public DateTime? DOB { get; set; }



        public string Age { get; set; }


        public string Contactno { get; set; }


        public string EmailID { get; set; }


        public string Gender { get; set; }


        public int? PlantypeID { get; set; }


        public int? WorkouttypeID { get; set; }


        public long Createdby { get; set; }

        public long ModifiedBy { get; set; }

        public string MemImagename { get; set; }

        public string MemImagePath { get; set; }

        //[DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? JoiningDate { get; set; }


        public string Address { get; set; }

        public long MainMemberID { get; set; }


        public int? forgenderid { get; set; }

        public int? userid { get; set; }

        //public Decimal? Amount { get; set; }


        public IEnumerable<SchemeMasterDTO> ListScheme { get; set; }

        public IEnumerable<PlanMasterDTO> ListPlan { get; set; }

        public IEnumerable<SelectListItem> Listgender { get; set; }


        public IEnumerable<CountryMasterDTO> ListCountry { get; set; }

        [NotMapped]
        public IEnumerable<StateMasterDTO> ListState { get; set; }


        public IEnumerable<CityMasterDTO> ListCity { get; set; }


        public IEnumerable<ReligionDTO> ListReligion { get; set; }


        public IEnumerable<CasteDTO> ListCaste { get; set; }


        public IEnumerable<SubCasteDTO> ListSubCaste { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> ListQualification { get; set; }


        public IEnumerable<string> GalleryImages { get; set; }


        public string FullName { get; set; }


        public string PaymentID { get; set; }

    }
}