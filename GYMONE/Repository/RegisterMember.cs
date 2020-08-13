using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GYMONE.Models;
using Dapper;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.EntityClient;
namespace GYMONE.Repository
{
    public class RegisterMember : IRegisterMember
    {

        public int InsertMember(MemberRegistrationDTO objMRDTO)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@MemID", objMRDTO.MemID);
                para.Add("@MemberFName", objMRDTO.MemberFName);
                para.Add("@MemberLName", objMRDTO.MemberLName);
                para.Add("@MemberMName", objMRDTO.MemberMName);
                para.Add("@Address", objMRDTO.Address);
                para.Add("@DOB", objMRDTO.DOB);
                para.Add("@Age", objMRDTO.Age);
                para.Add("@Contactno", objMRDTO.Contactno);
                para.Add("@EmailID", objMRDTO.EmailID);
                para.Add("@Gender", objMRDTO.Gender);
                para.Add("@PlantypeID", objMRDTO.PlantypeID);
                para.Add("@WorkouttypeID", objMRDTO.WorkouttypeID);
                para.Add("@Createdby", objMRDTO.Createdby);
                para.Add("@JoiningDate", objMRDTO.JoiningDate);
                para.Add("@ModifiedBy", 0);
                para.Add("@userid", objMRDTO.userid);
                para.Add("@forgenderid", objMRDTO.forgenderid);
                para.Add("@profilecreatedby", objMRDTO.profilecreatedby);
                para.Add("@MemIDOUT", dbType: DbType.Int32, direction: ParameterDirection.Output);
                con.Execute("SprocMemberRegistrationInsertUpdateSingleItem", para, null, 0, CommandType.StoredProcedure);
                int MemID = para.Get<int>("MemIDOUT");
                return MemID;
            }
        }

        public IEnumerable<MemberRegistrationDTO> GetMember()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Db"].ToString()))
            {
                return con.Query<MemberRegistrationDTO>("sprocMemberRegistrationSelectList", null, null, true, 0, commandType: CommandType.StoredProcedure);
            }
        }

        public MemberRegistrationDTO GetMemberbyID(string MemID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@MemID", MemID);
                return con.Query<MemberRegistrationDTO>("sprocMemberRegistrationSelectSingleItem", para, null, true, 0, commandType: CommandType.StoredProcedure).Single(); 
            }
        }

        public int UpdateMember(MemberRegistrationDTO objMRDTO)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@MemID", objMRDTO.MemID);
                para.Add("@MemberFName", objMRDTO.MemberFName);
                para.Add("@MemberLName", objMRDTO.MemberLName);
                para.Add("@MemberMName", objMRDTO.MemberMName);
                para.Add("@Address", objMRDTO.Address);
                para.Add("@DOB", objMRDTO.DOB);
                para.Add("@Age", objMRDTO.Age);
                para.Add("@Contactno", objMRDTO.Contactno);
                para.Add("@EmailID", objMRDTO.EmailID);
                para.Add("@Gender", objMRDTO.Gender);
                para.Add("@PlantypeID", objMRDTO.PlantypeID);
                para.Add("@WorkouttypeID", objMRDTO.WorkouttypeID);
                para.Add("@Createdby", objMRDTO.Createdby);
                para.Add("@JoiningDate", objMRDTO.JoiningDate);

                para.Add("@countryid", objMRDTO.countryid);
                para.Add("@stateid", objMRDTO.stateid);
                para.Add("@cityid", objMRDTO.cityid);
                para.Add("@religionid", objMRDTO.religionid);
                para.Add("@casteid", objMRDTO.casteid);
                para.Add("@subcasteid", objMRDTO.subcasteid);

                para.Add("@Feet", objMRDTO.Feet);
                para.Add("@Inches", objMRDTO.Inches);
                para.Add("@Color", objMRDTO.color);
                para.Add("@Weight", objMRDTO.Weight);
                para.Add("@marritalmemberstatus", objMRDTO.marritalmemberstatus);


                para.Add("@AnyDisability", objMRDTO.AnyDisability);
                para.Add("@fatherstatus", objMRDTO.fatherstatus);
                para.Add("@mothername", objMRDTO.mothername);
                para.Add("@motherstatus", objMRDTO.motherstatus);
                para.Add("@noofbrothers", objMRDTO.noofbrothers);
                para.Add("@noofsisters", objMRDTO.noofsisters);
                para.Add("@marriedbrother", objMRDTO.marriedbrother);
                para.Add("@marriedsisters", objMRDTO.marriedsisters);
                para.Add("@highestqualification", objMRDTO.highestqualification);
                para.Add("@otherqualification", objMRDTO.otherqualification);
                para.Add("@diet", objMRDTO.diet);


                para.Add("@ModifiedBy", 0);
                para.Add("@MemIDOUT", dbType: DbType.Int32, direction: ParameterDirection.Output);
                con.Execute("SprocMemberRegistrationInsertUpdateSingleItem", para, null, 0, CommandType.StoredProcedure);
                int MemID = para.Get<int>("MemIDOUT");
                return MemID;
            }
        }

        public void DeleteMember(string MemID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {

                string Query = "delete from MemberRegistration where MemID =" + MemID;

                var value = con.Query(Query, null, null, true, 0, CommandType.Text);


                //string val = string.Empty;
                //var para = new DynamicParameters();
                //para.Add("@MemID", MemID);
                //val = con.Query<string>("sprocMemberRegistrationDeleteSingleItem", para, null, true, 0, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public string GetAmount(string PlanID, string WorkTypeID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                string val = string.Empty;
                var para = new DynamicParameters();
                para.Add("@PlanID", PlanID);
                para.Add("@SchemeID", WorkTypeID);
                return val = con.Query<string>("Usp_GetAmount_reg", para, null, true, 0, commandType: CommandType.StoredProcedure).SingleOrDefault();
            }
        }

        public int UpdateImage(MemberRegistrationDTO objDTO)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@MemID", objDTO.MemID);
                para.Add("@MemImagename", objDTO.MemImagename);
                para.Add("@MemImagePath", objDTO.MemImagePath);
                para.Add("@MemIDOUT", dbType: DbType.Int32, direction: ParameterDirection.Output);
                con.Execute("spUpdateImg", para, null, 0, CommandType.StoredProcedure);
                int MemID = para.Get<int>("MemIDOUT");
                return MemID;
            }
        }
    }
}