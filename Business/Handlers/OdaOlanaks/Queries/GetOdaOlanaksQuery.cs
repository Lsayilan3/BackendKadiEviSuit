
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

namespace Business.Handlers.OdaOlanaks.Queries
{

    public class GetOdaOlanaksQuery : IRequest<IDataResult<IEnumerable<OdaOlanak>>>
    {
        public class GetOdaOlanaksQueryHandler : IRequestHandler<GetOdaOlanaksQuery, IDataResult<IEnumerable<OdaOlanak>>>
        {
            private readonly IOdaOlanakRepository _odaOlanakRepository;
            private readonly IMediator _mediator;

            public GetOdaOlanaksQueryHandler(IOdaOlanakRepository odaOlanakRepository, IMediator mediator)
            {
                _odaOlanakRepository = odaOlanakRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<OdaOlanak>>> Handle(GetOdaOlanaksQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<OdaOlanak>>(await _odaOlanakRepository.GetListAsync());
            }
        }
    }
}