using Microsoft.Extensions.DependencyInjection;
using MyBlog.Data.Abstract;
using MyBlog.Data.Concrete;
using MyBlog.Data.Concrete.EntitiyFramework.Contexts;
using MyBlog.Services.Abstract;
using MyBlog.Services.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Services.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection loadMyServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddDbContext<MyBlogContext>();
            serviceCollection.AddScoped<IUnitOfWork,UnitOfWork>();
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<IArticleService, ArticleManager>();
            return serviceCollection;
        }
    }
}
