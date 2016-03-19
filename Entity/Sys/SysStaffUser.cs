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
    [Table("SysStaffUser")]
    public class SysStaffUser : SysUser
    {
        /// <summary>
        /// 真实姓名
        /// </summary>
        [MaxLength(50)]
        public string RealName { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        [MaxLength(20)]
        public string Tel { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(20)]
        public string Mobile { get; set; }

    }
}
