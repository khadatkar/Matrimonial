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
    public class CityMaster : ICityMaster
    {
        public void InsertCity(CityMasterDTO City)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@CityName", City.CityName);
                paramater.Add("@StateId", City.StateId);
                paramater.Add("@CountryId", City.CountryId);
                var value = con.Query<int>("sprocCityMasterInsertUpdateSingleItem", paramater, null, true, 0, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<CityMasterDTO> GetCity()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var ListofCity = con.Query<CityMasterDTO>("sprocCityMasterSelectList", null, null, true, 0, commandType: CommandType.StoredProcedure);
                return ListofCity;
            }
        }

        public CityMasterDTO GetCityByID(string CityID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@CityID", CityID);
                var City_list = con.Query<CityMasterDTO>("sprocCityMasterSelectSingleItem", paramater, null, true, 0, CommandType.StoredProcedure).Single();
                return City_list;
            }
        }

        public void UpdateCity(CityMasterDTO City)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@Id", City.Id);
                paramater.Add("@CityName", City.CityName);
                paramater.Add("@StateId", City.StateId);
                paramater.Add("@CountryId", City.CountryId);
                var value = con.Query<int>("sprocCityMasterInsertUpdateSingleItem", paramater, null, true, 0, commandType: CommandType.StoredProcedure);
            }

        }

        public void DeleteCity(string CityID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                string query = "delete from tblCity where Id = " + CityID;
                //var para = new DynamicParameters();
                //para.Add("@PlanID", PlanID); // Normal Parameters  
                var value = con.Query(query, null, null, true, 0, CommandType.Text);
            }
        }

        public bool CitynameExists(string Cityname)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@Citymaster", Cityname); // Normal Parameters  
                var value = con.Query<string>("Usp_checkCity", para, null, true, 0, CommandType.StoredProcedure).First();

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
        public IEnumerable<CityMasterDTO> GetCityByStateID(string StateID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@StateID", StateID);
                var City_list = con.Query<CityMasterDTO>("Usp_GetCityByStateID", paramater, null, true, 0, CommandType.StoredProcedure);
                return City_list;
            }
        }

        public IEnumerable<StateMasterDTO> GetStateByCountryID(string CountryID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@CountryID", CountryID);
                var State_list = con.Query<StateMasterDTO>("Usp_GetStateByCountryID", paramater, null, true, 0, CommandType.StoredProcedure);
                return State_list;
            }
        }
    }
}