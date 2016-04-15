using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lumos.Entity
{
    [Table("SysMenu")]
    public class SysMenu
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        ///菜单名称
        /// </summary>
        [MaxLength(128)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 菜单父ID
        /// </summary>
        public int PId { get; set; }

        /// <summary>
        /// 菜单URL
        /// </summary>
        [MaxLength(256)]
        public string Url { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(512)]
        public string Description { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }


    }
}
