using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Entities.ComplexTypes;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;
using MyBlog.Mvc.Areas.Admin.Models;
using MyBlog.Mvc.Helpers.Abstract;
using MyBlog.Services.Abstract;
using MyBlog.Shared.Utilities.Results.ComplexTypes;
using NToastNotify;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ArticleController : BaseController
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _ımapper;
        private readonly IImageHelper _imageHelper;
        private readonly IToastNotification _toastNotification;

        public ArticleController(IArticleService articleService, ICategoryService categoryService, UserManager<User> userManager, IMapper ımapper, IImageHelper imageHelper, IToastNotification toastNotification) : base(userManager, ımapper, imageHelper)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            _toastNotification = toastNotification;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _articleService.GetAllByNonDeleted();
            if (result.resultStatus == ResultStatus.Succes)
            {
                return View(result.Data);
            }
            return NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var result = await _categoryService.GetAllByNonDeleted();
            if (result.resultStatus == ResultStatus.Succes)
            {
                return View(new ArticleAddViewModel
                {
                    Categories =result.Data.Categories

            });
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Add(ArticleAddViewModel articleAddViewModel)
        {
            if (ModelState.IsValid)
            {
                var articleDto = Mapper.Map<ArticleAddDto>(articleAddViewModel);
                var imageResult = await ImageHelper.Upload(articleAddViewModel.Title,articleAddViewModel.Thumbnail,PictureType.Post);
                articleDto.Thumbnail = imageResult.Data.FullName;
                var result = await _articleService.Add(articleDto,LoggedInUser.UserName,LoggedInUser.Id);
                if (result.resultStatus == ResultStatus.Succes)
                {
                    _toastNotification.AddSuccessToastMessage(result.Message);
                    return RedirectToAction("Index","Article");
                }
                else
                {
                    ModelState.AddModelError("",result.Message);
                }
            }
            var categories = await _categoryService.GetAllByNonDeletedAndActive();
            articleAddViewModel.Categories = categories.Data.Categories;
            return View(articleAddViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int articleId)
        {
            var articleResult = await _articleService.GetArticleUpdateDto(articleId);
            var categoreisResult = await _categoryService.GetAllByNonDeletedAndActive();
            if (articleResult.resultStatus==ResultStatus.Succes&&categoreisResult.resultStatus==ResultStatus.Succes)
            {
                var articleUpdate = Mapper.Map<ArticleUpdateViewModel>(articleResult.Data);
                articleUpdate.Categories = categoreisResult.Data.Categories;
                return View(articleUpdate);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        public async Task<IActionResult> Update(ArticleUpdateViewModel articleUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                bool isNewThumbnail = false;
                var oldThumbnail = articleUpdateViewModel.Thumbnail;
                if (articleUpdateViewModel.ThumbnailFile != null)
                {
                    var uploadedImage = await _imageHelper.Upload(articleUpdateViewModel.Title, articleUpdateViewModel.ThumbnailFile, PictureType.Post);
                    articleUpdateViewModel.Thumbnail = uploadedImage.resultStatus == ResultStatus.Succes ? uploadedImage.Data.FolderName : "postImages/defaultThumbnail.jpg";
                    if (oldThumbnail != "postImages/defaultThumbnail.jpg")
                    {
                        isNewThumbnail = true;
                    }
                }
                    var articleUpdateDto = Mapper.Map<ArticleUpdateDto>(articleUpdateViewModel);
                    var result = await _articleService.Update(articleUpdateDto,LoggedInUser.UserName);
                    if (result.resultStatus==ResultStatus.Succes)
                    {
                        if (isNewThumbnail)
                        {
                            ImageHelper.Delete(oldThumbnail);
                        }
                    _toastNotification.AddSuccessToastMessage(result.Message);
                    return RedirectToAction("Index","Article");
                    }
                    else
                    {
                        ModelState.AddModelError("",result.Message);
                    }
                
            }
            var categories = await _categoryService.GetAllByNonDeletedAndActive();
            articleUpdateViewModel.Categories = categories.Data.Categories;
            return View(articleUpdateViewModel);
        }
       
        [HttpPost]
        public async Task<JsonResult> Delete(int articleId)
        {
            var result = await _articleService.Delete(articleId,LoggedInUser.UserName);
            var articleResult = JsonSerializer.Serialize(result);
            return Json(articleResult);
        }

        [HttpGet]
        public async Task<JsonResult> GetAllArticles()
        {
            var articles = await _articleService.GetAllByNonDeletedAndActive();
            var result = JsonSerializer.Serialize(articles, new JsonSerializerOptions { 
                ReferenceHandler= ReferenceHandler.Preserve
            });
            return Json(result);
        }
    }
}
