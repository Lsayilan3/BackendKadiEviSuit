
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


namespace Business.Handlers.Olanaklars.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteOlanaklarCommand : IRequest<IResult>
    {
        public int OlanaklarId { get; set; }

        public class DeleteOlanaklarCommandHandler : IRequestHandler<DeleteOlanaklarCommand, IResult>
        {
            private readonly IOlanaklarRepository _olanaklarRepository;
            private readonly IMediator _mediator;

            public DeleteOlanaklarCommandHandler(IOlanaklarRepository olanaklarRepository, IMediator mediator)
            {
                _olanaklarRepository = olanaklarRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteOlanaklarCommand request, CancellationToken cancellationToken)
            {
                var olanaklarToDelete = _olanaklarRepository.Get(p => p.OlanaklarId == request.OlanaklarId);

                _olanaklarRepository.Delete(olanaklarToDelete);
                await _olanaklarRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

