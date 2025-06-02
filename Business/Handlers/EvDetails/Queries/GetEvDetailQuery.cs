
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.EvDetails.Queries
{
    public class GetEvDetailQuery : IRequest<IDataResult<EvDetail>>
    {
        public int EvDetailId { get; set; }

        public class GetEvDetailQueryHandler : IRequestHandler<GetEvDetailQuery, IDataResult<EvDetail>>
        {
            private readonly IEvDetailRepository _evDetailRepository;
            private readonly IMediator _mediator;

            public GetEvDetailQueryHandler(IEvDetailRepository evDetailRepository, IMediator mediator)
            {
                _evDetailRepository = evDetailRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<EvDetail>> Handle(GetEvDetailQuery request, CancellationToken cancellationToken)
            {
                var evDetail = await _evDetailRepository.GetAsync(p => p.EvDetailId == request.EvDetailId);
                return new SuccessDataResult<EvDetail>(evDetail);
            }
        }
    }
}
