
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.OdaEkServices.Queries
{
    public class GetOdaEkServiceQuery : IRequest<IDataResult<OdaEkService>>
    {
        public int OdaEkServiceId { get; set; }

        public class GetOdaEkServiceQueryHandler : IRequestHandler<GetOdaEkServiceQuery, IDataResult<OdaEkService>>
        {
            private readonly IOdaEkServiceRepository _odaEkServiceRepository;
            private readonly IMediator _mediator;

            public GetOdaEkServiceQueryHandler(IOdaEkServiceRepository odaEkServiceRepository, IMediator mediator)
            {
                _odaEkServiceRepository = odaEkServiceRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<OdaEkService>> Handle(GetOdaEkServiceQuery request, CancellationToken cancellationToken)
            {
                var odaEkService = await _odaEkServiceRepository.GetAsync(p => p.OdaEkServiceId == request.OdaEkServiceId);
                return new SuccessDataResult<OdaEkService>(odaEkService);
            }
        }
    }
}
