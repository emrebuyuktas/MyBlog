using MyBlog.Data.Abstract;
using MyBlog.Data.Concrete.EntitiyFramework.Contexts;
using MyBlog.Data.Concrete.EntitiyFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyBlogContext _context;
        private EfArticleRepository _articleRepository;
        private EfCategoryRepository _efCategoryRepository;
        private EfCommentRepository _efCommentRepository;
        public UnitOfWork(MyBlogContext context)
        {
            _context = context;
        }
        public IArticleRepository Articles => _articleRepository ??= new EfArticleRepository(_context);//?? operatörü ile bir nesnenin null olup olmadığı kontrol edilebilir, eğer null ise newleyip dönüyruz.

        public ICategoryRepository Categories => _efCategoryRepository ??= new EfCategoryRepository(_context);

        public ICommentRepository Comments => _efCommentRepository ??= new EfCommentRepository(_context);

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
