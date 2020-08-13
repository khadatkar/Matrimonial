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

    ////[MyExceptionHandler]
    [Authorize(Roles = "Admin")]
    public class CountryController : Controller
    {
        ICountryMaster objICountryMaster;
        //
        // GET: /Country/

        public CountryController()
        {
            objICountryMaster = new CountryMaster();
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new CountryMasterDTO());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CountryMasterDTO objCountryMasterDTO)
        {
            if (ModelState.IsValid)
            {

                objICountryMaster.InsertCountry(objCountryMasterDTO);
                TempData["Message"] = "Country Create Successfully.";
                return RedirectToAction("Create");
            }
            else
            {
                ModelState.AddModelError("Error", "Please enter Country Name ");
            }

            ModelState.Remove("Country");

            return View(objCountryMasterDTO);
        }

        public ActionResult CountryNameExists(string Country)
        {
            var result = objICountryMaster.CountryNameExists(Country);
            return Json(!result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Details()
        {
            return View(objICountryMaster.GetCountries());
        }

        [HttpGet]
        public ActionResult Edit(string ID)
        {
            var Model = objICountryMaster.GetCountryByID(ID);
            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CountryMasterDTO objCountryMasterDTO)
        {
            objICountryMaster.UpdateCountry(objCountryMasterDTO);
            TempData["MessageUpdate"] = "Country Updated Successfully.";
            return RedirectToAction("Details");
        }

        [HttpGet]
        public ActionResult Delete(string ID)
        {
            objICountryMaster.DeleteCountry(ID);
            return RedirectToAction("Details");
        }

    }
}
