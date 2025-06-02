
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class OdaEkServiceRepository : EfEntityRepositoryBase<OdaEkService, ProjectDbContext>, IOdaEkServiceRepository
    {
        public OdaEkServiceRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
