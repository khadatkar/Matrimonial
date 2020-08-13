using GYMONE.Models;
using GYMONE.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Web.Security;
using GYMONE.Repository;

namespace GYMONE.Controllers
{
    public class CMSDemoController : Controller
    {
        IPaymentlisting objIPaymentlisting;
        RegisterMemberController objmem = new RegisterMemberController();

        #region multi Filter

        //
        // GET: /CMSDemo/

        //old Index Method

        //public ActionResult Index(int? memberid, int? genderid, int? religionid, int? casteid, int? subcasteid,
        //    int? countryid, int? stateid, int? cityid
        //    )
        //{

        //    List<MemberRegistrationVM> MemberList = null;
        //    string gender1 = "0";
        //    if (genderid != 0 || genderid != null)
        //    {
        //        gender1 = Convert.ToString(genderid);
        //    }
        //    else
        //    {
        //        gender1 = "0";
        //    }

        //    using (Db db = new Db())
        //    {
        //        //Where(x => catId == null || catId == 0 || x.CategoryId == catId)
        //        //MemberList = db.Members.ToArray().Where( x => x.religionid == religionid).Select(x => new MemberRegistrationVM(x)).ToList();

        //        MemberList = db.Members.ToArray().Select(x => new MemberRegistrationVM(x)).ToList();

        //        //religion
        //        if (religionid != 0 && religionid != null)
        //            MemberList = db.Members.ToArray().Where(x => religionid == null || religionid == 0 || x.religionid == religionid).Select(x => new MemberRegistrationVM(x)).ToList();

        //        //gender
        //        if (genderid != 0 && genderid != null)
        //            MemberList = db.Members.ToArray().Where(x1 => gender1 == null || gender1 == "0" || x1.Gender.Equals(gender1)).Select(x => new MemberRegistrationVM(x)).ToList();

        //        //caste
        //        if (casteid != 0 && casteid != null)
        //            MemberList = db.Members.ToArray().Where(x => casteid == null || casteid == 0 || x.casteid == casteid).Select(x => new MemberRegistrationVM(x)).ToList();

        //        //state
        //        if (stateid != 0 && stateid != null)
        //            MemberList = db.Members.ToArray().Where(x => stateid == null || stateid == 0 || x.stateid == stateid).Select(x => new MemberRegistrationVM(x)).ToList();

        //        //city
        //        if (cityid != 0 && cityid != null)
        //            MemberList = db.Members.ToArray().Where(x => cityid == null || cityid == 0 || x.cityid == cityid).Select(x => new MemberRegistrationVM(x)).ToList();

        //        //subcasteid
        //        if (subcasteid != 0 && subcasteid != null)
        //            MemberList = db.Members.ToArray().Where(x => subcasteid == null || subcasteid == 0 || x.subcasteid == subcasteid).Select(x => new MemberRegistrationVM(x)).ToList();
        //    }


        //    if (ViewBag.Gender == null)
        //    {
        //        ViewBag.Gender = new SelectList(obj.BindGender(), "Value", "Text");
        //    }
        //    if (ViewBag.Country == null)
        //        ViewBag.Country = new SelectList(obj.BindListCountry(), "Id", "Country");
        //    if (ViewBag.State == null)
        //        ViewBag.State = new SelectList(obj.BindListState(), "Id", "State");
        //    if (ViewBag.City == null)
        //        ViewBag.City = new SelectList(obj.BindListCity(), "Id", "CityName");
        //    if (ViewBag.Religion == null)
        //        ViewBag.Religion = new SelectList(obj.BindListReligion(), "Id", "Religion");
        //    if (ViewBag.Caste == null)
        //        ViewBag.Caste = new SelectList(obj.BindListCaste(), "Id", "Caste");
        //    if (ViewBag.SubCaste == null)
        //        ViewBag.SubCaste = new SelectList(obj.BindListSubCaste(), "Id", "SubCaste");


        //    ViewBag.Selectedgender = genderid.ToString();

        //    ViewBag.Selectedreligion = religionid.ToString();
        //    ViewBag.Selectedcaste = casteid.ToString();
        //    ViewBag.Selectedsubcaste = subcasteid.ToString();

        //    ViewBag.Selectedcountry = countryid.ToString();
        //    ViewBag.Selectedstate = stateid.ToString();
        //    ViewBag.Selectedcity = cityid.ToString();


        //    return View(MemberList);
        //}

        #endregion

