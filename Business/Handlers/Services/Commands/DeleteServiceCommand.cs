
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using MediatR;
using System.Threading;
using System.Threading.Tasks;


namespace Business.Handlers.Services.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteServiceCommand : IRequest<IResult>
    {
        public int ServiceId { get; set; }

        public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand, IResult>
        {
            private readonly IServiceRepository _serviceRepository;
            private readonly IMediator _mediator;

            public DeleteServiceCommandHandler(IServiceRepository serviceRepository, IMediator mediator)
            {
                _serviceRepository = serviceRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
            {
                var serviceToDelete = _serviceRepository.Get(p => p.ServiceId == request.ServiceId);

                _serviceRepository.Delete(serviceToDelete);
                await _serviceRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

