using GYMONE.Models;
using GYMONE.Models.ViewModels;
using GYMONE.Repository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;

namespace GYMONE.Controllers
{
    public class DemoController : Controller
    {

        IRegisterMember objRegMem;
        ICityMaster objicity;
        IStateMaster objistate;





        public DemoController()
        {
            objicity = new CityMaster();
            objistate = new StateMaster();
            objRegMem = new RegisterMember();
        }

        [HttpGet]
        public ActionResult test()
        {
            List<MemberRegistrationVM> listmember = new List<MemberRegistrationVM>();

            //List<MemberRegistrationVM> listmember1 = new List<MemberRegistrationVM>();

            using (Db db = new Db())
            {

                listmember = db.Members.ToArray().Select(x => new MemberRegistrationVM(x)).ToList();

                foreach (var member in listmember)
                {
                    StateMasterDTO user = db.States.Where(x => x.Id == member.stateid).FirstOrDefault();
                    string statename = user.State;
                    listmember.Add(
                new MemberRegistrationVM()
                {
                    state = statename
                }
                );
                }



            }

            return View(listmember);
        }



        //
        // GET: /Demo/

        public ActionResult Index()
        {

            string markers = "[";
            
                        markers += "{";
                        markers += string.Format("'title': '{0}',", "Shubh Mangal Vivah Sanstha");
                        markers += string.Format("'lat': '{0}',", "21.131530");
                        markers += string.Format("'lng': '{0}',", "79.097580");
                        markers += string.Format("'description': '{0}'", "Shubh Mangal Vivah Sanstha,Medical Square");
                        markers += "},";
              

            markers += "];";
            ViewBag.Markers = markers;



            List<MemberRegistrationDTO> listOfMember;

            List<MemberRegistrationDTO> newlistOfMember = new List<MemberRegistrationDTO>();

            listOfMember = objRegMem.GetMember().OrderByDescending(x => x.MemID).Take(12).ToList();
            using (Db db = new Db())
            {

                foreach (var member in listOfMember)
                {
                    CityMasterDTO citydto = db.Cities.Where(x => x.Id == member.cityid).FirstOrDefault();
                    StateMasterDTO statedto = db.States.Where(x => x.Id == member.stateid).FirstOrDefault();
                    qualificationDTO qualidto = db.qualifications.Where(x => x.Id == member.highestqualification).FirstOrDefault();

                    //For City
                    if (citydto != null)
                    {
                        member.City = citydto.CityName;
                    }
                    else
                    {
                        member.City = null;
                    }

                    //For State
                    if (statedto != null)
                    {
                        member.State = statedto.State;
                    }
                    else
                    {
                        member.State = null;
                    }

                    //For Qualification
                    if (qualidto != null)
                    {
                        member.Qualification = qualidto.Qulification;
                    }
                    else
                    {
                        member.Qualification = null;
                    }
                    newlistOfMember.Add(member);
                }
            }

            ViewBag.listmember = newlistOfMember;

            return View(newlistOfMember);
        }

        public string getpassword(string username)
        {

            MembershipUser u = Membership.GetUser(username, false);
            string pass = u.GetPassword(null);
            return "Your Password is " + pass;
        }

        [HttpGet]
        public ActionResult Login()
        {
            // Confirm user is not logged in

            string username = User.Identity.Name;

            if (!string.IsNullOrEmpty(username))
                return RedirectToAction("user-profile");
            //return RedirectToAction("user-profile");

            // Return view
            return View();
        }


        [HttpPost]
        public ActionResult Login(LoginUserVM model)
        {
            UserDTO user;
            // Check model state
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Check if the user is valid

            bool isValid = false;

            using (Db db = new Db())
            {
                if (db.Users.Any(x => x.Username.Equals(model.Username) && x.Password.Equals(model.Password)))
                {
                    isValid = true;
                }
            }

            if (!isValid)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(model);
            }
            else
            {
                using (Db db = new Db())
                {
                    FormsAuthentication.SetAuthCookie(model.Username, model.RememberMe);
                    //FormsAuthentication.GetRedirectUrl(model.Username, model.RememberMe);
                    user = db.Users.Where(x => x.Username == model.Username).FirstOrDefault();
                    Session["Name"] = model.Username;
                    Session["UserID"] = user.Id;
                    Session["LoginType"] = user.FirstName;

                    return Redirect(FormsAuthentication.GetRedirectUrl(model.Username, model.RememberMe));

                    //return RedirectToAction("Index", "Demo");
                    //if (db.Users.Any(x => x.Username.Equals("admin")))
                    //{
                    //    //return RedirectToAction("AdminDashboard", "Dashboard");

                    //}
                    //else
                    //{
                    //    return RedirectToAction("UserDashboard", "Dashboard");
                    //}

                }



            }
        }

