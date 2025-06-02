
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
using Business.Handlers.Anasayfas.ValidationRules;

namespace Business.Handlers.Anasayfas.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateAnasayfaCommand : IRequest<IResult>
    {

        public string Aciklama { get; set; }
        public string Photo { get; set; }
        public int Sira { get; set; }
        public int Dil { get; set; }


        public class CreateAnasayfaCommandHandler : IRequestHandler<CreateAnasayfaCommand, IResult>
        {
            private readonly IAnasayfaRepository _anasayfaRepository;
            private readonly IMediator _mediator;
            public CreateAnasayfaCommandHandler(IAnasayfaRepository anasayfaRepository, IMediator mediator)
            {
                _anasayfaRepository = anasayfaRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateAnasayfaValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateAnasayfaCommand request, CancellationToken cancellationToken)
            {
                //var isThereAnasayfaRecord = _anasayfaRepository.Query().Any(u => u.Aciklama == request.Aciklama);

                //if (isThereAnasayfaRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedAnasayfa = new Anasayfa
                {
                    Aciklama = request.Aciklama,
                    Photo = request.Photo,
                    Sira = request.Sira,
                    Dil = request.Dil,

                };

                _anasayfaRepository.Add(addedAnasayfa);
                await _anasayfaRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}