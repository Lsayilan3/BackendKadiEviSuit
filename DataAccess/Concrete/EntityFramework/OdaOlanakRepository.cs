
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class OdaOlanakRepository : EfEntityRepositoryBase<OdaOlanak, ProjectDbContext>, IOdaOlanakRepository
    {
        public OdaOlanakRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
