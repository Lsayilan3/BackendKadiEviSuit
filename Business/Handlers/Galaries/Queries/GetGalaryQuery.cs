
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Galaries.Queries
{
    public class GetGalaryQuery : IRequest<IDataResult<Galary>>
    {
        public int GalaryId { get; set; }

        public class GetGalaryQueryHandler : IRequestHandler<GetGalaryQuery, IDataResult<Galary>>
        {
            private readonly IGalaryRepository _galaryRepository;
            private readonly IMediator _mediator;

            public GetGalaryQueryHandler(IGalaryRepository galaryRepository, IMediator mediator)
            {
                _galaryRepository = galaryRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Galary>> Handle(GetGalaryQuery request, CancellationToken cancellationToken)
            {
                var galary = await _galaryRepository.GetAsync(p => p.GalaryId == request.GalaryId);
                return new SuccessDataResult<Galary>(galary);
            }
        }
    }
}
