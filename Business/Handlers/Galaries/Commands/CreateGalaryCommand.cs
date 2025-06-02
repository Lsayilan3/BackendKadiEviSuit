
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
using Business.Handlers.Galaries.ValidationRules;

namespace Business.Handlers.Galaries.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateGalaryCommand : IRequest<IResult>
    {

        public int EvId { get; set; }
        public string Photo { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public int ResimTipiId { get; set; }


        public class CreateGalaryCommandHandler : IRequestHandler<CreateGalaryCommand, IResult>
        {
            private readonly IGalaryRepository _galaryRepository;
            private readonly IMediator _mediator;
            public CreateGalaryCommandHandler(IGalaryRepository galaryRepository, IMediator mediator)
            {
                _galaryRepository = galaryRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateGalaryValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateGalaryCommand request, CancellationToken cancellationToken)
            {
                //var isThereGalaryRecord = _galaryRepository.Query().Any(u => u.EvId == request.EvId);

                //if (isThereGalaryRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedGalary = new Galary
                {
                    EvId = request.EvId,
                    Photo = request.Photo,
                    Baslik = request.Baslik,
                    Aciklama = request.Aciklama,
                    ResimTipiId = request.ResimTipiId,

                };

                _galaryRepository.Add(addedGalary);
                await _galaryRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}