using Lumos.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Models;
using Lumos.Common;
using System.Linq.Expressions;
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
        public ActionResult List(int? id = 1)
        {
            ProductViewModel model = new ProductViewModel();
            model.Products = CurrentDb.Product.Where(m => m.Retailer == id).ToList();
            //where x.Field<string>("fieldname").IndexOf("char")>0

            // Expression  d=Expression.ArrayIndex
            //string k=",White,";
            //var a = from c in CurrentDb.Product where k.Contains(c.Colors) select c; 

            string k = ",White,";
            var a = from c in CurrentDb.Product where c.Colors.ToString().Contains(k) select c;

            //var a = from c in CurrentDb.Product where c.Colors.IndexOf("Gray")>0 select c;
            var b = a.ToList();
            if (Request.Cookies[CommonSetting.CartProductsCookiesName] != null)
            {
                string strCartProducts = System.Web.HttpUtility.UrlDecode(Request.Cookies[CommonSetting.CartProductsCookiesName].Value.ToString());
                List<Product> cartProducts = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Product>>(strCartProducts);
                cartProducts = cartProducts.OrderByDescending(c => c.CreateTime).ToList();
                model.CartProducts = cartProducts;
            }


            return View(model);
        }

        public JsonResult GetList(string Retailer, string[] Category, string[] Color, string[] Material)
        {

            var b = "";
            var sql = "select * from Product";
            sql += " where 1=1 ";
            if (Category != null)
            {
                if (!Category.Contains("0"))
                {
                    string s = " and Category in (";
                    for (var i = 0; i < Category.Length; i++)
                    {
                        s += "'" + Category[i] + "',";
                    }
                    s = s.Substring(0, s.Length - 1);
                    s += ")";
                    sql += s;
                }
            }


            if (Color != null)
            {
                string s = " ";
                if (Color.Length > 0)
                {
                    s += " and (";
                    for (var i = 0; i < Color.Length; i++)
                    {
                        s += " charindex('" + Color[i] + "',Colors)>0 or";
                    }

                    s = s.Substring(0, s.Length - 2);
                    s += ")";
                }

                sql += s;
            }



            List<Product> products = CurrentDb.Database.SqlQuery<Product>(sql).ToList();
            return Json(ResultType.Success, products);
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