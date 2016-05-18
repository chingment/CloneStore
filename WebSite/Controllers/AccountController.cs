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

        public List<string> GetColors()
        {
            List<string> colors = new List<string>();
            colors.Add("Red");
            colors.Add("Green");
            colors.Add("Black");
            colors.Add("Orange");
            colors.Add("Pink");
            colors.Add("White");
            colors.Add("Gray");
            colors.Add("Blue");
            return colors;
        }

        //
        // GET: /Account/
        public ViewResult SignIn()
        {
            return View();
        }

        public ViewResult SignUp()
        {
            SignUpViewModel model = new SignUpViewModel();
            var retailers = CurrentDb.Retailer.Where(m => m.IsDelete == false).OrderByDescending(m => m.Priority).ToList();
            if (retailers != null)
            {
                model.Retailers = retailers;
            }

            model.Colors = GetColors();

            return View(model);
        }

        public ViewResult MyCart()
        {
            MyCartViewModel model = new MyCartViewModel();
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
        public JsonResult SignUp(SignUpViewModel model)
        {
            SysClientUser user = new SysClientUser();
            user.UserName = model.UserName;
            user.PasswordHash = model.Password;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.FavoriteColors = model.Colors == null ? null : string.Join(",", model.Colors);
            // user.FavoriteRetailers = model.Retailers == null ? null : string.Join(",", model.Retailers);
            user.Address = model.Address;
            var relay = new AspNetIdentiyAuthorizeRelay<SysClientUser>(CurrentDb);

            if (relay.UserExists(user.UserName.Trim()))
            {
                return Json("text/html", ResultType.Failure, "This account already exists");
            }

            bool r = relay.CreateUser(user);
            if (!r)
            {
                return Json("text/html", ResultType.Failure, "Failure");
            }

            CurrentDb.SaveChanges();


            SignIn(model.UserName, model.Password, false);



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
        public JsonResult SignIn(SignInViewModel model)
        {

            var signInResult = SignIn(model.UserName, model.Password, model.IsRememberMe);
            CustomJsonResult result = new CustomJsonResult();
            GoToViewModel gotoViewModel = new GoToViewModel();


            if (signInResult.ResultType == Enumeration.LoginResult.Failure)
            {
                gotoViewModel.Url = @"/Account/SignIn";
                if (signInResult.ResultTip == Enumeration.LoginResultTip.UserNotExist || signInResult.ResultTip == Enumeration.LoginResultTip.UserPasswordIncorrect)
                {
                    return Json(ResultType.Failure, gotoViewModel, "Account or password is incorrect. Please try again!");
                }

                if (signInResult.ResultTip == Enumeration.LoginResultTip.UserDisabled)
                {
                    gotoViewModel.Url = @"/Account/SignIn";
                    return Json(ResultType.Failure, gotoViewModel, "The account has been disabled");
                }

                if (signInResult.ResultTip == Enumeration.LoginResultTip.UserDeleted)
                {
                    gotoViewModel.Url = @"/Account/SignIn";
                    return Json(ResultType.Failure, gotoViewModel, "The account has been deleted ");
                }
            }


            gotoViewModel.Url = @"/Home/Index";



            return Json(ResultType.Success, gotoViewModel); ;
        }

        private LoginResult<SysClientUser> SignIn(string username, string password, bool isrememberme)
        {
            string loginIp =CommonUtils.GetIP();


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
                log.Info(FormatUtils.LoginInWeb(result.User.Id, result.User.UserName));
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
            log.Info(FormatUtils.LoginOffWeb(User.Identity.GetUserId<int>(), User.Identity.GetUserName()));
            var identity = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            identity.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}