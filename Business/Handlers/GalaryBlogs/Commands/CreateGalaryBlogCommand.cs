
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
using Business.Handlers.GalaryBlogs.ValidationRules;

namespace Business.Handlers.GalaryBlogs.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CreateGalaryBlogCommand : IRequest<IResult>
    {

        public int BlogId { get; set; }
        public string Photo { get; set; }
        public string Baslik { get; set; }
        public string Aciklama { get; set; }


        public class CreateGalaryBlogCommandHandler : IRequestHandler<CreateGalaryBlogCommand, IResult>
        {
            private readonly IGalaryBlogRepository _galaryBlogRepository;
            private readonly IMediator _mediator;
            public CreateGalaryBlogCommandHandler(IGalaryBlogRepository galaryBlogRepository, IMediator mediator)
            {
                _galaryBlogRepository = galaryBlogRepository;
                _mediator = mediator;
            }

            [ValidationAspect(typeof(CreateGalaryBlogValidator), Priority = 1)]
            [CacheRemoveAspect("Get")]
            [LogAspect(typeof(FileLogger))]
            [SecuredOperation(Priority = 1)]
            public async Task<IResult> Handle(CreateGalaryBlogCommand request, CancellationToken cancellationToken)
            {
                //var isThereGalaryBlogRecord = _galaryBlogRepository.Query().Any(u => u.BlogId == request.BlogId);

                //if (isThereGalaryBlogRecord == true)
                //    return new ErrorResult(Messages.NameAlreadyExist);

                var addedGalaryBlog = new GalaryBlog
                {
                    BlogId = request.BlogId,
                    Photo = request.Photo,
                    Baslik = request.Baslik,
                    Aciklama = request.Aciklama,

                };

                _galaryBlogRepository.Add(addedGalaryBlog);
                await _galaryBlogRepository.SaveChangesAsync();
                return new SuccessResult(Messages.Added);
            }
        }
    }
}