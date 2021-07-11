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
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Name).IsRequired(true);
            builder.Property(c => c.Name).HasMaxLength(70);
            builder.Property(c => c.Description).HasMaxLength(500);
            builder.Property(c => c.CreatedByName).IsRequired(true);
            builder.Property(c => c.CreatedByName).HasMaxLength(50);
            builder.Property(c => c.ModiefiedByName).IsRequired(true);
            builder.Property(c => c.ModiefiedByName).HasMaxLength(50);
            builder.Property(c => c.CreateDate).IsRequired(true);
            builder.Property(c => c.ModiefiedDate).IsRequired(true);
            builder.Property(c => c.IsActive).IsRequired(true);
            builder.Property(c => c.IsDeleted).IsRequired(true);
            builder.Property(c => c.Note).HasMaxLength(500);
            builder.ToTable("Categories");

            builder.HasData(new Category { 
                Id=1,
                Name="C#",
                Description="C# programlama dili ile ilgili güncel bilgiler",
                IsActive = true,
                IsDeleted = false,
                CreatedByName = "Inıtial",
                CreateDate = DateTime.Now,
                ModiefiedByName = "Initial",
                ModiefiedDate = DateTime.Now,
                Note = "C# kategorisi"
            },
            new Category
            {
                Id = 2,
                Name = "C++#",
                Description = "C++ programlama dili ile ilgili güncel bilgiler",
                IsActive = true,
                IsDeleted = false,
                CreatedByName = "Inıtial",
                CreateDate = DateTime.Now,
                ModiefiedByName = "Initial",
                ModiefiedDate = DateTime.Now,
                Note = "C++ kategorisi"
            },
             new Category
             {
                Id = 3,
                Name = "JavaScript",
                Description = "JavaScript programlama dili ile ilgili güncel bilgiler",
                IsActive = true,
                IsDeleted = false,
                CreatedByName = "Inıtial",
                CreateDate = DateTime.Now,
                ModiefiedByName = "Initial",
                ModiefiedDate = DateTime.Now,
                Note = "JavaScript kategorisi"
             }
            );




        }
    }
}
