using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using MyBlog.Entities.ComplexTypes;
using MyBlog.Entities.Dtos;
using MyBlog.Mvc.Helpers.Abstract;
using MyBlog.Shared.Utilities.Extensions;
using MyBlog.Shared.Utilities.Results.Abstract;
using MyBlog.Shared.Utilities.Results.ComplexTypes;
using MyBlog.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Mvc.Helpers.Concrete
{
    public class ImageHelper : IImageHelper
    {

        private readonly IWebHostEnvironment _env;
        private readonly string _wwwroot;
        private readonly string imgFolder = "img";
        private const string userImageFolder = "userImages";
        private const string postImageFolder = "poastImages";

        public ImageHelper(IWebHostEnvironment env)
        {
            _env = env;
            _wwwroot = _env.WebRootPath;
        }

        public IDataResult<ImageDeletedDto> Delete(string pictureName)
        {
                
                var fileToDelete = Path.Combine($"{_wwwroot}/{imgFolder}/", pictureName);
                if (System.IO.File.Exists(fileToDelete))
                {
                var fileInfo = new FileInfo(fileToDelete);
                var ımageDeletedDto = new ImageDeletedDto
                {
                    FullName = pictureName,
                    Extension=fileInfo.Extension,
                    Path=fileInfo.FullName,
                    Size=fileInfo.Length
                };
                    System.IO.File.Delete(fileToDelete);
                return new DataResult<ImageDeletedDto>(ResultStatus.Succes, ımageDeletedDto);
                }
            return new DataResult<ImageDeletedDto>(ResultStatus.Error, "Böyle bir resim bulunamadı", null);
        }

        public async Task<IDataResult<UploadedImageDto>> Upload(string name, IFormFile pictureFile, PictureType pictureType, string folderName=null)
        {
            folderName ??= pictureType == PictureType.User ? userImageFolder : postImageFolder;
            if (!Directory.Exists($"{_wwwroot}/{imgFolder}/{folderName}"))
            {
                Directory.CreateDirectory($"{_wwwroot}/{imgFolder}/{folderName}");
            }
            string oldFileName = Path.GetFileNameWithoutExtension(pictureFile.FileName);
            string fileExtension = Path.GetExtension(pictureFile.FileName);
            DateTime dateTime = DateTime.Now;
            string newFileName = $"{name}_{dateTime.FullDateAndTimeStringWithUnderScore()}{fileExtension}";
            var path = Path.Combine($"{_wwwroot}/{imgFolder}/{folderName}", newFileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await pictureFile.CopyToAsync(stream);
            }
            string message = pictureType == PictureType.User ? $"{name} adlı kullanıcının resmi başarıyla yüklenmiştir." : $"{name} adlı makalenin resmi başarıyla yüklenmiştir.";
            return new DataResult<UploadedImageDto>(ResultStatus.Succes,message,new UploadedImageDto 
            { 
                FullName=$"{folderName}/{newFileName}",
                OldName=oldFileName,
                Extension=fileExtension,
                FolderName=folderName,
                Path=path,
                Size=pictureFile.Length
            });
        }

    }
}
