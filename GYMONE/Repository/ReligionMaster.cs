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
    public class ReligionMaster : IReligionMaster
    {
        public void InsertReligion(ReligionDTO Religion)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();

                para.Add("@Religion", Religion.Religion);

                var value = con.Query<int>("ReligionInsertUpdateSingleItem", para, null, true, 0, CommandType.StoredProcedure);
            }
        }

        public IEnumerable<ReligionDTO> GetReligions()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var Listcountry = con.Query<ReligionDTO>("select * from tblReligion", null, null, true, 0, CommandType.Text).ToList();

                return Listcountry;
            }
        }

        public ReligionDTO GetReligionByID(string ReligionID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                string Query = "select * from tblReligion where Id =" + ReligionID;

                var Scheme_list = con.Query<ReligionDTO>(Query, null, null, true, 0, CommandType.Text).Single();

                return Scheme_list;
            }

        }

        public void UpdateReligion(ReligionDTO Religion)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@Id", Religion.Id); // Normal Parameters  
                para.Add("@Religion", Religion.Religion);
                var value = con.Query<int>("ReligionInsertUpdateSingleItem", para, null, true, 0, CommandType.StoredProcedure);
            }
        }

        public void DeleteReligion(string ReligionId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                string Query = "delete from tblReligion where Id =" + ReligionId;
                 
                var value = con.Query(Query, null, null, true, 0, CommandType.Text);
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public bool ReligionNameExists(string ReligionName)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@Religion", ReligionName); // Normal Parameters  
                var value = con.Query<string>("Usp_checkreligion", para, null, true, 0, CommandType.StoredProcedure).First();

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
    }
}