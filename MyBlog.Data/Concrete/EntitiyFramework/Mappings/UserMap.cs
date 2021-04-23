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
            builder.HasData(new User
            {
                Id=1,
                RoleId=1,
                FirstName="Enre",
                LastName="Büyüktaş",
                UserName="Emre Büyüktaş",
                Email="emrebyk348@gmail.com",
                IsActive=true,
                IsDeleted=false,
                CreatedByName="Inıtial",
                CreateDate=DateTime.Now,
                ModiefiedByName="Initial",
                ModiefiedDate=DateTime.Now,
                Description="İlk admin kullanıcı",
                Note="Admin",
                PasswordHash=Encoding.ASCII.GetBytes("0192023a7bbd73250516f069df18b500"),
                Picture= "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcSX4wVGjMQ37PaO4PdUVEAliSLi8-c2gJ1zvQ&usqp=CAU"
            });
        }
    }
}
