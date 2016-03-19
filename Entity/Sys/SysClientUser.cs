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
        /// <summary>
        /// 主营范围
        /// </summary>
        [MaxLength(2)]
        public string MainBizRange { get; set; }

        /// <summary>
        /// 认证帐号类型
        /// </summary>
        public int RealAccountType { get; set; }

        /// <summary>
        /// 真实姓名 或公司名称
        /// </summary>
        [MaxLength(50)]
        public string RealUserName { get; set; }

        /// <summary>
        /// 用户证件类型
        /// </summary>
        public int RealUserIdType { get; set; }

        /// <summary>
        /// 用户证件号码
        /// </summary>
        [MaxLength(50)]
        public string RealUserIdNo { get; set; }

        /// <summary>
        /// 用户证件图片URL
        /// </summary>
        [MaxLength(500)]
        public string RealUserIdPic { get; set; }

        /// <summary>
        /// 真实公司机构代码
        /// </summary>
        [MaxLength(50)]
        public string RealCompanyOrgCode { get; set; }

        /// <summary>
        /// 真实公司法人
        /// </summary>
        [MaxLength(50)]
        public string RealCompanyLegal { get; set; }

        /// <summary>
        /// 真实公司法人证件图片URL
        /// </summary>
        [MaxLength(500)]
        public string RealCompanyLegalIdPic { get; set; }

        /// <summary>
        /// 真实公司营业执照
        /// </summary>
        [MaxLength(500)]
        public string RealCompanyBizPic { get; set; }

        /// <summary>
        /// 真实公司税务执照
        /// </summary>
        [MaxLength(500)]
        public string RealCompanyTaxPic { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [MaxLength(50)]
        public string Contact { get; set; }

        /// <summary>
        /// 联系人性别
        /// </summary>
        public Enumeration.Sex ContactSex { get; set; }
        
        /// <summary>
        /// 联系人手机
        /// </summary>
        [MaxLength(20)]
        public string ContactMoblie { get; set; }

        /// <summary>
        /// 联系人电话号码
        /// </summary>
        [MaxLength(20)]
        public string ContactTel { get; set; }

        /// <summary>
        /// 联系人邮件
        /// </summary>
        [MaxLength(100)]
        public string ContactEmail { get; set; }

        /// <summary>
        /// 联系人QQ
        /// </summary>
        [MaxLength(20)]
        public string ContactQQ { get; set; }

        /// <summary>
        /// 联系人微信
        /// </summary>
        [MaxLength(50)]
        public string ContactWeChat { get; set; }

        /// <summary>
        /// 联系人生日
        /// </summary>
        public DateTime? ContactBirthday { get; set; }

        /// <summary>
        /// 联系人证件类型
        /// </summary>
        public int ContactIdType { get; set; }

        /// <summary>
        /// 联系人证件号码
        /// </summary>
        [MaxLength(50)]
        public string ContactIdNo { get; set; }

        /// <summary>
        /// 联系人证件图片URL
        /// </summary>
        [MaxLength(500)]
        public string ContactIdPic { get; set; }

        /// <summary>
        /// 联系人城市编号
        /// </summary>
        [MaxLength(50)]
        public string ContactAreaCode { get; set; }

        /// <summary>
        /// 联系人城市名称
        /// </summary>
        [MaxLength(50)]
        public string ContactArea { get; set; }

        /// <summary>
        /// 联系人详细地址
        /// </summary>
        [MaxLength(500)]
        public string ContactAddress { get; set; }

        /// <summary>
        /// 联系人保证金比例
        /// </summary>
        public int PremiumScale { get; set; }
    }
}
