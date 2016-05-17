using Lumos.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Areas.Manager.Models.Menu
{
    public class MenuModel
    {
        public int Id { get; set; }

        public int PId { get; set; }

        public string Name { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string[] Permission { get; set; }

        public MenuModel()
        {
            this.SysPermission = new List<SysPermission>();
            this.SysMenuPermission = new List<SysMenuPermission>();
        }

        public List<SysPermission> SysPermission { get; set; }
        public List<SysMenuPermission> SysMenuPermission { get; set; }
    }
}