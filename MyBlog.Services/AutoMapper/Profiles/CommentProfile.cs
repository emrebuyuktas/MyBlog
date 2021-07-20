using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;

namespace MyBlog.Services.AutoMapper.Profiles
{
    public class CommentProfile:Profile
    {
        public CommentProfile()
        {
            CreateMap<CommentAddDto, Comment>()
                .ForMember(dest => dest.CreateDate, opt => opt.MapFrom(x => DateTime.Now))
                .ForMember(dest => dest.ModiefiedDate, opt => opt.MapFrom(x => DateTime.Now))
                .ForMember(dest => dest.ModiefiedByName, opt => opt.MapFrom(x => x.CreatedByName))
                .ForMember(dest=>dest.IsActive,opt=>opt.MapFrom(x=>false));
            CreateMap<CommentUpdateDto, Comment>()
                .ForMember(dest => dest.ModiefiedDate, opt => opt.MapFrom(x => DateTime.Now));
            CreateMap<Comment, CommentUpdateDto>();

        }
    }
}
