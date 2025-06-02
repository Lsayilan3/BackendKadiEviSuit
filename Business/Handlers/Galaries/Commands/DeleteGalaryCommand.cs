
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


namespace Business.Handlers.Galaries.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteGalaryCommand : IRequest<IResult>
    {
        public int GalaryId { get; set; }

        public class DeleteGalaryCommandHandler : IRequestHandler<DeleteGalaryCommand, IResult>
        {
            private readonly IGalaryRepository _galaryRepository;
            private readonly IMediator _mediator;

            public DeleteGalaryCommandHandler(IGalaryRepository galaryRepository, IMediator mediator)
            {
                _galaryRepository = galaryRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteGalaryCommand request, CancellationToken cancellationToken)
            {
                var galaryToDelete = _galaryRepository.Get(p => p.GalaryId == request.GalaryId);

                _galaryRepository.Delete(galaryToDelete);
                await _galaryRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

