using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;
using MyBlog.Shared.Utilities.Results.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Services.Abstract
{
    public interface ICategoryService
    {
        Task<IDataResult<CategoryDto>> Get(int categoryId);//belirli bir kategoriyi dönderecek
        Task<IDataResult<CategoryListDto>> GetAll();//bütün kategorileri getir
        Task<IDataResult<CategoryListDto>> GetAllByNonDeleted();//silinmemiş kategorileri alır
        Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActive();
        Task<IResult> Add(CategoryAddDto categoryAddDto,string createdByName); //kategori ekleme işlemi yapacak, ekleme işleminin durumuna göre result dönecek
        Task<IResult> Update(CategoryUpdateDto categoryUpdateDto,string modifiedByName);
        Task<IResult> Delete(int categoryId, string modifiedByName);
        Task<IResult> HardDelete(int categoryId);

    }
}
