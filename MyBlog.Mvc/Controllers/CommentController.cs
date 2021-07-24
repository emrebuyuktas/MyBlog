using Microsoft.AspNetCore.Mvc;
using MyBlog.Entities.Dtos;
using MyBlog.Mvc.Models;
using MyBlog.Services.Abstract;
using MyBlog.Shared.Utilities;
using MyBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyBlog.Mvc.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [HttpPost]
        public async Task<JsonResult> Add(CommentAddDto commentAddDto)
        {
            if (ModelState.IsValid)
            {
                var result = await _commentService.AddAsync(commentAddDto);
                if (result.resultStatus==ResultStatus.Succes)
                {
                    var commentAddAjaxViewModel = JsonSerializer.Serialize(new CommentAddAajaxViewModel { 
                        CommentDto=result.Data,
                        CommentAddPartial=await this.RenderViewToStringAsync("_CommentAddPartial",commentAddDto)
                    },new JsonSerializerOptions {
                    ReferenceHandler= ReferenceHandler.Preserve
                    });
                    return Json(commentAddAjaxViewModel);
                }
                ModelState.AddModelError("",result.Message);
            }
            var commentErrorAjaxViewModel = JsonSerializer.Serialize(new CommentAddAajaxViewModel
            {
                CommentAddDto=commentAddDto,
                CommentAddPartial = await this.RenderViewToStringAsync("_CommentAddPartial", commentAddDto)
            });
            return Json(commentErrorAjaxViewModel);
        }
    }
}
