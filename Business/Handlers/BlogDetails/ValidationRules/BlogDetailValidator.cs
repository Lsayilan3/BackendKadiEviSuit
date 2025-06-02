
using Business.Handlers.BlogDetails.Commands;
using FluentValidation;

namespace Business.Handlers.BlogDetails.ValidationRules
{

    public class CreateBlogDetailValidator : AbstractValidator<CreateBlogDetailCommand>
    {
        public CreateBlogDetailValidator()
        {
            //RuleFor(x => x.BlogId).NotEmpty();
            //RuleFor(x => x.Tarih).NotEmpty();
            //RuleFor(x => x.Yer).NotEmpty();
            //RuleFor(x => x.Baslik).NotEmpty();
            //RuleFor(x => x.Aciklama).NotEmpty();
            //RuleFor(x => x.Editor).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();
            //RuleFor(x => x.Dil).NotEmpty();

        }
    }
    public class UpdateBlogDetailValidator : AbstractValidator<UpdateBlogDetailCommand>
    {
        public UpdateBlogDetailValidator()
        {
            //RuleFor(x => x.BlogId).NotEmpty();
            //RuleFor(x => x.Tarih).NotEmpty();
            //RuleFor(x => x.Yer).NotEmpty();
            //RuleFor(x => x.Baslik).NotEmpty();
            //RuleFor(x => x.Aciklama).NotEmpty();
            //RuleFor(x => x.Editor).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();
            //RuleFor(x => x.Dil).NotEmpty();

        }
    }
}