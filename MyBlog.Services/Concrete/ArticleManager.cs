﻿using AutoMapper;
using MyBlog.Data.Abstract;
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
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Services.Concrete
{
    class ArticleManager : ManagerBase,IArticleService
    {

        public ArticleManager(IUnitOfWork unitOfWork,IMapper mapper):base(unitOfWork,mapper)
        {

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