        [HttpGet]
        [ActionName("user-profile")]
        [Authorize]
        public ActionResult UserProfile()
        {
            // Get username
            string username = User.Identity.Name;

            // Declare model
            UserProfileVM model;

            using (Db db = new Db())
            {
                // Get user
                UserDTO dto = db.Users.FirstOrDefault(x => x.Username == username);

                // Build model
                model = new UserProfileVM(dto);
            }

            // Return view with model
            return View("UserProfile", model);
        }

        // POST: /account/user-profile
        [HttpPost]
        [ActionName("user-profile")]
        [Authorize]
        public ActionResult UserProfile(UserProfileVM model)
        {
            // Check model state
            if (!ModelState.IsValid)
            {
                return View("UserProfile", model);
            }

            // Check if passwords match if need be
            if (!string.IsNullOrWhiteSpace(model.Password))
            {
                if (!model.Password.Equals(model.ConfirmPassword))
                {
                    ModelState.AddModelError("", "Passwords do not match.");
                    return View("UserProfile", model);
                }
            }

            using (Db db = new Db())
            {
                // Get username
                string username = User.Identity.Name;

                // Make sure username is unique
                if (db.Users.Where(x => x.Id != model.Id).Any(x => x.Username == username))
                {
                    ModelState.AddModelError("", "Username " + model.Username + " already exists.");
                    model.Username = "";
                    return View("UserProfile", model);
                }


                //Save In User Profile

                #region User Profile


                // Edit DTO
                UserDTO dto = db.Users.Find(model.Id);

                dto.FirstName = model.FirstName;
                dto.LastName = model.LastName;
                dto.EmailAddress = model.EmailAddress;
                dto.Username = model.Username;

                if (!string.IsNullOrWhiteSpace(model.Password))
                {
                    dto.Password = model.Password;
                }

                // Save
                db.SaveChanges();

                #endregion

            }

            // Set TempData message
            TempData["SM"] = "You have edited your profile!";

            // Redirect
            return Redirect("~/Demo/user-profile");
        }

        [ActionName("create-account")]
        [HttpGet]
        public ActionResult CreateAccount()
        {
            return View("CreateAccount");
        }

        // POST: /account/create-account
        [ActionName("create-account")]
        [HttpPost]
        public ActionResult CreateAccount(UserVM model)
        {
            // Check model state
            if (!ModelState.IsValid)
            {
                return View("CreateAccount", model);
            }

            // Check if passwords match
            if (!model.Password.Equals(model.ConfirmPassword))
            {
                ModelState.AddModelError("", "Passwords do not match.");
                return View("CreateAccount", model);
            }

            using (Db db = new Db())
            {
                // Make sure username is unique
                if (db.Users.Any(x => x.Username.Equals(model.Username)))
                {
                    ModelState.AddModelError("", "Username " + model.Username + " is taken.");
                    model.Username = "";
                    return View("CreateAccount", model);
                }

                // Create userDTO
                UserDTO userDTO = new UserDTO()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    EmailAddress = model.EmailAddress,
                    Username = model.Username,
                    Password = model.Password
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


                //Add to Member

                MemberRegDTO memdto = new MemberRegDTO();

                memdto.MemberFName = model.FirstName;
                memdto.MemberLName = model.LastName;
                memdto.EmailID = model.EmailAddress;
                memdto.Contactno = model.Contactno;

                db.Members.Add(memdto);
                db.SaveChanges();

            }

            // Create a TempData message
            TempData["SM"] = "You are now registered and can login.";

            // Redirect
            return Redirect("~/Demo/login");
        }


        // GET: /account/Logout
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("~/Demo/login");
        }


        [Authorize]
        public ActionResult UserNavPartial()
        {
            // Get username
            string username = User.Identity.Name;

            // Declare model
            UserNavPartialVM model;

            using (Db db = new Db())
            {
                // Get the user
                UserDTO dto = db.Users.FirstOrDefault(x => x.Username == username);

                // Build the model
                model = new UserNavPartialVM()
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName
                };
            }

            // Return partial view with model
            return PartialView(model);
        }

        public ActionResult ALLUSERS()
        {
            List<UserDTO> listOfUser;

            using (Db db = new Db())
            {
                listOfUser = db.Users.ToArray().ToList();
            }

            return View(listOfUser);
        }

    }
}
