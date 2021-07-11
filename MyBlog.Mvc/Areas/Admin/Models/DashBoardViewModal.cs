using MyBlog.Entities.Concrete;
using MyBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Mvc.Areas.Admin.Models
{
    public class DashBoardViewModal
    {
        public int CategoriesCount { get; set; }
        public int ArticlesCount { get; set; }
        public int CommnetsCount { get; set; }
        public int UsersCount { get; set; }
        public ArticleListdDto Articles { get; set; }
    }
}
