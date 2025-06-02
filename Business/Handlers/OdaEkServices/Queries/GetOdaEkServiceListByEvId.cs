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

namespace Business.Handlers.OdaEkServices.Queries
{

    public class GetOdaEkServiceListByEvId : IRequest<IDataResult<IEnumerable<OdaEkService>>>
    {
        public int EvId { get; set; }
        public int Dil { get; set; }

        public class GetOdaEkServicesQueryHandler : IRequestHandler<GetOdaEkServiceListByEvId, IDataResult<IEnumerable<OdaEkService>>>
        {
            private readonly IOdaEkServiceRepository _odaEkServiceRepository;
            private readonly IMediator _mediator;

            public GetOdaEkServicesQueryHandler(IOdaEkServiceRepository odaEkServiceRepository, IMediator mediator)
            {
                _odaEkServiceRepository = odaEkServiceRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OdaEkService>>> Handle(GetOdaEkServiceListByEvId request, CancellationToken cancellationToken)
            {
                if (request.Dil == 0)
                {
                    return new SuccessDataResult<IEnumerable<OdaEkService>>(await _odaEkServiceRepository.GetListAsync(x => x.EvId == request.EvId));
                }
                return new SuccessDataResult<IEnumerable<OdaEkService>>(await _odaEkServiceRepository.GetListAsync(x => x.EvId == request.EvId && x.Dil == request.Dil));
            }
        }
    }
}
