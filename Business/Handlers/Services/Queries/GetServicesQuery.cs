
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

namespace Business.Handlers.Services.Queries
{

    public class GetServicesQuery : IRequest<IDataResult<IEnumerable<Service>>>
    {
        public class GetServicesQueryHandler : IRequestHandler<GetServicesQuery, IDataResult<IEnumerable<Service>>>
        {
            private readonly IServiceRepository _serviceRepository;
            private readonly IMediator _mediator;

            public GetServicesQueryHandler(IServiceRepository serviceRepository, IMediator mediator)
            {
                _serviceRepository = serviceRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Service>>> Handle(GetServicesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Service>>(await _serviceRepository.GetListAsync());
            }
        }
    }
}