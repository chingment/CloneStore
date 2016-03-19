using System.Web;
using System.Web.Mvc;
using WebSite.Areas.Manager;
using WebSite.Areas.Manager.Controllers;

namespace WebSite
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //监控引用
            filters.Add(new ManagerStatisticsTrackerAttribute());
        }
    }
}
