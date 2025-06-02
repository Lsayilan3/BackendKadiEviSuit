
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Olanaklars.Queries
{
    public class GetOlanaklarQuery : IRequest<IDataResult<Olanaklar>>
    {
        public int OlanaklarId { get; set; }

        public class GetOlanaklarQueryHandler : IRequestHandler<GetOlanaklarQuery, IDataResult<Olanaklar>>
        {
            private readonly IOlanaklarRepository _olanaklarRepository;
            private readonly IMediator _mediator;

            public GetOlanaklarQueryHandler(IOlanaklarRepository olanaklarRepository, IMediator mediator)
            {
                _olanaklarRepository = olanaklarRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Olanaklar>> Handle(GetOlanaklarQuery request, CancellationToken cancellationToken)
            {
                var olanaklar = await _olanaklarRepository.GetAsync(p => p.OlanaklarId == request.OlanaklarId);
                return new SuccessDataResult<Olanaklar>(olanaklar);
            }
        }
    }
}
