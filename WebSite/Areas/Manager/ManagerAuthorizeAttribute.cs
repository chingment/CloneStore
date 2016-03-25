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
        /// <summary>
        /// 权限代码
        /// </summary>
        public string Permission { get; set; }

        public bool IsCheckHasMenu { get; set; }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);


            bool skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true)|| filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), inherit: true);
            if (skipAuthorization)
            {
                return;
            }

            //if (HttpContext.Current.Request.IsAuthenticated)
            //{
            //    if (!IsCheckHasMenu)
            //    {
            //        string menuUrl = filterContext.RequestContext.HttpContext.Request.Url.AbsolutePath;
            //        bool IsHasMenu = HttpContext.Current.User.Identity.IsInMenu(menuUrl);
            //        if (!IsHasMenu)
            //        {
            //            bool isAjaxRequest = filterContext.RequestContext.HttpContext.Request.IsAjaxRequest();
            //            if (isAjaxRequest)
            //            {
            //                CustomJsonResult jsonResult = new CustomJsonResult(ResultType.Failure, "您没有访问页面权限!");
            //                jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            //                filterContext.Result = jsonResult;
            //            }
            //            else
            //            {
            //                //MessageBoxModel messbox = new MessageBoxModel();
            //                //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "MessageBox" }));
            //                filterContext.RequestContext.HttpContext.Response.Write("您没有访问页面权限!");
            //                filterContext.RequestContext.HttpContext.Response.End();
            //            }
            //        }
            //    }
            //}

            #region 判断是否有该权限
            if (Permission != null)
            {
                bool IsHasPermission = HttpContext.Current.User.Identity.IsInPermission(Permission);
                if (!IsHasPermission)
                {
                    bool isAjaxRequest = filterContext.RequestContext.HttpContext.Request.IsAjaxRequest();
                    if (isAjaxRequest)
                    {
                        CustomJsonResult jsonResult = new CustomJsonResult(ResultType.Failure, "您没有对应的操作权限!");
                        jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                        filterContext.Result = jsonResult;
                    }
                    else
                    {
                        //MessageBoxModel messbox = new MessageBoxModel();
                        //filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "MessageBox" }));
                        filterContext.RequestContext.HttpContext.Response.Write("您没有对应的操作权限!");
                        filterContext.RequestContext.HttpContext.Response.End();
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