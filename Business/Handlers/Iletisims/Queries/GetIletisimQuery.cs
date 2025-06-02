
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Iletisims.Queries
{
    public class GetIletisimQuery : IRequest<IDataResult<Iletisim>>
    {
        public int IletisimId { get; set; }

        public class GetIletisimQueryHandler : IRequestHandler<GetIletisimQuery, IDataResult<Iletisim>>
        {
            private readonly IIletisimRepository _iletisimRepository;
            private readonly IMediator _mediator;

            public GetIletisimQueryHandler(IIletisimRepository iletisimRepository, IMediator mediator)
            {
                _iletisimRepository = iletisimRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Iletisim>> Handle(GetIletisimQuery request, CancellationToken cancellationToken)
            {
                var iletisim = await _iletisimRepository.GetAsync(p => p.IletisimId == request.IletisimId);
                return new SuccessDataResult<Iletisim>(iletisim);
            }
        }
    }
}
