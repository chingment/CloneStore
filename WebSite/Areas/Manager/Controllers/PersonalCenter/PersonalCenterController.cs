using Lumos.Common;
using Lumos.DAL.AuthorizeRelay;
using Lumos.Entity;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Areas.Manager.Controllers
{
    public class PersonalCenterController : ManagerBaseController
    {
        //
        // GET: /PersonalCenter/
        public ViewResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult ChangePassword(FormCollection fc)
        {
            string oldPassword = fc["txt_OldPassword"].Trim();
            string newPassword = fc["txt_NewPassword"].Trim();
            var identity = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            bool result =identity.ChangePassword(User.Identity.GetUserId<int>(), oldPassword, newPassword);
            if (result)
            {
                return Json(ResultType.Success, "Sucees");
            }
            else
            {
                return Json(ResultType.Failure, "The old password is not correct！");
            }
        }
	}
}