using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;


namespace System.Web
{
    public static class HtmlPermissionExtensions
    {
        public static IHtmlString IsInPermission(this HtmlHelper helper, object value, string permission)
        {
            bool isHas = HttpContext.Current.User.Identity.IsInPermission(permission);
            if (isHas)
            {
                return helper.Raw(value);
            }
            else
            {
                return helper.Raw("");
            }
        }

        public static IHtmlString IsInMenu(this HtmlHelper helper, object value, string menuurls)
        {
             return helper.Raw("");
        }
    }
}
