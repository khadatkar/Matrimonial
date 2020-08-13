using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using GYMONE.Filters;
using GYMONE.Models;
using GYMONE.Repository;
using GYMONE.Models.ViewModels;

namespace GYMONE.Controllers
{

    //[MyExceptionHandler]
    //[Authorize(Roles = "Admin")]
    public class RegisterMemberController : Controller
    {
        #region member declaration

        ISchemeMaster objscheme;
        IPlanMaster objPlan;
        IRegisterMember objRegMem;
        IPaymentDetails objpay;

        ICityMaster objicity;
        IStateMaster objistate;
        ICountryMaster objicountry;

        ISubCaste objisubcaste;
        ICasteMaster objicastmaster;
        IReligionMaster objireligionmaster;

        #endregion


        public RegisterMemberController()
        {
            objscheme = new SchemeMaster();
            objPlan = new PlanMaster();
            objRegMem = new RegisterMember();
            objpay = new PaymentDetails();

            objicity = new CityMaster();
            objistate = new StateMaster();
            objicountry = new CountryMaster();

            objisubcaste = new SubCasteMaster();
            objicastmaster = new CasteMaster();
            objireligionmaster = new ReligionMaster();



        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(objRegMem.GetMember());
        }

        [HttpGet]

        public ActionResult Create()
        {
            MemberRegistrationDTO objMR = new MemberRegistrationDTO();

            objMR.ListScheme = BindListScheme();
            objMR.ListPlan = BindListPlan();
            objMR.Listgender = BindGender();
            ViewData["SelectedPlan"] = string.Empty;

            return View(objMR);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MemberRegistrationDTO obj, FormCollection frm, HttpPostedFileBase file)
        {
            try
            {
                // Check if passwords match
                if (!obj.Password.Equals(obj.ConfirmPassword))
                {
                    ModelState.AddModelError("", "Passwords do not match.");
                    return View("CreateAccount", obj);
                }


                if (string.IsNullOrEmpty(obj.MemberFName))
                {
                    ModelState.AddModelError("Error", "Please enter First Name");
                }
                else if (string.IsNullOrEmpty(obj.MemberMName))
                {
                    ModelState.AddModelError("Error", "Please enter Middle Name");
                }
                else if (string.IsNullOrEmpty(obj.MemberLName))
                {
                    ModelState.AddModelError("Error", "Please enter Last Name");
                }
                else if (string.IsNullOrEmpty(Convert.ToString(obj.DOB)))
                {
                    ModelState.AddModelError("Error", "Please select Birth Date");
                }
                else if (string.IsNullOrEmpty(Convert.ToString(obj.JoiningDate)))
                {
                    ModelState.AddModelError("Error", "Please select Joining Date");
                }
                else if (string.IsNullOrEmpty(obj.Age))
                {
                    ModelState.AddModelError("Error", "Please enter Age");
                }
                else if (string.IsNullOrEmpty(obj.EmailID))
                {
                    ModelState.AddModelError("Error", "Please enter EmailID");
                }
                else if (string.IsNullOrEmpty(Convert.ToString(obj.WorkouttypeID)))
                {
                    ModelState.AddModelError("Error", "Please select  Workouttype");
                }
                else if (string.IsNullOrEmpty(Convert.ToString(obj.PlantypeID)))
                {
                    ModelState.AddModelError("Error", "Please select Plan");
                }
                else if (string.IsNullOrEmpty(obj.Contactno))
                {
                    ModelState.AddModelError("Error", "Please enter Contactno");
                }
                else if (string.IsNullOrEmpty(obj.Address))
                {
                    ModelState.AddModelError("Error", "Please enter Address");
                }
                else if (string.IsNullOrEmpty(Convert.ToString(obj.Amount)))
                {
                    ModelState.AddModelError("Error", "Amount Cannot be Empty");
                }
                else
                {
                    //string[] parttime = obj.DOB.ToString().Split('-');
                    //int year1 = Convert.ToInt32(obj.DOB.Value.Year);
                    //int month1 = Convert.ToInt32(obj.DOB.Value.Month);
                    //int day1 = Convert.ToInt32(obj.DOB.Value.Day);
                    //DateTime birthdate = new DateTime(year1, month1, day1);
                    //obj.DOB = birthdate;

                    //string[] joing = obj.JoiningDate.ToString().Split('-');
                    //int year2 = Convert.ToInt32(obj.JoiningDate.Value.Year);
                    //int month2 = Convert.ToInt32(obj.JoiningDate.Value.Month);
                    //int day2 = Convert.ToInt32(obj.JoiningDate.Value.Day);
                    //DateTime joining = obj.JoiningDate;
                    //obj.JoiningDate = joining;


                    obj.Createdby = Convert.ToInt32(Session["UserID"]);


                    using (Db db = new Db())
                    {
                        // Make sure username is unique
                        if (db.Users.Any(x => x.Username.Equals(obj.Username)))
                        {
                            ModelState.AddModelError("", "Username " + obj.Username + " is taken.");
                            obj.Username = "";
                            return View("Create", obj);
                        }

                        // Create userDTO
                        UserDTO userDTO = new UserDTO()
                        {
                            FirstName = obj.MemberFName,
                            LastName = obj.MemberLName,
                            EmailAddress = obj.EmailID,
                            Username = obj.Username,
                            Password = obj.Password
                        };

                        // Add the DTO
                        db.Users.Add(userDTO);

                        // Save
                        db.SaveChanges();

                        // Add to UserRolesDTO
                        int id = userDTO.Id;

                        UserRoleDTO userRolesDTO = new UserRoleDTO()
                        {
                            UserId = id,
                            RoleId = 2
                        };

                        db.UserRoles.Add(userRolesDTO);
                        db.SaveChanges();

                        obj.userid = id;
                    }




                    int MemberID = objRegMem.InsertMember(obj); // Insert into MemberDetails
                    if (MemberID > 0)
                    {
                        int payresult = Pay(obj, MemberID); // Insert into Paymentdetails
                        if (payresult > 0)
                        {
                            TempData["Message"] = "Member Created Successfully.";

                            #region Upload Image

                            // Create necessary directories
                            var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\Uploads", Server.MapPath(@"\")));

                            var pathString1 = Path.Combine(originalDirectory.ToString(), "Members");
                            var pathString2 = Path.Combine(originalDirectory.ToString(), "Members\\" + MemberID.ToString());
                            var pathString3 = Path.Combine(originalDirectory.ToString(), "Members\\" + MemberID.ToString() + "\\Thumbs");
                            var pathString4 = Path.Combine(originalDirectory.ToString(), "Members\\" + MemberID.ToString() + "\\Gallery");
                            var pathString5 = Path.Combine(originalDirectory.ToString(), "Members\\" + MemberID.ToString() + "\\Gallery\\Thumbs");

                            if (!Directory.Exists(pathString1))
                                Directory.CreateDirectory(pathString1);

                            if (!Directory.Exists(pathString2))
                                Directory.CreateDirectory(pathString2);

                            if (!Directory.Exists(pathString3))
                                Directory.CreateDirectory(pathString3);

                            if (!Directory.Exists(pathString4))
                                Directory.CreateDirectory(pathString4);

                            if (!Directory.Exists(pathString5))
                                Directory.CreateDirectory(pathString5);

                            // Check if a file was uploaded
                            if (file != null && file.ContentLength > 0)
                            {
                                // Get file extension
                                string ext = file.ContentType.ToLower();

                                // Verify extension
                                if (ext != "image/jpg" &&
                                    ext != "image/jpeg" &&
                                    ext != "image/pjpeg" &&
                                    ext != "image/gif" &&
                                    ext != "image/x-png" &&
                                    ext != "image/png")
                                {
                                    obj.ListScheme = BindListScheme(); // binding scheme
                                    obj.ListPlan = BindListPlan(); // binding plan
                                    obj.Listgender = BindGender(); // binding gender
                                    ViewData["SelectedPlan"] = obj.PlantypeID; // Maintaining plan dropdowm list after postback

                                    return View(obj);

                                }

                                // Init image name
                                string imageName = file.FileName;

                                // Save image name to DTO
                                MemberRegistrationDTO obj1 = objRegMem.GetMemberbyID(MemberID.ToString());



                                // Set original and thumb image paths
                                var path = string.Format("{0}\\{1}", pathString2, imageName);
                                var path2 = string.Format("{0}\\{1}", pathString3, imageName);
                                var path3 = string.Format("{0}\\{1}", pathString4, imageName);

                                obj1.MemImagename = imageName;
                                obj1.MemImagePath = path;

                                objRegMem.UpdateImage(obj1);

                                // Save original
                                file.SaveAs(path);

                                // Create and save thumb
                                WebImage img = new WebImage(file.InputStream);
                                img.Resize(200, 200);
                                img.Save(path2);


                                img.AddTextWatermark("Shubh", "White", 12, "Regular", "Microsoft Sans Serif", "Right", "Bottom", 80, 5);
                                img.Resize(900, 750);
                                img.Save(path3);

                            }

                            #endregion


                        }
                        else
                        {
                            TempData["Message"] = "Some thing went wrong while Member Created .";
                        }
                    }
                    else
                    {
                        TempData["Message"] = "Some thing went wrong while Member Created .";
                    }

                    return RedirectToAction("Index");
                    //return View();
                    //return RedirectToAction("Create", "Demo");
                }

                obj.ListScheme = BindListScheme(); // binding scheme
                obj.ListPlan = BindListPlan(); // binding plan
                obj.Listgender = BindGender(); // binding gender
                ViewData["SelectedPlan"] = obj.PlantypeID; // Maintaining plan dropdowm list after postback




                //return RedirectToAction("Index", "Demo");

                return View(obj);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public ActionResult Edit(string MemID)
        {
            if (MemID == null || MemID == "")
            {
                MemID = Convert.ToString(Session["MemberRegID"]);
            }
            var model = objRegMem.GetMemberbyID(MemID);
            model.ListScheme = BindListScheme();
            model.ListPlan = BindListPlan();
            model.Listgender = BindGender();
            model.ListCountry = BindListCountry();
            model.ListState = BindListState();
            model.ListCity = BindListCity();
            model.ListReligion = BindListReligion();
            model.ListCaste = BindListCaste();
            model.ListSubCaste = BindListSubCaste();

            model.ListFatherStatus = Bindfatherstatus();



            using (Db db = new Db())
            {
                model.ListQualification = new SelectList(db.qualifications.ToList(), "Id", "Qulification");
            }

            


            //string[] joing = model.JoiningDate.ToString().Split('/');
            //int year2 = Convert.ToInt32(model.JoiningDate.Value.Year);
            //int month2 = Convert.ToInt32(model.JoiningDate.Value.Month);
            //int day2 = Convert.ToInt32(model.JoiningDate.Value.Day);
            //DateTime joining = new DateTime(year2, month2, day2);
            //model.JoiningDate = joining;

            ViewData["SelectedPlan"] = model.PlantypeID;
            ViewData["SelectedCity"] = model.cityid;
            ViewData["SelectedState"] = model.stateid;
            ViewData["SelectedCountry"] = model.countryid;
            ViewData["SelectedReligion"] = model.religionid;
            ViewData["SelectedCaste"] = model.casteid;
            ViewData["SelectedSubcaste"] = model.subcasteid;




            model.GalleryImages = Directory.EnumerateFiles(Server.MapPath("~/Images/Uploads/Members/" + MemID + "/Gallery/Thumbs"))
                                                .Select(fn => Path.GetFileName(fn));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MemberRegistrationDTO objMR, string actionType, HttpPostedFileBase file)
        {
            if (actionType == "Update")
            {
                if (objMR.MemID != 0 || Convert.ToString(objMR.MemID) != null)
                {
                    if (string.IsNullOrEmpty(objMR.MemberFName))
                    {
                        ModelState.AddModelError("Error", "Please enter First Name");
                    }
                    else if (string.IsNullOrEmpty(objMR.MemberMName))
                    {
                        ModelState.AddModelError("Error", "Please enter Middle Name");
                    }
                    else if (string.IsNullOrEmpty(objMR.MemberLName))
                    {
                        ModelState.AddModelError("Error", "Please enter Last Name");
                    }
                    else if (string.IsNullOrEmpty(Convert.ToString(objMR.DOB)))
                    {
                        ModelState.AddModelError("Error", "Please select Birth Date");
                    }
                    else if (string.IsNullOrEmpty(Convert.ToString(objMR.JoiningDate)))
                    {
                        ModelState.AddModelError("Error", "Please select Joining Date");
                    }
                    else if (string.IsNullOrEmpty(objMR.Age))
                    {
                        ModelState.AddModelError("Error", "Please enter Age");
                    }
                    else if (string.IsNullOrEmpty(objMR.EmailID))
                    {
                        ModelState.AddModelError("Error", "Please enter EmailID");
                    }
                    else if (string.IsNullOrEmpty(Convert.ToString(objMR.WorkouttypeID)))
                    {
                        ModelState.AddModelError("Error", "Please select  Workouttype");
                    }
                    else if (string.IsNullOrEmpty(Convert.ToString(objMR.PlantypeID)))
                    {
                        ModelState.AddModelError("Error", "Please select Plan");
                    }
                    else if (string.IsNullOrEmpty(objMR.Contactno))
                    {
                        ModelState.AddModelError("Error", "Please enter Contactno");
                    }
                    else if (string.IsNullOrEmpty(objMR.Address))
                    {
                        ModelState.AddModelError("Error", "Please enter Address");
                    }
                    else if (string.IsNullOrEmpty(Convert.ToString(objMR.Amount)))
                    {
                        ModelState.AddModelError("Error", "Amount Cannot be Empty");
                    }
                    else
                    {
                        //string[] parttime = objMR.DOB.ToString().Split('-');
                        //int year1 = Convert.ToInt32(objMR.DOB.Value.Year);
                        //int month1 = Convert.ToInt32(objMR.DOB.Value.Month);
                        //int day1 = Convert.ToInt32(objMR.DOB.Value.Day);
                        //DateTime birthdate = new DateTime(year1, month1, day1);
                        //objMR.DOB = birthdate;

                        //string[] joing = objMR.JoiningDate.ToString().Split('-');
                        //int year2 = Convert.ToInt32(objMR.JoiningDate.Value.Year);
                        //int month2 = Convert.ToInt32(objMR.JoiningDate.Value.Month);
                        //int day2 = Convert.ToInt32(objMR.JoiningDate.Value.Day);
                        //DateTime joining = new DateTime(year2, month2, day2);
                        //objMR.JoiningDate = joining;

                        objMR.Createdby = Convert.ToInt32(Session["UserID"]);
                        int MemberID = objRegMem.UpdateMember(objMR); // Insert into MemberDetails
                        if (MemberID > 0)
                        {
                            int payresult = PayUpdate(objMR, MemberID);
                            TempData["MessageUpdate"] = "Member Details Updated Successfully.";

                            #region Image Upload

                            // Check for file upload
                            if (file != null && file.ContentLength > 0)
                            {

                                // Get extension
                                string ext = file.ContentType.ToLower();

                                // Verify extension
                                if (ext != "image/jpg" &&
                                    ext != "image/jpeg" &&
                                    ext != "image/pjpeg" &&
                                    ext != "image/gif" &&
                                    ext != "image/x-png" &&
                                    ext != "image/png")
                                {
                                    objMR.ListScheme = BindListScheme(); // binding scheme
                                    objMR.ListPlan = BindListPlan(); // binding plan
                                    objMR.Listgender = BindGender(); // binding gender
                                    ViewData["SelectedPlan"] = objMR.PlantypeID; // Maintaining plan dropdowm list after postback
                                    ModelState.AddModelError("", "The image was not uploaded - wrong image extension.");
                                    return View(objMR);

                                }

                                // Set uplpad directory paths
                                var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\Uploads", Server.MapPath(@"\")));

                                var pathString1 = Path.Combine(originalDirectory.ToString(), "Members\\" + MemberID.ToString());
                                var pathString2 = Path.Combine(originalDirectory.ToString(), "Members\\" + MemberID.ToString() + "\\Thumbs");
                                var pathString4 = Path.Combine(originalDirectory.ToString(), "Members\\" + MemberID.ToString() + "\\Gallery");

                                // Delete files from directories

                                DirectoryInfo di1 = new DirectoryInfo(pathString1);
                                DirectoryInfo di2 = new DirectoryInfo(pathString2);

                                foreach (FileInfo file2 in di1.GetFiles())
                                    file2.Delete();

                                foreach (FileInfo file3 in di2.GetFiles())
                                    file3.Delete();

                                // Save image name

                                string imageName = file.FileName;


                                MemberRegistrationDTO dto = objRegMem.GetMemberbyID(MemberID.ToString());

                                // Save original and thumb images

                                var path = string.Format("{0}\\{1}", pathString1, imageName);
                                var path2 = string.Format("{0}\\{1}", pathString2, imageName);
                                var path3 = string.Format("{0}\\{1}", pathString4, imageName);

                                dto.MemImagename = imageName;
                                dto.MemImagePath = path;

                                objRegMem.UpdateImage(dto);


                                file.SaveAs(path);

                                WebImage img = new WebImage(file.InputStream);
                                img.Resize(900, 750);
                                img.Save(path3);


                                img.Resize(200, 200);
                                img.Save(path2);
                            }

                            #endregion

                        }
                        if (User.IsInRole("Admin"))
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            return RedirectToAction("Edit");
                        }
                    }



                    if (User.IsInRole("Admin"))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Edit");
                    }
                }



                else
                {
                    if (User.IsInRole("Admin"))
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        return RedirectToAction("Edit");
                    }
                }
            }
            else
            {
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Edit");
                }
            }
        }


        [HttpPost]
        public void SaveGalleryImages(int id)
        {
            // Loop through files
            foreach (string fileName in Request.Files)
            {
                // Init the file
                HttpPostedFileBase file = Request.Files[fileName];

                // Check it's not null
                if (file != null && file.ContentLength > 0)
                {
                    // Set directory paths
                    var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\Uploads", Server.MapPath(@"\")));

                    string pathString1 = Path.Combine(originalDirectory.ToString(), "Members\\" + id.ToString() + "\\Gallery");
                    string pathString2 = Path.Combine(originalDirectory.ToString(), "Members\\" + id.ToString() + "\\Gallery\\Thumbs");

                    // Set image paths
                    var path = string.Format("{0}\\{1}", pathString1, file.FileName);
                    var path2 = string.Format("{0}\\{1}", pathString2, file.FileName);

                    // Save original and thumb

                    file.SaveAs(path);
                    WebImage img = new WebImage(file.InputStream);
                    img.Resize(200, 200);
                    img.Save(path2);
                }

            }

        }

        [HttpPost]
        public void DeleteImage(int id, string imageName)
        {
            string fullPath1 = Request.MapPath("~/Images/Uploads/Members/" + id.ToString() + "/Gallery/" + imageName);
            string fullPath2 = Request.MapPath("~/Images/Uploads/Members/" + id.ToString() + "/Gallery/Thumbs/" + imageName);

            if (System.IO.File.Exists(fullPath1))
                System.IO.File.Delete(fullPath1);

            if (System.IO.File.Exists(fullPath2))
                System.IO.File.Delete(fullPath2);
        }


        [HttpGet]
        public ActionResult Delete(string MemID)
        {
            objRegMem.DeleteMember(MemID);

            // Delete product folder
            var originalDirectory = new DirectoryInfo(string.Format("{0}Images\\Uploads", Server.MapPath(@"\")));
            string pathString = Path.Combine(originalDirectory.ToString(), "Members\\" + MemID.ToString());

            if (Directory.Exists(pathString))
                Directory.Delete(pathString, true);

            return RedirectToAction("Index", "RegisterMember");

            //return RedirectToAction("Index");
        }



        #region Binding_Dropdown_code
        [NonAction] // if Method is not Action method then use NonAction
        public List<SchemeMasterDTO> BindListScheme()
        {
            List<SchemeMasterDTO> listscheme = new List<SchemeMasterDTO>()
            { new
            SchemeMasterDTO
            { SchemeID = 0,
                SchemeName = "Select" } };

            foreach (var item in objscheme.GetSchemes())
            {
                SchemeMasterDTO sm = new SchemeMasterDTO();
                sm.SchemeID = item.SchemeID;
                sm.SchemeName = item.SchemeName;
                listscheme.Add(sm);
            }
            return listscheme;

        }

        [NonAction]
        public List<PlanMasterDTO> BindListPlan()
        {
            List<PlanMasterDTO>
            ListPlan = new List<PlanMasterDTO>() { new PlanMasterDTO { PlanID = 0, PlanName = "Select" } };

            foreach (var item in objPlan.GetPlan())
            {
                PlanMasterDTO pm = new PlanMasterDTO();
                pm.PlanID = item.PlanID;
                pm.PlanName = item.PlanName;
                ListPlan.Add(pm);
            }


            return ListPlan;
        }

        [NonAction]
        public List<SelectListItem> BindGender()
        {

            List<SelectListItem> lgender = new List<SelectListItem>(){
            new SelectListItem { Text="Select Gender", Value="0", Selected=true},
            new SelectListItem {Text="Male", Value="1", Selected=false},
            new SelectListItem {Text="Female", Value="2", Selected=false}
            };
            return lgender;
        }

        [NonAction]
        public List<SelectListItem> Bindfatherstatus()
        {




            List<SelectListItem> lfatherstatus = new List<SelectListItem>(){

                new SelectListItem { Text = "Employed", Value="Employed" },
                new SelectListItem { Text = "Business", Value="Business" },
                new SelectListItem { Text = "Retired", Value="Retired" },
                new SelectListItem { Text = "Not Employed", Value="Not Employed" },
                new SelectListItem { Text = "Passed Away", Value="Passed Away" }


            //new SelectListItem { Text="Select Gender", Value="0", Selected=true},
            //new SelectListItem {Text="Male", Value="1", Selected=false},
            //new SelectListItem {Text="Female", Value="2", Selected=false}
            };
            return lfatherstatus;
        }

        [NonAction] // if Method is not Action method then use NonAction
        public List<CountryMasterDTO> BindListCountry()
        {
            List<CountryMasterDTO> listcountry = new List<CountryMasterDTO>()
            { new
            CountryMasterDTO
            { Id = 0,
                Country = "Select Country" } };

            foreach (var item in objicountry.GetCountries())
            {
                CountryMasterDTO sm = new CountryMasterDTO();
                sm.Id = item.Id;
                sm.Country = item.Country;
                listcountry.Add(sm);
            }
            return listcountry;
        }



        [NonAction] // if Method is not Action method then use NonAction
        public List<StateMasterDTO> BindListState()
        {
            List<StateMasterDTO> listState = new List<StateMasterDTO>()
            { new
            StateMasterDTO
            { Id = 0,
                State = "Select State" } };
            foreach (var item in objistate.GetState())
            {
                StateMasterDTO sm = new StateMasterDTO();
                sm.Id = item.Id;
                sm.State = item.State;
                listState.Add(sm);
            }
            return listState;
        }

        [NonAction] // if Method is not Action method then use NonAction
        public List<CityMasterDTO> BindListCity()
        {
            List<CityMasterDTO> listCity = new List<CityMasterDTO>()
            { new
            CityMasterDTO
            { Id = 0,
                CityName = "Select City" } };

            foreach (var item in objicity.GetCity())
            {
                CityMasterDTO sm = new CityMasterDTO();
                sm.Id = item.Id;
                sm.CityName = item.CityName;
                listCity.Add(sm);
            }
            return listCity;
        }

        [NonAction] // if Method is not Action method then use NonAction
        public List<ReligionDTO> BindListReligion()
        {
            List<ReligionDTO> listReligion = new List<ReligionDTO>()
            { new
            ReligionDTO
            { Id = 0,
                Religion = "Select Religion" } };

            foreach (var item in objireligionmaster.GetReligions())
            {
                ReligionDTO sm = new ReligionDTO();
                sm.Id = item.Id;
                sm.Religion = item.Religion;
                listReligion.Add(sm);
            }
            return listReligion;
        }

        [NonAction] // if Method is not Action method then use NonAction
        public List<CasteDTO> BindListCaste()
        {
            List<CasteDTO> listCaste = new List<CasteDTO>()
            { new
            CasteDTO
            { Id = 0,
                Caste = "Select Caste" } };

            foreach (var item in objicastmaster.GetCaste())
            {
                CasteDTO sm = new CasteDTO();
                sm.Id = item.Id;
                sm.Caste = item.Caste;
                listCaste.Add(sm);
            }
            return listCaste;
        }

        [NonAction] // if Method is not Action method then use NonAction
        public List<SubCasteDTO> BindListSubCaste()
        {
            List<SubCasteDTO> listSubCaste = new List<SubCasteDTO>()
            { new
            SubCasteDTO
            { Id = 0,
                SubCaste = "Select Sub Caste" } };

            foreach (var item in objisubcaste.GetSubCaste())
            {
                SubCasteDTO sm = new SubCasteDTO();
                sm.Id = item.Id;
                sm.SubCaste = item.SubCaste;
                listSubCaste.Add(sm);
            }
            return listSubCaste;
        }

        #endregion

        public JsonResult GetAmount(string PlanID, string WorkTypeID)
        {
            var amount = objRegMem.GetAmount(PlanID, WorkTypeID);
            return Json(amount, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPlan(string WorkTypeID)
        {
            var plandata = objPlan.GetPlanByWorkTypeID(WorkTypeID);
            return Json(plandata, JsonRequestBehavior.AllowGet);
        }

        [NonAction]
        public int Pay(MemberRegistrationDTO obj, int MemberID)
        {
            try
            {
                PaymentDetailsDTO PayPD = new PaymentDetailsDTO();
                PayPD.PaymentID = 0;
                PayPD.PlanID = Convert.ToInt32(obj.PlantypeID);
                PayPD.WorkouttypeID = Convert.ToInt32(obj.WorkouttypeID);

                string[] joing = obj.JoiningDate.ToString().Split('/');
                int year2 = Convert.ToInt32(obj.JoiningDate.Value.Year);
                int month2 = Convert.ToInt32(obj.JoiningDate.Value.Month);
                int day2 = Convert.ToInt32(obj.JoiningDate.Value.Day);
                DateTime joining = new DateTime(year2, month2, day2);
                PayPD.PaymentFromdt = joining;
                PayPD.PaymentAmount = Convert.ToDecimal(obj.Amount);
                PayPD.CreateUserID = Convert.ToInt32(Session["UserID"]);
                PayPD.ModifyUserID = 0;
                PayPD.MemberID = MemberID;

                int payresult = objpay.InsertPaymentDetails(PayPD);

                return payresult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [NonAction]
        public int PayUpdate(MemberRegistrationDTO obj, int MemberID)
        {
            try
            {
                PaymentDetailsDTO PayPD = new PaymentDetailsDTO();
                PayPD.PaymentID = 0;
                PayPD.PlanID = Convert.ToInt32(obj.PlantypeID);
                PayPD.WorkouttypeID = Convert.ToInt32(obj.WorkouttypeID);

                
                try
                {
                    PayPD.PaymentFromdt = Convert.ToDateTime(obj.JoiningDate);
                }
                catch (Exception ex)
                {
                    string[] joing = obj.JoiningDate.ToString().Split('-');
                    int year2 = Convert.ToInt32(obj.JoiningDate.Value.Year);
                    int month2 = Convert.ToInt32(obj.JoiningDate.Value.Month);
                    int day2 = Convert.ToInt32(obj.JoiningDate.Value.Day);
                    DateTime joining = new DateTime(year2, month2, day2);

                    PayPD.PaymentFromdt = joining;
                }
                PayPD.PaymentAmount = Convert.ToDecimal(obj.Amount);
                PayPD.CreateUserID = Convert.ToInt32(Session["UserID"]);
                PayPD.ModifyUserID = 0;
                PayPD.MemberID = MemberID;
                PayPD.PaymentID = Convert.ToInt32(obj.PaymentID);
                int payresult = objpay.UpdatePaymentDetails(PayPD);

                return payresult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult GetState(string CountryId)
        {
            var statedata = objistate.GetStateByCountryID(CountryId);
            return Json(statedata, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCity(string stateid)
        {
            var citydata = objicity.GetCityByStateID(stateid);
            return Json(citydata, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCaste(string ReligionId)
        {
            var Castedata = objicastmaster.GetCasteByReligionID(ReligionId);
            return Json(Castedata, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSubCaste(string CasteId)
        {
            var SubCastedata = objisubcaste.GetSubCasteByCasteID(CasteId);
            return Json(SubCastedata, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ReligionMenuPartial()
        {
            // Declare list of CategoryVM
            List<ReligionVM> ReligionVMList;

            // Init the list
            using (Db db = new Db())
            {
                ReligionVMList = db.Religions.ToArray().OrderBy(x => x.Religion).Select(x => new ReligionVM(x)).ToList();
            }

            // Return partial with list
            return PartialView(ReligionVMList);
        }

        public ActionResult MemberData(int id, int? genderid)
        {

            //Product => Member
            //Category => Religion

            // Declare a list of ProductVM
            List<MemberRegistrationVM> MemberList;



            using (Db db = new Db())
            {
                // Get Religion id
                //int religionid1 = Convert.ToInt32(ReligionId);
                ReligionDTO religionDTO = db.Religions.Where(x => x.Id == id).FirstOrDefault();

                int Religionid1 = religionDTO.Id;

                // Init the list

                MemberList = db.Members.ToArray().Where(x => x.religionid == Religionid1).Select(x => new MemberRegistrationVM(x)).ToList();
                // Get Member name
                var MemberReligion = db.Members.Where(x => x.religionid == Religionid1).FirstOrDefault();
                ViewBag.MemberReligion = religionDTO.Religion;
            }
            //using (Db db = new Db())
            //{
            // Get Religion id
            //int religionid1 = Convert.ToInt32(ReligionId);
            //ReligionDTO religionDTO = db.Religions.Where(x => x.Id == id).FirstOrDefault();

            //int Religionid1 = religionDTO.Id;

            // Init the list

            //MemberList = db.Members.ToArray().Where(x => x.Gender.Equals(genderid)).Select(x => new MemberRegistrationVM(x)).ToList();
            // Get Member name
            //var MemberReligion = db.Members.Where(x => x.religionid == Religionid1).FirstOrDefault();
            //ViewBag.MemberReligion = religionDTO.Religion;
            //}




            // Return view with list

            return View(MemberList);
        }


        [HttpGet]
        public ActionResult FilterMenuPartial()
        {
            MemberRegistrationDTO objMR = new MemberRegistrationDTO();

            objMR.ListScheme = BindListScheme();
            objMR.ListPlan = BindListPlan();
            objMR.Listgender = BindGender();

            objMR.ListCountry = BindListCountry();
            objMR.ListState = BindListState();
            objMR.ListCity = BindListCity();
            objMR.ListReligion = BindListReligion();
            objMR.ListCaste = BindListCaste();
            objMR.ListSubCaste = BindListSubCaste();

            return View(objMR);
        }
    }
}
