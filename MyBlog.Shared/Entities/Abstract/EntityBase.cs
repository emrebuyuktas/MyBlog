using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Shared.Entities.Abstract
{
    public abstract class EntityBase
    {
        public virtual int Id { get; set; }
        public virtual DateTime CreateDate { get; set; } = DateTime.Now; //abstract ve virtual olarak yazdığımız için override edilebilecek
        public virtual DateTime ModiefiedDate { get; set; } = DateTime.Now;
        public virtual bool IsDeleted { get; set; } = false;
        public virtual bool IsActive { get; set; } = true;
        public virtual string CreatedByName { get; set; } = "Admin";
        public virtual string ModiefiedByName { get; set; } = "Admin";
        public virtual string Note { get; set; }
    }
}
