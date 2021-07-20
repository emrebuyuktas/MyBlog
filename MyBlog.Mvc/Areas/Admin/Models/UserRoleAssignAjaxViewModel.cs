using MyBlog.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBlog.Mvc.Areas.Admin.Models
{
    public class UserRoleAssignAjaxViewModel
    {
        public UserRoleAssingDto UserRoleAssingDto { get; set; }
        public string RoleAssignPartial { get; set; }
        public UserDto UserDto { get; set; }
    }
}
