using GYMONE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GYMONE.Repository
{
    interface IReligionMaster
    {
        void InsertReligion(ReligionDTO Religion);
        IEnumerable<ReligionDTO> GetReligions();
        ReligionDTO GetReligionByID(string ReligionID);
        void UpdateReligion(ReligionDTO Religion);
        void DeleteReligion(string ReligionId);
        bool ReligionNameExists(string ReligionName);
    }
}
