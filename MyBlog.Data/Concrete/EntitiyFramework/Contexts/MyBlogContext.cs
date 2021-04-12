using Microsoft.EntityFrameworkCore;
using MyBlog.Data.Concrete.EntitiyFramework.Mappings;
using MyBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Data.Concrete.EntitiyFramework.Contexts
{
    public class MyBlogContext:DbContext
    {
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=MyBlog;Trusted_Connection=True;Connect Timeout=30;MultipleActiveResultSets=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ArticleMap ());//map işlemlerini uyguluyoruz
            modelBuilder.ApplyConfiguration(new CategoryMap ());
            modelBuilder.ApplyConfiguration(new CommentMap ());
            modelBuilder.ApplyConfiguration(new RoleMap ());
            modelBuilder.ApplyConfiguration(new UserMap ());
        }
    }
}
