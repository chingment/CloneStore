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
    public class ClientUserController : ManagerController
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


        public ViewResult Update(int userId)
        {
            return View();
        }

        public ViewResult Details(int id)
        {
            SysClientUser model = CurrentDb.SysClientUser.Find(id);
            return View(model);
        }

        #endregion

        #region 方法
        //获取客户列表
        public JsonResult GetUserList()
        {

            string userName = "";
            string firstName = "";
            string lastName = "";
            if (Request.Form["txt_UserName"] != null)
            {
                userName = Request.Form["txt_UserName"].Trim();
            }
            if (Request.Form["txt_FirstName"] != null)
            {
                firstName = Request.Form["txt_FirstName"].Trim();
            }

            if (Request.Form["txt_LastName"] != null)
            {
                lastName = Request.Form["txt_LastName"].Trim();
            }


            var list = (from u in CurrentDb.SysClientUser
                        where (userName.Length == 0 || u.UserName.Contains(userName)) && (userName.Length == 0 || u.UserName.Contains(userName)) && u.IsDelete == false
                        select new { u.Id, u.UserName, u.FirstName, u.LastName, u.Address, u.IsDisable, u.CreateTime });

            int total = list.Count();

            int pageIndex = int.Parse(Request.Form["pageindex"].ToString());
            int pageSize = 10;
            list = list.OrderByDescending(r => r.CreateTime).Skip(pageSize * (pageIndex)).Take(pageSize);

            PageEntity pageEntity = new PageEntity { PageSize = pageSize, TotalRecord = total, Rows = list };

            return Json(ResultType.Success, pageEntity, "");

        }

        //根据客户ID获取客户信息
        public JsonResult GetUserDetail(int userId)
        {
            var userManager = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            SysUser user = userManager.GetUser(userId);
            return Json(ResultType.Success, user, "");
        }

        //添加客户
        public JsonResult AddUser(FormCollection fc)
        {

            //SysClientUser user = new SysClientUser();
            //user.UserName = fc["txt_UserName"].Trim();
            //user.PasswordHash = CommonSetting.DefaultPassword;
            //user.MainBizRange = fc["ddl_MainBizRange"].Trim();
            //user.IsDisable = fc["rd_IsDisable"].Trim() == "0" ? false : true;
            //user.RealAccountType = (Enumeration.RealAccountType)int.Parse(fc["rd_RealAccountType"].Trim());

            //if (user.RealAccountType == Enumeration.RealAccountType.个人)
            //{
            //    user.RealUserName = fc["txt_RealUserName"].Trim();
            //    user.RealUserIdType = int.Parse(fc["ddl_RealUserIdType"].Trim());
            //    user.RealUserIdNo = fc["txt_RealUserIdNo"].Trim();
            //    user.RealUserIdPic = fc["file_RealUserIdPic_Value"].Trim();
            //    user.RealCompanyOrgCode = "";
            //    user.RealCompanyLegal = "";
            //    user.RealCompanyLegalIdPic = "";
            //    user.RealCompanyTaxPic = "";
            //    user.RealCompanyBizPic = "";
            //}
            //else if (user.RealAccountType == Enumeration.RealAccountType.企业)
            //{
            //    user.RealUserIdType = (int)Enumeration.UserIdType.身份证;
            //    user.RealUserIdNo = "";
            //    user.RealUserIdPic = "";

            //    user.RealUserName = fc["txt_RealCompanyName"].Trim();
            //    user.RealCompanyOrgCode = fc["txt_RealCompanyOrgCode"].Trim();
            //    user.RealCompanyLegal = fc["txt_RealCompanyLegal"].Trim();
            //    user.RealCompanyLegalIdPic = fc["file_RealCompanyLegalIdPic_Value"].Trim();
            //    user.RealCompanyTaxPic = fc["file_RealCompanyTaxPic_Value"].Trim();
            //    user.RealCompanyBizPic = fc["file_RealCompanyBizPic_Value"].Trim();
            //}

            //user.Contact = fc["txt_Contact"].Trim();
            //user.ContactSex = (Enumeration.Sex)int.Parse(fc["ddl_ContactSex"].Trim());
            //user.ContactBirthday = CommonUtility.ConverToShortDateTime(fc["txt_ContactBirthday"].Trim());
            //user.ContactTel = fc["txt_ContactTel"].Trim();
            //user.ContactMoblie = fc["txt_ContactMoblie"].Trim();
            //user.ContactEmail = fc["txt_ContactEmail"].Trim();
            //user.ContactQQ = fc["txt_ContactQQ"].Trim();
            //user.ContactWeChat = fc["txt_ContactWeChat"].Trim();
            //user.ContactIdType = int.Parse(fc["ddl_ContactIdType"].Trim());
            //user.ContactIdNo = fc["txt_ContactIdNo"].Trim();
            //user.ContactIdPic = fc["file_ContactIdPic_Value"].Trim();


            //user.ContactAreaCode = fc["txt_ContactAreaCode"].Trim();
            //user.ContactArea = fc["txt_ContactArea"].Trim() == "请选择地区" ? "" : fc["txt_ContactArea"].Trim();

            //user.ContactAddress = fc["txt_ContactAddress"].Trim();



            //user.IsDelete = false;
            //user.Creator = User.Identity.GetUserId<int>();
            //user.CreateTime = DateTime.Now;
            //user.IsModifyPwd = false;
            //user.IsSetSecurityPwd = false;
            //var userManager = new AspNetIdentiyAuthorizeRelay<SysClientUser>(CurrentDb);

            //if (userManager.UserExists(user.UserName.Trim()))
            //    return Json("text/html", ResultType.Failure, "账号已经存在");

            //bool r = userManager.CreateUser(user, user.PasswordHash);
            //if (!r)
            //    return Json("text/html", ResultType.Failure, "新建失败");

            //CurrentDb.SaveChanges();


            //return Json("text/html", ResultType.Success, "新建成功");
            return Json(ResultType.Success);
        }

        //修改客户
        public JsonResult UpdateUser(FormCollection fc)
        {
            //int userId = int.Parse(Request.QueryString["userId"].Trim());
            //var userManager = new AspNetIdentiyAuthorizeRelay<SysClientUser>(CurrentDb);
            //SysClientUser user = userManager.GetUser(userId);
            //user.MainBizRange = fc["ddl_MainBizRange"].Trim();
            //user.IsDisable = fc["rd_IsDisable"].Trim() == "0" ? false : true;
            //user.RealAccountType = (Enumeration.RealAccountType)int.Parse(fc["rd_RealAccountType"].Trim());
            //HttpPostedFileBase file_upload = null;
            //if (user.RealAccountType == Enumeration.RealAccountType.个人)
            //{
            //    user.RealUserName = fc["txt_RealUserName"].Trim();
            //    user.RealUserIdType = int.Parse(fc["ddl_RealUserIdType"].Trim());
            //    user.RealUserIdNo = fc["txt_RealUserIdNo"].Trim();

            //    file_upload = Request.Files["file_RealUserIdPic"];
            //    if (file_upload != null)
            //        if (file_upload.ContentLength != 0)
            //            user.RealUserIdPic = fc["file_RealUserIdPic_Value"].Trim();


            //    user.RealCompanyOrgCode = "";
            //    user.RealCompanyLegal = "";
            //    user.RealCompanyLegalIdPic = "";
            //    user.RealCompanyTaxPic = "";
            //    user.RealCompanyBizPic = "";


            //}
            //else if (user.RealAccountType == Enumeration.RealAccountType.企业)
            //{
            //    user.RealUserIdType = (int)Enumeration.UserIdType.身份证;
            //    user.RealUserIdNo = "";
            //    user.RealUserIdPic = "";

            //    user.RealUserName = fc["txt_RealCompanyName"].Trim();
            //    user.RealCompanyOrgCode = fc["txt_RealCompanyOrgCode"].Trim();
            //    user.RealCompanyLegal = fc["txt_RealCompanyLegal"].Trim();

            //    file_upload = Request.Files["file_RealCompanyLegalIdPic"];
            //    if (file_upload != null)
            //        if (file_upload.ContentLength != 0)
            //            user.RealCompanyLegalIdPic = fc["file_RealCompanyLegalIdPic_Value"].Trim();

            //    file_upload = Request.Files["file_RealCompanyTaxPic"];
            //    if (file_upload != null)
            //        if (file_upload.ContentLength != 0)
            //            user.RealCompanyTaxPic = fc["file_RealCompanyTaxPic_Value"].Trim();

            //    file_upload = Request.Files["file_RealCompanyBizPic"];
            //    if (file_upload != null)
            //        if (file_upload.ContentLength != 0)
            //            user.RealCompanyBizPic = fc["file_RealCompanyBizPic_Value"].Trim();
            //}

            //user.Contact = fc["txt_Contact"].ToString();
            //user.ContactSex = (Enumeration.Sex)int.Parse(fc["ddl_ContactSex"].Trim());
            //user.ContactBirthday = CommonUtility.ConverToShortDateTime(fc["txt_ContactBirthday"].Trim());
            //user.ContactTel = fc["txt_ContactTel"].Trim();
            //user.ContactMoblie = fc["txt_ContactMoblie"].Trim();
            //user.ContactEmail = fc["txt_ContactEmail"].Trim();
            //user.ContactQQ = fc["txt_ContactQQ"].Trim();
            //user.ContactWeChat = fc["txt_ContactWeChat"].Trim();
            //user.ContactIdType = int.Parse(fc["ddl_ContactIdType"].Trim());
            //user.ContactIdNo = fc["txt_ContactIdNo"].Trim();

            //file_upload = Request.Files["file_ContactIdPic"];
            //if (file_upload != null)
            //    if (file_upload.ContentLength != 0)
            //        user.ContactIdPic = fc["file_ContactIdPic_Value"].Trim();


            //user.ContactAreaCode = fc["txt_ContactAreaCode"].Trim();
            //user.ContactArea = fc["txt_ContactArea"].Trim() == "请选择地区" ? "" : fc["txt_ContactArea"].Trim();

            //user.ContactAddress = fc["txt_ContactAddress"].Trim();



            //user.Mender = User.Identity.GetUserId<int>();
            //user.LastUpdateTime = DateTime.Now;

            //bool r = userManager.UpdateUser(user, "");
            //if (r)
            //{
            //    CurrentDb.SaveChanges();
            //}


            //return Json("text/html", ResultType.Success, "保存成功");
            return Json(ResultType.Success);
        }

        //删除客户
        public JsonResult RemoveUser(FormCollection fc)
        {
            int[] userIds = fc["userIds"].ToString().Replace("'", "").Split(',').Select(s => int.Parse(s)).ToArray(); ;

            var identiy = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            foreach (int userId in userIds)
            {
                identiy.DeleteUser(userId);
            }
            return Json(ResultType.Success, "删除成功");
        }

        //重置客户密码
        public JsonResult ResetPassword(FormCollection fc)
        {
            int userId = int.Parse(fc["userId"].Trim());
            var identiy = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            string resetPwd = CommonSetting.DefaultPassword;
            bool result = identiy.ResetPassword(userId, resetPwd);
            if (result)
            {
                return Json(ResultType.Success, "重置成功");
            }
            else
            {
                return Json(ResultType.Failure, "重置失败");
            }
        }

        //重置客户密码
        public JsonResult ResetSecPassword(FormCollection fc)
        {
            //int userId = int.Parse(fc["userId"].Trim());
            //ClientProvider client = new ClientProvider();
            //CustomJsonResult result =client.ResetSecurityPassword(userId, CommonSetting.DefaultPassword);
            //return result;
            return Json(ResultType.Failure, "重置失败");
        }

        //获取取用户权限
        public JsonResult GetUserRoleTree(FormCollection fc)
        {
            int id = int.Parse(Request.Form["id"].ToString());

            var isCheckedIds = CurrentDb.SysUserRole.Where(x => x.UserId == id).Select(x => x.RoleId);

            object json = ConvertToZTreeJson(CurrentDb.Roles.ToArray(), isCheckedIds.ToArray(), "id", "pid", "name", "role");

            return Json(ResultType.Success, json);
        }
        #endregion
    }
}