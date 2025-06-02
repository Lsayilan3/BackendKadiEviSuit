
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


namespace Business.Handlers.OdaEkServices.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOdaEkServiceCommand : IRequest<IResult>
    {
        public int OdaEkServiceId { get; set; }

        public class DeleteOdaEkServiceCommandHandler : IRequestHandler<DeleteOdaEkServiceCommand, IResult>
        {
            private readonly IOdaEkServiceRepository _odaEkServiceRepository;
            private readonly IMediator _mediator;

            public DeleteOdaEkServiceCommandHandler(IOdaEkServiceRepository odaEkServiceRepository, IMediator mediator)
            {
                _odaEkServiceRepository = odaEkServiceRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOdaEkServiceCommand request, CancellationToken cancellationToken)
            {
                var odaEkServiceToDelete = _odaEkServiceRepository.Get(p => p.OdaEkServiceId == request.OdaEkServiceId);

                _odaEkServiceRepository.Delete(odaEkServiceToDelete);
                await _odaEkServiceRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

