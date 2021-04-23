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
    public interface IArticleService
    {
        Task<IDataResult<ArticleDto>> Get(int articleId);//belirli bir kategoriyi dönderecek
        Task<IDataResult<ArticleListdDto>> GetAll();//bütün kategorileri getir
        Task<IDataResult<ArticleListdDto>> GetAllByNonDeleted();//silinmemiş kategorileri alır
        Task<IDataResult<ArticleListdDto>> GetAllByNonDeletedAndActive();
        Task<IDataResult<ArticleListdDto>> GetAllByCategpry(int categoryId);
        Task<IResult> Add(ArticleAddDto articleleAddDto, string createdByName); //kategori ekleme işlemi yapacak, ekleme işleminin durumuna göre result dönecek
        Task<IResult> Update(ArticleUpdateDto articleUpdateDto, string modifiedByName);
        Task<IResult> Delete(int articleId, string modifiedByName);
        Task<IResult> HardDelete(int articleId);

    }
}
