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
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Text).IsRequired();
            builder.Property(c => c.Text).HasMaxLength(1000);
            builder.HasOne<Article>(c => c.Article).WithMany(a => a.Comments).HasForeignKey(c => c.ArticleId);
            builder.Property(c => c.CreatedByName).IsRequired(true);
            builder.Property(c => c.CreatedByName).HasMaxLength(50);
            builder.Property(c => c.ModiefiedByName).IsRequired(true);
            builder.Property(c => c.ModiefiedByName).HasMaxLength(50);
            builder.Property(c => c.CreateDate).IsRequired(true);
            builder.Property(c => c.ModiefiedDate).IsRequired(true);
            builder.Property(c => c.IsActive).IsRequired(true);
            builder.Property(c => c.IsDeleted).IsRequired(true);
            builder.Property(c => c.Note).HasMaxLength(500);
            builder.ToTable("Comments");


            builder.HasData(
                new Comment
                {
                    Id=1,
                    ArticleId=1,
                    Text= "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's " +
                    "standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book." +
                    " It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially " +
                    "unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, " +
                    "and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "Inıtial",
                    CreateDate = DateTime.Now,
                    ModiefiedByName = "Initial",
                    ModiefiedDate = DateTime.Now,
                    Note = "Comment"
                },
                new Comment
                {
                    Id = 2,
                    ArticleId = 2,
                    Text = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's " +
                    "standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book." +
                    " It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially " +
                    "unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, " +
                    "and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "Inıtial",
                    CreateDate = DateTime.Now,
                    ModiefiedByName = "Initial",
                    ModiefiedDate = DateTime.Now,
                    Note = "Comment"
                },
                new Comment
                {
                    Id = 3,
                    ArticleId = 3,
                    Text = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's " +
                    "standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book." +
                    " It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially " +
                    "unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, " +
                    "and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    IsActive = true,
                    IsDeleted = false,
                    CreatedByName = "Inıtial",
                    CreateDate = DateTime.Now,
                    ModiefiedByName = "Initial",
                    ModiefiedDate = DateTime.Now,
                    Note = "Comment"
                }

                );
        }
    }
}
