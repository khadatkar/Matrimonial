using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using GYMONE.Models;
using System.Configuration;
using System.Data;

namespace GYMONE.Repository
{
    public class CasteMaster : ICasteMaster
    {
        public void InsertCaste(CasteDTO caste)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@Caste", caste.Caste);
                paramater.Add("@ReligionId", caste.ReligionId);
                var value = con.Query<int>("CasteInsertUpdateSingleItem", paramater, null, true, 0, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<CasteDTO> GetCaste()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var ListofCaste = con.Query<CasteDTO>("sprocCasteMasterSelectList", null, null, true, 0, commandType: CommandType.StoredProcedure);
                return ListofCaste;
            }
        }

        public CasteDTO GetCasteByID(string CasteID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@CasteID", CasteID);
                var Caste_list = con.Query<CasteDTO>("sprocCasteMasterSelectSingleItem", paramater, null, true, 0, CommandType.StoredProcedure).Single();
                return Caste_list;
            }
        }

        public void UpdateCaste(CasteDTO Caste)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@Id", Caste.Id);
                paramater.Add("@Caste", Caste.Caste);
                paramater.Add("@ReligionId", Caste.ReligionId);
                var value = con.Query<int>("CasteInsertUpdateSingleItem", paramater, null, true, 0, commandType: CommandType.StoredProcedure);
            }

        }

        public void DeleteCaste(string CasteID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                string query = "delete from tblCaste where Id = " + CasteID;
                //var para = new DynamicParameters();
                //para.Add("@PlanID", PlanID); // Normal Parameters  
                var value = con.Query(query, null, null, true, 0, CommandType.Text);
            }
        }

        public bool CastenameExists(string Castename)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@Castemaster", Castename); // Normal Parameters  
                var value = con.Query<string>("Usp_checkCaste", para, null, true, 0, CommandType.StoredProcedure).First();

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

        public IEnumerable<CasteDTO> GetCasteByReligionID(string ReligionID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@ReligionID", ReligionID);
                var Caste_list = con.Query<CasteDTO>("Usp_GetCasteByReligionID", paramater, null, true, 0, CommandType.StoredProcedure);
                return Caste_list;
            }
        }
    }
}