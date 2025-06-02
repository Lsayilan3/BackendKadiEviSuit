
using Business.Handlers.Blogs.Commands;
using FluentValidation;

namespace Business.Handlers.Blogs.ValidationRules
{

    public class CreateBlogValidator : AbstractValidator<CreateBlogCommand>
    {
        public CreateBlogValidator()
        {
            //RuleFor(x => x.Tarih).NotEmpty();
            //RuleFor(x => x.Yer).NotEmpty();
            //RuleFor(x => x.Aciklama).NotEmpty();
            //RuleFor(x => x.Photo).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();
            //RuleFor(x => x.Dil).NotEmpty();

        }
    }
    public class UpdateBlogValidator : AbstractValidator<UpdateBlogCommand>
    {
        public UpdateBlogValidator()
        {
            //RuleFor(x => x.Tarih).NotEmpty();
            //RuleFor(x => x.Yer).NotEmpty();
            //RuleFor(x => x.Aciklama).NotEmpty();
            //RuleFor(x => x.Photo).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();
            //RuleFor(x => x.Dil).NotEmpty();

        }
    }
}