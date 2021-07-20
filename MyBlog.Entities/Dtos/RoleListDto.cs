using MyBlog.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Entities.Dtos
{
    public class RoleListDto
    {
        public IList<Role> Roles { get; set; }
    }
}
