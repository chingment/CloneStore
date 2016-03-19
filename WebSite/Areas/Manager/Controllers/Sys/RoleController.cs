using Lumos.Common;
using Lumos.DAL;
using Lumos.DAL.AuthorizeRelay;
using Lumos.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace WebSite.Areas.Manager.Controllers
{
    public class RoleController : ManagerBaseController
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

        public ViewResult RoleUser()
        {
            return View();
        }

        public ViewResult RoleUserSel()
        {
            return View();
        }

        public ViewResult RoleMenu()
        {
            return View();
        }

        public ViewResult RolePermission()
        {
            return View();
        }
        #endregion

        #region 方法
        /// <summary>
        /// 获取角色的树形列表格式是ZTreeJson
        /// </summary>
        /// <returns></returns>
        public JsonResult GetRoleTree()
        {
            object json = ConvertToZTreeJson(CurrentDb.Roles.ToArray(), "id", "pid", "name", "role");
            return Json(ResultType.Success, json);
        }

        /// <summary>
        /// 根据角色id获取信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public JsonResult GetRoleDetail(int roleId)
        {
            var identityManager = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            SysRole role = identityManager.RoleManager.FindById(roleId);
            return Json(ResultType.Success, role);
        }

        /// <summary>
        /// 获取角色的用户列表
        /// </summary>
        /// <param name="fc"></param>
        /// <returns></returns>
        public JsonResult GetRoleUserList(int roleId)
        {
            string userName = Request.Form["txt_UserName"].Trim();
            string realName = Request.Form["txt_RealName"].Trim();


            var list = (from ur in CurrentDb.SysUserRole
                        join r in CurrentDb.Roles on ur.RoleId equals r.Id
                        join u in CurrentDb.SysStaffUser on ur.UserId equals u.Id
                        where ur.RoleId == roleId &&
                            (userName.Length == 0 || u.UserName.Contains(userName)) &&
                            (realName.Length == 0 || u.RealName.Contains(realName)) &&
                              u.IsDelete == false
                        select new { ur.UserId, u.RealName, u.UserName, ur.RoleId, r.Name }).Distinct();

            int total = list.Count();

            int pageIndex = int.Parse(Request.Form["pageindex"].ToString());
            int pageSize = 10;
            list = list.OrderBy(r => r.UserId).Skip(pageSize * (pageIndex)).Take(pageSize);

            PageEntity pageEntity = new PageEntity { PageSize = pageSize, TotalRecord = total, Rows = list };

            return Json(ResultType.Success, pageEntity);
        }

        /// <summary>
        /// 获取未绑定到该角色的用户列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public JsonResult GetNotBindUsers(int roleId)
        {
            string userName = Request.Form["txt_UserName"].Trim();
            string realName = Request.Form["txt_RealName"].Trim();

            var list = (from u in CurrentDb.SysStaffUser
                        where !(from d in CurrentDb.SysUserRole

                                where d.RoleId == roleId
                                select d.UserId).Contains(u.Id)

                        where
                                               (userName.Length == 0 || u.UserName.Contains(userName)) &&
                                               (realName.Length == 0 || u.RealName.Contains(realName)) &&
                                                u.IsDelete == false
                        select new { u.Id, u.RealName, u.UserName }).Distinct();

            int total = list.Count();

            int pageIndex = int.Parse(Request.Form["pageindex"].ToString());
            int pageSize = 10;
            list = list.OrderBy(r => r.Id).Skip(pageSize * (pageIndex)).Take(pageSize);

            PageEntity pageEntity = new PageEntity { PageSize = pageSize, TotalRecord = total, Rows = list };

            return Json(ResultType.Success, pageEntity);
        }

        /// <summary>
        /// 添加用户到该角色
        /// </summary>
        /// <param name="fc"></param>
        /// <returns></returns>
        public JsonResult AddRoleUser(int roleId, string userIds)
        {
            if (!string.IsNullOrWhiteSpace(userIds))
            {
                int[] arrUserIds = userIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();
                foreach (int userId in arrUserIds)
                {
                    var identityManager = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
                    identityManager.AddUserToRole(userId, roleId);
                }
            }
            return Json(ResultType.Success, "Success");
        }

        /// <summary>
        /// 移除角色用户
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="userIds"></param>
        /// <returns></returns>
        public JsonResult RemoveRoleUser(int roleId, string userIds)
        {
            if (!string.IsNullOrWhiteSpace(userIds))
            {
                int[] arrUserIds = userIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();
                foreach (int userId in arrUserIds)
                {
                    var identityManager = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
                    identityManager.RemoveUserFromRole(userId, roleId);
                }
            }

            return Json(ResultType.Success, "Success");
        }

        /// <summary>
        /// 获取角色菜单
        /// </summary>
        /// <param name="fc"></param>
        /// <returns></returns>
        public JsonResult GetRoleMenuTreeList(int roleId)
        {

            var identity = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            var roleMenus = identity.GetRoleMenus(roleId);
            var isCheckedIds = from p in roleMenus select p.Id;

            object json = ConvertToZTreeJson(CurrentDb.SysMenu.OrderByDescending(m => m.Priority).ToArray(), isCheckedIds.ToArray(), "id", "pid", "name", "menu");

            return Json(ResultType.Success, json);
        }

        /// <summary>
        /// 保存角色所选择的菜单
        /// </summary>
        /// <param name="fc"></param>
        /// <returns></returns>
        public JsonResult SaveRoleMenu(int roleId, string menuIds)
        {
            var identityManager = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            int[] arrMenuIds = menuIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();

            identityManager.SaveRoleMenu(roleId, arrMenuIds);

            return Json(ResultType.Success, "Success");
        }

        public JsonResult GetRolePermissionTreeList(int roleId)
        {

            List<string> checkedPermissions = CurrentDb.SysRolePermission.Where(r => r.RoleId == roleId).Select(p => p.PermissionId).ToList();
            var identity = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            object json = ConvertToZTreeJson(identity.GetPermissionList(new PermissionCode()).ToArray(), checkedPermissions.ToArray(), "id", "pid", "name", "opfun");

            return Json(ResultType.Success, json);
        }

        /// <summary>
        /// 保存角色所选择权限
        /// </summary>
        /// <param name="fc"></param>
        /// <returns></returns>
        public JsonResult SaveRolePermission(int roleId, string permissionIds)
        {
            var identityManager = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            string[] arrPermussionIds = permissionIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            identityManager.SaveRolePermission(roleId, arrPermussionIds);
            return Json(ResultType.Success, "Success");
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="fc"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Create(FormCollection fc)
        {
            string pId = fc["pId"].Trim();
            string txt_RoleName = fc["txt_Name"].Trim();
            string txt_Description = fc["txt_Description"].ToString();

            var identityManager = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            if (!identityManager.RoleExists(txt_RoleName))
            {
                SysRole role = new SysRole();
                role.PId = int.Parse(pId);
                role.Name = txt_RoleName;
                role.Description = txt_Description;
                identityManager.CreateRole(role);
                return Json(ResultType.Success, "Success");
            }
            else
            {
                return Json(ResultType.Failure, "The role is exist");
            }
        }

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="fc"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Update(FormCollection fc)
        {
            int roleId = int.Parse(fc["roleId"].Trim());
            string txt_RoleName = fc["txt_Name"].Trim();
            string txt_Description = fc["txt_Description"].ToString();

            var identityManager = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);

            SysRole role = identityManager.RoleManager.FindById(roleId);
            role.Name = txt_RoleName;
            role.Description = txt_Description;

            identityManager.UpdateRole(role);
            return Json(ResultType.Success, "Success");
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="fc"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Delete(FormCollection fc)
        {
            int[] roleIds = fc["roleIds"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();
            var identityManager = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            foreach (int roleId in roleIds)
            {
                identityManager.DeleteRole(roleId);
            }
            return Json(ResultType.Success, "Success");
        }

        #endregion
    }
}