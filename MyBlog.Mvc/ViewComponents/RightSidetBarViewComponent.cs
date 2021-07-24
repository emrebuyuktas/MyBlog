using Microsoft.AspNetCore.Mvc;
using MyBlog.Mvc.Models;
using MyBlog.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Mvc.ViewComponents
{
    public class RightSidetBarViewComponent:ViewComponent
    {
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;

        public RightSidetBarViewComponent(ICategoryService categoryService, IArticleService articleService)
        {
            _categoryService = categoryService;
            _articleService = articleService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categoryResult = await _categoryService.GetAllByNonDeletedAndActive();
            var articleResult = await _articleService.GetAllByViewCount(isAscending:false,takeSize:5);
            return View(new RightSidetBarViewModel { 
                Categories=categoryResult.Data.Categories,
                Articles=articleResult.Data.Articles
            });
        }
    }
}
