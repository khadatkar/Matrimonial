using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GYMONE.Models;
using System.Web;

namespace GYMONE.Repository
{
    interface IStateMaster
    {
        void InsertState(StateMasterDTO State); // C
        IEnumerable<StateMasterDTO> GetState(); // R
        StateMasterDTO GetStateByID(string StateID); // R
        void UpdateState(StateMasterDTO State); //U
        void DeleteState(string StateID); //D
        bool StatenameExists(string Statename);
        IEnumerable<StateMasterDTO> GetStateByCountryID(string CountryID); // R
    }
}
