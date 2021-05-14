using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;
using MyBlog.Mvc.Areas.Admin.Models;
using MyBlog.Shared.Utilities;
using MyBlog.Shared.Utilities.Extensions;
using MyBlog.Shared.Utilities.Results.ComplexTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyBlog.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _usermanager; //uyguladağımız user kütüphanesini kullanıyoruz
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        public UserController(UserManager<User> userManager, IMapper mapper, IWebHostEnvironment env)
        {
            _usermanager = userManager;
            _mapper = mapper;
            _env = env;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _usermanager.Users.ToListAsync();
            return View(new UserListDto
            {
                Users=users,
                ResultStatus=ResultStatus.Succes
            });
        }
        [HttpGet]
        public async Task<JsonResult> GetAllUsers()
        {
           var users = await _usermanager.Users.ToListAsync();
           var userListDto=JsonSerializer.Serialize(new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Succes
            },new JsonSerializerOptions
            { 
                ReferenceHandler=ReferenceHandler.Preserve //birbirine referans edebilecek değerler olduğu için veriyoruz
            });
            return Json(userListDto);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_UserAddPartialView");
        }
        [HttpPost]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
            if (ModelState.IsValid)
            {
                userAddDto.Picture = await ImageUpload(userAddDto);
                var user=_mapper.Map<User>(userAddDto);
                var result = await _usermanager.CreateAsync(user,userAddDto.Password);
                if (result.Succeeded)
                {
                    var userAddAjaxModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
                    {
                        UserDto=new UserDto
                        {
                            ResultStatus=ResultStatus.Succes,
                            Message= $"{user.UserName} adlı kullanıcı başarıyla eklenmiştir",
                            User=user
                        },
                        UserAddPartial=await this.RenderViewToStringAsync("_UserAddPartialView", userAddDto)
                    });
                    return Json(userAddAjaxModel);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                        var userAddAjaxErrorModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
                        {
                            UserAddDto=userAddDto,
                            UserAddPartial=await this.RenderViewToStringAsync("_UserAddPartialView", userAddDto)
                        });
                        return Json(userAddAjaxErrorModel);
                    }
            }
            var userAddAjaxErrorModelState = JsonSerializer.Serialize(new UserAddAjaxViewModel
            {
                UserAddDto = userAddDto,
                UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartialView", userAddDto)
            });
            return Json(userAddAjaxErrorModelState);

        }
        public async Task<string> ImageUpload(UserAddDto userAddDto)
        {
            string wwwroot = _env.WebRootPath;
            string fileExtension = Path.GetExtension(userAddDto.PictureFile.FileName);
            DateTime dateTime = DateTime.Now;
            string fileName = $"{userAddDto.UserName}_{dateTime.FullDateAndTimeStringWithUnderScore()}{fileExtension}";
            var path = Path.Combine($"{wwwroot}/img", fileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await userAddDto.PictureFile.CopyToAsync(stream);
            }
            return fileName; 
        }
    }
}
