using Microsoft.AspNetCore.Mvc;
using MyBlog.Services.Abstract;
using MyBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAll();
            if (result.resultStatus==ResultStatus.Succes)
            {
                return View(result.Data);
            }
            return View();
        }
    }
}
