using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBlog.Entities.Concrete;
using MyBlog.Mvc.AutoMapper.Profiles;
using MyBlog.Mvc.Filters;
using MyBlog.Mvc.Helpers.Abstract;
using MyBlog.Mvc.Helpers.Concrete;
using MyBlog.Services.AutoMapper.Profiles;
using MyBlog.Services.Extensions;
using MyBlog.Shared.Utilities.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyBlog.Mvc
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AboutUsPageInfo>(Configuration.GetSection("AboutUsPageInfo"));
            services.Configure<WebSiteInfo>(Configuration.GetSection("WebSiteInfo"));
            services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.Configure<ArticleRightSideBarWidgetOptions>(Configuration.GetSection("ArticleRightSideBarWidgetOptions"));
            services.ConfigureWritable<AboutUsPageInfo>(Configuration.GetSection("AboutUsPageInfo"));
            services.ConfigureWritable<WebSiteInfo>(Configuration.GetSection("WebSiteInfo"));
            services.ConfigureWritable<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
            services.ConfigureWritable<ArticleRightSideBarWidgetOptions>(Configuration.GetSection("ArticleRightSideBarWidgetOptions"));
            services.AddControllersWithViews(options=> {
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(value=>"Bu alan bo? ge?ilemez.");
                options.Filters.Add<MvcExceptionFilter>();
            }).AddRazorRuntimeCompilation().AddJsonOptions(opt=> {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            }).AddNToastNotifyToastr();
            services.AddSession();
            services.AddAutoMapper(typeof(CategoryProfile), typeof(ArticleProfile),typeof(UserProfile),typeof(ViewModelsProfile));
            services.loadMyServices(connectionString:Configuration.GetConnectionString("LocalDb"));
            services.AddScoped<IImageHelper, ImageHelper>();
            services.ConfigureApplicationCookie(option=>
            {
                option.LoginPath = new PathString("/Admin/Auth/Login");
                option.LogoutPath = new PathString("/Admin/Auth/Logout");
                option.Cookie = new CookieBuilder
                {
                    Name="MyBlog",
                    HttpOnly=true, //gelen istekler sadece http ?zerinden kar??lan?r
                    SameSite=SameSiteMode.Strict, //d?? kaynaklaradan gelen isteklere cevap vermememizi sa?lar
                    SecurePolicy=CookieSecurePolicy.SameAsRequest //ger?ek projelerde always olarak kullan?l?r
                };
                option.SlidingExpiration = true; //kullan?c?n?n tekrar giri? yapmas? gereken s?reyi belirliyoruz
                option.ExpireTimeSpan = System.TimeSpan.FromDays(7);
                option.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied"); //sisteme giri? yapm?? ama yetkisi olmayan alana eri?meye ?al??an ki?ileri y?nlendiriyoruz
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }
            app.UseSession();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseNToastNotify();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(name:"Admin",areaName:"Admin",pattern:"Admin/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name:"article",
                    pattern:"{title}/{articleId}",
                    defaults:new {controller="Article",action="Detail"}
                    );
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
