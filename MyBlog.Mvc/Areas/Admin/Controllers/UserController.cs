using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Entities.ComplexTypes;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;
using MyBlog.Mvc.Areas.Admin.Models;
using MyBlog.Mvc.Helpers.Abstract;
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
        private readonly IMapper _mapper;
        private readonly IImageHelper _ımageHelper;
        public UserController(UserManager<User> userManager, IMapper mapper, SignInManager<User> signInManager, IImageHelper ımageHelper)
        {
            _usermanager = userManager;
            _mapper = mapper;
            _signInManager = signInManager;
            _ımageHelper = ımageHelper;
        }
        [Authorize(Roles ="SuperAdmin,User.Read")]
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
        [Authorize(Roles = "SuperAdmin,User.Read")]
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
        [Authorize(Roles = "SuperAdmin,User.Create")]
        public IActionResult Add()
        {
            return PartialView("_UserAddPartialView");
        }
        [HttpPost]
        [Authorize(Roles = "SuperAdmin,User.Create")]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
            if (ModelState.IsValid)
            {
                var uploadedUserImage = await _ımageHelper.Upload(userAddDto.UserName,userAddDto.PictureFile,PictureType.User);
                userAddDto.Picture = uploadedUserImage.resultStatus==ResultStatus.Succes? uploadedUserImage.Data.FullName
                    :"userImages/defaultUser.png";
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
        [HttpPost]
        [Authorize(Roles = "SuperAdmin,User.Delete")]
        public async Task<JsonResult> Delete(int userId)
        {
            var user = await _usermanager.FindByIdAsync(userId.ToString());
            var result = await _usermanager.DeleteAsync(user);
            if (result.Succeeded)
            {
                if (user.Picture!="userImages/defaultUser.png")
                {
                    _ımageHelper.Delete(user.Picture);
                }
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
        [Authorize(Roles = "SuperAdmin,User.Update")]
        public async Task<PartialViewResult> Update(int userId)
        {
            var user = await _usermanager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var userUpdateDto = _mapper.Map<UserUpdateDto>(user);
            return PartialView("_UserUpdatePartialView", userUpdateDto);
        }
        [HttpPost]
        [Authorize(Roles = "SuperAdmin,User.Update")]
        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
        {
            if (ModelState.IsValid)
            {
                bool isNewPictureUploaded = false;
                var oldUser = await _usermanager.FindByIdAsync(userUpdateDto.Id.ToString());
                var oldUserPicture = oldUser.Picture;
                if (userUpdateDto.PictureFile != null)
                {
                    var uploadedUserImage = await _ımageHelper.Upload(userUpdateDto.UserName, userUpdateDto.PictureFile,PictureType.User);
                    userUpdateDto.Picture = uploadedUserImage.resultStatus == ResultStatus.Succes ? uploadedUserImage.Data.FullName
                        : "userImages/defaultUser.png";
                    if (oldUserPicture != "userImages/defaultUser.png")
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
                        _ımageHelper.Delete(oldUserPicture);
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
                    var uploadedUserImage = await _ımageHelper.Upload(userUpdateDto.UserName, userUpdateDto.PictureFile,PictureType.User);
                    userUpdateDto.Picture = uploadedUserImage.resultStatus == ResultStatus.Succes ? uploadedUserImage.Data.FullName
                        : "userImages/defaultUser.png";
                    if (oldUserPicture != "userImages/defaultUser.png")
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
                        _ımageHelper.Delete(oldUserPicture);
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
        [Authorize(Roles = "SuperAdmin,User.Read")]
        [HttpGet]
        public async Task<PartialViewResult> GetDetail(int userId)
        {
            var user = await _usermanager.Users.SingleOrDefaultAsync(u => u.Id == userId);
            return PartialView("_GetDetailPartial", new UserDto { User = user });
        }
    }
}
