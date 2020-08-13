using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GYMONE.Models;

namespace GYMONE.Repository
{
    interface ISubCaste 
    {
        void InsertSubCaste(SubCasteDTO SubCaste);
        IEnumerable<SubCasteDTO> GetSubCaste();
        SubCasteDTO GetSubCasteByID(string SubCasteID);
        void UpdateSubCaste(SubCasteDTO SubCaste);
        void DeleteSubCaste(string SubCasteID);
        bool SubCastenameExists(string SubCastename);
        IEnumerable<SubCasteDTO> GetSubCasteByCasteID(string CasteID);
    }
}
