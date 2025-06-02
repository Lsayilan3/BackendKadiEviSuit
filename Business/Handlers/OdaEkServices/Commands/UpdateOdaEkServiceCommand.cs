
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
using Business.Handlers.OdaEkServices.ValidationRules;


namespace Business.Handlers.OdaEkServices.Commands
{


    public class UpdateOdaEkServiceCommand : IRequest<IResult>
    {
        public int OdaEkServiceId { get; set; }
        public int EvId { get; set; }
        public string Baslik { get; set; }
        public string Icon { get; set; }
        public string Aciklama { get; set; }
        public int Sira { get; set; }
        public int Dil { get; set; }

        public class UpdateOdaEkServiceCommandHandler : IRequestHandler<UpdateOdaEkServiceCommand, IResult>
        {
            private readonly IOdaEkServiceRepository _odaEkServiceRepository;
            private readonly IMediator _mediator;

            public UpdateOdaEkServiceCommandHandler(IOdaEkServiceRepository odaEkServiceRepository, IMediator mediator)
            {
                _odaEkServiceRepository = odaEkServiceRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOdaEkServiceValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOdaEkServiceCommand request, CancellationToken cancellationToken)
            {
                var isThereOdaEkServiceRecord = await _odaEkServiceRepository.GetAsync(u => u.OdaEkServiceId == request.OdaEkServiceId);


                isThereOdaEkServiceRecord.EvId = request.EvId;
                isThereOdaEkServiceRecord.Baslik = request.Baslik;
                isThereOdaEkServiceRecord.Icon = request.Icon;
                isThereOdaEkServiceRecord.Aciklama = request.Aciklama;
                isThereOdaEkServiceRecord.Sira = request.Sira;
                isThereOdaEkServiceRecord.Dil = request.Dil;


                _odaEkServiceRepository.Update(isThereOdaEkServiceRecord);
                await _odaEkServiceRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

