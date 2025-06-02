
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

namespace Business.Handlers.Iletisims.Queries
{

    public class GetIletisimsQuery : IRequest<IDataResult<IEnumerable<Iletisim>>>
    {
        public class GetIletisimsQueryHandler : IRequestHandler<GetIletisimsQuery, IDataResult<IEnumerable<Iletisim>>>
        {
            private readonly IIletisimRepository _iletisimRepository;
            private readonly IMediator _mediator;

            public GetIletisimsQueryHandler(IIletisimRepository iletisimRepository, IMediator mediator)
            {
                _iletisimRepository = iletisimRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Iletisim>>> Handle(GetIletisimsQuery request, CancellationToken cancellationToken)
            {
                return new SuccessDataResult<IEnumerable<Iletisim>>(await _iletisimRepository.GetListAsync());
            }
        }
    }
}