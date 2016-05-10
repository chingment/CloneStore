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
using WebSite.Models.Account;

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
            MyCartModel model = new MyCartModel();
            model.Retailers = CurrentDb.Retailer.Where(m => m.IsDelete == false).OrderByDescending(m => m.Priority).ToList();
            if (Request.Cookies[CommonSetting.CartProductsCookiesName] != null)
            {
                string strCartProducts = System.Web.HttpUtility.UrlDecode(Request.Cookies[CommonSetting.CartProductsCookiesName].Value.ToString());
                List<Product> products = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Product>>(strCartProducts);
                model.Clothes = products.Where(m => m.Category == "Clothes").OrderByDescending(c => c.CreateTime).ToList();
                model.Pants = products.Where(m => m.Category == "Pants").OrderByDescending(c => c.CreateTime).ToList();
                model.Shoes = products.Where(m => m.Category == "Shoes").OrderByDescending(c => c.CreateTime).ToList();
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


            SignIn(model.txt_UserName, model.txt_Password, false);



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

            var signInResult = SignIn(model.txt_UserName, model.txt_Password, model.ckb_RememberMe);
            CustomJsonResult result = new CustomJsonResult();
            GoToViewModel gotoViewModel = new GoToViewModel();


            if (signInResult.ResultType == Enumeration.LoginResult.Failure)
            {
                gotoViewModel.GotoUrl = @"/Account/SignIn";
                if (signInResult.ResultTip == Enumeration.LoginResultTip.UserNotExist || signInResult.ResultTip == Enumeration.LoginResultTip.UserPasswordIncorrect)
                {
                    return Json(ResultType.Failure, gotoViewModel, "Account or password is incorrect. Please try again!");
                }

                if (signInResult.ResultTip == Enumeration.LoginResultTip.UserDisabled)
                {
                    gotoViewModel.GotoUrl = @"/Account/SignIn";
                    return Json(ResultType.Failure, gotoViewModel, "The account has been disabled");
                }

                if (signInResult.ResultTip == Enumeration.LoginResultTip.UserDeleted)
                {
                    gotoViewModel.GotoUrl = @"/Account/SignIn";
                    return Json(ResultType.Failure, gotoViewModel, "The account has been deleted ");
                }
            }


            gotoViewModel.GotoUrl = @"/Home/Index";



            return Json(ResultType.Success, gotoViewModel); ;
        }

        private LoginResult<SysClientUser> SignIn(string username, string password, bool isrememberme)
        {
            string loginIp = "113.108.198.138";


            SysUserLoginHistory userLoginHistory = new SysUserLoginHistory();
            userLoginHistory.Ip = loginIp;
            //userLoginHistory.Country = ipInfo.Country;
            //userLoginHistory.Province = ipInfo.Province;
            //userLoginHistory.City = ipInfo.City;
            userLoginHistory.LoginType = Enumeration.LoginType.Computer;
            var identity = new AspNetIdentiyAuthorizeRelay<SysClientUser>(CurrentDb);

            var result = identity.Login(username, password, isrememberme, userLoginHistory);


            if (result.User != null)
            {
                ILog log = LogManager.GetLogger(CommonSetting.LoggerLoginWeb);
                log.Info(FormatUtility.LoginInWeb(result.User.Id, result.User.UserName));
            }

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