
using Business.Constants;
using Business.BusinessAspects;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Core.Aspects.Autofac.Validation;
using Business.Handlers.Services.ValidationRules;


namespace Business.Handlers.Services.Commands
{


    public class UpdateServiceCommand : IRequest<IResult>
    {
        public int ServiceId { get; set; }
        public string Photo { get; set; }

        public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, IResult>
        {
            private readonly IServiceRepository _serviceRepository;
            private readonly IMediator _mediator;

            public UpdateServiceCommandHandler(IServiceRepository serviceRepository, IMediator mediator)
            {
                _serviceRepository = serviceRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateServiceValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
            {
                var isThereServiceRecord = await _serviceRepository.GetAsync(u => u.ServiceId == request.ServiceId);


                isThereServiceRecord.Photo = request.Photo;


                _serviceRepository.Update(isThereServiceRecord);
                await _serviceRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

