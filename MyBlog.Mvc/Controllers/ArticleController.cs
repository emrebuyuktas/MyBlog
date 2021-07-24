using Microsoft.AspNetCore.Mvc;
using MyBlog.Entities.ComplexTypes;
using MyBlog.Mvc.Models;
using MyBlog.Services.Abstract;
using MyBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Mvc.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }
        [HttpGet]
        public async Task<IActionResult> Search(string keyword,int currentPage=1,int pageSize=5,bool isAscending=false)
        {
            var searchResult = await _articleService.Search(keyword, currentPage, pageSize, isAscending);
            return View(new ArticleSearchViewModel { 
                ArticleLisDto=searchResult.Data,
                Keyword=keyword
            });
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int articleId)
        {
            var article = await _articleService.Get(articleId);
            if (article.resultStatus==ResultStatus.Succes)
            {
                var userArticles = await _articleService.GetAllByUserIdOnFilter(article.Data.Article.UserId,FilterBy.Category,OrderBy.Date,false,10,
                    article.Data.Article.CategoryId,DateTime.Now,DateTime.Now,0,99999,0,99999);
                await _articleService.IncreaseViewCountAsync(articleId);
                return View(new ArticleDetailViewModel { 
                    ArticleDto=article.Data,
                    ArticleDetailRightSiteBarViewModel=new ArticleDetailRightSiteBarViewModel
                    {
                        ArticleListdDto=userArticles.Data,
                        Header="Kullanıcının aynı kategori içindeki en çok okunan makaleleri.",
                        User=article.Data.Article.User
                    }

                });
            }
            else
            {
                return NotFound();
            }
        }
    }
}
