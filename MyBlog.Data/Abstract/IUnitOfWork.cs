using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Data.Abstract
{
    //unity of work tüm repoistorilerimizi tek yerden yönetebilmemizi sağlar
    public interface IUnitOfWork:IAsyncDisposable
    {
        IArticleRepository Articles { get; }
        ICategoryRepository Categories { get; }
        ICommentRepository Comments { get; }
        Task<int> SaveAsync();
    }
}
