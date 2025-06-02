
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.OdaOlanaks.Queries
{
    public class GetOdaOlanakQuery : IRequest<IDataResult<OdaOlanak>>
    {
        public int OdaOlanakId { get; set; }

        public class GetOdaOlanakQueryHandler : IRequestHandler<GetOdaOlanakQuery, IDataResult<OdaOlanak>>
        {
            private readonly IOdaOlanakRepository _odaOlanakRepository;
            private readonly IMediator _mediator;

            public GetOdaOlanakQueryHandler(IOdaOlanakRepository odaOlanakRepository, IMediator mediator)
            {
                _odaOlanakRepository = odaOlanakRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<OdaOlanak>> Handle(GetOdaOlanakQuery request, CancellationToken cancellationToken)
            {
                var odaOlanak = await _odaOlanakRepository.GetAsync(p => p.OdaOlanakId == request.OdaOlanakId);
                return new SuccessDataResult<OdaOlanak>(odaOlanak);
            }
        }
    }
}
