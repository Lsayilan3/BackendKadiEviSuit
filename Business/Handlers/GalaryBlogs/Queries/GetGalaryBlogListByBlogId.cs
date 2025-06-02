
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

    public class GetGalaryBlogListByBlogId : IRequest<IDataResult<IEnumerable<GalaryBlog>>>
    {
        public int BlogId { get; set; }

        public class GetGalaryBlogsQueryHandler : IRequestHandler<GetGalaryBlogListByBlogId, IDataResult<IEnumerable<GalaryBlog>>>
        {
            private readonly IGalaryBlogRepository _galaryBlogsRepository;
            private readonly IMediator _mediator;

            public GetGalaryBlogsQueryHandler(IGalaryBlogRepository galaryBlogsRepository, IMediator mediator)
            {
                _galaryBlogsRepository = galaryBlogsRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<GalaryBlog>>> Handle(GetGalaryBlogListByBlogId request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<GalaryBlog>>(await _galaryBlogsRepository.GetListAsync(x => x.BlogId == request.BlogId));
            }
        }
    }
}
