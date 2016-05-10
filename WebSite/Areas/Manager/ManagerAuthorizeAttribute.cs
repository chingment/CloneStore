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
                bool IsHasPermission = HttpContext.Current.User.Identity.IsInPermission(permissions);

                if (!IsHasPermission)
                {
                    bool isAjaxRequest = filterContext.RequestContext.HttpContext.Request.IsAjaxRequest();
                    if (isAjaxRequest)
                    {
                        CustomJsonResult jsonResult = new CustomJsonResult(ResultType.Failure, "您没有对应的操作权限!");
                        jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                        filterContext.Result = jsonResult;
                        return;
                    }
                    else
                    {
                        filterContext.RequestContext.HttpContext.Response.Write("<div style=\"text-align:center;\">您没有对应的操作权限!</div>");
                        filterContext.RequestContext.HttpContext.Response.End();
                        return;
                    }
                }
            }
            #endregion
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);

            filterContext.Result = new RedirectResult("/Manager/Home/Login");

        }
    }
    #endregion
}