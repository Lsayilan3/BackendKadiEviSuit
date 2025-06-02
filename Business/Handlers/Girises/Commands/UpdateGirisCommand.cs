
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
using Business.Handlers.Girises.ValidationRules;


namespace Business.Handlers.Girises.Commands
{


    public class UpdateGirisCommand : IRequest<IResult>
    {
        public int GirisId { get; set; }
        public string Baslik { get; set; }
        public string PBir { get; set; }
        public string PIki { get; set; }
        public string Photo { get; set; }
        public int Sira { get; set; }
        public int Dil { get; set; }

        public class UpdateGirisCommandHandler : IRequestHandler<UpdateGirisCommand, IResult>
        {
            private readonly IGirisRepository _girisRepository;
            private readonly IMediator _mediator;

            public UpdateGirisCommandHandler(IGirisRepository girisRepository, IMediator mediator)
            {
                _girisRepository = girisRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateGirisValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateGirisCommand request, CancellationToken cancellationToken)
            {
                var isThereGirisRecord = await _girisRepository.GetAsync(u => u.GirisId == request.GirisId);


                isThereGirisRecord.Baslik = request.Baslik;
                isThereGirisRecord.PBir = request.PBir;
                isThereGirisRecord.PIki = request.PIki;
                isThereGirisRecord.Photo = request.Photo;
                isThereGirisRecord.Sira = request.Sira;
                isThereGirisRecord.Dil = request.Dil;


                _girisRepository.Update(isThereGirisRecord);
                await _girisRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

