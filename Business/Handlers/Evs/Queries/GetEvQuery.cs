
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Evs.Queries
{
    public class GetEvQuery : IRequest<IDataResult<Ev>>
    {
        public int EvId { get; set; }

        public class GetEvQueryHandler : IRequestHandler<GetEvQuery, IDataResult<Ev>>
        {
            private readonly IEvRepository _evRepository;
            private readonly IMediator _mediator;

            public GetEvQueryHandler(IEvRepository evRepository, IMediator mediator)
            {
                _evRepository = evRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Ev>> Handle(GetEvQuery request, CancellationToken cancellationToken)
            {
                var ev = await _evRepository.GetAsync(p => p.EvId == request.EvId);
                return new SuccessDataResult<Ev>(ev);
            }
        }
    }
}
