using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyBlog.Mvc.AutoMapper.Profiles;
using MyBlog.Mvc.Helpers.Abstract;
using MyBlog.Mvc.Helpers.Concrete;
using MyBlog.Services.AutoMapper.Profiles;
using MyBlog.Services.Extensions;
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation().AddJsonOptions(opt=> {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
            }).AddNToastNotifyToastr();
            services.AddSession();
            services.AddAutoMapper(typeof(CategoryProfile), typeof(ArticleProfile),typeof(UserProfile),typeof(ViewModelsProfile));
            services.loadMyServices(connectionString:Configuration.GetConnectionString("LocalDb"));
            services.AddScoped<IImageHelper, ImageHelper>();
            services.ConfigureApplicationCookie(option=>
            {
                option.LoginPath = new PathString("/Admin/User/Login");
                option.LogoutPath = new PathString("/Admin/User/Logout");
                option.Cookie = new CookieBuilder
                {
                    Name="MyBlog",
                    HttpOnly=true, //gelen istekler sadece http üzerinden karþýlanýr
                    SameSite=SameSiteMode.Strict, //dýþ kaynaklaradan gelen isteklere cevap vermememizi saðlar
                    SecurePolicy=CookieSecurePolicy.SameAsRequest //gerçek projelerde always olarak kullanýlýr
                };
                option.SlidingExpiration = true; //kullanýcýnýn tekrar giriþ yapmasý gereken süreyi belirliyoruz
                option.ExpireTimeSpan = System.TimeSpan.FromDays(7);
                option.AccessDeniedPath = new PathString("/Admin/User/AccessDenied"); //sisteme giriþ yapmýþ ama yetkisi olmayan alana eriþmeye çalýþan kiþileri yönlendiriyoruz
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
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
