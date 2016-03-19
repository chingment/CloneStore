using Lumos.DAL.AuthorizeRelay;
using Lumos.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lumos.DAL
{

    public class FxDbContext : AuthorizeRelayDbContext
    {

        //public FxDbContext()
        //    : base("DefaultConnection")
        //{
        //   // this.Configuration.ProxyCreationEnabled = false;
        //}


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }


    //public class FxContextDatabaseInitializerForDropCreateDatabaseAlways : DropCreateDatabaseAlways<FxDbContext>
    //{
    //    protected override void Seed(FxDbContext context)
    //    {
    //        //初始用户
    //        context.SysStaffUser.Add(new SysStaffUser() { Id = 1, UserName = "admin", SecurityStamp="61c7b4a2-4197-4d32-b9a5-629425fc2000",PasswordHash="AD5hJcUUIJ4NxikOI2O1ChwVgoGYwPXDxGHp+nSIX8GHEeQw5h0C9mECSFyXeo/kCw==", IsDelete = false, IsDisable = false, IsModifyPwd = false, Creator = 0, CreateTime = DateTime.Now, IsSetSecurityPwd = false, PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = false, AccessFailedCount = 0 });

    //        //初始角色
    //        context.Roles.Add(new SysRole() { Id = 1, Name = "系统角色", PId = 0, Description = "" });

    //        //初始角色用户
    //        context.SysUserRole.Add(new SysUserRole() { UserId = 1, RoleId = 1 });

    //        //初始菜单
    //        context.SysMenu.Add(new SysMenu() { Id = 1, Name = "后台系统菜单", PId = 0, Url = "#", Description = "", Priority = 0 });
    //        context.SysMenu.Add(new SysMenu() { Id = 2, Name = "系统设置", PId = 1, Url = "#", Description = "", Priority = 0 });
    //        context.SysMenu.Add(new SysMenu() { Id = 3, Name = "菜单设置", PId = 2, Url = "/Sys/Menu/Index", Description = "", Priority = 6 });
    //        context.SysMenu.Add(new SysMenu() { Id = 4, Name = "角色设置", PId = 2, Url = "/Sys/Role/Index", Description = "", Priority = 5 });
    //        context.SysMenu.Add(new SysMenu() { Id = 5, Name = "用户管理", PId = 2, Url = "/Sys/User/Index", Description = "", Priority = 4 });
    //        context.SysMenu.Add(new SysMenu() { Id = 6, Name = "日志查看", PId = 2, Url = "/LogView/Index", Description = "", Priority = 3 });
    //        context.SysMenu.Add(new SysMenu() { Id = 7, Name = "个人中心", PId = 1, Url = "#", Description = "", Priority = 4 });
    //        context.SysMenu.Add(new SysMenu() { Id = 8, Name = "修改密码", PId = 7, Url = "/PersonalCenter/ChangePassword", Description = "", Priority = 3 });

    //        //初始角色菜单
    //        context.SysRoleMenu.Add(new SysRoleMenu() { MenuId = 1, RoleId = 1 });
    //        context.SysRoleMenu.Add(new SysRoleMenu() { MenuId = 2, RoleId = 1 });
    //        context.SysRoleMenu.Add(new SysRoleMenu() { MenuId = 3, RoleId = 1 });
    //        context.SysRoleMenu.Add(new SysRoleMenu() { MenuId = 4, RoleId = 1 });
    //        context.SysRoleMenu.Add(new SysRoleMenu() { MenuId = 5, RoleId = 1 });
    //        context.SysRoleMenu.Add(new SysRoleMenu() { MenuId = 6, RoleId = 1 });
    //        context.SysRoleMenu.Add(new SysRoleMenu() { MenuId = 7, RoleId = 1 });
    //        context.SysRoleMenu.Add(new SysRoleMenu() { MenuId = 8, RoleId = 1 });

    //        base.Seed(context);
    //    }
    //}

    public class FxContextDatabaseInitializerForCreateDatabaseIfNotExists : CreateDatabaseIfNotExists<FxDbContext>
    {
        protected override void Seed(FxDbContext context)
        {
            //初始用户
            context.SysStaffUser.Add(new SysStaffUser() { Id = 1, UserName = "admin", SecurityStamp = "61c7b4a2-4197-4d32-b9a5-629425fc2000", PasswordHash = "AD5hJcUUIJ4NxikOI2O1ChwVgoGYwPXDxGHp+nSIX8GHEeQw5h0C9mECSFyXeo/kCw==", IsDelete = false, IsDisable = false, IsModifyPwd = false, Creator = 0, CreateTime = DateTime.Now, IsSetSecurityPwd = false, PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = false, AccessFailedCount = 0 });

            //初始角色
            context.Roles.Add(new SysRole() { Id = 1, Name = "Root Role", PId = 0, Description = "" });

            //初始角色用户
            context.SysUserRole.Add(new SysUserRole() { UserId = 1, RoleId = 1 });

            List<SysMenu> sysMenus = new List<SysMenu>();
            //初始菜单
            sysMenus.Add(new SysMenu() { Id = 1, Name = "Background System Menu", PId = 0, Url = "#", Description = "", Priority = 0 });
            sysMenus.Add(new SysMenu() { Id = 2, Name = "System Admin", PId = 1, Url = "#", Description = "", Priority = 0 });
            sysMenus.Add(new SysMenu() { Id = 3, Name = "Menus", PId = 2, Url = "/Sys/Menu/Index", Description = "", Priority = 6 });
            sysMenus.Add(new SysMenu() { Id = 4, Name = "Roles", PId = 2, Url = "/Sys/Role/Index", Description = "", Priority = 5 });
            sysMenus.Add(new SysMenu() { Id = 5, Name = "Users", PId = 2, Url = "/Sys/User/Index", Description = "", Priority = 4 });
            sysMenus.Add(new SysMenu() { Id = 6, Name = "LogView", PId = 2, Url = "/Sys/LogView/Index", Description = "", Priority = 3 });
            sysMenus.Add(new SysMenu() { Id = 7, Name = "My Account", PId = 1, Url = "#", Description = "", Priority = 4 });
            sysMenus.Add(new SysMenu() { Id = 8, Name = "Modify Password", PId = 7, Url = "/PersonalCenter/ChangePassword", Description = "", Priority = 3 });


            //初始菜单
            foreach (var m in sysMenus)
            {
                context.SysMenu.Add(m);
            }

            //初始角色菜单
            foreach (var m in sysMenus)
            {
                context.SysRoleMenu.Add(new SysRoleMenu() { MenuId = m.Id, RoleId = 1 });
            }

            base.Seed(context);
        }
    }

    //public class FxContextDatabaseInitializerForDropCreateDatabaseIfModelChanges : DropCreateDatabaseIfModelChanges<FxDbContext>
    //{
    //    protected override void Seed(FxDbContext context)
    //    {
    //        //初始用户
    //        context.SysStaffUser.Add(new SysStaffUser() { Id = 1, UserName = "admin", SecurityStamp = "61c7b4a2-4197-4d32-b9a5-629425fc2000", PasswordHash = "AD5hJcUUIJ4NxikOI2O1ChwVgoGYwPXDxGHp+nSIX8GHEeQw5h0C9mECSFyXeo/kCw==", IsDelete = false, IsDisable = false, IsModifyPwd = false, Creator = 0, CreateTime = DateTime.Now, IsSetSecurityPwd = false, PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = false, AccessFailedCount = 0 });

    //        //初始角色
    //        context.Roles.Add(new SysRole() { Id = 1, Name = "系统角色", PId = 0, Description = "" });

    //        //初始角色用户
    //        context.SysUserRole.Add(new SysUserRole() { UserId = 1, RoleId = 1 });

    //        //初始菜单
    //        context.SysMenu.Add(new SysMenu() { Id = 1, Name = "后台系统菜单", PId = 0, Url = "#", Description = "", Priority = 0 });
    //        context.SysMenu.Add(new SysMenu() { Id = 2, Name = "系统设置", PId = 1, Url = "#", Description = "", Priority = 0 });
    //        context.SysMenu.Add(new SysMenu() { Id = 3, Name = "菜单设置", PId = 2, Url = "/Sys/Menu/Index", Description = "", Priority = 6 });
    //        context.SysMenu.Add(new SysMenu() { Id = 4, Name = "角色设置", PId = 2, Url = "/Sys/Role/Index", Description = "", Priority = 5 });
    //        context.SysMenu.Add(new SysMenu() { Id = 5, Name = "用户管理", PId = 2, Url = "/Sys/User/Index", Description = "", Priority = 4 });
    //        context.SysMenu.Add(new SysMenu() { Id = 6, Name = "日志查看", PId = 2, Url = "/LogView/Index", Description = "", Priority = 3 });
    //        context.SysMenu.Add(new SysMenu() { Id = 7, Name = "个人中心", PId = 1, Url = "#", Description = "", Priority = 4 });
    //        context.SysMenu.Add(new SysMenu() { Id = 8, Name = "修改密码", PId = 7, Url = "/PersonalCenter/ChangePassword", Description = "", Priority = 3 });

    //        //初始角色菜单
    //        context.SysRoleMenu.Add(new SysRoleMenu() { MenuId = 1, RoleId = 1 });
    //        context.SysRoleMenu.Add(new SysRoleMenu() { MenuId = 2, RoleId = 1 });
    //        context.SysRoleMenu.Add(new SysRoleMenu() { MenuId = 3, RoleId = 1 });
    //        context.SysRoleMenu.Add(new SysRoleMenu() { MenuId = 4, RoleId = 1 });
    //        context.SysRoleMenu.Add(new SysRoleMenu() { MenuId = 5, RoleId = 1 });
    //        context.SysRoleMenu.Add(new SysRoleMenu() { MenuId = 6, RoleId = 1 });
    //        context.SysRoleMenu.Add(new SysRoleMenu() { MenuId = 7, RoleId = 1 });
    //        context.SysRoleMenu.Add(new SysRoleMenu() { MenuId = 8, RoleId = 1 });

    //        base.Seed(context);
    //    }
    //}

}
