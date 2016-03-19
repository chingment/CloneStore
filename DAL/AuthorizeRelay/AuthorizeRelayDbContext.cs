using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lumos.Entity;
namespace Lumos.DAL.AuthorizeRelay
{
    public class AuthorizeRelayDbContext : IdentityDbContext<SysUser, SysRole, int, SysUserLogin,SysUserRole, SysUserClaim>
    {
        public AuthorizeRelayDbContext()
            : base("DefaultConnection")
        {
           // this.Configuration.ProxyCreationEnabled = false;
        }
        public DateTime GetDataBaseDateTime()
        {
            return this.Database.SqlQuery<DateTime>("select getdate()").FirstOrDefault();
        }

        public IDbSet<SysMenu> SysMenu { get; set; }

        public IDbSet<SysPermission> SysPermission { get; set; }

        public IDbSet<SysRoleMenu> SysRoleMenu { get; set; }

        public IDbSet<SysRolePermission> SysRolePermission { get; set; }

        public IDbSet<SysUserRole> SysUserRole { get; set; }

        public IDbSet<SysUserLogin> SysUserLogin { get; set; }

        public IDbSet<SysUserClaim> SysUserClaim { get; set; }

        public IDbSet<SysStaffUser> SysStaffUser { get; set; }

        public IDbSet<SysProvinceCity> SysProvinceCity { get; set; }

        public IDbSet<SysClientUser> SysClientUser { get; set; }

        public AuthorizeRelayDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            // this.Configuration.ProxyCreationEnabled = false;
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //必须继承调用 base.OnModelCreating(modelBuilder);
            base.OnModelCreating(modelBuilder);
            // Identity创建的表是带Identity开头的 这里重新定义以Sys开头
            modelBuilder.Entity<SysUser>().ToTable("SysUser");
            modelBuilder.Entity<SysUserRole>().HasKey(r => new { r.RoleId, r.UserId }).ToTable("SysUserRole");
            modelBuilder.Entity<SysRole>().ToTable("SysRole");
            modelBuilder.Entity<SysUserLogin>().ToTable("SysUserLogin");
            modelBuilder.Entity<SysUserClaim>().ToTable("SysUserClaim");
        }
    }
}
