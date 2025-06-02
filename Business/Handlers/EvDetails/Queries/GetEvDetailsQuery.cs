
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

namespace Business.Handlers.EvDetails.Queries
{

    public class GetEvDetailsQuery : IRequest<IDataResult<IEnumerable<EvDetail>>>
    {
        public class GetEvDetailsQueryHandler : IRequestHandler<GetEvDetailsQuery, IDataResult<IEnumerable<EvDetail>>>
        {
            private readonly IEvDetailRepository _evDetailRepository;
            private readonly IMediator _mediator;

            public GetEvDetailsQueryHandler(IEvDetailRepository evDetailRepository, IMediator mediator)
            {
                _evDetailRepository = evDetailRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<EvDetail>>> Handle(GetEvDetailsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<EvDetail>>(await _evDetailRepository.GetListAsync());
            }
        }
    }
}