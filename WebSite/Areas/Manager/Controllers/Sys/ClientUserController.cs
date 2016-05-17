using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Lumos.Entity;
using Lumos.DAL.AuthorizeRelay;
using Lumos.Common;
using WebSite.Areas.Manager.Models.ClientUser;

namespace WebSite.Areas.Manager.Controllers
{
    [ManagerAuthorize(PermissionCode.ClientManagement)]
    public class ClientUserController : ManagerController
    {
        #region 视图

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Add()
        {
            ClientUserModel model = new ClientUserModel();
            return View();
        }

        public ViewResult Update(int id)
        {
            ClientUserModel model = GetClientUserModel(id);
            return View(model);
        }
        #endregion

        #region 方法
        private ClientUserModel GetClientUserModel(int userId)
        {
            ClientUserModel model = new ClientUserModel();
            SysClientUser user = CurrentDb.SysClientUser.Where(m => m.Id == userId).FirstOrDefault();
            model.Id = user.Id;
            model.UserName = user.UserName;
            model.PhoneNumber = user.PhoneNumber;
            model.Email = user.Email;
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            return model;
        }

        public JsonResult GetList(ClientUserSearchCondition condition)
        {
            var list = (from u in CurrentDb.SysClientUser
                        where (condition.UserName == null || u.UserName.Contains(condition.UserName)) &&
                        (condition.FullName == null || u.FullName.Contains(condition.FullName)) &&
                        (condition.UserName == null || u.UserName.Contains(condition.UserName)) &&
                        u.IsDelete == false
                        select new { u.Id, u.UserName, u.FirstName, u.LastName, u.Email, u.PhoneNumber, u.Address, u.CreateTime, u.IsDelete });

            int total = list.Count();

            int pageIndex = condition.PageIndex;
            int pageSize = 10;
            list = list.OrderByDescending(r => r.CreateTime).Skip(pageSize * (pageIndex)).Take(pageSize);

            PageEntity pageEntity = new PageEntity { PageSize = pageSize, TotalRecord = total, Rows = list };

            return Json(ResultType.Success, pageEntity, "");
        }

        public JsonResult GetUserRoleTree(int userId = 0)
        {

            var isCheckedIds = CurrentDb.SysUserRole.Where(x => x.UserId == userId).Select(x => x.RoleId);
            object json = ConvertToZTreeJson(CurrentDb.Roles.ToArray(), isCheckedIds.ToArray(), "id", "pid", "name", "role");

            return Json(ResultType.Success, json);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Add(ClientUserModel model)
        {

            SysClientUser user = new SysClientUser();
            user.UserName = model.UserName;
            // user.FullName = model.FullName;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PasswordHash = model.Password;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.IsModifyPwd = false;
            user.IsDelete = false;
            user.IsDisable = false;
            user.Creator = User.Identity.GetUserId<int>();
            user.CreateTime = DateTime.Now;
            int[] userRoleIds = model.UserRoleIds;


            var identiy = new AspNetIdentiyAuthorizeRelay<SysClientUser>(CurrentDb);


            if (identiy.UserExists(user.UserName.Trim()))
                return Json(ResultType.Failure, ManagerOperateTipUtils.USER_EXISTS);


            bool r = identiy.CreateUser(user, model.UserRoleIds);
            if (!r)
                return Json(ResultType.Failure, ManagerOperateTipUtils.ADD_FAILURE);



            return Json(ResultType.Success, ManagerOperateTipUtils.ADD_SUCCESS);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Update(ClientUserModel model)
        {

            var identiy = new AspNetIdentiyAuthorizeRelay<SysClientUser>(CurrentDb);
            SysClientUser user = identiy.GetUser(model.Id);

            //user.FullName = model.FullName;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;
            user.Mender = User.Identity.GetUserId<int>();
            user.LastUpdateTime = DateTime.Now;
            int[] userRoleIds = model.UserRoleIds;

            bool r = identiy.UpdateUser(user, model.Password, model.UserRoleIds);
            if (!r)
            {
                return Json(ResultType.Failure, ManagerOperateTipUtils.UPDATE_FAILURE);
            }
            return Json(ResultType.Success, ManagerOperateTipUtils.UPDATE_SUCCESS);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Delete(int[] userIds)
        {

            var identiy = new AspNetIdentiyAuthorizeRelay<SysClientUser>(CurrentDb);

            var reusult = identiy.DeleteUser(userIds);

            if (!reusult)
            {
                return Json(ResultType.Failure, ManagerOperateTipUtils.DELETE_FAILURE);
            }

            return Json(ResultType.Success, ManagerOperateTipUtils.DELETE_SUCCESS);
        }


        #endregion
    }
}