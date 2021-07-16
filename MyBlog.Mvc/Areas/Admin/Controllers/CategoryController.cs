using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;
using MyBlog.Mvc.Areas.Admin.Models;
using MyBlog.Mvc.Helpers.Abstract;
using MyBlog.Services.Abstract;
using MyBlog.Shared.Utilities;
using MyBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService,UserManager<User> userManager,IMapper mapper,IImageHelper ımageHelper):base(userManager,mapper,ımageHelper)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAllByNonDeleted();
            return View(result.Data);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_CategoryAddPartialView");
        }
        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddDto CategoryAddDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.Add(CategoryAddDto,LoggedInUser.UserName);
                if (result.resultStatus == ResultStatus.Succes)
                {
                    var categoryAjaxModel = JsonSerializer.Serialize(new CategoryAddAjaxViewMode 
                    {
                        CategoryDto=result.Data,
                        CategoryAddPartial=await this.RenderViewToStringAsync("_CategoryAddPartialView",CategoryAddDto)
                    });
                    return Json(categoryAjaxModel);
                }
            }
            var categoryAjaxErrorModel = JsonSerializer.Serialize(new CategoryAddAjaxViewMode
            {
                CategoryAddPartial = await this.RenderViewToStringAsync("_CategoryAddPartialView", CategoryAddDto)
            });
            return Json(categoryAjaxErrorModel);
        }
        public async Task<JsonResult> GetAllCategories()
        {
            var result = await _categoryService.GetAllByNonDeleted();
            var categories = JsonSerializer.Serialize(result.Data, new JsonSerializerOptions
            {
               ReferenceHandler=ReferenceHandler.Preserve
            });
            return Json(categories);
        }
        [HttpPost]
        public async Task<JsonResult> Delete(int categoryId)
        {
            var result = await _categoryService.Delete(categoryId, LoggedInUser.UserName);
            var deletedCategory = JsonSerializer.Serialize(result.Data);
            return Json(deletedCategory);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int categoryId)
        {
            var result = await _categoryService.GetCategoryUpdateDto(categoryId);
            if (result.resultStatus==ResultStatus.Succes)
            {
                return PartialView("_CategoyUpdatePartialView", result.Data);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _categoryService.Update(categoryUpdateDto, LoggedInUser.UserName);
                if (result.resultStatus == ResultStatus.Succes)
                {
                    var categoryUpdateAjaxModel = JsonSerializer.Serialize(new CategoryUpdateAjaxViewModel
                    {
                        CategoryDto = result.Data,
                        categoryUpdatePartial = await this.RenderViewToStringAsync("_CategoryUpdatePartialView", categoryUpdateDto)
                    });
                    return Json(categoryUpdateAjaxModel);
                }
            }
            var categoryAjaxErrorModel = JsonSerializer.Serialize(new CategoryUpdateAjaxViewModel
            {
                categoryUpdatePartial = await this.RenderViewToStringAsync("_CategoryAddPartialView", categoryUpdateDto)
            });
            return Json(categoryAjaxErrorModel);
        }
    }
}
