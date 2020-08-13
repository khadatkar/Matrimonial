using GYMONE.Models;
using GYMONE.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GYMONE.Controllers
{
    [Authorize(Roles = "Admin")]
    public class QualificationController : Controller
    {
        //
        // GET: /Qualification/

        public ActionResult Index()
        {
            // Declare list of QualificationVM
            List<qualificationVM> QualificationList;

            using (Db db = new Db())
            {
                // Init the list
                QualificationList = db.qualifications.ToArray().OrderBy(x => x.Id).Select(x => new qualificationVM(x)).ToList();
            }

            // Return view with list
            return View(QualificationList);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(qualificationVM model)
        {
            // Check model state
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (Db db = new Db())
            {

                // Init qualificationDTO
                qualificationDTO dto = new qualificationDTO();

                // DTO title
                dto.Qulification = model.Qulification;

                
                // Make sure qualification are unique
                if (db.qualifications.Any(x => x.Qulification == model.Qulification))
                {
                    ModelState.AddModelError("", "That Qualification already exists.");
                    return View(model);
                }

               

                // Save DTO
                db.qualifications.Add(dto);
                db.SaveChanges();
            }

            // Set TempData message
            TempData["notice"] = "You have added a new Qualification!";

            // Redirect
            return RedirectToAction("Create");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            // Declare pageVM
            qualificationVM model;

            using (Db db = new Db())
            {
                // Get the page
                qualificationDTO dto = db.qualifications.Find(id);

                // Confirm page exists
                if (dto == null)
                {
                    return Content("The Qualification does not exist.");
                }

                // Init pageVM
                model = new qualificationVM(dto);
            }

            // Return view with model
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(qualificationVM model)
        {
            // Check model state
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (Db db = new Db())
            {
                // Get page id
                int id = model.Id;

                // Init slug
                string slug = "home";

                // Get the page
                qualificationDTO dto = db.qualifications.Find(id);

                // DTO the title
                dto.Qulification = model.Qulification;


                // Make sure title and slug are unique
                if (db.qualifications.Where(x => x.Id != id).Any(x => x.Qulification == model.Qulification))
                {
                    ModelState.AddModelError("", "That Qualification already exists.");
                    return View(model);
                }
                // Save the DTO
                db.SaveChanges();
            }

            // Set TempData message
            TempData["notice"] = "You have edited the Qualification!";

            // Redirect
            return RedirectToAction("Edit");
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (Db db = new Db())
            {
                // Get the page
                qualificationDTO dto = db.qualifications.Find(id);

                // Remove the page
                db.qualifications.Remove(dto);

                // Save
                db.SaveChanges();
            }

            // Redirect
            return RedirectToAction("Index");
        }

    }
}
