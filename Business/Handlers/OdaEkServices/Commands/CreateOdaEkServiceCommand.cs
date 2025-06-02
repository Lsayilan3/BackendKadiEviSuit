
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
using Business.Handlers.OdaEkServices.ValidationRules;

namespace Business.Handlers.OdaEkServices.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOdaEkServiceCommand : IRequest<IResult>
    {

        public int EvId { get; set; }
        public string Baslik { get; set; }
        public string Icon { get; set; }
        public string Aciklama { get; set; }
        public int Sira { get; set; }
        public int Dil { get; set; }


        public class CreateOdaEkServiceCommandHandler : IRequestHandler<CreateOdaEkServiceCommand, IResult>
        {
            private readonly IOdaEkServiceRepository _odaEkServiceRepository;
            private readonly IMediator _mediator;
            public CreateOdaEkServiceCommandHandler(IOdaEkServiceRepository odaEkServiceRepository, IMediator mediator)
            {
                _odaEkServiceRepository = odaEkServiceRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOdaEkServiceValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOdaEkServiceCommand request, CancellationToken cancellationToken)
            {
                //var isThereOdaEkServiceRecord = _odaEkServiceRepository.Query().Any(u => u.EvId == request.EvId);

                //if (isThereOdaEkServiceRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOdaEkService = new OdaEkService
                {
                    EvId = request.EvId,
                    Baslik = request.Baslik,
                    Icon = request.Icon,
                    Aciklama = request.Aciklama,
                    Sira = request.Sira,
                    Dil = request.Dil,

                };

                _odaEkServiceRepository.Add(addedOdaEkService);
                await _odaEkServiceRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}