
using Business.BusinessAspects;
using Core.Aspects.Autofac.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Aspects.Autofac.Caching;

namespace Business.Handlers.GalaryBlogs.Queries
{

    public class GetGalaryBlogsQuery : IRequest<IDataResult<IEnumerable<GalaryBlog>>>
    {
        public class GetGalaryBlogsQueryHandler : IRequestHandler<GetGalaryBlogsQuery, IDataResult<IEnumerable<GalaryBlog>>>
        {
            private readonly IGalaryBlogRepository _galaryBlogRepository;
            private readonly IMediator _mediator;

            public GetGalaryBlogsQueryHandler(IGalaryBlogRepository galaryBlogRepository, IMediator mediator)
            {
                _galaryBlogRepository = galaryBlogRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<GalaryBlog>>> Handle(GetGalaryBlogsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<GalaryBlog>>(await _galaryBlogRepository.GetListAsync());
            }
        }
    }
}