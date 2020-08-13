using Dapper;
using GYMONE.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace GYMONE.Repository
{
    public class SubCasteMaster : ISubCaste
    {
        public void InsertSubCaste(SubCasteDTO SubCaste)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@subcasteName", SubCaste.SubCaste);
                paramater.Add("@CasteId", SubCaste.CasteId);
                paramater.Add("@ReligionId", SubCaste.ReligionId);
                var value = con.Query<int>("subcasteInsertUpdateSingleItem", paramater, null, true, 0, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<SubCasteDTO> GetSubCaste()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var ListofSubCaste = con.Query<SubCasteDTO>("subcasteSelectList", null, null, true, 0, commandType: CommandType.StoredProcedure);
                return ListofSubCaste;
            }
        }

        public SubCasteDTO GetSubCasteByID(string SubCasteID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@subcasteID", SubCasteID);
                var subcaste_list = con.Query<SubCasteDTO>("subcasteSelectSingleItem", paramater, null, true, 0, CommandType.StoredProcedure).Single();
                return subcaste_list;
            }
        }

        public void UpdateSubCaste(SubCasteDTO SubCaste)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@Id", SubCaste.Id);
                paramater.Add("@subcasteName", SubCaste.SubCaste);
                paramater.Add("@CasteId", SubCaste.CasteId);
                paramater.Add("@ReligionId", SubCaste.ReligionId);
                var value = con.Query<int>("subcasteInsertUpdateSingleItem", paramater, null, true, 0, commandType: CommandType.StoredProcedure);
            }
        }

        public void DeleteSubCaste(string SubCasteID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                string query = "delete from tblSubCaste where Id = " + SubCasteID;
                //var para = new DynamicParameters();
                //para.Add("@PlanID", PlanID); // Normal Parameters  
                var value = con.Query(query, null, null, true, 0, CommandType.Text);
            }
        }

        public bool SubCastenameExists(string SubCastename)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@subcastemaster", SubCastename); // Normal Parameters  
                var value = con.Query<string>("Usp_checksubcaste", para, null, true, 0, CommandType.StoredProcedure).First();

                if (value == "1")
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
        }

        public IEnumerable<SubCasteDTO> GetSubCasteByCasteID(string CasteID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@CasteId", CasteID);
                var subcaste_list = con.Query<SubCasteDTO>("Usp_GetsubcasteByCasteID", paramater, null, true, 0, CommandType.StoredProcedure);
                return subcaste_list;
            }
        }
    }
}