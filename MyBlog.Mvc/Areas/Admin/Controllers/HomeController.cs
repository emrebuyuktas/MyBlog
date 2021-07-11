using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Entities.Concrete;
using MyBlog.Mvc.Areas.Admin.Models;
using MyBlog.Services.Abstract;
using MyBlog.Shared.Utilities.Results.ComplexTypes;
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
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;
        private readonly ICommentService _commentService;
        private readonly UserManager<User> _userManager;

        public HomeController(ICategoryService categoryService, IArticleService articleService, ICommentService commentService , UserManager<User> userManager)
        {
            _categoryService = categoryService;
            _articleService = articleService;
            _commentService = commentService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var categoriesCount = await _categoryService.CountByIsDeleted();
            var artcilesCount = await _articleService.CountByIsDeleted();
            var commentCount = await _commentService.CountByIsDeleted();
            var userCount = await _userManager.Users.CountAsync();
            var allArtciles = await _articleService.GetAll();
            if (categoriesCount.resultStatus==ResultStatus.Succes && artcilesCount.resultStatus == ResultStatus.Succes && 
                commentCount.resultStatus == ResultStatus.Succes && userCount >-1 && allArtciles.resultStatus==ResultStatus.Succes
                )
            {
                return View(new DashBoardViewModal
                {
                    CategoriesCount=categoriesCount.Data,
                    ArticlesCount=artcilesCount.Data,
                    CommnetsCount=commentCount.Data,
                    UsersCount=userCount,
                    Articles=allArtciles.Data
                });
            }
            else
            {
                return NotFound();
            }
        }
    }
}
