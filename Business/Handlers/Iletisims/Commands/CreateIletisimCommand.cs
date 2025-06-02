
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
using Business.Handlers.Iletisims.ValidationRules;

namespace Business.Handlers.Iletisims.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateIletisimCommand : IRequest<IResult>
    {

        public string Isim { get; set; }
        public string SoyIsim { get; set; }
        public string Mail { get; set; }
        public string Soru { get; set; }
        public string Mesaj { get; set; }
        public System.DateTime CraeteDate { get; set; }


        public class CreateIletisimCommandHandler : IRequestHandler<CreateIletisimCommand, IResult>
        {
            private readonly IIletisimRepository _iletisimRepository;
            private readonly IMediator _mediator;
            public CreateIletisimCommandHandler(IIletisimRepository iletisimRepository, IMediator mediator)
            {
                _iletisimRepository = iletisimRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateIletisimValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            //[SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateIletisimCommand request, CancellationToken cancellationToken)
            {
                //var isThereIletisimRecord = _iletisimRepository.Query().Any(u => u.Isim == request.Isim);

                //if (isThereIletisimRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedIletisim = new Iletisim
                {
                    Isim = request.Isim,
                    SoyIsim = request.SoyIsim,
                    Mail = request.Mail,
                    Soru = request.Soru,
                    Mesaj = request.Mesaj,
                    CraeteDate = System.DateTime.UtcNow,

                };

                _iletisimRepository.Add(addedIletisim);
                await _iletisimRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}