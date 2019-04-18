using System;
using System.Collections.Generic;
using System.Text;
using CMS.Entities.Concrete;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace CMS.DataAccess.Concrete.EntityFramework
{
    public class CmsContext : DbContext
    {
        public CmsContext()
            : base("DbConnection")
        { }
        public virtual DbSet<ClassTypes> ClassType { get; set; }
        public virtual DbSet<ClassTypeInfo> ClassTypeInfo { get; set; }
        public virtual DbSet<Classes> Classes { get; set; }
        public virtual DbSet<ClassInfo> ClassInfo { get; set; }
        public virtual DbSet<Banners> Banners { get; set; }
        public virtual DbSet<BannerInfo> BannerInfo { get; set; }
        public virtual DbSet<Lists> Lists { get; set; }
        public virtual DbSet<ListInfo> ListInfo { get; set; }
        public virtual DbSet<Menus> Menus { get; set; }
        public virtual DbSet<MenuInfo> MenuInfo { get; set; }
        public virtual DbSet<ContentClasses> ContentClasses { get; set; }
        public virtual DbSet<ContentImages> ContentImages { get; set; }
        public virtual DbSet<ContentInfo> ContentInfo { get; set; }
        public virtual DbSet<Contents> Contents { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        public virtual DbSet<UserTypes> UserTypes { get; set; }



    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
    }
}
}
