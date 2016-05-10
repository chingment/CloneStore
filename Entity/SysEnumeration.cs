using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumos.Entity
{

    /// <summary>
    /// 系统的枚举
    /// </summary>
    public partial class Enumeration
    {
        /// <summary>
        /// 验证邮箱的用途
        /// </summary>
        public enum VerifyEmailUsage
        {
            /// <summary>
            /// 未知
            /// </summary>
            Unknow = 0,
            /// <summary>
            ///  注册用户
            /// </summary>
            RegisterAccount = 1,
            /// <summary>
            /// 忘记密码
            /// </summary>
            ForgetPassword = 2
        }

        /// <summary>
        /// 登录类型
        /// </summary>
        public enum LoginType
        {
            /// <summary>
            /// 未知
            /// </summary>
            Unknow = 0,
            /// <summary>
            /// 电脑
            /// </summary>
            Computer = 1,
            /// <summary>
            /// 手机网站
            /// </summary>
            MobileWeb = 2,
            /// <summary>
            /// APP
            /// </summary>
            App = 3
        }

        /// <summary>
        /// 登录结果
        /// </summary>
        public enum LoginResult
        {
            /// <summary>
            /// 未知
            /// </summary>
            Unknow = 0,
            /// <summary>
            /// 成功
            /// </summary>
            Success = 1,
            /// <summary>
            /// 失败
            /// </summary>
            Failure = 2
        }

        public enum LoginResultTip
        {
            Unknow = 0,
            VerifyPass = 1,
            UserNotExist = 2,
            UserPasswordIncorrect = 3,
            UserDisabled = 4,
            UserDeleted = 5
        }


        /// <summary>
        /// 控件类型
        /// </summary>
        public enum InputType
        {
            CheckBox = 0,
            Hidden = 1,
            Password = 2,
            Radio = 3,
            Text = 4,
            Select = 5
        }


        /// <summary>
        /// 是否有效
        /// </summary>
        public enum Disable
        {
            /// <summary>
            /// 是
            /// </summary>
            Yes = 0,
            /// <summary>
            /// 否
            /// </summary>
            No = 1
        }

        /// <summary>
        /// 性别
        /// </summary>
        public enum Sex
        {
            /// <summary>
            /// 未知
            /// </summary>
            Unknow = 0,
            /// <summary>
            /// 男
            /// </summary>
            Male = 1,
            /// <summary>
            /// 女
            /// </summary>
            Female = 2
        }

        /// <summary>
        /// 证件类型
        /// </summary>
        public enum DocumentType
        {
            /// <summary>
            /// 未知
            /// </summary>
            Unknow = 0,
            /// <summary>
            /// 身份证
            /// </summary>
            IdCard = 1,
            /// <summary>
            /// 护照
            /// </summary>
            Passport = 2
        }

        /// <summary>
        /// 操作类型
        /// </summary>
        public enum OpesrationType
        {
            /// <summary>
            /// 未知
            /// </summary>
            Unknow = 0,
            /// <summary>
            /// 新增
            /// </summary>
            New = 1, 
            /// <summary>
            /// 修改
            /// </summary>
            Update = 2,
            /// <summary>
            /// 删除
            /// </summary>
            Delete = 3
        }
    }
}
