﻿
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
namespace DataAccess.Concrete.EntityFramework
{
    public class EvRepository : EfEntityRepositoryBase<Ev, ProjectDbContext>, IEvRepository
    {
        public EvRepository(ProjectDbContext context) : base(context)
        {
        }
    }
}
