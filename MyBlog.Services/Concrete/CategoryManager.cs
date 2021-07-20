using AutoMapper;
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
    public class CategoryManager : ManagerBase,ICategoryService
    {

        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper):base(unitOfWork, mapper)
        {

        }

        public async Task<IDataResult<CategoryDto>> Add(CategoryAddDto categoryAddDto, string createdByName)
        {
            var addedCategory=await UnitOfWork.Categories.AddASync(new Category 
            { 
                 Name=categoryAddDto.Name,
                 Description=categoryAddDto.Description,
                 Note=categoryAddDto.Note,
                 IsActive=categoryAddDto.IsActive,
                 CreatedByName=createdByName,
                 CreateDate=DateTime.Now,
                 ModiefiedByName=createdByName,
                 ModiefiedDate=DateTime.Now,
                 IsDeleted=false
            });
            await UnitOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Succes,Messages.Category.Add(addedCategory.Name),new CategoryDto 
            { 
                Category=addedCategory,
                ResultStatus=ResultStatus.Succes,
                Message= $"{categoryAddDto.Name} adlı kategori başarıyla eklenmiştir"
            });
        }

        public async Task<IDataResult<int>> Count()
        {
            var categoriesCount = await UnitOfWork.Categories.CountAsync();
            if (categoriesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Succes,categoriesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılaşıldı", -1);
            }
        }

        public async Task<IDataResult<int>> CountByNonDeleted()
        {
            var categoriesCount = await UnitOfWork.Categories.CountAsync(c=>!c.IsDeleted);
            if (categoriesCount > -1)
            {
                return new DataResult<int>(ResultStatus.Succes, categoriesCount);
            }
            else
            {
                return new DataResult<int>(ResultStatus.Error, $"Beklenmeyen bir hata ile karşılaşıldı", -1);
            }
        }

        public async Task<IDataResult<CategoryDto>> Delete(int categoryId, string modifiedByName)
        {
            var category = await UnitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                category.IsDeleted = true;
                category.IsActive = false;
                category.ModiefiedByName = modifiedByName;
                category.ModiefiedDate = DateTime.Now;
                var deletedCategory = await UnitOfWork.Categories.UpdateAsync(category);
                await UnitOfWork.SaveAsync();
                return new DataResult<CategoryDto>(ResultStatus.Succes, Messages.Category.Delete(deletedCategory.Name), new CategoryDto
                {
                    Category = deletedCategory,
                    ResultStatus = ResultStatus.Succes,
                    Message = Messages.Category.Delete(deletedCategory.Name)
                });
            }
            return new DataResult<CategoryDto>(ResultStatus.Succes,"Böyle adlı kategori bulunamadı", new CategoryDto
            {
                Category = null,
                ResultStatus = ResultStatus.Error,
                Message = "Böyle adlı kategori bulunamadı"
            });
        }

        public async Task<IDataResult<CategoryDto>> Get(int categoryId)
        {
            var category=await UnitOfWork.Categories.GetAsync(c=>c.Id==categoryId,c=>c.Articles);
            if (category != null)
                return new DataResult<CategoryDto>(ResultStatus.Succes, new CategoryDto { 
                Category=category,
                ResultStatus=ResultStatus.Succes
                });
            return new DataResult<CategoryDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural:false), new CategoryDto 
            {
                Category=category,
                ResultStatus=ResultStatus.Error,
                Message = Messages.Category.NotFound(isPlural: false)
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAll()
        {
            //var categories = await _unityOfWork.Categories.GetAllAsync(null,c=>c.Articles);
            var categories = await UnitOfWork.Categories.GetAllAsync(null);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Succes, new CategoryListDto
                {

                    Categories = categories,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: true), new CategoryListDto
            {

                Categories = categories,
                ResultStatus = ResultStatus.Error,
                Message="Hiçbir kategori bulunamadı."
  
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByDeleted()
        {
            var categories = await UnitOfWork.Categories.GetAllAsync(c => c.IsDeleted, c => c.Articles);
            if (categories.Count > -1)
                return new DataResult<CategoryListDto>(ResultStatus.Succes, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Succes
                });
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Hiçbir kategori bulunamadı", null);
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeleted()
        {
            var categories = await UnitOfWork.Categories.GetAllAsync(c=>!c.IsDeleted,c=>c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Succes, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Succes,
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: true), new CategoryListDto
            {
                Categories = categories,
                ResultStatus = ResultStatus.Error,
               Message = "Aktif hiçbir kategori bulunamadı."
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActive()
        {
            var categories = await UnitOfWork.Categories.GetAllAsync(c => !c.IsDeleted && c.IsActive, c => c.Articles);
            if (categories.Count > -1)
                return new DataResult<CategoryListDto>(ResultStatus.Succes, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Succes
                });
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Hiçbir kategori bulunamadı", null);
        }

        public async Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDto(int categoryId)
        {
            var result = await UnitOfWork.Categories.AnyAsync(c=>c.Id==categoryId);
            if (result)
            {
                var category = await UnitOfWork.Categories.GetAsync(c => c.Id == categoryId);
                var categoryUpdateDto = Mapper.Map<CategoryUpdateDto>(category);
                return new DataResult<CategoryUpdateDto>(ResultStatus.Succes, categoryUpdateDto);
            }
            return new DataResult<CategoryUpdateDto>(ResultStatus.Error, Messages.Category.NotFound(isPlural: false), null);
        }

        public async Task<IResult> HardDelete(int categoryId)
        {

            var category = await UnitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                await UnitOfWork.Categories.DeleteAsync(category);
                await UnitOfWork.SaveAsync();
                return new Result(ResultStatus.Succes, Messages.Category.HardDelete(category.Name));
            }
            return new Result(ResultStatus.Error, $"{category.Name} adlı kategori silinemedi");
        }

        public async Task<IDataResult<CategoryDto>> UndoDelete(int categoryId, string modifiedByName)
        {
            var category = await UnitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                category.IsDeleted = false;
                category.IsActive = true;
                category.ModiefiedByName = modifiedByName;
                category.ModiefiedDate = DateTime.Now;
                var deletedCategory = await UnitOfWork.Categories.UpdateAsync(category);
                await UnitOfWork.SaveAsync();
                return new DataResult<CategoryDto>(ResultStatus.Succes, Messages.Category.UndoDelete(deletedCategory.Name), new CategoryDto
                {
                    Category = deletedCategory,
                    ResultStatus = ResultStatus.Succes,
                    Message = Messages.Category.UndoDelete(deletedCategory.Name)
                });
            }
            return new DataResult<CategoryDto>(ResultStatus.Succes, "Böyle adlı kategori bulunamadı", new CategoryDto
            {
                Category = null,
                ResultStatus = ResultStatus.Error,
                Message = "Böyle adlı kategori bulunamadı"
            });
        }

        public async Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            var category = await UnitOfWork.Categories.GetAsync(c => c.Id == categoryUpdateDto.Id);
            //bu kısımlar automapper ile değiştirilebilir!
                category.Name = categoryUpdateDto.Name;
                category.Description = categoryUpdateDto.Description;
                category.Note = categoryUpdateDto.Note;
                category.IsActive = categoryUpdateDto.IsActive;
                category.IsDeleted = categoryUpdateDto.IsDeleted;
                category.ModiefiedByName = modifiedByName;
                category.ModiefiedDate = DateTime.Now;
                var updatedCategpry=await UnitOfWork.Categories.UpdateAsync(category);
                await UnitOfWork.SaveAsync();
                return new DataResult<CategoryDto>(ResultStatus.Succes, Messages.Category.Update(updatedCategpry.Name),new CategoryDto 
                {
                    Category=updatedCategpry,
                    ResultStatus=ResultStatus.Succes,
                    Message= Messages.Category.Update(updatedCategpry.Name)
                });
        }
    }
}
