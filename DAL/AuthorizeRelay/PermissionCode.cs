using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lumos.DAL.AuthorizeRelay;

namespace System
{

    /// <summary>
    /// 权限代码
    /// </summary>
    public class PermissionCode
    {
        public const string None = "1";

        #region 用户管理
        public const string 用户管理 = "10000";
        [PermissionCode(PId = PermissionCode.用户管理)]

        public const string 用户查询 = "10001";

        [PermissionCode(PId = PermissionCode.用户管理)]

        public const string 用户新增 = "10002";

        [PermissionCode(PId = PermissionCode.用户管理)]

        public const string 用户修改 = "10003";

        [PermissionCode(PId = PermissionCode.用户管理)]
        public const string 用户删除 = "10004";
        #endregion 用户管理

        #region 角色管理
        public const string 角色管理 = "2000";

        [PermissionCode(PId = PermissionCode.角色管理)]

        public const string 角色新增 = "20001";

        [PermissionCode(PId = PermissionCode.角色管理)]

        public const string 角色修改 = "20002";

        [PermissionCode(PId = PermissionCode.角色管理)]
        public const string 角色删除 = "20003";

        [PermissionCode(PId = PermissionCode.角色管理)]
        public const string 角色用户选择 = "20004";

        [PermissionCode(PId = PermissionCode.角色管理)]
        public const string 角色菜单选择 = "20005";

        [PermissionCode(PId = PermissionCode.角色管理)]
        public const string 角色权限选择 = "20006";
        #endregion 角色管理

        #region  菜单管理
        public const string 菜单管理 = "30000";

        [PermissionCode(PId = PermissionCode.菜单管理)]

        public const string 菜单查询 = "30001";

        [PermissionCode(PId = PermissionCode.菜单管理)]

        public const string 菜单新增 = "30002";

        [PermissionCode(PId = PermissionCode.菜单管理)]

        public const string 菜单修改 = "30003";

        [PermissionCode(PId = PermissionCode.菜单管理)]
        public const string 菜单删除 = "30004";
        #endregion 菜单管理

 

        public const string 测试 = "300222";

    }
}
