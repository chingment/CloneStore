using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.AspNet.Identity;
using System.Reflection;
using log4net;
using Lumos.Common;
using System.Globalization;
using Lumos.Mvc;

namespace WebSite.Areas.Manager
{
    #region 授权过滤器
    // 摘要:
    //     继承Authorize属性
    //     扩展Permission权限代码,用来控制用户是否拥有该类或方法的权限
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class ManagerAuthorizeAttribute : AuthorizeAttribute
    {
        public ManagerAuthorizeAttribute(params string[] permissions)
        {
            if (permissions != null)
            {
                if (permissions.Length > 0)
                {
                    this.permissions = permissions;
                }
            }

        }

        /// <summary>
        /// 权限代码
        /// </summary>
        private string[] permissions { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);


            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true) || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true);
            if (skipAuthorization)
            {
                return;
            }


            #region 判断是否有该权限
            if (permissions != null)
            {

                MessageBoxModel messageBox = new MessageBoxModel();
                messageBox.No = Guid.NewGuid().ToString();
                messageBox.Type = MessageBoxTip.Exception;
                messageBox.Title = "You do not have permission to access the possible link timeout ";

                if (!filterContext.HttpContext.Request.IsAuthenticated)
                {
                    messageBox.Content = "Please re <a href=\"javascript:void(0)\" onclick=\"window.top.location.href='" + ManagerUtils.GetLoginPage() + "'\">sigin</a>";
                }

                bool IsHasPermission = HttpContext.Current.User.Identity.IsInPermission(permissions);

                if (!IsHasPermission)
                {
                    bool isAjaxRequest = filterContext.RequestContext.HttpContext.Request.IsAjaxRequest();
                    if (isAjaxRequest)
                    {
                        CustomJsonResult jsonResult = new CustomJsonResult(ResultType.Exception, messageBox.No, messageBox.Title, messageBox);
                        jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                        filterContext.Result = jsonResult;
                        filterContext.Result.ExecuteResult(filterContext);
                        filterContext.HttpContext.Response.End();
                        return;
                    }
                    else
                    {
                        string masterName = "_LayoutHome";
                        if (filterContext.HttpContext.Request.QueryString["dialogtitle"] != null)
                        {
                            masterName = "_Layout";
                        }

                        filterContext.Result = new ViewResult { ViewName = "MessageBox", MasterName = masterName, ViewData = new ViewDataDictionary { Model = messageBox } };
                        return;
                    }
                }
            }
            #endregion
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);

            filterContext.Result = new RedirectResult(ManagerUtils.GetLoginPage());

        }
    }
    #endregion
}