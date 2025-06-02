
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class IletisimRepository : EfEntityRepositoryBase<Iletisim, ProjectDbContext>, IIletisimRepository
    {
        public IletisimRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
