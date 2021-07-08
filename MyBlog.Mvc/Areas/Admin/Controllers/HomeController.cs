using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize] //kullanıcının kimlik kontrolündne geçme zorunluluğu
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
