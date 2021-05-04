using AutoMapper;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Services.AutoMapper.Profiles
{
    public class CategoryProfile:Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryAddDto,Category>().ForMember(dest=>dest.CreateDate,opt=>opt.MapFrom(x=>DateTime.Now));
            CreateMap<CategoryUpdateDto, Category>().ForMember(dest => dest.ModiefiedDate, opt => opt.MapFrom(x => DateTime.Now));
            CreateMap<Category, CategoryUpdateDto>();
        }
    }
}
