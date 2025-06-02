
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
using Business.Handlers.Olanaklars.ValidationRules;


namespace Business.Handlers.Olanaklars.Commands
{


    public class UpdateOlanaklarCommand : IRequest<IResult>
    {
        public int OlanaklarId { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public string Photo { get; set; }
        public int Sira { get; set; }
        public int Dil { get; set; }

        public class UpdateOlanaklarCommandHandler : IRequestHandler<UpdateOlanaklarCommand, IResult>
        {
            private readonly IOlanaklarRepository _olanaklarRepository;
            private readonly IMediator _mediator;

            public UpdateOlanaklarCommandHandler(IOlanaklarRepository olanaklarRepository, IMediator mediator)
            {
                _olanaklarRepository = olanaklarRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateOlanaklarValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateOlanaklarCommand request, CancellationToken cancellationToken)
            {
                var isThereOlanaklarRecord = await _olanaklarRepository.GetAsync(u => u.OlanaklarId == request.OlanaklarId);


                isThereOlanaklarRecord.Baslik = request.Baslik;
                isThereOlanaklarRecord.Aciklama = request.Aciklama;
                isThereOlanaklarRecord.Photo = request.Photo;
                isThereOlanaklarRecord.Sira = request.Sira;
                isThereOlanaklarRecord.Dil = request.Dil;


                _olanaklarRepository.Update(isThereOlanaklarRecord);
                await _olanaklarRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

