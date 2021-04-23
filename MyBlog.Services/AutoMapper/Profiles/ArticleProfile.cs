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
    public class ArticleProfile:Profile
    {
        public ArticleProfile()
        {
            CreateMap<ArticleAddDto, Article>().ForMember(dest=>dest.CreateDate,opt=>opt.MapFrom(x=>DateTime.Now));
            CreateMap<ArticleUpdateDto, Article>().ForMember(dest=>dest.ModiefiedDate,opt=>opt.MapFrom(x=>DateTime.Now));
        }
    }
}
