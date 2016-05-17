using log4net;
using Lumos.Common;
using Lumos.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class WebSiteExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        void IExceptionFilter.OnException(ExceptionContext filterContext)
        {
            ILog log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
            bool isAjaxRequest = filterContext.RequestContext.HttpContext.Request.IsAjaxRequest();
            string controller = (string)filterContext.RouteData.Values["controller"];
            string action = (string)filterContext.RouteData.Values["action"];


            MessageBoxModel messageBox = new MessageBoxModel();
            messageBox.No = Guid.NewGuid().ToString();
            messageBox.Type = MessageBoxTip.Exception;
            messageBox.Title = "Hmmm... It seems we are unable to process your request";
            messageBox.Content = "<a href=\"/Home/Index\">Return HomePage</a>";
           
            //判断是否异步调用
            if (isAjaxRequest)
            {
                messageBox.IsPopup = true;
                CustomJsonResult jsonResult = new CustomJsonResult(ResultType.Exception, messageBox.No, messageBox.Title, messageBox);
                jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                filterContext.Result = jsonResult;
                filterContext.Result.ExecuteResult(filterContext);
                filterContext.HttpContext.Response.End();
            }
            else
            {
                messageBox.IsPopup = false;
                filterContext.Result = new ViewResult { ViewName = "MessageBox", MasterName = "_LayoutHome", ViewData = new ViewDataDictionary { Model = messageBox } };
            }


            filterContext.ExceptionHandled = true;
            log.Error("发生异常错误[编号:" + messageBox.No + "]", filterContext.Exception);

        }
    }
}