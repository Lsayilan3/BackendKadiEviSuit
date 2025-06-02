
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


namespace Business.Handlers.EvDetails.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteEvDetailCommand : IRequest<IResult>
    {
        public int EvDetailId { get; set; }

        public class DeleteEvDetailCommandHandler : IRequestHandler<DeleteEvDetailCommand, IResult>
        {
            private readonly IEvDetailRepository _evDetailRepository;
            private readonly IMediator _mediator;

            public DeleteEvDetailCommandHandler(IEvDetailRepository evDetailRepository, IMediator mediator)
            {
                _evDetailRepository = evDetailRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteEvDetailCommand request, CancellationToken cancellationToken)
            {
                var evDetailToDelete = _evDetailRepository.Get(p => p.EvDetailId == request.EvDetailId);

                _evDetailRepository.Delete(evDetailToDelete);
                await _evDetailRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

