
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Girises.Queries
{
    public class GetGirisQuery : IRequest<IDataResult<Giris>>
    {
        public int GirisId { get; set; }

        public class GetGirisQueryHandler : IRequestHandler<GetGirisQuery, IDataResult<Giris>>
        {
            private readonly IGirisRepository _girisRepository;
            private readonly IMediator _mediator;

            public GetGirisQueryHandler(IGirisRepository girisRepository, IMediator mediator)
            {
                _girisRepository = girisRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Giris>> Handle(GetGirisQuery request, CancellationToken cancellationToken)
            {
                var giris = await _girisRepository.GetAsync(p => p.GirisId == request.GirisId);
                return new SuccessDataResult<Giris>(giris);
            }
        }
    }
}
