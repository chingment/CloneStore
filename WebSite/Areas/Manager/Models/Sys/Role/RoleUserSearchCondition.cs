using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Areas.Manager.Models.Role
{
    public class RoleUserSearchCondition: SearchCondition
    {
        public int RoleId { get; set; }

        public string UserName { get; set; }
        public string FullName { get; set; }
    }
}