        public ActionResult Index(int? page, int? genderid)
        {
            string username = User.Identity.Name;

            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login", "Demo");


            List<MemberRegistrationVM> MemberListVM;
            List<MemberRegistrationVM> newlistOfMember = new List<MemberRegistrationVM>();

            MemberRegDTO obj;

            using (Db db = new Db())
            {
                int userid = Convert.ToInt32(Session["UserID"]);

                if (userid == 0)
                {
                    FormsAuthentication.SignOut();
                    return RedirectToAction("Login", "Demo");
                }

                obj = db.Members.FirstOrDefault(x => x.userid == userid);
            }


            var pageNumber = page ?? 1;

            using (Db db = new Db())
            {

                if (!username.Equals("Admin"))
                {
                    string gender = null;

                    if (obj != null)
                        gender = Convert.ToString(obj.forgenderid);

                    MemberListVM = db.Members.ToArray().
                        Where(x => x.Gender.Equals(gender) || gender.Equals(null) && x.userid != obj.userid).
                        Select(x => new MemberRegistrationVM(x)).ToList();
                }
                else
                {
                    MemberListVM = db.Members.ToArray().
                        Select(x => new MemberRegistrationVM(x)).ToList();
                }

                foreach (var member in MemberListVM)
                {
                    CityMasterDTO citydto = db.Cities.Where(x => x.Id == member.cityid).FirstOrDefault();
                    CasteDTO castedto = db.Castes.Where(x => x.Id == member.casteid).FirstOrDefault();
                    ReligionDTO religiondto = db.Religions.Where(x => x.Id == member.religionid).FirstOrDefault();

                    //For City
                    if (citydto != null)
                    {
                        member.City = citydto.CityName;
                    }
                    else
                    {
                        member.City = null;
                    }

                    if (castedto != null)
                    {
                        member.caste = castedto.Caste;
                    }
                    else
                    {
                        member.caste = null;
                    }
                    if (religiondto != null)
                    {
                        member.religion = religiondto.Religion;
                    }
                    else
                    {
                        member.religion = null;
                    }

                    newlistOfMember.Add(member);

                }

                // Populate categories select list
                //ViewBag.Gender = new SelectList(obj.BindGender(), "Value", "Text");

                // Set selected category
                //ViewBag.Selectedgender = genderid.ToString();
            }

            // Set pagination
            var onePageOfMembers = newlistOfMember.ToPagedList(pageNumber, 10);
            ViewBag.OnePageOfMembers = onePageOfMembers;

            // Return view with list
            return View(newlistOfMember);
        }

        #region Json Method

