using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using MyBlog.Entities.Concrete;
using MyBlog.Mvc.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Mvc.Areas.Admin.ViewComponents
{
    public class AdminMenuViewComponent:ViewComponent
    {
        private readonly UserManager<User> _userManager;

        public AdminMenuViewComponent(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User); //login olan kullanıcıya HttpContext.User ile ulaşılabilir
            if (user == null)
                return Content("Kullanıcı bulunamadı.");
            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null)
                return Content("Roller bulunamadı.");
            return View(new UserWithRolesViewModel { 
            User=user,
            Roles=roles
            });
        }
    }
}
