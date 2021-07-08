﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private readonly SignInManager<User> _signInManager;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        public UserController(UserManager<User> userManager, IMapper mapper, IWebHostEnvironment env,SignInManager<User> signInManager)
        {
            _usermanager = userManager;
            _mapper = mapper;
            _env = env;
            _signInManager = signInManager;
        }
        [Authorize(Roles ="Admin")]
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
        public IActionResult Login()
        {
            return View("UserLogin");
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _usermanager.FindByEmailAsync(userLoginDto.Email);
                if (user!=null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user,userLoginDto.Password,userLoginDto.RememberMe,false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("","E posta adresiniz yada şifreniz yanlıştır");
                        return View("UserLogin");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "E posta adresiniz yada şifreniz yanlıştır");
                    return View("UserLogin");
                }
            }
            else
            {
                return View("UserLogin");
            }
            
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home",new {Area="" });
        }
        [HttpGet]
        public ViewResult AccessDenied()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return PartialView("_UserAddPartialView");
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
            if (ModelState.IsValid)
            {
                userAddDto.Picture = await ImageUpload(userAddDto.UserName,userAddDto.PictureFile);
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
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> Delete(int userId)
        {
            var user = await _usermanager.FindByIdAsync(userId.ToString());
            var result = await _usermanager.DeleteAsync(user);
            if (result.Succeeded)
            {
                var deletedUser = JsonSerializer.Serialize(new UserDto
                {
                    ResultStatus = ResultStatus.Succes,
                    Message = $"{user.UserName} adlı kullanıcı başarıyla silinmiştir",
                    User = user
                });
                return Json(deletedUser);
            }
            else
            {
                string errorMessages = String.Empty;
                foreach (var error in result.Errors)
                {
                   errorMessages= $"*{error.Description}\n";
                }
                var deletedUserErrorModel = JsonSerializer.Serialize(new UserDto
                {
                    ResultStatus=ResultStatus.Error,
                    Message=$"{user.UserName} adlı kullanıcı başarıyla silinirken bazı hatalar oluştu.\n {errorMessages}",
                    User=user
                });
                return Json(deletedUserErrorModel);
            }
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<PartialViewResult> Update(int userId)
        {
            var user = await _usermanager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var userUpdateDto = _mapper.Map<UserUpdateDto>(user);
            return PartialView("_UserUpdatePartialView ", userUpdateDto);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
        {
            if (ModelState.IsValid)
            {
                bool isNewPictureUploaded = false;
                var oldUser = await _usermanager.FindByIdAsync(userUpdateDto.Id.ToString());
                var oldUserPicture = oldUser.Picture;
                if (userUpdateDto.PictureFile != null)
                {
                    userUpdateDto.Picture = await ImageUpload(userUpdateDto.UserName, userUpdateDto.PictureFile);
                    isNewPictureUploaded = true;
                }

                var updatedUser = _mapper.Map<UserUpdateDto, User>(userUpdateDto, oldUser);
                var result = await _usermanager.UpdateAsync(updatedUser);
                if (result.Succeeded)
                {
                    if (isNewPictureUploaded)
                    {
                        imageDelete(oldUserPicture);
                    }

                    var userUpdateViewModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                    {
                        UserDto = new UserDto
                        {
                            ResultStatus = ResultStatus.Succes,
                            Message = $"{updatedUser.UserName} adlı kullanıcı başarıyla güncellenmiştir.",
                            User = updatedUser
                        },
                        UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartialView", userUpdateDto)
                    });
                    return Json(userUpdateViewModel);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    var userUpdateErorViewModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                    {
                        UserUpdateDto = userUpdateDto,
                        UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartialView", userUpdateDto)
                    });
                    return Json(userUpdateErorViewModel);
                }

            }
            else
            {
                var userUpdateModelStateErrorViewModel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                {
                    UserUpdateDto = userUpdateDto,
                    UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartialView", userUpdateDto)
                });
                return Json(userUpdateModelStateErrorViewModel);
            }
        }
        [HttpGet]
        [Authorize]
        public async Task<ViewResult> ChangeDetails()
        {
            var user = await _usermanager.GetUserAsync(HttpContext.User);
            var updateDto = _mapper.Map<UserUpdateDto>(user);
            return View(updateDto);
        }
        [HttpPost]
        [Authorize]
        public async Task<ViewResult> ChangeDetails(UserUpdateDto userUpdateDto)
        {
            if (ModelState.IsValid)
            {
                bool isNewPictureUploaded = false;
                var oldUser = await _usermanager.GetUserAsync(HttpContext.User);
                var oldUserPicture = oldUser.Picture;
                if (userUpdateDto.PictureFile != null)
                {
                    userUpdateDto.Picture = await ImageUpload(userUpdateDto.UserName, userUpdateDto.PictureFile);
                    if (oldUserPicture != "defaultUser.png")
                    {
                        isNewPictureUploaded = true;
                    }
                }

                var updatedUser = _mapper.Map<UserUpdateDto, User>(userUpdateDto, oldUser);
                var result = await _usermanager.UpdateAsync(updatedUser);
                if (result.Succeeded)
                {
                    if (isNewPictureUploaded)
                    {
                        imageDelete(oldUserPicture);
                    }
                    TempData.Add("SuccessMessage", $"{userUpdateDto.UserName} adlı kullanıcı başarıyla güncellenmiştir.");
                    return View(userUpdateDto);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(userUpdateDto);
                }

            }
            else
            {
                return View(userUpdateDto);
            }
        }
        [HttpGet]
        [Authorize]
        public ViewResult PasswordChange()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PasswordChange(UserPasswordChangeDto userPasswordChangeDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _usermanager.GetUserAsync(HttpContext.User);
                var isVerified = await _usermanager.CheckPasswordAsync(user,userPasswordChangeDto.CurrentPassword);
                if (isVerified)
                {
                    var result = await _usermanager.ChangePasswordAsync(user,userPasswordChangeDto.CurrentPassword,userPasswordChangeDto.NewPassword);
                    if (result.Succeeded)
                    {
                        await _usermanager.UpdateSecurityStampAsync(user);
                        await _signInManager.SignOutAsync();
                        await _signInManager.PasswordSignInAsync(user, userPasswordChangeDto.NewPassword,true,false);
                        TempData.Add("SuccessMessage", $"{user.UserName} adlı kullanıcının şifresi başarıyla güncellenmiştir.");
                        return View();
                    }
                    else
                    {
                        ModelState.AddModelError("","Lütfen girmiş olduğunuz şu anki şifrenizi kontrol ediniz");
                        return View(userPasswordChangeDto);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Lütfen girmiş olduğunuz bilgileri kontrol ediniz");
                return View(userPasswordChangeDto);
            }
            return View();
        }

        [Authorize(Roles = "Admin,Editor")]
        public async Task<string> ImageUpload(string useName,IFormFile pictureFile)
        {
            string wwwroot = _env.WebRootPath;
            string fileExtension = Path.GetExtension(pictureFile.FileName);
            DateTime dateTime = DateTime.Now;
            string fileName = $"{useName}_{dateTime.FullDateAndTimeStringWithUnderScore()}{fileExtension}";
            var path = Path.Combine($"{wwwroot}/img", fileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await pictureFile.CopyToAsync(stream);
            }
            return fileName;
        }
        [Authorize(Roles = "Admin,Editor")]
        public bool imageDelete(string pictureName)
        {
            string wwwroot = _env.WebRootPath;
            var fileToDelete = Path.Combine($"{wwwroot}/img", pictureName);
            if (System.IO.File.Exists(fileToDelete))
            {
                System.IO.File.Delete(fileToDelete);
                return true;
            }
            return false;
        }
    }
}
