
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
using Business.Handlers.Girises.ValidationRules;

namespace Business.Handlers.Girises.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateGirisCommand : IRequest<IResult>
    {

        public string Baslik { get; set; }
        public string PBir { get; set; }
        public string PIki { get; set; }
        public string Photo { get; set; }
        public int Sira { get; set; }
        public int Dil { get; set; }


        public class CreateGirisCommandHandler : IRequestHandler<CreateGirisCommand, IResult>
        {
            private readonly IGirisRepository _girisRepository;
            private readonly IMediator _mediator;
            public CreateGirisCommandHandler(IGirisRepository girisRepository, IMediator mediator)
            {
                _girisRepository = girisRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateGirisValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateGirisCommand request, CancellationToken cancellationToken)
            {
                //var isThereGirisRecord = _girisRepository.Query().Any(u => u.Baslik == request.Baslik);

                //if (isThereGirisRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedGiris = new Giris
                {
                    Baslik = request.Baslik,
                    PBir = request.PBir,
                    PIki = request.PIki,
                    Photo = request.Photo,
                    Sira = request.Sira,
                    Dil = request.Dil,

                };

                _girisRepository.Add(addedGiris);
                await _girisRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}