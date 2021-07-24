using MyBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Mvc.Models
{
    public class ArticleSearchViewModel
    {
        public ArticleListdDto ArticleLisDto { get; set; }
        public string Keyword { get; set; }
    }
}
