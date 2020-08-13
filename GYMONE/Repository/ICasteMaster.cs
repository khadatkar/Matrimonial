using GYMONE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GYMONE.Repository
{
    interface ICasteMaster
    {
        void InsertCaste(CasteDTO caste);
        IEnumerable<CasteDTO> GetCaste();
        CasteDTO GetCasteByID(string CasteID);
        void UpdateCaste(CasteDTO Caste);
        void DeleteCaste(string CasteID);
        bool CastenameExists(string Castename);
        IEnumerable<CasteDTO> GetCasteByReligionID(string ReligionID);
    }
}
