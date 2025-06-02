
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

namespace Business.Handlers.Galaries.Queries
{

    public class GetGalariesQuery : IRequest<IDataResult<IEnumerable<Galary>>>
    {
        public class GetGalariesQueryHandler : IRequestHandler<GetGalariesQuery, IDataResult<IEnumerable<Galary>>>
        {
            private readonly IGalaryRepository _galaryRepository;
            private readonly IMediator _mediator;

            public GetGalariesQueryHandler(IGalaryRepository galaryRepository, IMediator mediator)
            {
                _galaryRepository = galaryRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Galary>>> Handle(GetGalariesQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Galary>>(await _galaryRepository.GetListAsync());
            }
        }
    }
}