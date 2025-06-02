
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class GalaryRepository : EfEntityRepositoryBase<Galary, ProjectDbContext>, IGalaryRepository
    {
        public GalaryRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
