
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

    public class GetOdaEkServicesQuery : IRequest<IDataResult<IEnumerable<OdaEkService>>>
    {
        public class GetOdaEkServicesQueryHandler : IRequestHandler<GetOdaEkServicesQuery, IDataResult<IEnumerable<OdaEkService>>>
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
            public async Task<IDataResult<IEnumerable<OdaEkService>>> Handle(GetOdaEkServicesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OdaEkService>>(await _odaEkServiceRepository.GetListAsync());
            }
        }
    }
}