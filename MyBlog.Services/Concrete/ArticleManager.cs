using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyBlog.Data.Abstract;
using MyBlog.Entities.ComplexTypes;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;
using MyBlog.Services.Abstract;
using MyBlog.Services.Utilities;
using MyBlog.Shared.Utilities.Results.Abstract;
using MyBlog.Shared.Utilities.Results.ComplexTypes;
using MyBlog.Shared.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Services.Concrete
{
    class ArticleManager : ManagerBase,IArticleService
    {
        private readonly UserManager<User> _userManager;
        public ArticleManager(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager) : base(unitOfWork, mapper)
        {
            _userManager = userManager;
        }

        public async Task<IResult> Add(ArticleAddDto articleleAddDto, string createdByName, int userId)
        {
            var article = Mapper.Map<Article>(articleleAddDto); //gelen articleadddto mapper sayesinde article a dönüitürülecek bir tür model biding
            article.CreatedByName = createdByName;
            article.ModiefiedByName = createdByName;
            article.UserId = userId;
            await UnitOfWork.Articles.AddASync(article);
            await UnitOfWork.SaveAsync();
            return new Result(ResultStatus.Succes,$"{articleleAddDto.Title} başlıklı makale başarıyla eklendi");

        }

        public async Task<IDataResult<int>> Count()
        {
            var articlesCount = await UnitOfWork.Articles.CountAsync();
            if (articlesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Succes, articlesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılaşıldı", -1);
            }
        }

        public async Task<IDataResult<int>> CountByNonDeleted()
        {
            var articlesCount = await UnitOfWork.Articles.CountAsync(c=>!c.IsDeleted);
            if (articlesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Succes, articlesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılaşıldı", -1);
            }
        }

        public async Task<IResult> Delete(int articleId, string modifiedByName)
        {
            var result = await UnitOfWork.Articles.AnyAsync(a=>a.Id==articleId);
            if (result)
            {
                var article = await UnitOfWork.Articles.GetAsync(a => a.Id == articleId);
                article.IsActive = false;
                article.IsDeleted = true;
                article.ModiefiedByName = modifiedByName;
                article.ModiefiedDate = DateTime.Now;
                await UnitOfWork.Articles.UpdateAsync(article);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStatus.Succes, $"{article.Title} başlıklı makale başarıyla silindi");
            }
            return new Result(ResultStatus.Error, "böyle bir makale bulunamadı");
        }

        public async Task<IDataResult<ArticleDto>> Get(int articleId)
        {
            var article = await UnitOfWork.Articles.GetAsync(a=>a.Id==articleId,a=>a.User,a=>a.Category);//params sayesinde istediğimiz kadar parametre gönderebiliriz.

            if (article != null)
            {
                var articlesComment = await UnitOfWork.Comments.GetAllAsync(c => c.ArticleId == articleId && !c.IsDeleted && c.IsActive);
                return new DataResult<ArticleDto>(ResultStatus.Succes, new ArticleDto
                {
                    Article=article,
                    ResultStatus=ResultStatus.Succes
                });
            }
            return new DataResult<ArticleDto>(ResultStatus.Error,Messages.Article.NotFound(isPlural:false),null);
        }

        public async Task<IDataResult<ArticleListdDto>> GetAll()
        {
            var articles = await UnitOfWork.Articles.GetAllAsync(null,a=>a.User,a=>a.Category);
            if (articles.Count>-1)
            {
                return new DataResult<ArticleListdDto>(ResultStatus.Succes,new ArticleListdDto 
                { 
                    Articles=articles,
                    ResultStatus=ResultStatus.Succes
                });
            }
            return new DataResult<ArticleListdDto>(ResultStatus.Error, Messages.Article.NotFound(isPlural:true),null);
        }

        public async Task<IDataResult<ArticleListdDto>> GetAllByCategpry(int categoryId)
        {
            var result = await UnitOfWork.Categories.AnyAsync(c=>c.Id==categoryId);
            if (result == true)
            {
                var articles = await UnitOfWork.Articles.GetAllAsync(a => a.CategoryId == categoryId && !a.IsDeleted && a.IsActive, a => a.User, a => a.Category);
                if (articles.Count > -1)
                {
                    return new DataResult<ArticleListdDto>(ResultStatus.Succes, new ArticleListdDto
                    {
                        Articles = articles,
                        ResultStatus = ResultStatus.Succes
                    });
                }
                return new DataResult<ArticleListdDto>(ResultStatus.Error, "Kategori bulunamadı", null);
            }
            return new DataResult<ArticleListdDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı", null);
        }

        public async Task<IDataResult<ArticleListdDto>> GetAllByDeleted()
        {
            var articles = await UnitOfWork.Articles.GetAllAsync(a => a.IsDeleted, a => a.User, a => a.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListdDto>(ResultStatus.Succes, new ArticleListdDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<ArticleListdDto>(ResultStatus.Error, "Makaleler bulunamadı", null);
        }

        public async Task<IDataResult<ArticleListdDto>> GetAllByNonDeleted()
        {
            var articles = await UnitOfWork.Articles.GetAllAsync(a=>!a.IsDeleted, a => a.User, a => a.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListdDto>(ResultStatus.Succes, new ArticleListdDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<ArticleListdDto>(ResultStatus.Error, "Makaleler bulunamadı", null);
        }

        public async Task<IDataResult<ArticleListdDto>> GetAllByNonDeletedAndActive()
        {
            var articles = await UnitOfWork.Articles.GetAllAsync(a => !a.IsDeleted && a.IsActive, a => a.User, a => a.Category);
            if (articles.Count > -1)
            {
                return new DataResult<ArticleListdDto>(ResultStatus.Succes, new ArticleListdDto
                {
                    Articles = articles,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<ArticleListdDto>(ResultStatus.Error, "Makaleler bulunamadı", null);
        }

        public async Task<IDataResult<ArticleListdDto>> GetAllByPaging(int? categoryId, int currentPage = 1, int pageSize = 5, bool isAscending = false)
        {
            pageSize = pageSize > 20 ? 20 : pageSize;
            var articles = categoryId == null ? await UnitOfWork.Articles.GetAllAsync(a => a.IsActive && !a.IsDeleted, a => a.Category, a => a.User) :
                await UnitOfWork.Articles.GetAllAsync(a =>a.CategoryId==categoryId &&a.IsActive && !a.IsDeleted, a => a.Category, a => a.User);
            var sortedArticles = isAscending ? articles.OrderBy(a => a.Date).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList() :
                articles.OrderByDescending(a => a.Date).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return new DataResult<ArticleListdDto>(ResultStatus.Succes,new ArticleListdDto { 
                Articles=sortedArticles,
                CategoryId=categoryId==null?null:categoryId.Value,
                CurrentPage=currentPage,
                PageSize=pageSize,
                TotalCount=articles.Count,
                IsAscending=isAscending
            });
        }

        public async Task<IDataResult<ArticleListdDto>> GetAllByUserIdOnFilter(int userId, FilterBy filterBy, OrderBy orderBy, bool isAscending, int takeSize, int categoryId, DateTime startAt, DateTime endTime, int minViewCount, int maxViewCount, int minCommentCount, int maxCommentCount)
        {
            var anyUser = await _userManager.Users.AnyAsync(u=>u.Id==userId);
            if (!anyUser)
            {
                return new DataResult<ArticleListdDto>(ResultStatus.Error,$"{userId} numaralı kullanıcı bulunamadı",null);
            }
            var userArticles = await UnitOfWork.Articles.GetAllAsync(a => a.IsActive && !a.IsDeleted && a.UserId == userId);
            List<Article> sortedArticles = new List<Article>();
            switch (filterBy)
            {
                case FilterBy.Category:
                    switch (orderBy)
                    {
                        case OrderBy.Date:
                            sortedArticles = isAscending ? userArticles.Where(a => a.Id == categoryId).Take(takeSize).OrderBy(a => a.Date).ToList() :
                                userArticles.Where(a => a.Id == categoryId).Take(takeSize).OrderByDescending(a => a.Date).ToList();
                            break;
                        case OrderBy.ViewCount:
                            sortedArticles = isAscending ? userArticles.Where(a => a.Id == categoryId).Take(takeSize).OrderBy(a => a.ViewsCount).ToList() :
                                userArticles.Where(a => a.Id == categoryId).Take(takeSize).OrderByDescending(a => a.ViewsCount).ToList();
                            break;
                        case OrderBy.CommentCount:
                            sortedArticles = isAscending ? userArticles.Where(a => a.Id == categoryId).Take(takeSize).OrderBy(a => a.CommentCount).ToList() :
                                userArticles.Where(a => a.Id == categoryId).Take(takeSize).OrderByDescending(a => a.CommentCount).ToList();
                            break;
                    }
                    break;
                case FilterBy.Date:
                    switch (orderBy)
                    {
                        case OrderBy.Date:
                            sortedArticles = isAscending ? userArticles.Where(a => a.Date>= startAt && a.Date <= endTime).Take(takeSize).OrderBy(a => a.Date).ToList() :
                                userArticles.Where(a => a.Date >= startAt && a.Date <= endTime).Take(takeSize).OrderByDescending(a => a.Date).ToList();
                            break;
                        case OrderBy.ViewCount:
                            sortedArticles = isAscending ? userArticles.Where(a => a.Date >= startAt && a.Date <= endTime).Take(takeSize).OrderBy(a => a.ViewsCount).ToList() :
                                userArticles.Where(a => a.Date >= startAt && a.Date <= endTime).Take(takeSize).OrderByDescending(a => a.ViewsCount).ToList();
                            break;
                        case OrderBy.CommentCount:
                            sortedArticles = isAscending ? userArticles.Where(a => a.Date >= startAt && a.Date <= endTime).OrderBy(a => a.CommentCount).ToList() :
                                userArticles.Where(a => a.Date >= startAt && a.Date <= endTime).Take(takeSize).OrderByDescending(a => a.CommentCount).ToList();
                            break;
                    }
                    break;
                case FilterBy.ViewCount:
                    switch (orderBy)
                    {
                        case OrderBy.Date:
                            sortedArticles = isAscending ? userArticles.Where(a => a.ViewsCount >= minViewCount && a.ViewsCount <= maxViewCount).Take(takeSize).OrderBy(a => a.Date).ToList() :
                                userArticles.Where(a => a.ViewsCount >= minViewCount && a.ViewsCount <= maxViewCount).Take(takeSize).OrderByDescending(a => a.Date).ToList();
                            break;
                        case OrderBy.ViewCount:
                            sortedArticles = isAscending ? userArticles.Where(a => a.ViewsCount >= minViewCount && a.ViewsCount <= maxViewCount).Take(takeSize).OrderBy(a => a.ViewsCount).ToList() :
                                userArticles.Where(a => a.ViewsCount >= minViewCount && a.ViewsCount <= maxViewCount).Take(takeSize).OrderByDescending(a => a.ViewsCount).ToList();
                            break;
                        case OrderBy.CommentCount:
                            sortedArticles = isAscending ? userArticles.Where(a => a.ViewsCount >= minViewCount && a.ViewsCount <= maxViewCount).OrderBy(a => a.CommentCount).ToList() :
                                userArticles.Where(a => a.ViewsCount >= minViewCount && a.ViewsCount <= maxViewCount).Take(takeSize).OrderByDescending(a => a.CommentCount).ToList();
                            break;
                    }
                    break;
                case FilterBy.CommentCount:
                    switch (orderBy)
                    {
                        case OrderBy.Date:
                            sortedArticles = isAscending ? userArticles.Where(a => a.CommentCount >= minCommentCount && a.CommentCount <= maxCommentCount).Take(takeSize).OrderBy(a => a.Date).ToList() :
                                userArticles.Where(a => a.CommentCount >= minCommentCount && a.CommentCount <= maxCommentCount).Take(takeSize).OrderByDescending(a => a.Date).ToList();
                            break;
                        case OrderBy.ViewCount:
                            sortedArticles = isAscending ? userArticles.Where(a => a.CommentCount >= minCommentCount && a.CommentCount <= maxCommentCount).Take(takeSize).OrderBy(a => a.ViewsCount).ToList() :
                                userArticles.Where(a => a.CommentCount >= minCommentCount && a.CommentCount <= maxCommentCount).Take(takeSize).OrderByDescending(a => a.ViewsCount).ToList();
                            break;
                        case OrderBy.CommentCount:
                            sortedArticles = isAscending ? userArticles.Where(a => a.CommentCount >= minCommentCount && a.CommentCount <= maxCommentCount).OrderBy(a => a.CommentCount).ToList() :
                                userArticles.Where(a => a.CommentCount >= minCommentCount && a.CommentCount <= maxCommentCount).Take(takeSize).OrderByDescending(a => a.CommentCount).ToList();
                            break;
                    }
                    break;
            }
            return new DataResult<ArticleListdDto>(ResultStatus.Succes, new ArticleListdDto { 
                Articles=sortedArticles
            });
        }

        public async Task<IDataResult<ArticleListdDto>> GetAllByViewCount(bool isAscending, int takeSize)
        {
            var articles = await UnitOfWork.Articles.GetAllAsync(a=>a.IsActive&&!a.IsDeleted,c=>c.Category,a=>a.User);
            var sortedArticles = isAscending ? articles.OrderBy(a=>a.ViewsCount):articles.OrderByDescending(a=>a.ViewsCount);
            return new DataResult<ArticleListdDto>(ResultStatus.Succes,new ArticleListdDto { 
                Articles=takeSize==null ? sortedArticles.ToList():sortedArticles.Take(takeSize).ToList()
            }); 
        }

        public async Task<IDataResult<ArticleUpdateDto>> GetArticleUpdateDto(int articleId)
        {
            var result = await UnitOfWork.Articles.AnyAsync(c => c.Id == articleId);
            if (result)
            {
                var article = await UnitOfWork.Articles.GetAsync(c => c.Id == articleId);
                var articleUpdateDto = Mapper.Map<ArticleUpdateDto>(article);
                return new DataResult<ArticleUpdateDto>(ResultStatus.Succes, articleUpdateDto);
            }
            return new DataResult<ArticleUpdateDto>(ResultStatus.Error, Messages.Article.NotFound(isPlural: false), null);
        }

        public async Task<IResult> HardDelete(int articleId)
        {
            var result = await UnitOfWork.Articles.AnyAsync(a => a.Id == articleId);
            if (result)
            {
                var article = await UnitOfWork.Articles.GetAsync(a => a.Id == articleId);
                await UnitOfWork.Articles.DeleteAsync(article);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStatus.Succes, $"{article.Title} başlıklı makale veritabanından başarıyla silindi");
            }
            return new Result(ResultStatus.Error, "böyle bir makale bulunamadı");
        }

        public async Task<IResult> IncreaseViewCountAsync(int articleId)
        {
            var article = await UnitOfWork.Articles.GetAsync(a=>a.Id==articleId);
            if (article==null)
            {
                return new Result(ResultStatus.Error, Messages.Article.NotFound(false));
            }
            article.ViewsCount += 1;
             await UnitOfWork.Articles.UpdateAsync(article);
            await UnitOfWork.SaveAsync();
            return new Result(ResultStatus.Succes, Messages.Article.IncreaseViewCount(article.Title));
        }

        public async Task<IDataResult<ArticleListdDto>> Search(string keyword, int currentPage  = 1, int pageSize = 5, bool isAscending = false)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {

                var articles = await UnitOfWork.Articles.GetAllAsync(a => a.IsActive && !a.IsDeleted, a => a.Category, a => a.User);
                var sortedArticles = isAscending ? articles.OrderBy(a => a.Date).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList() :
                    articles.OrderByDescending(a => a.Date).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                return new DataResult<ArticleListdDto>(ResultStatus.Succes, new ArticleListdDto
                {
                    Articles = sortedArticles,
                    CurrentPage = currentPage,
                    PageSize = pageSize,
                    TotalCount = articles.Count
                });
            }
            var searchArticle = await UnitOfWork.Articles.Search(new List<Expression<Func<Article, bool>>> { 
                (a)=>a.Title.Contains(keyword),
                (a)=>a.Category.Name.Contains(keyword),
                (a)=>a.SeoTags.Contains(keyword),
                (a)=>a.SeoDescription.Contains(keyword)
            },a=>a.Category,a=>a.User);

            var searchSortedArticles = isAscending ? searchArticle.OrderBy(a => a.Date).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList() :
                searchArticle.OrderByDescending(a => a.Date).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
            return new DataResult<ArticleListdDto>(ResultStatus.Succes, new ArticleListdDto
            {
                Articles = searchSortedArticles,
                CurrentPage = currentPage,
                PageSize = pageSize,
                TotalCount = searchArticle.Count
            });
        }

        public async Task<IResult> UndoDelete(int articleId, string modifiedByName)
        {
            var result = await UnitOfWork.Articles.AnyAsync(a => a.Id == articleId);
            if (result)
            {
                var article = await UnitOfWork.Articles.GetAsync(a => a.Id == articleId);
                article.IsActive = true;
                article.IsDeleted = false;
                article.ModiefiedByName = modifiedByName;
                article.ModiefiedDate = DateTime.Now;
                await UnitOfWork.Articles.UpdateAsync(article);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStatus.Succes, Messages.Article.UndoDelete(article.Title));
            }
            return new Result(ResultStatus.Error, "böyle bir makale bulunamadı");
        }

        public async Task<IResult> Update(ArticleUpdateDto articleUpdateDto, string modifiedByName)
        {
            var oldArticle = await UnitOfWork.Articles.GetAsync(a=>a.Id==articleUpdateDto.Id);
            var article = Mapper.Map<ArticleUpdateDto,Article>(articleUpdateDto,oldArticle);
            article.ModiefiedByName = modifiedByName;
            await UnitOfWork.Articles.UpdateAsync(article);
            await UnitOfWork.SaveAsync();
            return new Result(ResultStatus.Succes, $"{articleUpdateDto.Title} başlıklı makale başarıyla güncellendi");
        }
    }
}
