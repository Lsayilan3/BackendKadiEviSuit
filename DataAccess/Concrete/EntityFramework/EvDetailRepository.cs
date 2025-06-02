
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class EvDetailRepository : EfEntityRepositoryBase<EvDetail, ProjectDbContext>, IEvDetailRepository
    {
        public EvDetailRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
