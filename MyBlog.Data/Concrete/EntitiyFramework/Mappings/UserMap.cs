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
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u=>u.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(u=>u.Email).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(50);
            builder.HasIndex(u => u.Email).IsUnique();//emailin birden fazla kullanılmasını engelliyor
            builder.Property(u => u.UserName).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(30);
            builder.HasIndex(u => u.UserName).IsUnique();
            builder.Property(u => u.PasswordHash).IsRequired();
            builder.Property(U => U.PasswordHash).HasColumnType("VARBINARY(500)");
            builder.Property(u => u.Description).HasMaxLength(500);
            builder.Property(u => u.FirstName).IsRequired();
            builder.Property(u => u.FirstName).HasMaxLength(30);
            builder.Property(u => u.LastName).IsRequired();
            builder.Property(u => u.LastName).HasMaxLength(50);
            builder.Property(u => u.Picture).IsRequired();
            builder.Property(u => u.Picture).HasMaxLength(250);
            builder.HasOne<Role>(u => u.Role).WithMany(r => r.Users).HasForeignKey(u => u.RoleId);
            builder.Property(u=> u.CreatedByName).IsRequired(true);
            builder.Property(u => u.CreatedByName).HasMaxLength(50);
            builder.Property(u => u.ModiefiedByName).IsRequired(true);
            builder.Property(u => u.ModiefiedByName).HasMaxLength(50);
            builder.Property(u => u.CreateDate).IsRequired(true);
            builder.Property(u => u.ModiefiedDate).IsRequired(true);
            builder.Property(u => u.IsActive).IsRequired(true);
            builder.Property(u => u.IsDeleted).IsRequired(true);
            builder.Property(u => u.Note).HasMaxLength(500);
            builder.ToTable("Users");
        }
    }
}
