using MyBlog.Entities.ComplexTypes;
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
        Task<IDataResult<ArticleUpdateDto>> GetArticleUpdateDto(int articleId);
        Task<IDataResult<ArticleListdDto>> GetAll();//bütün kategorileri getir
        Task<IDataResult<ArticleListdDto>> GetAllByNonDeleted();//silinmemiş kategorileri alır
        Task<IDataResult<ArticleListdDto>> GetAllByDeleted();
        Task<IDataResult<ArticleListdDto>> GetAllByNonDeletedAndActive();
        Task<IDataResult<ArticleListdDto>> GetAllByCategpry(int categoryId);
        Task<IDataResult<ArticleListdDto>> GetAllByViewCount(bool isAscending,int takeSize);
        Task<IDataResult<ArticleListdDto>> GetAllByPaging(int? categoryId,int currentPage=1,int pageSize=5,bool isAscending=false);
        Task<IDataResult<ArticleListdDto>> GetAllByUserIdOnFilter(int userId,FilterBy filterBy,OrderBy orderBy, bool isAscending, int takeSize,
            int categoryId,DateTime startAt,DateTime endTime,int minViewCount, int maxViewCount, int minCommentCount, int maxCommentCount);
        Task<IDataResult<ArticleListdDto>> Search(string keyword, int currentPage= 1, int pageSize = 5, bool isAscending = false);
        Task<IResult> IncreaseViewCountAsync(int articleId);
        Task<IResult> Add(ArticleAddDto articleleAddDto, string createdByName,int userId); //kategori ekleme işlemi yapacak, ekleme işleminin durumuna göre result dönecek
        Task<IResult> Update(ArticleUpdateDto articleUpdateDto, string modifiedByName);
        Task<IResult> Delete(int articleId, string modifiedByName);
        Task<IResult> HardDelete(int articleId);
        Task<IResult> UndoDelete(int articleId, string modifiedByName);
        Task<IDataResult<int>> Count();
        Task<IDataResult<int>> CountByNonDeleted();

    }
}
