using Lumos.Common;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Areas.Manager
{
    #region 基类BaseController异常过滤器
    /// <summary>
    /// 控制方法异常扑捉,跳转页面,记录信息
    /// </summary>
    public class ManagerBaseExceptionAttribute : FilterAttribute, IExceptionFilter
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

            if (isAjaxRequest)
            {
                //异步调用处理方式,返回JSON字符串

                string exceptionName = filterContext.Exception.GetType().Name;
                switch (exceptionName)
                {
                    case "HttpRequestValidationException":
                        this._Message = "输入的字符不符合规范！";
                        break;
                }

                CustomJsonResult jsonResult = new CustomJsonResult(ResultType.Exception, this._Message);
                jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                filterContext.Result = jsonResult;
                filterContext.Result.ExecuteResult(filterContext);
                filterContext.HttpContext.Response.End();

                filterContext.ExceptionHandled = true;//标识错误已经处理，Application_Error 不在捕捉该错误
            }
            else
            {
                //非异步调用处理方式,返回文本

                //输出文本方式
                string stackTrace = string.Format("{0}:{1}<br/>{2}<br/>{3}", controller, action, this.Message, filterContext.Exception.StackTrace);
                filterContext.RequestContext.HttpContext.Response.Write(stackTrace);

                //跳转到指定的URL方式
                //filterContext.Result = new RedirectResult(this.ErrorUrl);
                //filterContext.Result.ExecuteResult(filterContext);

                filterContext.ExceptionHandled = true;//标识错误已经处理，Application_Error 不在捕捉该错误
            }
            log.Error(this._Message, filterContext.Exception);

        }
    }

    #endregion
}