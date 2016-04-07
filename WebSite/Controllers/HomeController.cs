﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            System.Web.HttpContext.Current.Response.Cookies.Remove("cartProducts");
            var cartProducts = Request.Cookies["cartProducts"];
            if (cartProducts != null)
            {
                cartProducts.Expires = DateTime.Now.AddHours(-48);
                Response.Cookies.Add(cartProducts);
            }
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult ContactUs()
        {
            return View();
        }

        public ActionResult Edit(MyModels models)
        {
            var a = models;
            return View();
        }
	}
}