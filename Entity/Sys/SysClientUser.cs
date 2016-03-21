using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumos.Entity
{
    [Table("SysClientUser")]
    public class SysClientUser : SysUser
    {
        public string FavoriteRetailers { get; set; }

        public string FavoriteColors { get; set; }
    }
}
