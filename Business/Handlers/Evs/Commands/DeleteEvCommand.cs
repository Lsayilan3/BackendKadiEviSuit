
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


namespace Business.Handlers.Evs.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteEvCommand : IRequest<IResult>
    {
        public int EvId { get; set; }

        public class DeleteEvCommandHandler : IRequestHandler<DeleteEvCommand, IResult>
        {
            private readonly IEvRepository _evRepository;
            private readonly IMediator _mediator;

            public DeleteEvCommandHandler(IEvRepository evRepository, IMediator mediator)
            {
                _evRepository = evRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteEvCommand request, CancellationToken cancellationToken)
            {
                var evToDelete = _evRepository.Get(p => p.EvId == request.EvId);

                _evRepository.Delete(evToDelete);
                await _evRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

