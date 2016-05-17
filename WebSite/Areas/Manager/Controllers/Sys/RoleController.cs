using Lumos.Common;
using Lumos.DAL.AuthorizeRelay;
using Lumos.Entity;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;
using WebSite.Areas.Manager.Models.Role;

namespace WebSite.Areas.Manager.Controllers
{
    [ManagerAuthorize(PermissionCode.RoleManagement)]
    public class RoleController : ManagerController
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

        public ViewResult AddUserToRole()
        {
            return View();
        }

        #endregion

        #region 方法
        public JsonResult GetRoleTree()
        {
            object json = ConvertToZTreeJson(CurrentDb.Roles.ToArray(), "id", "pid", "name", "role");
            return Json(ResultType.Success, json);
        }


        public JsonResult GetDetail(int roleId)
        {
            var identity = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            SysRole role = identity.RoleManager.FindById(roleId);
            return Json(ResultType.Success, role);
        }

        public JsonResult GetRoleMenuTreeList(int roleId)
        {

            var identity = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            var roleMenus = identity.GetRoleMenus(roleId);
            var isCheckedIds = from p in roleMenus select p.Id;

            object json = ConvertToZTreeJson(CurrentDb.SysMenu.OrderByDescending(m => m.Priority).ToArray(), isCheckedIds.ToArray(), "id", "pid", "name", "menu");

            return Json(ResultType.Success, json);
        }

        public JsonResult GetRoleUserList(RoleUserSearchCondition condition)
        {



            var list = (from ur in CurrentDb.SysUserRole
                        join r in CurrentDb.Roles on ur.RoleId equals r.Id
                        join u in CurrentDb.Users on ur.UserId equals u.Id
                        where ur.RoleId == condition.RoleId &&
                            (condition.UserName == null || u.UserName.Contains(condition.UserName)) &&
                            (condition.FullName == null || u.FullName.Contains(condition.FullName)) &&
                              u.IsDelete == false
                        select new { ur.UserId, ur.RoleId, u.FullName, u.UserName }).Distinct();

            int total = list.Count();

            int pageIndex = condition.PageIndex;
            int pageSize = 10;
            list = list.OrderBy(r => r.UserId).Skip(pageSize * (pageIndex)).Take(pageSize);

            PageEntity pageEntity = new PageEntity { PageSize = pageSize, TotalRecord = total, Rows = list };

            return Json(ResultType.Success, pageEntity);
        }

        public JsonResult GetNotBindUsers(RoleUserSearchCondition condition)
        {

            var list = (from u in CurrentDb.Users
                        where !(from d in CurrentDb.SysUserRole

                                where d.RoleId == condition.RoleId
                                select d.UserId).Contains(u.Id)

                        where
                                             (condition.UserName == null || u.UserName.Contains(condition.UserName)) &&
                            (condition.FullName == null || u.FullName.Contains(condition.FullName)) &&
                                                u.IsDelete == false
                        select new { u.Id, u.FullName, u.UserName }).Distinct();

            int total = list.Count();

            int pageIndex = condition.PageIndex;
            int pageSize = 10;
            list = list.OrderBy(r => r.Id).Skip(pageSize * (pageIndex)).Take(pageSize);

            PageEntity pageEntity = new PageEntity { PageSize = pageSize, TotalRecord = total, Rows = list };

            return Json(ResultType.Success, pageEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AddUserToRole(int roleId, int[] userIds)
        {
            var identityManager = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            foreach (int userId in userIds)
            {
                identityManager.AddUserToRole(roleId,userId);
            }

            return Json(ResultType.Success, ManagerOperateTipUtils.SELECT_SUCCESS);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult RemoveUserFromRole(int roleId, int[] userIds)
        {

            var identityManager = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);

            foreach (int userId in userIds)
            {
                identityManager.RemoveUserFromRole(roleId, userId);
            }

            return Json(ResultType.Success, ManagerOperateTipUtils.REMOVE_SUCCESS);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult SaveRoleMenu(int roleId, int[] menuIds)
        {
            var identity = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            identity.SaveRoleMenu(roleId, menuIds);

            return Json(ResultType.Success, ManagerOperateTipUtils.SAVE_SUCCESS);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Add(RoleModel model)
        {
            var identityManager = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            if (!identityManager.RoleExists(model.Name))
            {
                SysRole role = new SysRole();
                role.PId = model.PId;
                role.Name = model.Name;
                role.Description = model.Description;
                identityManager.CreateRole(role);
                return Json(ResultType.Success, ManagerOperateTipUtils.ADD_SUCCESS);
            }
            else
            {
                return Json(ResultType.Failure, ManagerOperateTipUtils.ROLE_EXISTS);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Update(RoleModel model)
        {

            var identityManager = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);

            SysRole role = identityManager.RoleManager.FindById(model.Id);
            role.Name = model.Name;
            role.Description = model.Description;

            identityManager.UpdateRole(role);
            return Json(ResultType.Success, ManagerOperateTipUtils.UPDATE_SUCCESS);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Delete(int[] id)
        {

            var identityManager = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            foreach (int roleId in id)
            {
                identityManager.DeleteRole(roleId);
            }
            return Json(ResultType.Success, ManagerOperateTipUtils.DELETE_SUCCESS);
        }

        #endregion
    }
}