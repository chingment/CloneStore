using log4net;
using Lumos.Common;
using Lumos.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Lumos.Mvc;

namespace WebSite.Controllers
{
    public class WebBaseController : BaseController
    {


        public WebBaseController()
        {
            _currentDb = new FxDbContext();
        }



        private FxDbContext _currentDb;
        public FxDbContext CurrentDb
        {
            get
            {
                return _currentDb;
            }
        }

        protected ILog Log
        {
            get
            {
                return LogManager.GetLogger(this.GetType());
            }
        }

        protected static ILog GetLog(Type t)
        {
            return LogManager.GetLogger(t);
        }

        protected static ILog GetLog()
        {
            //return LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //有问题，子类调用，返回的还是父类的logger

            var trace = new System.Diagnostics.StackTrace();
            Type baseType = typeof(BaseController);
            for (int i = 0; i < trace.FrameCount; i++)
            {
                var frame = trace.GetFrame(i);
                var method = frame.GetMethod();
                var type = method.DeclaringType;
                if (type.IsSubclassOf(baseType)) return GetLog(type);
            }
            return LogManager.GetLogger(baseType);
        }

        protected void SetTrackID()
        {
            if (ThreadContext.Properties["trackid"] == null)
                ThreadContext.Properties["trackid"] = DateTime.Now.TimeOfDay.TotalMilliseconds.ToString("00000000"); //Guid.NewGuid().ToString("N");
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            ILog log = LogManager.GetLogger(CommonSetting.LoggerAccessWeb);
            log.Info(FormatUtility.AccessWeb(User.Identity.GetUserId<int>(), User.Identity.GetUserName()));
        }

 
    }
}