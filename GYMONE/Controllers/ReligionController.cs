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
    public class ReligionController : Controller
    {
        IReligionMaster objIReligionMaster;
        //
        // GET: /Country/

        public ReligionController()
        {
            objIReligionMaster = new ReligionMaster();
        }

        public ActionResult Index()
        {
            return View(objIReligionMaster.GetReligions());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new ReligionDTO());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReligionDTO objReligionMasterDTO)
        {
            if (ModelState.IsValid)
            {
                objIReligionMaster.InsertReligion(objReligionMasterDTO);
                TempData["Message"] = "Religion Added Successfully.";
                return RedirectToAction("Create");
            }
            else
            {
                ModelState.AddModelError("Error", "Please enter Religion Name ");
            }

            ModelState.Remove("Religion");

            return View(objReligionMasterDTO);
        }

        public ActionResult ReligionNameExists(string Religion)
        {
            var result = objIReligionMaster.ReligionNameExists(Religion);
            return Json(!result, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult Edit(string ID)
        {
            var Model = objIReligionMaster.GetReligionByID(ID);
            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ReligionDTO objReligionMasterDTO)
        {
            objIReligionMaster.UpdateReligion(objReligionMasterDTO);
            TempData["MessageUpdate"] = "Religion Updated Successfully.";
            return RedirectToAction("Details");
        }

        [HttpGet]
        public ActionResult Delete(string ID)
        {
            objIReligionMaster.DeleteReligion(ID);
            return RedirectToAction("Details");
        }

        

    }
}
