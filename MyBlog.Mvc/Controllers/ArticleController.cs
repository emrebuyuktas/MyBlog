using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyBlog.Entities.ComplexTypes;
using MyBlog.Entities.Concrete;
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
        private readonly ArticleRightSideBarWidgetOptions _articleRightSideBarWidgetOptions;

        public ArticleController(IArticleService articleService, IOptionsSnapshot<ArticleRightSideBarWidgetOptions> articleRightSideBarWidgetOptions)
        {
            _articleService = articleService;
            _articleRightSideBarWidgetOptions = articleRightSideBarWidgetOptions.Value;
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
                var userArticles = await _articleService.GetAllByUserIdOnFilter(article.Data.Article.UserId,
                    _articleRightSideBarWidgetOptions.FilterBy, _articleRightSideBarWidgetOptions.OrderBy, _articleRightSideBarWidgetOptions.IsAscending, _articleRightSideBarWidgetOptions.TakeSize, _articleRightSideBarWidgetOptions.CategoryId, _articleRightSideBarWidgetOptions.StartAt,
                    _articleRightSideBarWidgetOptions.EndAt, _articleRightSideBarWidgetOptions.MinViewCount, _articleRightSideBarWidgetOptions.MaxViewCount, _articleRightSideBarWidgetOptions.MinCommentCount, _articleRightSideBarWidgetOptions.MaxCommentCount);
                await _articleService.IncreaseViewCountAsync(articleId);
                return View(new ArticleDetailViewModel { 
                    ArticleDto=article.Data,
                    ArticleDetailRightSiteBarViewModel=new ArticleDetailRightSiteBarViewModel
                    {
                        ArticleListdDto=userArticles.Data,
                        Header=_articleRightSideBarWidgetOptions.Header,
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
