using MVC.Business.Business;
using MVC.Shared.Common;
using MVC.Models;
using MVC.Shared.Common;
using MVC.Web.Library;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class UserController : Controller
    {
        IUserBusiness buss;
        public UserController(IUserBusiness _buss)
        {
            buss = _buss;
        }

        private static string AddEditId = "110";
        private const string Defaultpassword = "FunOlympic1";

        // GET: User
        public ActionResult Index()
        {
            ViewData["NewBreadCrumb"] = ProjectGrid.GetAddBreadCum("User|User List|", "User", AddEditId);
            return View();
        }
       


        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserModel model)
        {
            StaticData.GetUser();
            if (ModelState.IsValid)
            {
                if (model.Password != model.CPassword)
                {
                    StaticData.SetMessageInSession(1, "The password and confirm passseord doesnot match");
                    ModelState.AddModelError("", "error");
                    return View(model);
                }

                UserCommon common = new UserCommon();
                common.ID = model.UserId;
                common.UserName = model.UserName;
                common.Password = StaticData.Base64Encode(model.Password);
                common.CPassword = StaticData.Base64Encode(model.CPassword);
                var response = buss.Register(common);
                StaticData.SetMessageInSession(response);
                if (response.ErrorCode == 1)
                {
                    ModelState.AddModelError("", response.Message);
                    ViewData["ErrorMessage"] = response.Message;
                    return View(model);
                }
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult ChangeUserPassword(UserModel model)
        {
            try
            {
                if (model.Password != model.CPassword)
                {
                    StaticData.SetMessageInSession(1, "The password and confirm passseord doesnot match");
                    ModelState.AddModelError("", "error");
                    return View(model);
                }
                var id = Request.QueryString["id"];
                var changepwd = StaticData.Base64Encode(model.Password);
                var data = buss.Changepassword(StaticData.GetUser(), changepwd, id);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}