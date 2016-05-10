using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Newtonsoft.Json;
using System.Web.UI.WebControls;
using Lumos.Entity;
using Lumos.DAL;
using Lumos.DAL.AuthorizeRelay;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using Newtonsoft.Json.Converters;
using Lumos.Common;

namespace WebSite.Areas.Manager.Controllers
{
    [ManagerAuthorize(PermissionCode.用户管理)]
    public class UserController : ManagerController
    {

        #region 视图

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Add()
        {
            return View();
        }

        public ViewResult Update()
        {
            return View();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetUserList()
        {

            string userName = "";
            string realName = "";
            if (Request.Form["txt_UserName"] != null)
            {
                userName = Request.Form["txt_UserName"].Trim();
            }
            if (Request.Form["txt_RealName"] != null)
            {
                realName = Request.Form["txt_RealName"].Trim();
            }



            var list = (from u in CurrentDb.SysStaffUser
                        where (userName.Length == 0 || u.UserName.Contains(userName)) &&
                            (realName.Length == 0 || u.RealName.Contains(realName)) && u.IsDelete == false
                        select new { u.Id, u.RealName, u.UserName, u.Email, u.Tel, u.Mobile, u.CreateTime, u.IsDelete });

            int total = list.Count();

            int pageIndex = int.Parse(Request.Form["pageindex"].ToString());
            int pageSize = 10;
            list = list.OrderByDescending(r => r.CreateTime).Skip(pageSize * (pageIndex)).Take(pageSize);

            PageEntity pageEntity = new PageEntity { PageSize = pageSize, TotalRecord = total, Rows = list };


            return Json(ResultType.Success, pageEntity, "");
        }

        //获取用户信息
        public JsonResult GetUserDetail(FormCollection fc)
        {
            int id = int.Parse(fc["userId"].Trim());
            var userManager = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            SysUser user = userManager.GetUser(id);
            return Json(ResultType.Success, user);
        }

        //添加用户信息
        public JsonResult AddUser(FormCollection fc)
        {

            SysStaffUser user = new SysStaffUser();
            user.UserName = fc["txt_UserName"].Trim();
            user.RealName = fc["txt_RealName"].Trim();
            user.PasswordHash = fc["txt_Password"].Trim();
            user.Email = fc["txt_Email"].Trim();
            user.Tel = fc["txt_Tel"].Trim();
            user.Mobile = fc["txt_Mobile"].Trim();
            user.IsModifyPwd = false;
            user.IsDelete = false;
            user.IsDisable = false;
            user.Creator = User.Identity.GetUserId<int>();
            user.CreateTime = DateTime.Now;
            int[] roleIds = fc["txt_RoleIds"].Trim().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();


            var userManager = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);


            if (userManager.UserExists(user.UserName.Trim()))
                return Json(ResultType.Failure, "The Account is exist");


            bool r = userManager.CreateUser(user, user.PasswordHash);
            if (!r)
                return Json(ResultType.Failure, "Failure");



            List<SysUserRole> userRoleList = CurrentDb.SysUserRole.Where(m => m.UserId == user.Id).ToList();
            foreach (var userRole in userRoleList)
            {
                CurrentDb.SysUserRole.Remove(userRole);
            }

            foreach (int roleId in roleIds)
            {
                CurrentDb.SysUserRole.Add(new SysUserRole { UserId = user.Id, RoleId = roleId });
            }

            CurrentDb.SaveChanges();

            return Json(ResultType.Success, "Success");

        }

        //修改用户信息
        public JsonResult UpdateUser(FormCollection fc)
        {
            int id = int.Parse(fc["userId"].Trim());
            var userManager = new AspNetIdentiyAuthorizeRelay<SysStaffUser>(CurrentDb);
            SysStaffUser user = userManager.GetUser(id);

            user.RealName = fc["txt_RealName"].Trim();
            user.UserName = fc["txt_UserName"].Trim();
            user.Email = fc["txt_Email"].Trim();
            user.Tel = fc["txt_Tel"].Trim();
            user.Mobile = fc["txt_Mobile"].Trim();
            user.Mender = User.Identity.GetUserId<int>();
            user.LastUpdateTime = DateTime.Now;
            int[] roleIds = fc["txt_RoleIds"].Trim().Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();

            bool r = userManager.UpdateUser(user, fc["txt_Password"].ToString());
            if (r)
            {

                List<SysUserRole> userRoleList = CurrentDb.SysUserRole.Where(m => m.UserId == user.Id).ToList();

                foreach (var userRole in userRoleList)
                {
                    CurrentDb.SysUserRole.Remove(userRole);
                }

                foreach (int roleId in roleIds)
                {
                    CurrentDb.SysUserRole.Add(new SysUserRole { UserId = user.Id, RoleId = roleId });
                }

                CurrentDb.SaveChanges();

                return Json(ResultType.Success, "Success");
            }
            else
            {
                return Json(ResultType.Failure, "Failure");
            }
        }
        //删除用户
        public JsonResult RemoveUser(FormCollection fc)
        {
            int[] userIds = fc["userIds"].ToString().Replace("'", "").Split(',').Select(s => int.Parse(s)).ToArray(); ;

            var identiy = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            foreach (int userId in userIds)
            {
                identiy.DeleteUser(userId);
            }
            return Json(ResultType.Success, "Success");
        }

        /// <summary>
        /// 取用户权限
        /// </summary>
        /// <param name="fc"></param>
        /// <returns></returns>
        public JsonResult GetUserRoleTree(FormCollection fc)
        {
            string s_id = Request.Form["userId"].ToString().Trim();
            int id = s_id == "" ? 0 : int.Parse(s_id);


            //var isCheckedIds = CurrentDb.SysUserRole.Where(x => x.UserId == id).Select(x => x.RoleId);
            //var userId = User.Identity.GetUserId<int>();
            //var userRoleIds = (from q in CurrentDb.SysUserRole where q.UserId == userId select q.RoleId).ToList().Distinct().ToArray();
            //var role = (from q in CurrentDb.Roles where userRoleIds.Contains(q.Id) select q).ToArray();



            var isCheckedIds = CurrentDb.SysUserRole.Where(x => x.UserId == id).Select(x => x.RoleId);
            object json = ConvertToZTreeJson(CurrentDb.Roles.ToArray(), isCheckedIds.ToArray(), "id", "pid", "name", "role");

            return Json(ResultType.Success, json);
        }

        /// <summary>
        /// 保存用户权限
        /// </summary>
        /// <param name="fc"></param>
        /// <returns></returns>
        public JsonResult SaveUserRole(FormCollection fc)
        {
            int userId = int.Parse(Request.Form["userId"].ToString());
            int[] arrRoleIds = fc["roleIds"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();

            List<SysUserRole> userRoleList = CurrentDb.SysUserRole.Where(r => r.UserId == userId).ToList();

            foreach (var userRole in userRoleList)
            {
                CurrentDb.SysUserRole.Remove(userRole);
            }


            foreach (int roleId in arrRoleIds)
            {
                CurrentDb.SysUserRole.Add(new SysUserRole { UserId = userId, RoleId = roleId });
            }

            CurrentDb.SaveChanges();

            return Json(ResultType.Success);
        }

        #endregion

    }
}
