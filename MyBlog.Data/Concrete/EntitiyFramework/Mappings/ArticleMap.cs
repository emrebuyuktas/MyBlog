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
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(a => a.Id);//id alanı primary key yapıldı
            builder.Property(a => a.Id).ValueGeneratedOnAdd();//her veri eklendikçe id artacak
            builder.Property(a => a.Title).HasMaxLength(100);//title maksimum 100 karakter olacak
            builder.Property(a => a.Title).IsRequired(true);
            builder.Property(a => a.Content).IsRequired(true);
            builder.Property(a => a.Content).HasColumnType("NVARCHAR(MAX)");//nvarchar tipinde ve maksimum uzunlukta olacak
            builder.Property(a => a.Date).IsRequired(true);
            builder.Property(a => a.SeoAuthor).IsRequired(true);
            builder.Property(a => a.SeoAuthor).HasMaxLength(50);
            builder.Property(a => a.SeoDescription).HasMaxLength(150);
            builder.Property(a => a.SeoDescription).IsRequired(true);
            builder.Property(a => a.SeoTags).IsRequired(true);
            builder.Property(a => a.SeoTags).HasMaxLength(70);
            builder.Property(a => a.ViewsCount).IsRequired(true);
            builder.Property(a => a.CommentCount).IsRequired(true);
            builder.Property(a => a.Thumbnail).IsRequired(true);
            builder.Property(a => a.Thumbnail).HasMaxLength(250);
            builder.Property(a => a.CreatedByName).IsRequired(true);
            builder.Property(a => a.CreatedByName).HasMaxLength(50);
            builder.Property(a => a.ModiefiedByName).IsRequired(true);
            builder.Property(a => a.ModiefiedByName).HasMaxLength(50);
            builder.Property(a => a.CreateDate).IsRequired(true);
            builder.Property(a => a.ModiefiedDate).IsRequired(true);
            builder.Property(a => a.IsActive).IsRequired(true);
            builder.Property(a => a.IsDeleted).IsRequired(true);
            builder.Property(a => a.Note).HasMaxLength(500);
            builder.HasOne<Category>(a => a.Category).WithMany(c=>c.Articles).HasForeignKey(a=>a.CategoryId);//bire çok ilişki oluşturduk,bir kategory birden fazla makaleye sahip olabilir.
            builder.HasOne<User>(a=>a.User).WithMany(u=>u.Articles).HasForeignKey(a=>a.UserId);//bir user birden fazla makaleye sahip olabilir.
            builder.ToTable("Articles");


        }
    }
}
