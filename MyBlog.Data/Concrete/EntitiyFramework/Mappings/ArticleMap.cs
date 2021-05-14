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


            //builder.HasData(
            //    new Article
            //    {
            //        Id=1,
            //        CategoryId=1,
            //        Title="C# 9.0 ve .NET yenilikleri",
            //        Content= "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's " +
            //        "standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book." +
            //        " It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially " +
            //        "unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, " +
            //        "and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
            //        Thumbnail="Default1.jpg",
            //        SeoDescription= "C# 9.0 ve .NET yenilikleri",
            //        SeoTags= "C# 9.0, .NET, asp net core",
            //        SeoAuthor="Emre Büyüktaş",
            //        Date=DateTime.Now,
            //        IsActive = true,
            //        IsDeleted = false,
            //        CreatedByName = "Inıtial",
            //        CreateDate = DateTime.Now,
            //        ModiefiedByName = "Initial",
            //        ModiefiedDate = DateTime.Now,
            //        Note = "C# kategorisi",
            //        UserId=1,
            //        ViewsCount=100,
            //        CommentCount=1
            //    },
            //    new Article
            //    {
            //        Id = 2,
            //        CategoryId = 2,
            //        Title = "C++ 11 yenilikleri",
            //        Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's " +
            //        "standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book." +
            //        " It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially " +
            //        "unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, " +
            //        "and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
            //        Thumbnail = "Default1.jpg",
            //        SeoDescription = "C++ 11 yenilikleri",
            //        SeoTags = "C++ 11",
            //        SeoAuthor = "Emre Büyüktaş",
            //        Date = DateTime.Now,
            //        IsActive = true,
            //        IsDeleted = false,
            //        CreatedByName = "Inıtial",
            //        CreateDate = DateTime.Now,
            //        ModiefiedByName = "Initial",
            //        ModiefiedDate = DateTime.Now,
            //        Note = "C++ kategorisi",
            //        UserId = 1,
            //        ViewsCount = 100,
            //        CommentCount = 1
            //    },
            //    new Article
            //    {
            //        Id = 3,
            //        CategoryId = 3,
            //        Title = "JavaScript yenilikleri",
            //        Content = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's " +
            //        "standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book." +
            //        " It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially " +
            //        "unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, " +
            //        "and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
            //        Thumbnail = "Default1.jpg",
            //        SeoDescription = "JavaScript yenilikleri",
            //        SeoTags = "JavaScript",
            //        SeoAuthor = "Emre Büyüktaş",
            //        Date = DateTime.Now,
            //        IsActive = true,
            //        IsDeleted = false,
            //        CreatedByName = "Inıtial",
            //        CreateDate = DateTime.Now,
            //        ModiefiedByName = "Initial",
            //        ModiefiedDate = DateTime.Now,
            //        Note = "JavaScript",
            //        UserId = 1,
            //        ViewsCount = 100,
            //        CommentCount = 1
            //    }
            //    );


        }
    }
}
