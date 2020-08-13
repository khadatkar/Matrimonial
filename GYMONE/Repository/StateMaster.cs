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
    public class StateMaster : IStateMaster
    {
        public void InsertState(StateMasterDTO State)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@State", State.State);
                paramater.Add("@CountryId", State.CountryID);
                var value = con.Query<int>("sprocStateMasterInsertUpdateSingleItem", paramater, null, true, 0, commandType: CommandType.StoredProcedure);
            }
        }

        public IEnumerable<StateMasterDTO> GetState()
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var ListofState = con.Query<StateMasterDTO>("sprocStateMasterSelectList", null, null, true, 0, commandType: CommandType.StoredProcedure);
                return ListofState;
            }
        }

        public StateMasterDTO GetStateByID(string StateID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@StateID", StateID);
                var State_list = con.Query<StateMasterDTO>("sprocStateMasterSelectSingleItem", paramater, null, true, 0, CommandType.StoredProcedure).Single();
                return State_list;
            }
        }

        public void UpdateState(StateMasterDTO State)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var paramater = new DynamicParameters();
                paramater.Add("@Id", State.Id);
                paramater.Add("@State", State.State);
                paramater.Add("@CountryId", State.CountryID);
                var value = con.Query<int>("sprocStateMasterInsertUpdateSingleItem", paramater, null, true, 0, commandType: CommandType.StoredProcedure);
            }

        }

        public void DeleteState(string StateID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                string query = "delete from tblState where Id = " + StateID;
                //var para = new DynamicParameters();
                //para.Add("@PlanID", PlanID); // Normal Parameters  
                var value = con.Query(query, null, null, true, 0, CommandType.Text);
            }
        }

        public bool StatenameExists(string Statename)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mystring"].ToString()))
            {
                var para = new DynamicParameters();
                para.Add("@Statemaster", Statename); // Normal Parameters  
                var value = con.Query<string>("Usp_checkState", para, null, true, 0, CommandType.StoredProcedure).First();

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