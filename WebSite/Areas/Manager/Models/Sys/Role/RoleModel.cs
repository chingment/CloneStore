using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Areas.Manager.Models.Role
{
    public class RoleModel
    {
        public int Id { get; set; }

        public int PId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}