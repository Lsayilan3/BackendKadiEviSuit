
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class GalaryBlogRepository : EfEntityRepositoryBase<GalaryBlog, ProjectDbContext>, IGalaryBlogRepository
    {
        public GalaryBlogRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
