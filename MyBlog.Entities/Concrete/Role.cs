using MyBlog.Shared.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MyBlog.Entities.Concrete
{
    public class Role:IdentityRole<int> //int pk ile oluşacak
    {

    }
}
