using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Models;
using WebSite.Models.Home;

namespace WebSite.Controllers
{
    public class HomeController : WebSiteController
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            System.Web.HttpContext.Current.Response.Cookies.Remove("cartProducts");
            var cartProducts = Request.Cookies["cartProducts"];
            if (cartProducts != null)
            {
                cartProducts.Expires = DateTime.Now.AddHours(-48);
                Response.Cookies.Add(cartProducts);
            }

            IndexModel model = new IndexModel();
            model.Retailers = CurrentDb.Retailer.Where(m => m.IsDelete == false).OrderByDescending(m => m.Priority).ToList();

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult AboutUs()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ContactUs()
        {
            return View();
        }

    }
}