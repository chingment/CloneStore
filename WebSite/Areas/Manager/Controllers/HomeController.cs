using Lumos.Common;
using Lumos.DAL.AuthorizeRelay;
using Lumos.Entity;
using log4net;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using WebSite.Areas.Manager.Models.Home;
using Lumos.Mvc;

namespace WebSite.Areas.Manager.Controllers
{

    public class HomeController : ManagerController
    {

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Main()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            Session["ManagerLoginVerifyCode"] = null;
            if (Request.IsAuthenticated)
            {
                return Redirect(ManagerUtils.GetHomePage());
            }

            return View();
        }


        public ViewResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [CheckVerifyCode("ManagerLoginVerifyCode")]
        public JsonResult Login(LoginModel model)
        {
            GoToViewModel gotoViewModel = new GoToViewModel();
            gotoViewModel.Url = ManagerUtils.GetLoginPage();
            var result = SignIn(model.UserName, model.Password, model.IsRememberMe);


            if (result.ResultType == Enumeration.LoginResult.Failure)
            {

                if (result.ResultTip == Enumeration.LoginResultTip.UserNotExist || result.ResultTip == Enumeration.LoginResultTip.UserPasswordIncorrect)
                {
                    return Json(ResultType.Failure, gotoViewModel, ManagerOperateTipUtils.LOGIN_USERNAMEORPASSWORDINCORRECT);
                }

                if (result.ResultTip == Enumeration.LoginResultTip.UserDisabled)
                {
                    return Json(ResultType.Failure, gotoViewModel, ManagerOperateTipUtils.LOGIN_ACCOUNT_DISABLED);
                }

                if (result.ResultTip == Enumeration.LoginResultTip.UserDeleted)
                {
                    return Json(ResultType.Failure, gotoViewModel, ManagerOperateTipUtils.LOGIN_ACCOUNT_DELETE);
                }
            }

            gotoViewModel.Url = ManagerUtils.GetHomePage();
            return Json(ResultType.Success, gotoViewModel, ManagerOperateTipUtils.LOGIN_SUCCESS);

        }



        private LoginResult<SysStaffUser> SignIn(string username, string password, bool isrememberme)
        {

            SysUserLoginHistory userLoginHistory = new SysUserLoginHistory();
            userLoginHistory.Ip = CommonUtils.GetIP();
            //userLoginHistory.Country = ipInfo.Country;
            //userLoginHistory.Province = ipInfo.Province;
            //userLoginHistory.City = ipInfo.City;
            userLoginHistory.LoginType = Enumeration.LoginType.Computer;
            var identity = new AspNetIdentiyAuthorizeRelay<SysStaffUser>(CurrentDb);
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
        public ActionResult LogOff()
        {
            ILog log = LogManager.GetLogger(CommonSetting.LoggerLoginWeb);
            log.Info(FormatUtils.LoginOffWeb(User.Identity.GetUserId<int>(), User.Identity.GetUserName()));
            var identity = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            identity.SignOut();
            return Redirect(ManagerUtils.GetLoginPage());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ChangePassword(ChangePasswordModel model)
        {
            string oldPassword = model.OldPassword;
            string newPassword = model.NewPassword;
            var authorizeRelay = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            bool result = authorizeRelay.ChangePassword(User.Identity.GetUserId<int>(), oldPassword, newPassword);

            if (!result)
            {
                return Json(ResultType.Failure, ManagerOperateTipUtils.CHANGEPASSWORD_OLDPASSWORDINCORRECT);
            }



            if (Request.IsAuthenticated)
            {
                authorizeRelay.SignOut();
            }


            return Json(ResultType.Success, "Please re sigin,click <a href=\"" + ManagerUtils.GetLoginPage() + "\">sigin</a>");

        }

    }
}