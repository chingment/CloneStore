using Lumos.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Models;
using Lumos.Common;
namespace WebSite.Controllers
{
    public class ProductController : WebSiteController
    {
        public static List<T> GetRandomList<T>(List<T> inputList)
        {
            //Copy to a array
            T[] copyArray = new T[inputList.Count];
            inputList.CopyTo(copyArray);

            //Add range
            List<T> copyList = new List<T>();
            copyList.AddRange(copyArray);

            //Set outputList and random
            List<T> outputList = new List<T>();
            Random rd = new Random(DateTime.Now.Millisecond);

            while (copyList.Count > 0)
            {
                //Select an index and item
                int rdIndex = rd.Next(0, copyList.Count - 1);
                T remove = copyList[rdIndex];

                //remove it from copyList and add it to output
                copyList.Remove(remove);
                outputList.Add(remove);
            }
            return outputList;
        }


        //
        // GET: /Product/
        public ActionResult List(int? id=1)
        {
            ProductViewModel model = new ProductViewModel();
            model.Products = GetRandomList(CurrentDb.Product.Where(m => m.Retailer == id).ToList());

            if (Request.Cookies[CommonSetting.CartProductsCookiesName] != null)
            {

                string strCartProducts = System.Web.HttpUtility.UrlDecode(Request.Cookies[CommonSetting.CartProductsCookiesName].Value.ToString());
                model.CartProducts = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Product>>(strCartProducts);
            }


            return View(model);
        }


        public JsonResult SetMyCartCookies(string cartProducts)
        {
            HttpCookie cookie = new HttpCookie(CommonSetting.CartProductsCookiesName);
            cookie.Value = cartProducts;
            cookie.Expires = DateTime.Now.AddDays(7);
            Response.Cookies.Add(cookie);
            return Json(ResultType.Success, "");
        }
    }
}