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
    public class StateController : Controller
    {
        //
        // GET: /State/

        ICountryMaster objCountryMaster;
        IStateMaster objStateMaster;

        public StateController()
        {
            objCountryMaster = new CountryMaster();
            objStateMaster = new StateMaster();
        }

        public ActionResult Index()
        {
            return View(objStateMaster.GetState());
        }

        [HttpGet]
        public ActionResult Create()
        {
            StateMasterDTO objState = new StateMasterDTO();

            List<CountryMasterDTO> listCountry = new List<CountryMasterDTO>()
                {
                    new CountryMasterDTO{
                    Id = 0, Country = "Select Country"
                    }
                };

            foreach (var item in objCountryMaster.GetCountries())
            {
                CountryMasterDTO cm = new CountryMasterDTO();
                cm.Id = item.Id;
                cm.Country = item.Country;
                listCountry.Add(cm);
            }

            objState.ListCountry = listCountry;

            return View(objState);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StateMasterDTO objstate, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (objstate.CountryID == 0)
                    {
                        ModelState.AddModelError("CountryMessage", "Please select Country");
                        Method(objstate);
                        return View(objstate);
                    }
                    else
                    {

                        objstate.Id = 0;
                        objStateMaster.InsertState(objstate);
                        TempData["notice"] = "State Added Successfully";
                        return RedirectToAction("Create");

                    }
                }
                else
                {
                    Method(objstate);
                    return View(objstate);
                }
            }
            catch
            {
                Method(objstate);
                return View(objstate);
            }
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            //StateMasterDTO model1 = (StateMasterDTO)objStateMaster.GetStateByCountryID(Convert.ToString(id));
            var Model = objStateMaster.GetStateByID(Convert.ToString(id));
            EditMethod(Model);
            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(StateMasterDTO objstate, FormCollection collection, string actionType)
        {
            if (actionType == "Update")
            {

                try
                {

                    if (ModelState.IsValid)
                    {
                        if (objstate.CountryID == 0)
                        {
                            ModelState.AddModelError("CountryMessage", "Please select Country");
                            Method(objstate);
                            return View(objstate);
                        }
                        else
                        {
                            objStateMaster.UpdateState(objstate);   
                            TempData["notice"] = "State Updated Successfully";

                            return RedirectToAction("Index");

                        }
                    }
                    else
                    {
                        Method(objstate);
                        return View(objstate);
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

        public void Method(StateMasterDTO objstate)
        {
            ModelState.Remove("ListCountry");
            List<CountryMasterDTO> listCountry = new List<CountryMasterDTO>()
                {
                    new CountryMasterDTO{
                    Id = 0, Country = "Select Country"
                    }
                };

            foreach (var item in objCountryMaster.GetCountries())
            {
                CountryMasterDTO cm = new CountryMasterDTO();
                cm.Id = item.Id;
                cm.Country = item.Country;
                listCountry.Add(cm);
            }

            objstate.ListCountry = listCountry;
        }

        public void EditMethod(StateMasterDTO objstate)
        {
            ModelState.Remove("ListCountry");
            List<CountryMasterDTO> listCountry = new List<CountryMasterDTO>()
                {
                    new CountryMasterDTO{
                    Id = 0, Country = "Select Country"
                    }
                };

            foreach (var item in objCountryMaster.GetCountries())
            {
                CountryMasterDTO cm = new CountryMasterDTO();
                cm.Id = item.Id;
                cm.Country = item.Country;
                listCountry.Add(cm);
            }

            objstate.ListCountry = listCountry;
            objstate.CountryID = objstate.CountryID;
        }

        public ActionResult StatenameExists(string Statename)
        {
            var result = objStateMaster.StatenameExists(Statename);
            return Json(!result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Delete(int id)
        {
            objStateMaster.DeleteState(Convert.ToString(id));
            return RedirectToAction("Index");
        }

    }
}
