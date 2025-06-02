
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
using Business.Handlers.Olanaklars.ValidationRules;

namespace Business.Handlers.Olanaklars.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateOlanaklarCommand : IRequest<IResult>
    {

        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string Photo { get; set; }
        public int Sira { get; set; }
        public int Dil { get; set; }


        public class CreateOlanaklarCommandHandler : IRequestHandler<CreateOlanaklarCommand, IResult>
        {
            private readonly IOlanaklarRepository _olanaklarRepository;
            private readonly IMediator _mediator;
            public CreateOlanaklarCommandHandler(IOlanaklarRepository olanaklarRepository, IMediator mediator)
            {
                _olanaklarRepository = olanaklarRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateOlanaklarValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateOlanaklarCommand request, CancellationToken cancellationToken)
            {
                //var isThereOlanaklarRecord = _olanaklarRepository.Query().Any(u => u.Baslik == request.Baslik);

                //if (isThereOlanaklarRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedOlanaklar = new Olanaklar
                {
                    Baslik = request.Baslik,
                    Aciklama = request.Aciklama,
                    Photo = request.Photo,
                    Sira = request.Sira,
                    Dil = request.Dil,

                };

                _olanaklarRepository.Add(addedOlanaklar);
                await _olanaklarRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}