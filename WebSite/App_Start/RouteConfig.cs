using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebSite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



            routes.MapRoute(
    name: "Default",
    url: "{controller}/{action}/{id}",
    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
    namespaces: new string[] { "WebSite.Controllers" }
);

            routes.MapRoute(
name: "ProductList",
url: "{controller}/{action}/{retailer}/{category}",
defaults: new { controller = "Product", action = "List", retailer = "0", category = "0" },
namespaces: new string[] { "WebSite.Controllers" }
);




        }
    }
}
