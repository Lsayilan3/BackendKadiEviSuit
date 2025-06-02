
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
using Business.Handlers.GalaryBlogs.ValidationRules;


namespace Business.Handlers.GalaryBlogs.Commands
{


    public class UpdateGalaryBlogCommand : IRequest<IResult>
    {
        public int GalaryBlogId { get; set; }
        public int BlogId { get; set; }
        public string Photo { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }

        public class UpdateGalaryBlogCommandHandler : IRequestHandler<UpdateGalaryBlogCommand, IResult>
        {
            private readonly IGalaryBlogRepository _galaryBlogRepository;
            private readonly IMediator _mediator;

            public UpdateGalaryBlogCommandHandler(IGalaryBlogRepository galaryBlogRepository, IMediator mediator)
            {
                _galaryBlogRepository = galaryBlogRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(UpdateGalaryBlogValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(UpdateGalaryBlogCommand request, CancellationToken cancellationToken)
            {
                var isThereGalaryBlogRecord = await _galaryBlogRepository.GetAsync(u => u.GalaryBlogId == request.GalaryBlogId);


                isThereGalaryBlogRecord.BlogId = request.BlogId;
                isThereGalaryBlogRecord.Photo = request.Photo;
                isThereGalaryBlogRecord.Baslik = request.Baslik;
                isThereGalaryBlogRecord.Aciklama = request.Aciklama;


                _galaryBlogRepository.Update(isThereGalaryBlogRecord);
                await _galaryBlogRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Updated);
            }
        }
    }
}

