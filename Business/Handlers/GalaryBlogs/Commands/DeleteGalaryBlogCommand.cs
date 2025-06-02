
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.GalaryBlogs.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteGalaryBlogCommand : IRequest<IResult>
    {
        public int GalaryBlogId { get; set; }

        public class DeleteGalaryBlogCommandHandler : IRequestHandler<DeleteGalaryBlogCommand, IResult>
        {
            private readonly IGalaryBlogRepository _galaryBlogRepository;
            private readonly IMediator _mediator;

            public DeleteGalaryBlogCommandHandler(IGalaryBlogRepository galaryBlogRepository, IMediator mediator)
            {
                _galaryBlogRepository = galaryBlogRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteGalaryBlogCommand request, CancellationToken cancellationToken)
            {
                var galaryBlogToDelete = _galaryBlogRepository.Get(p => p.GalaryBlogId == request.GalaryBlogId);

                _galaryBlogRepository.Delete(galaryBlogToDelete);
                await _galaryBlogRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

