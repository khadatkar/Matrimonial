using GYMONE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GYMONE.Repository;
using GYMONE.Filters;

namespace GYMONE.Controllers
{
    //[MyExceptionHandler]
    //[Authorize(Roles = "Admin")]
    public class CityController : Controller
    {
        ICityMaster objicity;
        IStateMaster objistate;
        ICountryMaster objicountry;
        //


        public CityController()
        {
            objicity = new CityMaster();
            objistate = new StateMaster();
            objicountry = new CountryMaster();
        }

        // GET: /City/

        public ActionResult Index()
        {
            return View(objicity.GetCity());
        }

        [HttpGet]
        public ActionResult Create()
        {
            CityMasterDTO objcity = new CityMasterDTO();

            List<CountryMasterDTO> listCountry = new List<CountryMasterDTO>()
                {
                    new CountryMasterDTO{
                    Id = 0, Country = "Select Country"
                    }
                };

            foreach (var item in objicountry.GetCountries())
            {
                CountryMasterDTO cm = new CountryMasterDTO();
                cm.Id = item.Id;
                cm.Country = item.Country;
                listCountry.Add(cm);
            }

            objcity.ListCountry = listCountry;

            List<StateMasterDTO> liststate = new List<StateMasterDTO>()
                {
                    new StateMasterDTO{
                    Id = 0, State = "Select State"
                    }
                };

            foreach (var item in objistate.GetState())
            {
                StateMasterDTO cm = new StateMasterDTO();
                cm.Id = item.Id;
                cm.State = item.State;
                liststate.Add(cm);
            }

            objcity.ListState = liststate;

            return View(objcity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CityMasterDTO model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model.CountryId == 0)
                    {
                        ModelState.AddModelError("CountryMessage", "Please select Country");
                        Method(model);
                        return View(model);
                    }
                    else if (model.StateId == 0)
                    {
                        ModelState.AddModelError("StateMessage", "Please select State");
                        Method(model);
                        return View(model);
                    }
                    else
                    {

                        model.Id = 0;
                        objicity.InsertCity(model);
                        TempData["notice"] = "City Added Successfully";
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

        public ActionResult CitynameExists(string Cityname)
        {
            var result = objicity.CitynameExists(Cityname);
            return Json(!result, JsonRequestBehavior.AllowGet);
        }

        public void Method(CityMasterDTO objcity)
        {
            ModelState.Remove("ListCountry");
            ModelState.Remove("ListState");
            List<CountryMasterDTO> listCountry = new List<CountryMasterDTO>()
                {
                    new CountryMasterDTO{
                    Id = 0, Country = "Select Country"
                    }
                };

            foreach (var item in objicountry.GetCountries())
            {
                CountryMasterDTO cm = new CountryMasterDTO();
                cm.Id = item.Id;
                cm.Country = item.Country;
                listCountry.Add(cm);
            }

            objcity.ListCountry = listCountry;

            List<StateMasterDTO> liststate = new List<StateMasterDTO>()
                {
                    new StateMasterDTO{
                    Id = 0, State = "Select State"
                    }
                };

            foreach (var item in objistate.GetState())
            {
                StateMasterDTO cm = new StateMasterDTO();
                cm.Id = item.Id;
                cm.State = item.State;
                liststate.Add(cm);
            }

            objcity.ListState = liststate;
        }

        public JsonResult GetState(string CountryId)
        {
            var statedata = objistate.GetStateByCountryID(CountryId);
            return Json(statedata, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            var Model = objicity.GetCityByID(Convert.ToString(Id));
            EditMethod(Model);
            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CityMasterDTO objcity, FormCollection collection, string actionType)
        {
            if (actionType == "Update")
            {

                try
                {

                    if (ModelState.IsValid)
                    {
                        if (objcity.CountryId == 0)
                        {
                            ModelState.AddModelError("CountryMessage", "Please select Country");
                            Method(objcity);
                            return View(objcity);
                        }
                        else if (objcity.StateId == 0)
                        {
                            ModelState.AddModelError("StateMessage", "Please select State");
                            Method(objcity);
                            return View(objcity);
                        }
                        else
                        {
                            objicity.UpdateCity(objcity);
                            TempData["notice"] = "City Updated Successfully";

                            return RedirectToAction("Index");

                        }
                    }
                    else
                    {
                        Method(objcity);
                        return View(objcity);
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

        public void EditMethod(CityMasterDTO objcity)
        {

            ModelState.Remove("ListCountry");
            ModelState.Remove("ListState");
            List<CountryMasterDTO> listCountry = new List<CountryMasterDTO>()
                {
                    new CountryMasterDTO{
                    Id = 0, Country = "Select Country"
                    }
                };

            foreach (var item in objicountry.GetCountries())
            {
                CountryMasterDTO cm = new CountryMasterDTO();
                cm.Id = item.Id;
                cm.Country = item.Country;
                listCountry.Add(cm);
            }

            objcity.ListCountry = listCountry;

            List<StateMasterDTO> liststate = new List<StateMasterDTO>()
                {
                    new StateMasterDTO{
                    Id = 0, State = "Select State"
                    }
                };

            foreach (var item in objistate.GetState())
            {
                StateMasterDTO cm = new StateMasterDTO();
                cm.Id = item.Id;
                cm.State = item.State;
                liststate.Add(cm);
            }

            objcity.ListState = liststate;
        }

        public ActionResult Delete(int id)
        {
            objicity.DeleteCity(Convert.ToString(id));
            return RedirectToAction("Index");
        }
    }
}
