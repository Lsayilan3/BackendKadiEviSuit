
using Business.BusinessAspects;
using Business.Constants;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Serilog.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Business.Handlers.Services.ValidationRules;

namespace Business.Handlers.Services.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateServiceCommand : IRequest<IResult>
    {

        public string Photo { get; set; }


        public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, IResult>
        {
            private readonly IServiceRepository _serviceRepository;
            private readonly IMediator _mediator;
            public CreateServiceCommandHandler(IServiceRepository serviceRepository, IMediator mediator)
            {
                _serviceRepository = serviceRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateServiceValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
            {
                //var isThereServiceRecord = _serviceRepository.Query().Any(u => u.Photo == request.Photo);

                //if (isThereServiceRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedService = new Service
                {
                    Photo = request.Photo,

                };

                _serviceRepository.Add(addedService);
                await _serviceRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}