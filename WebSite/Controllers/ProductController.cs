using Lumos.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSite.Models;
using Lumos.Common;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Data;

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



            var query = (from u in CurrentDb.Product
                         where (u.Retailer == id)
                         select u);

            int pageIndex = 2;
            int pageSize = 10;
            query = query.OrderByDescending(r => r.Id).Take(pageIndex * pageSize);

            model.OmittedProductsPages = pageIndex;
            model.Products = query.ToList();

            if (Request.Cookies[CommonSetting.CartProductsCookiesName] != null)
            {
                string strCartProducts = System.Web.HttpUtility.UrlDecode(Request.Cookies[CommonSetting.CartProductsCookiesName].Value.ToString());
                List<Product> cartProducts = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Product>>(strCartProducts);
                cartProducts = cartProducts.OrderByDescending(c => c.CreateTime).ToList();
                model.CartProducts = cartProducts;
            }


            return View(model);
        }

        public JsonResult GetList(bool IsAppend, string Retailer, string[] Category, string[] Color, string[] Material, int omittedProductsPages)
        {




            //var b = "";
            //var sql = "select * from Product";
            //sql += " where 1=1 ";
            //if (Category != null)
            //{
            //    if (!Category.Contains("0"))
            //    {
            //        string s = " and Category in (";
            //        for (var i = 0; i < Category.Length; i++)
            //        {
            //            s += "'" + Category[i] + "',";
            //        }
            //        s = s.Substring(0, s.Length - 1);
            //        s += ")";
            //        sql += s;
            //    }
            //}


            //if (Color != null)
            //{
            //    string s = " ";
            //    if (Color.Length > 0)
            //    {
            //        s += " and (";
            //        for (var i = 0; i < Color.Length; i++)
            //        {
            //            s += " charindex('" + Color[i] + "',Colors)>0 or";
            //        }

            //        s = s.Substring(0, s.Length - 2);
            //        s += ")";
            //    }

            //    sql += s;
            //}



            QueryParam qp = new QueryParam();
            qp.TableName = " Product  a ";
            qp.ReturnFields = " * ";
            qp.Orderfld = " Id desc ";
            qp.PrimaryKey = " Id ";

            if (IsAppend)
            {
                qp.PageIndex = omittedProductsPages + 1;
                qp.PageSize = 10;
            }
            else
            {
                qp.PageIndex = 0;
                qp.PageSize = omittedProductsPages * 10;
            }


            qp.Where = " 1='1' ";

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
                    qp.Where += s;
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
                qp.Where += s;
            }


            List<Product> products = CurrentDb.Database.GetPageReocrdByProc(qp).Tables[0].ToList<Product>();

            //   List<Product> products = CurrentDb.Database.SqlQuery<Product>(sql).ToList();
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