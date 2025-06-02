
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

namespace Business.Handlers.Evs.Queries
{

    public class GetEvsQuery : IRequest<IDataResult<IEnumerable<Ev>>>
    {
        public int Dil { get; set; }
        public class GetEvsQueryHandler : IRequestHandler<GetEvsQuery, IDataResult<IEnumerable<Ev>>>
        {
            private readonly IEvRepository _evRepository;
            private readonly IMediator _mediator;

            public GetEvsQueryHandler(IEvRepository evRepository, IMediator mediator)
            {
                _evRepository = evRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Ev>>> Handle(GetEvsQuery request, CancellationToken cancellationToken)
            {
                if (request.Dil == 0)
                {
                    return new SuccessDataResult<IEnumerable<Ev>>(await _evRepository.GetListAsync());
                }
                return new SuccessDataResult<IEnumerable<Ev>>(await _evRepository.GetListAsync(x => x.Dil == request.Dil));
            }
        }
    }
}