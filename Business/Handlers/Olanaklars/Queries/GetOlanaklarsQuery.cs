
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

namespace Business.Handlers.Olanaklars.Queries
{

    public class GetOlanaklarsQuery : IRequest<IDataResult<IEnumerable<Olanaklar>>>
    {
        public int Dil { get; set; }
        public class GetOlanaklarsQueryHandler : IRequestHandler<GetOlanaklarsQuery, IDataResult<IEnumerable<Olanaklar>>>
        {
            private readonly IOlanaklarRepository _olanaklarRepository;
            private readonly IMediator _mediator;

            public GetOlanaklarsQueryHandler(IOlanaklarRepository olanaklarRepository, IMediator mediator)
            {
                _olanaklarRepository = olanaklarRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Olanaklar>>> Handle(GetOlanaklarsQuery request, CancellationToken cancellationToken)
            {
                if (request.Dil == 0)
                {
                    return new SuccessDataResult<IEnumerable<Olanaklar>>(await _olanaklarRepository.GetListAsync());
                }
                return new SuccessDataResult<IEnumerable<Olanaklar>>(await _olanaklarRepository.GetListAsync(x => x.Dil == request.Dil));
            }
        }
    }
}