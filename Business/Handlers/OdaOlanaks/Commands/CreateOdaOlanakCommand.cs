
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
using Business.Handlers.OdaOlanaks.ValidationRules;

namespace Business.Handlers.OdaOlanaks.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOdaOlanakCommand : IRequest<IResult>
    {

        public int EvId { get; set; }
        public string Baslik { get; set; }
        public string Icon { get; set; }
        public string Aciklama { get; set; }
        public int Sira { get; set; }
        public int Dil { get; set; }


        public class CreateOdaOlanakCommandHandler : IRequestHandler<CreateOdaOlanakCommand, IResult>
        {
            private readonly IOdaOlanakRepository _odaOlanakRepository;
            private readonly IMediator _mediator;
            public CreateOdaOlanakCommandHandler(IOdaOlanakRepository odaOlanakRepository, IMediator mediator)
            {
                _odaOlanakRepository = odaOlanakRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOdaOlanakValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOdaOlanakCommand request, CancellationToken cancellationToken)
            {
                //var isThereOdaOlanakRecord = _odaOlanakRepository.Query().Any(u => u.EvId == request.EvId);

                //if (isThereOdaOlanakRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOdaOlanak = new OdaOlanak
                {
                    EvId = request.EvId,
                    Baslik = request.Baslik,
                    Icon = request.Icon,
                    Aciklama = request.Aciklama,
                    Sira = request.Sira,
                    Dil = request.Dil,

                };

                _odaOlanakRepository.Add(addedOdaOlanak);
                await _odaOlanakRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}