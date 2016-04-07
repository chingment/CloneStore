using log4net;
using Lumos.Common;
using Lumos.DAL.AuthorizeRelay;
using Lumos.Entity;
using Lumos.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace WebSite.Controllers
{
    public class AccountController : WebSiteController
    {
        //
        // GET: /Account/
        public ViewResult SignIn()
        {
            return View();
        }

        public ViewResult SignUp()
        {
            return View();
        }

        public ViewResult MyCart()
        {
            MyCartViewModel model = new MyCartViewModel();

            if (Request.Cookies[CommonSetting.CartProductsCookiesName] != null)
            {
                string strCartProducts = System.Web.HttpUtility.UrlDecode(Request.Cookies[CommonSetting.CartProductsCookiesName].Value.ToString());
                List<Product> products = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Product>>(strCartProducts);
                model.Clothes = products.Where(m => m.Category == "Clothes").ToList();
                model.Pants = products.Where(m => m.Category == "Pants").ToList();
                model.Shoes = products.Where(m => m.Category == "Shoes").ToList();
            }

            return View(model);
        }


        [HttpPost]
        public JsonResult SignUp(RegisterModel model)
        {
            SysClientUser user = new SysClientUser();
            user.UserName = model.txt_UserName;
            user.PasswordHash = model.txt_Password;
            user.FirstName = model.txt_FirstName;
            user.LastName = model.txt_LastName;
            user.FavoriteColors = model.cb_Colors == null ? null : string.Join(",", model.cb_Colors);
            user.FavoriteRetailers = model.cb_Retailers == null ? null : string.Join(",", model.cb_Retailers);
            user.Address = model.txt_Address;
            user.CreateTime = DateTime.Now;
            user.Creator = 0;
            var relay = new AspNetIdentiyAuthorizeRelay<SysClientUser>(CurrentDb);

            if (relay.UserExists(user.UserName.Trim()))
            {
                return Json("text/html", ResultType.Failure, "This account already exists");
            }

            bool r = relay.CreateUser(user, user.PasswordHash);
            if (!r)
            {
                return Json("text/html", ResultType.Failure, "Failure");
            }

            CurrentDb.SaveChanges();


            relay.SignIn(model.txt_UserName, model.txt_Password, false);



            return Json(ResultType.Success, "Success");
        }


        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public JsonResult SignIn(LoginModel model)
        {
            var identity = new AspNetIdentiyAuthorizeRelay<SysClientUser>(CurrentDb);
            var user = identity.SignIn(model.txt_UserName, model.txt_Password, model.ckb_RememberMe);
            CustomJsonResult result = new CustomJsonResult();
            GoToViewModel gotoViewModel = new GoToViewModel();
            int userId = 0;
            string userName = model.txt_UserName;
            if (user != null)
            {
                userId = user.Id;
                userName = user.UserName;
                if (!user.IsDelete)
                {
                    if (user.IsDisable)
                    {
                        result.Result = ResultType.Failure;
                        result.Message = "The account has been disabled";
                        gotoViewModel.GotoUrl = @"/Home/Login";
                    }
                    else
                    {
                        result.Result = ResultType.Success;
                        result.Message = "Success";
                        gotoViewModel.GotoUrl = @"/Home/Index";
                    }
                }
                else
                {
                    result.Result = ResultType.Failure;
                    result.Message = "Account or password is incorrect. Please try again!";
                    gotoViewModel.GotoUrl = @"/Home/Login";
                }
            }
            else
            {
                result.Result = ResultType.Failure;
                result.Message = "Account or password is incorrect. Please try again!";
                gotoViewModel.GotoUrl = @"/Home/Login";
            }

            result.Content = gotoViewModel;

            ILog log = LogManager.GetLogger(CommonSetting.LoggerLoginWeb);
            log.Info(FormatUtility.LoginInWeb(userId, userName, result.Message));

            return result;
        }

        /// <summary>
        /// 退出方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignOut()
        {
            ILog log = LogManager.GetLogger(CommonSetting.LoggerLoginWeb);
            log.Info(FormatUtility.LoginOffWeb(User.Identity.GetUserId<int>(), User.Identity.GetUserName()));
            var identity = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            identity.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}