using Lumos.Common;
using Lumos.DAL;
using Lumos.DAL.AuthorizeRelay;
using Lumos.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebSite.Areas.Manager.Controllers
{
    public class MenuController : ManagerBaseController
    {
        //
        // GET: /Menu/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult Sort()
        {
            return View();
        }

        /// <summary>
        /// 获取菜单详细信息
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <returns></returns>
        public JsonResult GetMenuDetail(int menuId)
        {
            SysMenu role = CurrentDb.SysMenu.First(u => u.Id == menuId);
            return Json(ResultType.Success, role);
        }

        /// <summary>
        /// 获取菜单树形列表
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMenuTree(int pId)
        {
            SysMenu[] arr;
            if (pId == 0)
            {
                arr = CurrentDb.SysMenu.OrderByDescending(m => m.Priority).ToArray();
            }
            else
            {
                arr = CurrentDb.SysMenu.Where(m => m.PId == pId).OrderByDescending(m => m.Priority).ToArray();
            }

            object json = ConvertToZTreeJson(arr, "id", "pid", "name", "menu");
            return Json(ResultType.Success, json);
        }


        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="fc"></param>
        /// <returns></returns>
        [HttpPost]
        [NoResubmit]
        public JsonResult AddMenu(FormCollection fc)
        {
            var identityManager = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            int pId = int.Parse(fc["pId"].Trim());
            SysMenu model = new SysMenu();
            model.PId = pId;
            model.Name = fc["txt_Name"].Trim();
            model.Url = fc["txt_Url"].Trim();
            model.Description = fc["txt_Description"].ToString();
            identityManager.CreateMenu(model);
            return Json(ResultType.Success, "Success");

        }

        //[ValidateInput(false)]
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="fc"></param>
        /// <returns></returns>
        public JsonResult UpdateMenu(FormCollection fc)
        {
            int menuId = int.Parse(fc["Id"].Trim());
            SysMenu model = CurrentDb.SysMenu.Find(menuId);
            model.Name = fc["txt_Name"].Trim();
            model.Url = fc["txt_Url"].Trim();
            model.Description = fc["txt_Description"].ToString();
            var identityManager = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            identityManager.UpdateMenu(model);
            return Json(ResultType.Success, "Success");

        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="fc"></param>
        /// <returns></returns>
        public JsonResult RemoveMenu(FormCollection fc)
        {
            var identityManager = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            int[] menuIds = fc["menuIds"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(s => int.Parse(s)).ToArray();
            foreach (int menuId in menuIds)
            {
                identityManager.DeleteMenu(menuId);
            }
            return Json(ResultType.Success, "Success");
        }

        public JsonResult SaveSort()
        {

            for (int i = 0; i < Request.Form.Count; i++)
            {
                string name = Request.Form.AllKeys[i].ToString();
                if (name.IndexOf("menuId") > -1)
                {
                    int id = int.Parse(name.Split('_')[1].Trim());
                    int priority = int.Parse(Request.Form[i].Trim());
                    SysMenu model = CurrentDb.SysMenu.Where(m => m.Id == id).FirstOrDefault();
                    model.Priority = priority;
                    CurrentDb.SaveChanges();
                }
            }
            return Json(ResultType.Success, "Success");

        }

    }
}