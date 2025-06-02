
using Business.BusinessAspects;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;


namespace Business.Handlers.Services.Queries
{
    public class GetServiceQuery : IRequest<IDataResult<Service>>
    {
        public int ServiceId { get; set; }

        public class GetServiceQueryHandler : IRequestHandler<GetServiceQuery, IDataResult<Service>>
        {
            private readonly IServiceRepository _serviceRepository;
            private readonly IMediator _mediator;

            public GetServiceQueryHandler(IServiceRepository serviceRepository, IMediator mediator)
            {
                _serviceRepository = serviceRepository;
                _mediator = mediator;
            }
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IDataResult<Service>> Handle(GetServiceQuery request, CancellationToken cancellationToken)
            {
                var service = await _serviceRepository.GetAsync(p => p.ServiceId == request.ServiceId);
                return new SuccessDataResult<Service>(service);
            }
        }
    }
}
