using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Data.Concrete.EntitiyFramework.Mappings
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();
            builder.Property(r => r.Name).IsRequired();
            builder.Property(r => r.Name).HasMaxLength(30);
            builder.Property(r => r.Description).IsRequired();
            builder.Property(r => r.Description).HasMaxLength(250);
            builder.Property(r => r.CreatedByName).IsRequired(true);
            builder.Property(r => r.CreatedByName).HasMaxLength(50);
            builder.Property(r => r.ModiefiedByName).IsRequired(true);
            builder.Property(r => r.ModiefiedByName).HasMaxLength(50);
            builder.Property(r => r.CreateDate).IsRequired(true);
            builder.Property(r => r.ModiefiedDate).IsRequired(true);
            builder.Property(r => r.IsActive).IsRequired(true);
            builder.Property(r => r.IsDeleted).IsRequired(true);
            builder.Property(r => r.Note).HasMaxLength(500);
            builder.ToTable("Roles");
            //veri tabanı oluşurken buradaki veriler oluşturulacak
            builder.HasData(new Role { 
            Id=1,
            Name="Admin",
            Description="Admin rolü tüm haklara sahiptir",
            IsActive=true,
            IsDeleted=false,
            CreatedByName="InitialCrate",
            CreateDate=DateTime.Now,
            ModiefiedByName="InitialModified",
            ModiefiedDate=DateTime.Now,
            Note="Admin Rolü"
            });
        }
    }
}
