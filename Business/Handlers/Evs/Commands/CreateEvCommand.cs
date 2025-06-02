
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
using Business.Handlers.Evs.ValidationRules;

namespace Business.Handlers.Evs.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateEvCommand : IRequest<IResult>
    {

        public string Baslik { get; set; }
        public string Url { get; set; }
        public string Photo { get; set; }
        public int Sira { get; set; }
        public int Dil { get; set; }


        public class CreateEvCommandHandler : IRequestHandler<CreateEvCommand, IResult>
        {
            private readonly IEvRepository _evRepository;
            private readonly IMediator _mediator;
            public CreateEvCommandHandler(IEvRepository evRepository, IMediator mediator)
            {
                _evRepository = evRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateEvValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateEvCommand request, CancellationToken cancellationToken)
            {
                //var isThereEvRecord = _evRepository.Query().Any(u => u.Baslik == request.Baslik);

                //if (isThereEvRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedEv = new Ev
                {
                    Baslik = request.Baslik,
                    Url = request.Url,
                    Photo = request.Photo,
                    Sira = request.Sira,
                    Dil = request.Dil,

                };

                _evRepository.Add(addedEv);
                await _evRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}