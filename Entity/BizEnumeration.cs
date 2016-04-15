using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumos.Entity
{
    /// <summary>
    /// 业务的枚举
    /// </summary>
    public partial class Enumeration
    {
        public enum WithdrawAccountType
        {
            /// <summary>
            /// 未知
            /// </summary>
            Unknow = 0,
            /// <summary>
            ///  企业
            /// </summary>
            Enterprise = 1,
            /// <summary>
            /// 个人
            /// </summary>
            Personal = 2
        }
    }
}
