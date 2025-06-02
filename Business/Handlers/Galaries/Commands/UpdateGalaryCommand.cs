
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
using Business.Handlers.Galaries.ValidationRules;


namespace Business.Handlers.Galaries.Commands
{


    public class UpdateGalaryCommand : IRequest<IResult>
    {
        public int GalaryId { get; set; }
        public int EvId { get; set; }
        public string Photo { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }
        public int ResimTipiId { get; set; }

        public class UpdateGalaryCommandHandler : IRequestHandler<UpdateGalaryCommand, IResult>
        {
            private readonly IGalaryRepository _galaryRepository;
            private readonly IMediator _mediator;

            public UpdateGalaryCommandHandler(IGalaryRepository galaryRepository, IMediator mediator)
            {
                _galaryRepository = galaryRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateGalaryValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateGalaryCommand request, CancellationToken cancellationToken)
            {
                var isThereGalaryRecord = await _galaryRepository.GetAsync(u => u.GalaryId == request.GalaryId);


                isThereGalaryRecord.EvId = request.EvId;
                isThereGalaryRecord.Photo = request.Photo;
                isThereGalaryRecord.Baslik = request.Baslik;
                isThereGalaryRecord.Aciklama = request.Aciklama;
                isThereGalaryRecord.ResimTipiId = request.ResimTipiId;


                _galaryRepository.Update(isThereGalaryRecord);
                await _galaryRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

