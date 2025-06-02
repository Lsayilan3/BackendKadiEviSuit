
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
using Business.Handlers.EvDetails.ValidationRules;

namespace Business.Handlers.EvDetails.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateEvDetailCommand : IRequest<IResult>
    {

        public int EvId { get; set; }
        public string Baslik { get; set; }
        public string P { get; set; }
        public string CocukBaslik { get; set; }
        public string CocukP { get; set; }
        public string Editor { get; set; }
        public int Sira { get; set; }
        public int Dil { get; set; }


        public class CreateEvDetailCommandHandler : IRequestHandler<CreateEvDetailCommand, IResult>
        {
            private readonly IEvDetailRepository _evDetailRepository;
            private readonly IMediator _mediator;
            public CreateEvDetailCommandHandler(IEvDetailRepository evDetailRepository, IMediator mediator)
            {
                _evDetailRepository = evDetailRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateEvDetailValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateEvDetailCommand request, CancellationToken cancellationToken)
            {
                //var isThereEvDetailRecord = _evDetailRepository.Query().Any(u => u.EvId == request.EvId);

                //if (isThereEvDetailRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedEvDetail = new EvDetail
                {
                    EvId = request.EvId,
                    Baslik = request.Baslik,
                    P = request.P,
                    CocukBaslik = request.CocukBaslik,
                    CocukP = request.CocukP,
                    Editor = request.Editor,
                    Sira = request.Sira,
                    Dil = request.Dil,

                };

                _evDetailRepository.Add(addedEvDetail);
                await _evDetailRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}