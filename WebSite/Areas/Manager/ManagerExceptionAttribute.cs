using Lumos.Common;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WebSite.Areas.Manager.Models;
using Lumos.Mvc;

namespace WebSite.Areas.Manager
{
    #region 基类BaseController异常过滤器
    /// <summary>
    /// 控制方法异常扑捉,跳转页面,记录信息
    /// </summary>
    public class ManagerExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        private string _Message = "An exception error occurred";
        public string Message
        {
            get
            {
                return _Message;
            }
            set
            {
                _Message = value;
            }
        }


        private string _ErrorUrl = "~/Error.htm";
        public string ErrorUrl
        {
            get
            {
                return _ErrorUrl;
            }
            set
            {
                _ErrorUrl = value;
            }
        }


        void IExceptionFilter.OnException(ExceptionContext filterContext)
        {
            ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            bool isAjaxRequest = filterContext.RequestContext.HttpContext.Request.IsAjaxRequest();
            string controller = (string)filterContext.RouteData.Values["controller"];
            string action = (string)filterContext.RouteData.Values["action"];


            MessageBoxModel messageBox = new MessageBoxModel();
            messageBox.No = Guid.NewGuid().ToString();
            messageBox.Type = MessageBoxTip.Exception;
            messageBox.Title = "I'm sorry,Access error";
            messageBox.Content = "<a href=\"javascript:void(0)\" onclick=\"window.top.location.href='" + ManagerUtils.GetHomePage() + "'\">Go Home</a>";

            if (ManagerUtils.CanViewErrorStackTrace())
            {
                messageBox.ErrorStackTrace = CommonUtils.ToHtml(filterContext.Exception.Message + "\r\n" + filterContext.Exception.StackTrace);
            }


            //判断是否异步调用
            if (isAjaxRequest)
            {
                CustomJsonResult jsonResult = new CustomJsonResult(ResultType.Exception, messageBox.No, messageBox.Title, messageBox);
                jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                filterContext.Result = jsonResult;
                filterContext.Result.ExecuteResult(filterContext);
                filterContext.HttpContext.Response.End();
            }
            else
            {
                string masterName = "_LayoutHome";
                if (filterContext.HttpContext.Request.QueryString["dialogtitle"] != null)
                {
                    masterName = "_Layout";
                }

                                     
                filterContext.Result = new ViewResult {  ViewName = "MessageBox", MasterName = masterName, ViewData = new ViewDataDictionary { Model = messageBox } };

            }


            filterContext.ExceptionHandled = true;
            log.Error("An exception error occurred [number:" + messageBox.No + "]", filterContext.Exception);


        }
    }

    #endregion
}