using Microsoft.AspNetCore.Http;
using MyBlog.Entities.ComplexTypes;
using MyBlog.Entities.Dtos;
using MyBlog.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Mvc.Helpers.Abstract
{
    public interface IImageHelper
    {
        Task<IDataResult<UploadedImageDto>> Upload(string name,IFormFile pictureFile,PictureType pictureType,string folderName=null);
        IDataResult<ImageDeletedDto> Delete(string pictureName);
    }
}
