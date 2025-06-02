
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


namespace Business.Handlers.OdaOlanaks.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOdaOlanakCommand : IRequest<IResult>
    {
        public int OdaOlanakId { get; set; }

        public class DeleteOdaOlanakCommandHandler : IRequestHandler<DeleteOdaOlanakCommand, IResult>
        {
            private readonly IOdaOlanakRepository _odaOlanakRepository;
            private readonly IMediator _mediator;

            public DeleteOdaOlanakCommandHandler(IOdaOlanakRepository odaOlanakRepository, IMediator mediator)
            {
                _odaOlanakRepository = odaOlanakRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOdaOlanakCommand request, CancellationToken cancellationToken)
            {
                var odaOlanakToDelete = _odaOlanakRepository.Get(p => p.OdaOlanakId == request.OdaOlanakId);

                _odaOlanakRepository.Delete(odaOlanakToDelete);
                await _odaOlanakRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

