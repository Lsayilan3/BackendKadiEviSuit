
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

namespace Business.Handlers.Girises.Queries
{

    public class GetGirisesQuery : IRequest<IDataResult<IEnumerable<Giris>>>
    {
        public int Dil { get; set; }
        public class GetGirisesQueryHandler : IRequestHandler<GetGirisesQuery, IDataResult<IEnumerable<Giris>>>
        {
            private readonly IGirisRepository _girisRepository;
            private readonly IMediator _mediator;

            public GetGirisesQueryHandler(IGirisRepository girisRepository, IMediator mediator)
            {
                _girisRepository = girisRepository;
                _mediator = mediator;
            }

            [PerformanceAspect(5)]
            [CacheAspect(10)]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IDataResult<IEnumerable<Giris>>> Handle(GetGirisesQuery request, CancellationToken cancellationToken)
            {
                if (request.Dil == 0)
                {
                    return new SuccessDataResult<IEnumerable<Giris>>(await _girisRepository.GetListAsync());
                }
                return new SuccessDataResult<IEnumerable<Giris>>(await _girisRepository.GetListAsync(x => x.Dil == request.Dil));
            }
        }
    }
}