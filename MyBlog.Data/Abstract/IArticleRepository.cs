using MyBlog.Entities.Concrete;
using MyBlog.Shared.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Data.Abstract
{
    public interface IArticleRepository:IEntityRepository<Article>
    {
    }
}
