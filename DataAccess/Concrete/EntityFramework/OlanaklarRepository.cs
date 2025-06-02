
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class OlanaklarRepository : EfEntityRepositoryBase<Olanaklar, ProjectDbContext>, IOlanaklarRepository
    {
        public OlanaklarRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
