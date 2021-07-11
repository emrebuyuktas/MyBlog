using MyBlog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Shared.Data.Abstract
{

    //bir çok yerde ortak olarak kullanacağımız metodları generiz olarak tanımlıyoruz.
    public interface IEntityRepository<T> where T:class,IEntity,new()//newlenebilir olmalı
    {
        //Expression metod çağırıldığında içine parametre olarak verilecek lambda sorgularını belirtir.
        //Tek bir get işlemi için.
        Task<T> GetAsync(Expression<Func<T,bool>> predicate,params Expression<Func<T,object>>[] includeProperties); 
        //var kullanici=repository.GetaAsync(k=>k.id==15)
        //params sayesinde verilen birden fazla parametreyi arraye eklenecek, birden fazla lambda gönderebviliriz.
        //çoklu get işlemi için
        Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate=null, params Expression<Func<T, object>>[] includeProperties);//predicate null gelirse bütün veriler getirilecek eğer null gelmezse yapılan filtrelemeye göre gelecek.
        Task<T> AddASync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<bool> AnyAsync(Expression<Func<T, bool>> predicate); //kullanıcı vs eklenirken daha önce eklenmiş mi gibi kontroller için
        Task<int> CountAsync(Expression<Func<T, bool>> predicate=null);
    }
}
