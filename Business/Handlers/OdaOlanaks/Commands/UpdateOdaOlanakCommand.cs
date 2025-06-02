
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
using Business.Handlers.OdaOlanaks.ValidationRules;


namespace Business.Handlers.OdaOlanaks.Commands
{


    public class UpdateOdaOlanakCommand : IRequest<IResult>
    {
        public int OdaOlanakId { get; set; }
        public int EvId { get; set; }
        public string Baslik { get; set; }
        public string Icon { get; set; }
        public string Aciklama { get; set; }
        public int Sira { get; set; }
        public int Dil { get; set; }

        public class UpdateOdaOlanakCommandHandler : IRequestHandler<UpdateOdaOlanakCommand, IResult>
        {
            private readonly IOdaOlanakRepository _odaOlanakRepository;
            private readonly IMediator _mediator;

            public UpdateOdaOlanakCommandHandler(IOdaOlanakRepository odaOlanakRepository, IMediator mediator)
            {
                _odaOlanakRepository = odaOlanakRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOdaOlanakValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOdaOlanakCommand request, CancellationToken cancellationToken)
            {
                var isThereOdaOlanakRecord = await _odaOlanakRepository.GetAsync(u => u.OdaOlanakId == request.OdaOlanakId);


                isThereOdaOlanakRecord.EvId = request.EvId;
                isThereOdaOlanakRecord.Baslik = request.Baslik;
                isThereOdaOlanakRecord.Icon = request.Icon;
                isThereOdaOlanakRecord.Aciklama = request.Aciklama;
                isThereOdaOlanakRecord.Sira = request.Sira;
                isThereOdaOlanakRecord.Dil = request.Dil;


                _odaOlanakRepository.Update(isThereOdaOlanakRecord);
                await _odaOlanakRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

