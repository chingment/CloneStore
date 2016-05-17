using System;
using System.Linq;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Lumos.Entity;
using Lumos.DAL.AuthorizeRelay;
using Lumos.Common;
using WebSite.Areas.Manager.Models.Biz.Retailer;

namespace WebSite.Areas.Manager.Controllers
{
    [ManagerAuthorize(PermissionCode.UserManagement)]
    public class RetailerController : ManagerController
    {

        #region 视图

        public ViewResult Index()
        {
            return View();
        }

        //public ViewResult Add()
        //{
        //    UserModel model = new UserModel();
        //    return View();
        //}

        public ViewResult Update(int id)
        {
            RetailerModel model = GetModel(id);
            return View(model);
        }

        //public ViewResult Details(int id)
        //{
        //    RetailerModel model = GetModel(id);
        //    return View(model);
        //}
        #endregion

        #region 方法
        private RetailerModel GetModel(int userId)
        {
            RetailerModel model = new RetailerModel();
            Retailer user = CurrentDb.Retailer.Where(m => m.Id == userId).FirstOrDefault();
            model.Id = user.Id;
            model.Name = user.Name;
            model.BannerImg = user.BannerImg;
            model.Description = user.Description;
            return model;
        }

        public JsonResult GetList(RetailerSearchCondition condition)
        {
            var list = (from u in CurrentDb.Retailer
                        where (condition.Name == null || u.Name.Contains(condition.Name)) &&
                        u.IsDelete == false
                        select new { u.Id, u.Name, u.CreateTime, u.IsDelete });

            int total = list.Count();

            int pageIndex = condition.PageIndex;
            int pageSize = 10;
            list = list.OrderByDescending(r => r.CreateTime).Skip(pageSize * (pageIndex)).Take(pageSize);

            PageEntity pageEntity = new PageEntity { PageSize = pageSize, TotalRecord = total, Rows = list };

            return Json(ResultType.Success, pageEntity, "");
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult Add(UserModel model)
        //{

        //    SysUser user = new SysUser();
        //    user.UserName = model.UserName;
        //    // user.FullName = model.FullName;
        //    user.FirstName = model.FirstName;
        //    user.LastName = model.LastName;
        //    user.PasswordHash = model.Password;
        //    user.Email = model.Email;
        //    user.PhoneNumber = model.PhoneNumber;
        //    user.IsModifyPwd = false;
        //    user.IsDelete = false;
        //    user.IsDisable = false;
        //    user.Creator = User.Identity.GetUserId<int>();
        //    user.CreateTime = DateTime.Now;
        //    int[] userRoleIds = model.UserRoleIds;


        //    var identiy = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);


        //    if (identiy.UserExists(user.UserName.Trim()))
        //        return Json(ResultType.Failure, ManagerOperateTipUtils.USER_EXISTS);


        //    bool r = identiy.CreateUser(user, model.UserRoleIds);
        //    if (!r)
        //        return Json(ResultType.Failure, ManagerOperateTipUtils.ADD_FAILURE);



        //    return Json(ResultType.Success, ManagerOperateTipUtils.ADD_SUCCESS);

        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Update(RetailerModel model)
        {


            Retailer retailer = CurrentDb.Retailer.Where(m => m.Id == model.Id).FirstOrDefault();

            if (retailer != null)
            {
                retailer.BannerImg = model.BannerImg;
                retailer.Description = model.Description;
                retailer.Mender = User.Identity.GetUserId<int>();
                retailer.LastUpdateTime = DateTime.Now;
                CurrentDb.SaveChanges();
            }

            return Json(ResultType.Success, ManagerOperateTipUtils.UPDATE_SUCCESS);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public JsonResult Delete(int[] userIds)
        //{

        //    var identiy = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);

        //    var reusult = identiy.DeleteUser(userIds);

        //    if (!reusult)
        //    {
        //        return Json(ResultType.Failure, ManagerOperateTipUtils.DELETE_FAILURE);
        //    }

        //    return Json(ResultType.Success, ManagerOperateTipUtils.DELETE_SUCCESS);
        //}


        #endregion

    }
}
