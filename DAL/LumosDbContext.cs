﻿using Lumos.DAL.AuthorizeRelay;
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

    public class LumosDbContext : AuthorizeRelayDbContext
    {

        //public FxDbContext()
        //    : base("DefaultConnection")
        //{
        //   // this.Configuration.ProxyCreationEnabled = false;
        //}

        public IDbSet<Product> Product { get; set; }

        public IDbSet<Retailer> Retailer { get; set; }

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

    public class FxContextDatabaseInitializerForCreateDatabaseIfNotExists : CreateDatabaseIfNotExists<LumosDbContext>
    {
        protected override void Seed(LumosDbContext context)
        {
            //初始用户
            context.SysStaffUser.Add(new SysStaffUser() { Id = 1, UserName = "admin", SecurityStamp = "61c7b4a2-4197-4d32-b9a5-629425fc2000", PasswordHash = "AD5hJcUUIJ4NxikOI2O1ChwVgoGYwPXDxGHp+nSIX8GHEeQw5h0C9mECSFyXeo/kCw==", IsDelete = false, IsDisable = false, IsModifyPwd = false, Creator = 0, CreateTime = DateTime.Now, PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = false, AccessFailedCount = 0, RegisterTime = DateTime.Now });
            context.SysClientUser.Add(new SysClientUser() { Id = 2, UserName = "chingment", SecurityStamp = "61c7b4a2-4197-4d32-b9a5-629425fc2000", PasswordHash = "AD5hJcUUIJ4NxikOI2O1ChwVgoGYwPXDxGHp+nSIX8GHEeQw5h0C9mECSFyXeo/kCw==", IsDelete = false, IsDisable = false, IsModifyPwd = false, Creator = 0, CreateTime = DateTime.Now, PhoneNumberConfirmed = false, TwoFactorEnabled = false, LockoutEnabled = false, AccessFailedCount = 0, RegisterTime = DateTime.Now });

            //初始角色
            context.Roles.Add(new SysRole() { Id = 1, Name = "Root Role", PId = 0, Description = "" });

            //初始角色用户
            context.SysUserRole.Add(new SysUserRole() { UserId = 1, RoleId = 1 });

            List<SysMenu> sysMenus = new List<SysMenu>();
            //初始菜单
            sysMenus.Add(new SysMenu() { Id = 1, Name = "Background System Menu", PId = 0, Url = "#", Description = "", Priority = 0 });
            sysMenus.Add(new SysMenu() { Id = 2, Name = "System Admin", PId = 1, Url = "#", Description = "", Priority = 0 });
            sysMenus.Add(new SysMenu() { Id = 3, Name = "Menus", PId = 2, Url = "/Manager/Sys/Menu/Index", Description = "", Priority = 6 });
            sysMenus.Add(new SysMenu() { Id = 4, Name = "Roles", PId = 2, Url = "/Manager/Sys/Role/Index", Description = "", Priority = 5 });
            sysMenus.Add(new SysMenu() { Id = 5, Name = "Background Users ", PId = 2, Url = "/Manager/Sys/StaffUser/Index", Description = "", Priority = 4 });
            sysMenus.Add(new SysMenu() { Id = 6, Name = "All Users", PId = 2, Url = "/Manager/Sys/User/Index", Description = "", Priority = 4 });
            sysMenus.Add(new SysMenu() { Id = 7, Name = "LogView", PId = 2, Url = "/Manager/Sys/LogView/Index", Description = "", Priority = 3 });


            sysMenus.Add(new SysMenu() { Id = 8, Name = "User Management", PId = 1, Url = "#", Description = "", Priority = 0 });
            sysMenus.Add(new SysMenu() { Id = 9, Name = "User List", PId = 8, Url = "/Manager/Sys/ClientUser/Index", Description = "", Priority = 0 });

            sysMenus.Add(new SysMenu() { Id = 10, Name = "Retailer Management", PId = 1, Url = "#", Description = "", Priority = 0 });
            sysMenus.Add(new SysMenu() { Id = 11, Name = "Retailer List", PId = 10, Url = "/Manager/Biz/Retailer/Index", Description = "", Priority = 0 });

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

            //初始权限
            List<SysPermission> sysPermissions = GetPermissionList(new PermissionCode());
            foreach (var m in sysPermissions)
            {
                context.SysPermission.Add(new SysPermission() { Id = m.Id, PId = m.PId, Name = m.Name });
            }


            context.SysMenuPermission.Add(new SysMenuPermission() { MenuId = 3, PermissionId = PermissionCode.MenuManagement });
            context.SysMenuPermission.Add(new SysMenuPermission() { MenuId = 4, PermissionId = PermissionCode.RoleManagement });
            context.SysMenuPermission.Add(new SysMenuPermission() { MenuId = 5, PermissionId = PermissionCode.UserManagement });
            context.SysMenuPermission.Add(new SysMenuPermission() { MenuId = 10, PermissionId = PermissionCode.ClientManagement });



            List<Retailer> retailers = new List<Retailer>();
            retailers.Add(new Retailer() { Id = 1, Name = "Bonobos", BannerImg = "/images/01.jpg", CreateTime = DateTime.Now, Creator = 0, Priority = 0, IsDelete = false });
            retailers.Add(new Retailer() { Id = 2, Name = "Jcrew", BannerImg = "/images/02.jpg", CreateTime = DateTime.Now, Creator = 0, Priority = 0, IsDelete = false });
            retailers.Add(new Retailer() { Id = 3, Name = "Jack Erwin", BannerImg = "/images/03.jpg", CreateTime = DateTime.Now, Creator = 0, Priority = 0, IsDelete = false });
            retailers.Add(new Retailer() { Id = 4, Name = "Mr Porter", BannerImg = "/images/05.jpg", CreateTime = DateTime.Now, Creator = 0, Priority = 0, IsDelete = false });

            foreach (var m in retailers)
            {
                context.Retailer.Add(m);
            }

            Random r = new Random(unchecked((int)DateTime.Now.Ticks));
            string[] colors = new string[] { "Green", "Black", "Blue", "Orange", "Red", "Gray" };
            string[] materials = new string[] { "Cotton", "Wool", "Silk", "Linen", "Nylon", "Ramine" };



            List<Product> products = new List<Product>();
            for (int i = 0; i < 50; i++)
            {
                int rColorNum1 = r.Next(0, 5);
                int rColorNum2 = r.Next(0, 5);

                int rMaterialNum = r.Next(0, 5);


                int rPicNum = r.Next(0, 9);


                int rNum = r.Next(0, int.MaxValue);

                products.Add(new Product() { Name = "", RetailerId = 1, Category = "Clothes", Materials = materials[rMaterialNum], Colors = "" + colors[rColorNum1] + "," + colors[rColorNum2] + "", Style = "Striped", Price = 1000, Sizes = "S,M,L,XL", SmallImg = "/images/demo/saved/shirt/0" + rPicNum.ToString() + ".jpg", BigImg = "/images/demo/saved/shirt/0" + rPicNum.ToString() + ".jpg", RandomNo = rNum });
            }


            for (int i = 0; i < 50; i++)
            {

                int rColorNum1 = r.Next(0, 5);
                int rColorNum2 = r.Next(0, 5);
                int rMaterialNum = r.Next(0, 5);
                int rPicNum = r.Next(0, 9);


                int rNum = r.Next(0, int.MaxValue);
                products.Add(new Product() { Name = "", RetailerId = 1, Category = "Pants", Materials = materials[rMaterialNum], Colors = "" + colors[rColorNum1] + "," + colors[rColorNum2] + "", Style = "Striped", Price = 2000, Sizes = "30,31,32,33", SmallImg = "/images/demo/saved/pants/0" + rPicNum.ToString() + ".jpg", BigImg = "/images/demo/saved/pants/0" + rPicNum.ToString() + ".jpg", RandomNo = rNum });
            }


            for (int i = 0; i < 50; i++)
            {
                int rColorNum1 = r.Next(0, 5);
                int rColorNum2 = r.Next(0, 5);
                int rMaterialNum = r.Next(0, 5);
                int rPicNum = r.Next(0, 9);


                int rNum = r.Next(int.MaxValue);
                products.Add(new Product() { Id = 21, Name = "", RetailerId = 1, Category = "Shoes", Materials = materials[rMaterialNum], Colors = "" + colors[rColorNum1] + "," + colors[rColorNum2] + "", Style = "Striped", Price = 3000, Sizes = "34,35,36,37", SmallImg = "/images/demo/saved/shoes/0" + rPicNum.ToString() + ".jpg", BigImg = "/images/demo/saved/shoes/0" + rPicNum.ToString() + ".jpg", RandomNo = rNum });
            }


            foreach (var m in products)
            {
                context.Product.Add(m);
            }



            base.Seed(context);
        }

        public List<SysPermission> GetPermissionList(PermissionCode permission)
        {
            Type t = permission.GetType();
            List<SysPermission> list = new List<SysPermission>();
            list = GetPermissionList(t, list);
            return list;
        }


        private List<SysPermission> GetPermissionList(Type t, List<SysPermission> list)
        {
            if (t.Name != "Object")
            {
                System.Reflection.FieldInfo[] properties = t.GetFields();
                foreach (System.Reflection.FieldInfo property in properties)
                {
                    string pId = "0";
                    object[] typeAttributes = property.GetCustomAttributes(false);
                    foreach (PermissionCodeAttribute attribute in typeAttributes)
                    {
                        pId = attribute.PId;
                    }
                    object id = property.GetValue(null);
                    string name = property.Name;
                    SysPermission model = new SysPermission();
                    model.Id = id.ToString();
                    model.Name = name;
                    model.PId = pId;
                    list.Add(model);
                }
                list = GetPermissionList(t.BaseType, list);
            }
            return list;
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
