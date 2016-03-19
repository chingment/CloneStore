using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumos.Entity
{

    //角色信息表AspNetRoles
    //通过一个类的继承来扩展IdentityRole的属性对应的表是AspNetRoles表
    //在这里测试 添加了Description属性
       [Table("SysRole")]
    public class SysRole : IdentityRole<int, SysUserRole>
    {
        public SysRole()
        {

        }

        public SysRole(string name) : this() { Name = name; }

        /// <summary>
        /// 描述
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// 父角色ID
        /// </summary>
        public  int PId { get; set; }
    }
}
