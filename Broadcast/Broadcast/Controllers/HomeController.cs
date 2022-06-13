using Broadcast.Business.Business;
using Broadcast.Models;
using Broadcast.Shared.Common;
using Broadcast.Web.Library;
using CaptchaMvc.HtmlHelpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Broadcast.Controllers
{
    public class HomeController : Controller
    {

        IUserBusiness buss;
        public HomeController(IUserBusiness _buss)
        {
            buss = _buss;
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginModel model)
        {
            string ip = Request.ServerVariables["REMOTE_ADDR"]
                    , browser = Request.Browser.Browser + " Version :" + Request.Browser.Version;

            var login = new LoginCommon
            {
                UserName = model.UserName,
                Password = StaticData.Base64Encode(model.Password),
                Ip = ip,
                BrowserInfo = browser
            };
            var resp = buss.UserLogin(login);
            if (resp.Code == "0")
            {
                //if (resp.ForcePwdChange == true)
                //{
                //    UserModel user = new UserModel();
                //    user.UserName = model.UserName;
                //    return RedirectToAction("ChangePassword", "Home");
                //}
                Session["UserName"] = model.UserName;
                Session["sysDate"] = StaticData.DBToFrontDate(System.DateTime.Now.ToShortDateString());
                return RedirectToAction("Index", "Dashboard");
            }
            ViewData["msg"] = resp.Msg;
            return View(model);
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost] 
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserModel model) 
        {
            try
            {
                if (!this.IsCaptchaValid("Captcha is not valid"))
                {
                    return View();
                }
                else
                {
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
                        //common.Password = model.Password;

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
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }
        [HttpGet]
        public ActionResult LogOff()
        {
            StaticData.CheckSession();
            //WebSecurity.Logout();
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult ForcePasswordChange(UserModel model)
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

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(UserModel model)
        {
            try
            {
                StaticData.CheckSession();
                var user = StaticData.GetUser();
                if (model.Password != model.CPassword)
                {
                    StaticData.SetMessageInSession(1, "The password and confirm passseord doesnot match");
                    ModelState.AddModelError("", "error");
                    return View(model);
                }
                var id = Request.QueryString["id"];
                var oldpassword = StaticData.Base64Encode(model.OldPassword);
                var changepwd = StaticData.Base64Encode(model.Password);
                var ConfirmPassowrd = StaticData.Base64Encode(model.CPassword);
                var data = buss.Changepassword(StaticData.GetUser(), changepwd, id);
                if(data.ErrorCode == 1)
                {
                    ViewData["msg"] = data.Message;
                    return View(model);
                }
                return RedirectToAction("Index", "Home"); 
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
    }
}