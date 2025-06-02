
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class GirisRepository : EfEntityRepositoryBase<Giris, ProjectDbContext>, IGirisRepository
    {
        public GirisRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