        public JsonResult GetState(string CountryId)
        {
            var statedata = objmem.GetState(Convert.ToString(ViewBag.Selectedcountry));
            return Json(statedata, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCity(string stateid)
        {
            //string stateiddata = Convert.ToString(ViewBag.Selectedstate);
            var citydata = objmem.GetCity(stateid);
            return Json(citydata, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCaste(string ReligionId)
        {
            var Castedata = objmem.GetCaste(Convert.ToString(ViewBag.Selectedreligion));
            return Json(Castedata, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetSubCaste(string CasteId)
        {
            var SubCastedata = objmem.GetSubCaste(Convert.ToString(ViewBag.Selectedcaste));
            return Json(SubCastedata, JsonRequestBehavior.AllowGet);
        }
        #endregion

        //[ActionName("member-details")]
        [HttpGet]
        public ActionResult MemberDetails(string ID)
        {
            MemberRegDTO dto;
            MemberRegistrationVM model;
            int memberid = Convert.ToInt32(ID);
            using (Db db = new Db())
            {
                if (!db.Members.Any(x => x.MemID == memberid))
                {
                    return RedirectToAction("Index", "CMSDemo");
                }

                dto = db.Members.Where(x => x.MemID == memberid).FirstOrDefault();

                model = new MemberRegistrationVM(dto);

                int? countryid = dto.countryid;
                int? stateid = dto.stateid;
                int? cityid = dto.cityid;
                int? religionid = dto.religionid;
                int? casteid = dto.casteid;
                int? subcasteid = dto.subcasteid;
                int? qualificationid = dto.highestqualification;

                StateMasterDTO dtostate = db.States.FirstOrDefault(x => x.Id == stateid);
                CityMasterDTO dtocity = db.Cities.FirstOrDefault(x => x.Id == cityid);
                ReligionDTO dtoreligion = db.Religions.FirstOrDefault(x => x.Id == religionid);
                CasteDTO dtocaste = db.Castes.FirstOrDefault(x => x.Id == casteid);
                SubCasteDTO dtosubcaste = db.SubCastes.FirstOrDefault(x => x.Id == subcasteid);
                qualificationDTO dtoqualification = db.qualifications.FirstOrDefault(x => x.Id == qualificationid);


                if (dtoqualification != null)
                {
                    ViewBag.Qualification = dtoqualification.Qulification;
                }

                if (dtostate != null)
                {
                    ViewBag.State = dtostate.State;
                }
                if (dtocity != null)
                {
                    ViewBag.city = dtocity.CityName;
                }
                if (dtoreligion != null)
                    ViewBag.religion = dtoreligion.Religion;
                if (dtocaste != null)
                    ViewBag.caste = dtocaste.Caste;
                if (dtosubcaste != null)
                    ViewBag.subcaste = dtosubcaste.SubCaste;

            }

            model.GalleryImages = Directory.EnumerateFiles(Server.MapPath("~/Images/Uploads/Members/" + memberid + "/Gallery/Thumbs"))
                                                .Select(fn => Path.GetFileName(fn));

            return View("MemberDetails", model);
        }

        //Search Methods From Partial View
        #region Search Section

        public ActionResult MemberByReligion(int? page, int? religionid)
        {
            string username = User.Identity.Name;

            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login", "Demo");


            List<MemberRegistrationVM> MemberListVM;

            MemberRegDTO obj;

            using (Db db = new Db())
            {
                int userid = Convert.ToInt32(Session["UserID"]);

                if (userid == 0)
                {
                    FormsAuthentication.SignOut();
                    return RedirectToAction("Login", "Demo");
                }

                obj = db.Members.FirstOrDefault(x => x.userid == userid);
            }


            var pageNumber = page ?? 1;

            using (Db db = new Db())
            {

                if (!username.Equals("Admin"))
                {
                    string gender = null;

                    if (obj.forgenderid != null)
                        gender = Convert.ToString(obj.forgenderid);

                    MemberListVM = db.Members.ToArray().
                        Where(x => x.Gender.Equals(gender) && x.userid != obj.userid
                        && (x.religionid == religionid || religionid == null || religionid == 0)
                        ).
                        Select(x => new MemberRegistrationVM(x)).ToList();
                }
                else
                {
                    MemberListVM = db.Members.ToArray().Where(x => x.religionid == religionid || religionid == null || religionid == 0).
                        Select(x => new MemberRegistrationVM(x)).ToList();
                }


            }


            ViewBag.Religion = new SelectList(objmem.BindListReligion(), "Id", "Religion");

            ViewBag.Selectedreligion = religionid.ToString();

            // Set pagination
            var onePageOfMembers = MemberListVM.ToPagedList(pageNumber, 10);
            ViewBag.OnePageOfMembers = onePageOfMembers;

            // Return view with list
            return View(MemberListVM);


        }



        public JsonResult GetDatabyMemberNo(string term)
        {
            var list = objIPaymentlisting.ListofMemberNo(term);
            return Json(list, JsonRequestBehavior.AllowGet);
        }



        public ActionResult MemberByID(int? page, string memberid)
        {
            string username = User.Identity.Name;

            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login", "Demo");


            List<MemberRegistrationVM> MemberListVM;

            MemberRegDTO obj;

            using (Db db = new Db())
            {
                int userid = Convert.ToInt32(Session["UserID"]);

                if (userid == 0)
                {
                    FormsAuthentication.SignOut();
                    return RedirectToAction("Login", "Demo");
                }

                obj = db.Members.FirstOrDefault(x => x.userid == userid);
            }


            var pageNumber1 = page ?? 1;

            using (Db db = new Db())
            {

                if (!username.Equals("Admin"))
                {
                    string gender = null;

                    if (obj.forgenderid != null)
                        gender = Convert.ToString(obj.forgenderid);



                    MemberListVM = db.Members.ToArray().
                        Where(x => x.Gender.Equals(gender) && x.userid != obj.userid
                        && (x.MemberNo.Equals(memberid) || memberid == null || memberid == "0")
                        ).
                        Select(x => new MemberRegistrationVM(x)).ToList();
                }
                else
                {
                    MemberListVM = db.Members.ToArray().Where(x => x.MemberNo.Equals(memberid) || memberid == null || memberid == "0").
                        Select(x => new MemberRegistrationVM(x)).ToList();
                }


            }

            if (memberid != null)
            {
                ViewBag.selectedmemberid = memberid.ToString();
            }

            //ViewBag.Religion = new SelectList(objmem.BindListReligion(), "Id", "Religion");

            //ViewBag.Selectedreligion = religionid.ToString();

            // Set pagination
            var onePageOfMembers = MemberListVM.ToPagedList(pageNumber1, 10);
            ViewBag.OnePageOfMembers = onePageOfMembers;

            // Return view with list
            return View(MemberListVM);

            //return View();
        }

        public ActionResult MemberByName(int? page, string name)
        {
            return View();
        }

        public ActionResult MemberByGender(int? page, int? genderid)
        {
            return View();
        }

        public ActionResult EditMemberProfile()
        {

            string username = User.Identity.Name;

            if (string.IsNullOrEmpty(username))
                return RedirectToAction("Login", "Demo");

            MemberRegDTO obj;

            using (Db db = new Db())
            {
                int userid = Convert.ToInt32(Session["UserID"]);

                if (userid == 0)
                {
                    FormsAuthentication.SignOut();
                    return RedirectToAction("Login", "Demo");
                }

                obj = db.Members.FirstOrDefault(x => x.userid == userid);
            }

            string memberid = Convert.ToString(obj.MemID);
            Session["MemberRegID"] = memberid;
            return RedirectToAction("Edit", "RegisterMember");
        }

        #endregion

        [HttpPost]
        public ActionResult MemberDetails(MemberRegistrationVM model, string txtMessage)
        {
            using (Db db = new Db())
            {
                if (Session["UserID"] != null)
                {
                    MessageDTO dto = new MessageDTO();

                    dto.message = txtMessage;
                    dto.senderid = Convert.ToInt32(Session["UserID"]);
                    dto.receiverid = model.MemID;
                    dto.date = DateTime.Now;

                    db.Messages.Add(dto);
                    db.SaveChanges();
                }
                else
                {
                    return RedirectToAction("Index", "CMSDemo");
                }
            }

            return View("MemberDetails", model);
        }

        [HttpPost]
        public void SendMessage(string receiverid, string msg)
        {
            long recid = 0;

            if (receiverid != null || receiverid != "")
            {
                recid = Convert.ToInt64(receiverid);
            }
            MessageDTO msgdto = new MessageDTO();

            string username = User.Identity.Name;

            using (Db db = new Db())
            {
                var q = db.Users.FirstOrDefault(x => x.Username == username);
                int userId = q.Id;

                var mem = db.Members.FirstOrDefault(x => x.userid == userId);

                msgdto.senderid = mem.MemID;
                msgdto.receiverid = recid;
                msgdto.message = msg;
                msgdto.date = DateTime.Now;

                db.Messages.Add(msgdto);
                db.SaveChanges();
            }

        }

        [HttpGet]
        public ActionResult Messages(int? page, int? receiverId)
        {
            var pageNumber = page ?? 1;

            // Declare list of MessageVM

            List<MessageVM> messageList;
            List<MessageVM> newlistOfMessages = new List<MessageVM>();

            using (Db db = new Db())
            {
                // Init the list
                messageList = db.Messages.ToArray().Where(x => receiverId == null || receiverId == 0 || x.receiverid == receiverId).OrderByDescending(x => x.Id).Select(x => new MessageVM(x)).ToList();

                // Populate Receivers select list
                ViewBag.Receivers = new SelectList(db.Members.ToList(), "MemID", "MemberFName");
                ViewBag.SelectedReceiver = receiverId.ToString();

                foreach (var rowdata in messageList)
                {
                    MemberRegDTO senderuser = db.Members.Where(x => x.MemID == rowdata.senderid).FirstOrDefault();
                    MemberRegDTO receiveruser = db.Members.Where(x => x.MemID == rowdata.receiverid).FirstOrDefault();
                    string sender = senderuser.MemberFName + " " + senderuser.MemberLName;
                    string receiver = receiveruser.MemberFName + " " + receiveruser.MemberLName;
                    rowdata.sendername = sender;
                    rowdata.receivername = receiver;
                    newlistOfMessages.Add(rowdata);
                }
            }




            // Set pagination
            var onePageOfMessages = newlistOfMessages.ToPagedList(pageNumber, 10);
            ViewBag.OnePageOfMessages = onePageOfMessages;

            // Return view with list
            return View(newlistOfMessages);

        }

        [HttpGet]
        public ActionResult DeleteMessage(int id)
        {
            // Delete product from DB
            using (Db db = new Db())
            {
                MessageDTO dto = db.Messages.Find(id);
                
                db.Messages.Remove(dto);

                db.SaveChanges();
            }

            // Redirect
            return RedirectToAction("Messages");

        }
    }
}
