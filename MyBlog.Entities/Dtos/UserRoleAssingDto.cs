using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Entities.Dtos
{
    public class UserRoleAssingDto
    {
        public UserRoleAssingDto()
        {
            RoleAssignDtos = new List<RoleAssignDto>();
        }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public IList<RoleAssignDto> RoleAssignDtos { get; set; }
    }
}
