
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


namespace Business.Handlers.Iletisims.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteIletisimCommand : IRequest<IResult>
    {
        public int IletisimId { get; set; }

        public class DeleteIletisimCommandHandler : IRequestHandler<DeleteIletisimCommand, IResult>
        {
            private readonly IIletisimRepository _iletisimRepository;
            private readonly IMediator _mediator;

            public DeleteIletisimCommandHandler(IIletisimRepository iletisimRepository, IMediator mediator)
            {
                _iletisimRepository = iletisimRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteIletisimCommand request, CancellationToken cancellationToken)
            {
                var iletisimToDelete = _iletisimRepository.Get(p => p.IletisimId == request.IletisimId);

                _iletisimRepository.Delete(iletisimToDelete);
                await _iletisimRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

