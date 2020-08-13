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
    public class CountryMaster : ICountryMaster
    {
        public void InsertCountry(CountryMasterDTO Country)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@Id", Country.Id); // Normal Parameters  
                para.Add("@Country", Country.Country);

                var value = con.Query<int>("sprocCountryMasterInsertUpdateSingleItem", para, null, true, 0, CommandType.StoredProcedure);
            }
        }

        public IEnumerable<CountryMasterDTO> GetCountries()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var Listcountry = con.Query<CountryMasterDTO>("select * from tblCountry", null, null, true, 0, CommandType.Text).ToList();

                return Listcountry;
            }
        }

        public CountryMasterDTO GetCountryByID(string CountryID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                string Query = "select * from tblCountry where Id =" + CountryID;

                var Scheme_list = con.Query<CountryMasterDTO>(Query, null, null, true, 0, CommandType.Text).Single();

                return Scheme_list;
            }

        }

        public void UpdateCountry(CountryMasterDTO Country)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@Id", Country.Id); // Normal Parameters  
                para.Add("@Country", Country.Country);
                var value = con.Query<int>("sprocCountryMasterInsertUpdateSingleItem", para, null, true, 0, CommandType.StoredProcedure);
            }
        }

        public void DeleteCountry(string CountryId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                string Query = "delete from tblCountry where Id =" + CountryId;
                //var para = new DynamicParameters();
                //para.Add("@Id", CountryId); // Normal Parameters  
                var value = con.Query(Query, null, null, true, 0, CommandType.Text);
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public bool CountryNameExists(string CountryName)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@Country", CountryName); // Normal Parameters  
                var value = con.Query<string>("Usp_checkcountry", para, null, true, 0, CommandType.StoredProcedure).First();

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