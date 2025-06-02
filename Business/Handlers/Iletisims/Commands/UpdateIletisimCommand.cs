
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
using Business.Handlers.Iletisims.ValidationRules;


namespace Business.Handlers.Iletisims.Commands
{


    public class UpdateIletisimCommand : IRequest<IResult>
    {
        public int IletisimId { get; set; }
        public string Isim { get; set; }
        public string SoyIsim { get; set; }
        public string Mail { get; set; }
        public string Soru { get; set; }
        public string Mesaj { get; set; }
        public System.DateTime CraeteDate { get; set; }

        public class UpdateIletisimCommandHandler : IRequestHandler<UpdateIletisimCommand, IResult>
        {
            private readonly IIletisimRepository _iletisimRepository;
            private readonly IMediator _mediator;

            public UpdateIletisimCommandHandler(IIletisimRepository iletisimRepository, IMediator mediator)
            {
                _iletisimRepository = iletisimRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateIletisimValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateIletisimCommand request, CancellationToken cancellationToken)
            {
                var isThereIletisimRecord = await _iletisimRepository.GetAsync(u => u.IletisimId == request.IletisimId);


                isThereIletisimRecord.Isim = request.Isim;
                isThereIletisimRecord.SoyIsim = request.SoyIsim;
                isThereIletisimRecord.Mail = request.Mail;
                isThereIletisimRecord.Soru = request.Soru;
                isThereIletisimRecord.Mesaj = request.Mesaj;
                isThereIletisimRecord.CraeteDate = request.CraeteDate;


                _iletisimRepository.Update(isThereIletisimRecord);
                await _iletisimRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

