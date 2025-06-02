
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
using Business.Handlers.Evs.ValidationRules;


namespace Business.Handlers.Evs.Commands
{


    public class UpdateEvCommand : IRequest<IResult>
    {
        public int EvId { get; set; }
        public string Baslik { get; set; }
        public string Url { get; set; }
        public string Photo { get; set; }
        public int Sira { get; set; }
        public int Dil { get; set; }

        public class UpdateEvCommandHandler : IRequestHandler<UpdateEvCommand, IResult>
        {
            private readonly IEvRepository _evRepository;
            private readonly IMediator _mediator;

            public UpdateEvCommandHandler(IEvRepository evRepository, IMediator mediator)
            {
                _evRepository = evRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateEvValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateEvCommand request, CancellationToken cancellationToken)
            {
                var isThereEvRecord = await _evRepository.GetAsync(u => u.EvId == request.EvId);


                isThereEvRecord.Baslik = request.Baslik;
                isThereEvRecord.Url = request.Url;
                isThereEvRecord.Photo = request.Photo;
                isThereEvRecord.Sira = request.Sira;
                isThereEvRecord.Dil = request.Dil;


                _evRepository.Update(isThereEvRecord);
                await _evRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

