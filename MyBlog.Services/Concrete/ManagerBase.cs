using AutoMapper;
using MyBlog.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Services.Concrete
{
    public class ManagerBase
    {
        protected IUnitOfWork UnitOfWork { get; }
        protected IMapper Mapper { get; }
        public ManagerBase(IUnitOfWork unitOfWork, IMapper mapper)
        {
            UnitOfWork = unitOfWork;
            Mapper = mapper;
        }

    }
}
