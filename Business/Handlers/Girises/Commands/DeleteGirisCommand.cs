
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


namespace Business.Handlers.Girises.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class DeleteGirisCommand : IRequest<IResult>
    {
        public int GirisId { get; set; }

        public class DeleteGirisCommandHandler : IRequestHandler<DeleteGirisCommand, IResult>
        {
            private readonly IGirisRepository _girisRepository;
            private readonly IMediator _mediator;

            public DeleteGirisCommandHandler(IGirisRepository girisRepository, IMediator mediator)
            {
                _girisRepository = girisRepository;
                _mediator = mediator;
            }

            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(DeleteGirisCommand request, CancellationToken cancellationToken)
            {
                var girisToDelete = _girisRepository.Get(p => p.GirisId == request.GirisId);

                _girisRepository.Delete(girisToDelete);
                await _girisRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Deleted);
            }
        }
    }
}

