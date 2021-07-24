using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Mvc.Models
{
    public class ArticleDetailRightSiteBarViewModel
    {
        public string Header { get; set; }
        public ArticleListdDto ArticleListdDto { get; set; }
        public User User { get; set; }
    }
}
