using GYMONE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GYMONE.Repository;

namespace GYMONE.Controllers
{
    public class SubCasteController : Controller
    {
        ISubCaste objisubcaste;
        ICasteMaster objicastmaster;
        IReligionMaster objireligionmaster;

        public SubCasteController()
        {
            objisubcaste = new SubCasteMaster();
            objicastmaster = new CasteMaster();
            objireligionmaster = new ReligionMaster();

        }

        // GET: /City/

        public ActionResult Index()
        {
            SubCasteMaster sc = new SubCasteMaster();
            List<SubCasteDTO> lstsubcate = sc.GetSubCaste().ToList();
            //List<SubCasteDTO> lstsubcate = objisubcaste.GetSubCaste().ToList();
            return View(lstsubcate);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SubCasteDTO objsubcaste = new SubCasteDTO();


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

            objsubcaste.ListReligion = listReligion;

            List<CasteDTO> listcaste = new List<CasteDTO>()
                {
                    new CasteDTO{
                    Id = 0, Caste = "Select Caste"
                    }
                };
            foreach (var item in objicastmaster.GetCaste())
            {
                CasteDTO cm = new CasteDTO();
                cm.Id = item.Id;
                cm.Caste = item.Caste;
                listcaste.Add(cm);
            }
            objsubcaste.ListCaste = listcaste;
            return View(objsubcaste);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubCasteDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.ReligionId == 0)
                    {
                        ModelState.AddModelError("ReligionMessage", "Please select Religion");
                        Method(model);
                        return View(model);
                    }
                    else if (model.CasteId == 0)
                    {
                        ModelState.AddModelError("CasteMessage", "Please select Caste");
                        Method(model);
                        return View(model);
                    }
                    else
                    {

                        model.Id = 0;
                        SubCasteDTO obj = new SubCasteDTO();
                        obj.Id = model.Id;
                        obj.ReligionId = model.ReligionId;
                        obj.CasteId = model.CasteId;
                        obj.SubCaste = model.SubCaste;

                        objisubcaste.InsertSubCaste(obj);
                        TempData["notice"] = "Subcaste Added Successfully";
                        return RedirectToAction("Create");

                    }
                }
                else
                {
                    Method(model);
                    return View(model);
                }
            }
            catch
            {
                Method(model);
                return View(model);
            }
        }

        public ActionResult SubCastenameExists(string SubCastename)
        {
            var result = objisubcaste.SubCastenameExists(SubCastename);
            return Json(!result, JsonRequestBehavior.AllowGet);
        }

        public void Method(SubCasteDTO objsubcaste)
        {
            ModelState.Remove("ListReligion");
            ModelState.Remove("ListCaste");
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

            objsubcaste.ListReligion = listReligion;
            List<CasteDTO> listcaste = new List<CasteDTO>()
                {
                    new CasteDTO{
                    Id = 0, Caste= "Select Caste"
                    }
                };

            foreach (var item in objicastmaster.GetCaste())
            {
                CasteDTO cm = new CasteDTO();
                cm.Id = item.Id;
                cm.Caste = item.Caste;
                listcaste.Add(cm);
            }
            objsubcaste.ListCaste = listcaste;
        }

        public JsonResult GetCaste(string ReligionId)
        {
            var Castedata = objicastmaster.GetCasteByReligionID(ReligionId);
            return Json(Castedata, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var Model = objisubcaste.GetSubCasteByID(Convert.ToString(Id));
            EditMethod(Model);
            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SubCasteDTO objsubcaste, FormCollection collection, string actionType)
        {
            if (actionType == "Update")
            {

                try
                {

                    if (ModelState.IsValid)
                    {
                        if (objsubcaste.ReligionId == 0)
                        {
                            ModelState.AddModelError("ReligionMessage", "Please select Religion");
                            Method(objsubcaste);
                            return View(objsubcaste);
                        }
                        else if (objsubcaste.CasteId == 0)
                        {
                            ModelState.AddModelError("CasteMessage", "Please select Caste");
                            Method(objsubcaste);
                            return View(objsubcaste);
                        }
                        else
                        {
                            objisubcaste.UpdateSubCaste(objsubcaste);
                            
                            TempData["notice"] = "SubCaste Updated Successfully";

                            return RedirectToAction("Index");

                        }
                    }
                    else
                    {
                        Method(objsubcaste);
                        return View(objsubcaste);
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

        public void EditMethod(SubCasteDTO objsubcaste)
        {

            ModelState.Remove("ListReligion");
            ModelState.Remove("ListCaste");
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
            objsubcaste.ListReligion = listReligion;
            List<CasteDTO> listcaste = new List<CasteDTO>()
                {
                    new CasteDTO{
                    Id = 0, Caste = "Select Caste"
                    }
                };
            foreach (var item in objicastmaster.GetCaste())
            {
                CasteDTO cm = new CasteDTO();
                cm.Id = item.Id;
                cm.Caste = item.Caste;
                listcaste.Add(cm);
            }
            objsubcaste.ListCaste = listcaste;
        }

        public ActionResult Delete(int id)
        {
            objisubcaste.DeleteSubCaste(Convert.ToString(id));
            return RedirectToAction("Index");
        }

    }
}
