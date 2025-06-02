
using Business.Handlers.GalaryBlogs.Commands;
using FluentValidation;

namespace Business.Handlers.GalaryBlogs.ValidationRules
{

    public class CreateGalaryBlogValidator : AbstractValidator<CreateGalaryBlogCommand>
    {
        public CreateGalaryBlogValidator()
        {
            //RuleFor(x => x.BlogId).NotEmpty();
            //RuleFor(x => x.Photo).NotEmpty();
            //RuleFor(x => x.Baslik).NotEmpty();
            //RuleFor(x => x.Aciklama).NotEmpty();

        }
    }
    public class UpdateGalaryBlogValidator : AbstractValidator<UpdateGalaryBlogCommand>
    {
        public UpdateGalaryBlogValidator()
        {
            //RuleFor(x => x.BlogId).NotEmpty();
            //RuleFor(x => x.Photo).NotEmpty();
            //RuleFor(x => x.Baslik).NotEmpty();
            //RuleFor(x => x.Aciklama).NotEmpty();

        }
    }
}