
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.GalaryBlogs.Queries
{
    public class GetGalaryBlogQuery : IRequest<IDataResult<GalaryBlog>>
    {
        public int GalaryBlogId { get; set; }

        public class GetGalaryBlogQueryHandler : IRequestHandler<GetGalaryBlogQuery, IDataResult<GalaryBlog>>
        {
            private readonly IGalaryBlogRepository _galaryBlogRepository;
            private readonly IMediator _mediator;

            public GetGalaryBlogQueryHandler(IGalaryBlogRepository galaryBlogRepository, IMediator mediator)
            {
                _galaryBlogRepository = galaryBlogRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<GalaryBlog>> Handle(GetGalaryBlogQuery request, CancellationToken cancellationToken)
            {
                var galaryBlog = await _galaryBlogRepository.GetAsync(p => p.GalaryBlogId == request.GalaryBlogId);
                return new SuccessDataResult<GalaryBlog>(galaryBlog);
            }
        }
    }
}
