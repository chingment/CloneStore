using Lumos.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Areas.Manager.Models
{
    public class SysMenuViewModel
    {
        public SysMenuViewModel()
        {
            this.Menu = new SysMenu();
            this.Permission = new List<SysPermission>();
            this.MenuPermission = new List<SysMenuPermission>();
        }

        public SysMenu Menu { get; set; }
        public List<SysPermission> Permission { get; set; }
        public List<SysMenuPermission> MenuPermission { get; set; }
    }
}