
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
using Business.Handlers.EvDetails.ValidationRules;


namespace Business.Handlers.EvDetails.Commands
{


    public class UpdateEvDetailCommand : IRequest<IResult>
    {
        public int EvDetailId { get; set; }
        public int EvId { get; set; }
        public string Baslik { get; set; }
        public string P { get; set; }
        public string CocukBaslik { get; set; }
        public string CocukP { get; set; }
        public string Editor { get; set; }
        public int Sira { get; set; }
        public int Dil { get; set; }

        public class UpdateEvDetailCommandHandler : IRequestHandler<UpdateEvDetailCommand, IResult>
        {
            private readonly IEvDetailRepository _evDetailRepository;
            private readonly IMediator _mediator;

            public UpdateEvDetailCommandHandler(IEvDetailRepository evDetailRepository, IMediator mediator)
            {
                _evDetailRepository = evDetailRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateEvDetailValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateEvDetailCommand request, CancellationToken cancellationToken)
            {
                var isThereEvDetailRecord = await _evDetailRepository.GetAsync(u => u.EvDetailId == request.EvDetailId);


                isThereEvDetailRecord.EvId = request.EvId;
                isThereEvDetailRecord.Baslik = request.Baslik;
                isThereEvDetailRecord.P = request.P;
                isThereEvDetailRecord.CocukBaslik = request.CocukBaslik;
                isThereEvDetailRecord.CocukP = request.CocukP;
                isThereEvDetailRecord.Editor = request.Editor;
                isThereEvDetailRecord.Sira = request.Sira;
                isThereEvDetailRecord.Dil = request.Dil;


                _evDetailRepository.Update(isThereEvDetailRecord);
                await _evDetailRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

