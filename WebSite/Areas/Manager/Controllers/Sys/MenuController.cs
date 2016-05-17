using Lumos.Common;
using Lumos.DAL.AuthorizeRelay;
using Lumos.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebSite.Areas.Manager.Models.Menu;

namespace WebSite.Areas.Manager.Controllers
{
    [ManagerAuthorize(PermissionCode.MenuManagement)]
    public class MenuController : ManagerController
    {

        public ActionResult Index()
        {
            MenuModel mode = new MenuModel();
            var identity = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            List<SysPermission> list = identity.GetPermissionList(new PermissionCode());
            mode.SysPermission = list;
            return View(mode);
        }

        public ActionResult Add()
        {
            MenuModel mode = new MenuModel();
            var identity = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            List<SysPermission> list = identity.GetPermissionList(new PermissionCode());

            mode.SysPermission = list;
            return View(mode);
        }

        public ActionResult Sort()
        {
            return View();
        }



        public JsonResult GetDetail(int menuId)
        {
            MenuModel model = new MenuModel();
            SysMenu menu = CurrentDb.SysMenu.Where(u => u.Id == menuId).FirstOrDefault();
            model.SysMenuPermission = CurrentDb.SysMenuPermission.Where(u => u.MenuId == menuId).ToList();
            model.Id = menu.Id;
            model.Name = menu.Name;
            model.Description = menu.Description;
            model.Url = menu.Url;
            return Json(ResultType.Success, model);
        }

        public JsonResult GetTree(int pId)
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


        [HttpPost]
        [NoResubmit]
        [ValidateAntiForgeryToken]
        public JsonResult Add(MenuModel model)
        {
            var identity = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            int pId = model.PId;
            SysMenu menu = new SysMenu();
            menu.PId = pId;
            menu.Name = model.Name;
            menu.Url = model.Url;
            menu.Description = model.Description;
            identity.CreateMenu(menu, model.Permission);
            return Json(ResultType.Success, ManagerOperateTipUtils.ADD_SUCCESS);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Update(MenuModel model)
        {
            int menuId = model.Id;
            SysMenu menu = CurrentDb.SysMenu.Find(menuId);
            menu.Name = model.Name;
            menu.Url = model.Url;
            menu.Description = model.Description;
            var identityManager = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            identityManager.UpdateMenu(menu, model.Permission);
            return Json(ResultType.Success, ManagerOperateTipUtils.UPDATE_SUCCESS);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Delete(int[] id)
        {
            var identity = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);

            foreach (int menuId in id)
            {
                identity.DeleteMenu(menuId);
            }
            return Json(ResultType.Success, ManagerOperateTipUtils.DELETE_SUCCESS);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult UpdateSort()
        {

            for (int i = 0; i < Request.Form.Count; i++)
            {
                string name = Request.Form.AllKeys[i].ToString();
                if (name.IndexOf("menuId") > -1)
                {
                    int id = int.Parse(name.Split('_')[1].Trim());
                    int priority = int.Parse(Request.Form[i].Trim());
                    SysMenu model = CurrentDb.SysMenu.Where(m => m.Id == id).FirstOrDefault();
                    if (model != null)
                    {
                        model.Priority = priority;
                        CurrentDb.SaveChanges();
                    }
                }
            }
            return Json(ResultType.Success, ManagerOperateTipUtils.UPDATE_SUCCESS);

        }

    }
}