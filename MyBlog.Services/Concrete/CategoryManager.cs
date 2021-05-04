using AutoMapper;
using MyBlog.Data.Abstract;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;
using MyBlog.Services.Abstract;
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
    public class CategoryManager : ICategoryService
    {
        private readonly IUnitOfWork _unityOfWork;
        private readonly IMapper _mapper;

        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unityOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<CategoryDto>> Add(CategoryAddDto categoryAddDto, string createdByName)
        {
            var addedCategory=await _unityOfWork.Categories.AddASync(new Category 
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
            await _unityOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Succes, $"{categoryAddDto.Name} adlı kategori başarıyla eklenmiştir",new CategoryDto 
            { 
                Category=addedCategory,
                ResultStatus=ResultStatus.Succes,
                Message= $"{categoryAddDto.Name} adlı kategori başarıyla eklenmiştir"
            });
        }

        public async Task<IDataResult<CategoryDto>> Delete(int categoryId, string modifiedByName)
        {
            var category = await _unityOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                category.IsDeleted = true;
                category.IsActive = false;
                category.ModiefiedByName = modifiedByName;
                category.ModiefiedDate = DateTime.Now;
                var deletedCategory = await _unityOfWork.Categories.UpdateAsync(category);
                await _unityOfWork.SaveAsync();
                return new DataResult<CategoryDto>(ResultStatus.Succes, $"{deletedCategory.Name} adlı kategori başarıyla silinmiştir", new CategoryDto
                {
                    Category = deletedCategory,
                    ResultStatus = ResultStatus.Succes,
                    Message = $"{deletedCategory.Name} adlı kategori başarıyla silinmiştir"
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
            var category=await _unityOfWork.Categories.GetAsync(c=>c.Id==categoryId,c=>c.Articles);
            if (category != null)
                return new DataResult<CategoryDto>(ResultStatus.Succes, new CategoryDto { 
                Category=category,
                ResultStatus=ResultStatus.Succes
                });
            return new DataResult<CategoryDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı", new CategoryDto 
            {
                Category=category,
                ResultStatus=ResultStatus.Error,
                Message= "Böyle bir kategori bulunamadı"
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAll()
        {
            var categories = await _unityOfWork.Categories.GetAllAsync(null,c=>c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Succes, new CategoryListDto
                {

                    Categories = categories,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Hiçbir kategori bulunamadı", new CategoryListDto
            {

                Categories = categories,
                ResultStatus = ResultStatus.Error,
                Message="Hiçbir kategori bulunamadı."
  
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeleted()
        {
            var categories = await _unityOfWork.Categories.GetAllAsync(c=>!c.IsDeleted,c=>c.Articles);
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Succes, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Succes,
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Hiçbir kategori bulunamadı", new CategoryListDto
            {
                Categories = categories,
                ResultStatus = ResultStatus.Error,
               Message = "Aktif hiçbir kategori bulunamadı."
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActive()
        {
            var categories = await _unityOfWork.Categories.GetAllAsync(c => !c.IsDeleted && c.IsActive, c => c.Articles);
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
            var result = await _unityOfWork.Categories.AnyAsync(c=>c.Id==categoryId);
            if (result)
            {
                var category = await _unityOfWork.Categories.GetAsync(c => c.Id == categoryId);
                var categoryUpdateDto = _mapper.Map<CategoryUpdateDto>(category);
                return new DataResult<CategoryUpdateDto>(ResultStatus.Succes, categoryUpdateDto);
            }
            return new DataResult<CategoryUpdateDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı",null);
        }

        public async Task<IResult> HardDelete(int categoryId)
        {

            var category = await _unityOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                await _unityOfWork.Categories.DeleteAsync(category);
                await _unityOfWork.SaveAsync();
                return new Result(ResultStatus.Succes, $"{category.Name} adlı kategori başarıyla veritabanından silinmiştir");
            }
            return new Result(ResultStatus.Error, $"{category.Name} adlı kategori silinemedi");
        }

        public async Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            var category = await _unityOfWork.Categories.GetAsync(c => c.Id == categoryUpdateDto.Id);
            //bu kısımlar automapper ile değiştirilebilir!
                category.Name = categoryUpdateDto.Name;
                category.Description = categoryUpdateDto.Description;
                category.Note = categoryUpdateDto.Note;
                category.IsActive = categoryUpdateDto.IsActive;
                category.IsDeleted = categoryUpdateDto.IsDeleted;
                category.ModiefiedByName = modifiedByName;
                category.ModiefiedDate = DateTime.Now;
                var updatedCategpry=await _unityOfWork.Categories.UpdateAsync(category);
                await _unityOfWork.SaveAsync();
                return new DataResult<CategoryDto>(ResultStatus.Succes, $"{categoryUpdateDto.Name} adlı kategori başarıyla güncellenmiştir",new CategoryDto 
                {
                    Category=updatedCategpry,
                    ResultStatus=ResultStatus.Succes,
                    Message= $"{categoryUpdateDto.Name} adlı kategori başarıyla güncellenmiştir"
                });
        }
    }
}
