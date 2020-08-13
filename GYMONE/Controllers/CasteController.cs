using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GYMONE.Filters;
using GYMONE.Models;
using GYMONE.Repository;

namespace GYMONE.Controllers
{
    //[MyExceptionHandler]
    //[Authorize(Roles = "Admin")]
    public class CasteController : Controller
    {

        IReligionMaster objireligionmaster;
        ICasteMaster objicastemaster;

        public CasteController()
        {
            objireligionmaster = new ReligionMaster();
            objicastemaster = new CasteMaster();
        }

        public ActionResult Index()
        {
            return View(objicastemaster.GetCaste());
        }

        [HttpGet]
        public ActionResult Create()
        {
            CasteDTO objCaste = new CasteDTO();
            List<ReligionDTO> listReligion = new List<ReligionDTO>()
                {
                    new ReligionDTO{
                    Id = 0, Religion = "Select Religion"
                    }
                };

            foreach (var item in objireligionmaster.GetReligions())
            {
                ReligionDTO cm = new ReligionDTO();
                cm.Id = item.Id;
                cm.Religion = item.Religion;
                listReligion.Add(cm);
            }

            objCaste.ListReligion = listReligion;

            return View(objCaste);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CasteDTO objcaste, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    if (objcaste.ReligionId == 0)
                    {
                        ModelState.AddModelError("ReligionMessage", "Please select Religion");
                        Method(objcaste);
                        return View(objcaste);
                    }
                    else
                    {
                        objcaste.Id = 0;
                        objicastemaster.InsertCaste(objcaste);
                        TempData["notice"] = "Caste Added Successfully";
                        return RedirectToAction("Create");

                    }
                }
                else
                {
                    Method(objcaste);
                    return View(objcaste);
                }
            }
            catch
            {
                Method(objcaste);
                return View(objcaste);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //StateMasterDTO model1 = (StateMasterDTO)objStateMaster.GetStateByCountryID(Convert.ToString(id));
            var Model = objicastemaster.GetCasteByID(Convert.ToString(id));
            EditMethod(Model);
            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CasteDTO objcaste, FormCollection collection, string actionType)
        {
            if (actionType == "Update")
            {

                try
                {

                    if (ModelState.IsValid)
                    {
                        if (objcaste.ReligionId == 0)
                        {
                            ModelState.AddModelError("ReligionMessage", "Please select Religion");
                            Method(objcaste);
                            return View(objcaste);
                        }
                        else
                        {
                            objicastemaster.UpdateCaste(objcaste);
                              
                            TempData["notice"] = "Caste Updated Successfully";

                            return RedirectToAction("Index");

                        }
                    }
                    else
                    {
                        Method(objcaste);
                        return View(objcaste);
                    }
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public void Method(CasteDTO objcaste)
        {
            ModelState.Remove("ListReligion");
            List<ReligionDTO> listReligion = new List<ReligionDTO>()
                {
                    new ReligionDTO{
                    Id = 0, Religion = "Select Religion"
                    }
                };

            foreach (var item in objireligionmaster.GetReligions())
            {
                ReligionDTO cm = new ReligionDTO();
                cm.Id = item.Id;
                cm.Religion = item.Religion;
                listReligion.Add(cm);
            }

            objcaste.ListReligion = listReligion;
        }

        public void EditMethod(CasteDTO objcaste)
        {
            ModelState.Remove("ListReligion");
            List<ReligionDTO> listReligion = new List<ReligionDTO>()
                {
                    new ReligionDTO{
                    Id = 0, Religion = "Select Religion"
                    }
                };


            foreach (var item in objireligionmaster.GetReligions())
            {
                ReligionDTO cm = new ReligionDTO();
                cm.Id = item.Id;
                cm.Religion = item.Religion;
                listReligion.Add(cm);
            }

            objcaste.ListReligion = listReligion;
            objcaste.ReligionId = objcaste.ReligionId;
        }

        public ActionResult CastenameExists(string Castename)
        {
            var result = objicastemaster.CastenameExists(Castename);
            return Json(!result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            objicastemaster.DeleteCaste(Convert.ToString(id));
            return RedirectToAction("Index");
        }


    }
}
