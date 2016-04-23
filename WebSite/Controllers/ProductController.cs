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
using WebSite.Models.Product;

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
        public ActionResult List(int retailer = 1, string category = null)
        {


            string[] categorys = null;
            if(category != null)
            {
                categorys = new string[1] { category };
            }
    

           ListModel model = new ListModel();

            model.Retailer = retailer;
            model.OmittedProductsPages = 2;
            model.Products = GetProductList(false, retailer, categorys, null, null, model.OmittedProductsPages);
            model.Retailers = CurrentDb.Retailer.Where(m => m.IsDelete == false).OrderByDescending(m => m.Priority).ToList();
            if (Request.Cookies[CommonSetting.CartProductsCookiesName] != null)
            {
                string strCartProducts = System.Web.HttpUtility.UrlDecode(Request.Cookies[CommonSetting.CartProductsCookiesName].Value.ToString());
                List<Product> cartProducts = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Product>>(strCartProducts);
                cartProducts = cartProducts.OrderByDescending(c => c.CreateTime).ToList();
                model.CartProducts = cartProducts;
            }


            return View(model);
        }


        public List<Product> GetProductList(bool IsAppend, int Retailer, string[] Category, string[] Color, string Material, int omittedProductsPages)
        {
            QueryParam qp = new QueryParam();
            qp.TableName = " Product  a ";
            qp.ReturnFields = " * ";
            qp.Orderfld = " RandomNo desc ";
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

            if (Retailer != 0)
            {
                qp.Where += " and RetailerId='" + Retailer + "'";
            }

            if (!string.IsNullOrEmpty(Material))
            {
                qp.Where += " and Materials='" + Material + "'";
            }


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
            return products;
        }

        public JsonResult GetList(bool IsAppend, int Retailer, string[] Category, string[] Color, string Material, int omittedProductsPages)
        {
            List<Product> products = GetProductList(IsAppend, Retailer, Category, Color, Material, omittedProductsPages);
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