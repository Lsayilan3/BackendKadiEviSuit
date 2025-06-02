
using Business.Handlers.Evs.Commands;
using FluentValidation;

namespace Business.Handlers.Evs.ValidationRules
{

    public class CreateEvValidator : AbstractValidator<CreateEvCommand>
    {
        public CreateEvValidator()
        {
            //RuleFor(x => x.Baslik).NotEmpty();
            //RuleFor(x => x.Url).NotEmpty();
            //RuleFor(x => x.Photo).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();
            //RuleFor(x => x.Dil).NotEmpty();

        }
    }
    public class UpdateEvValidator : AbstractValidator<UpdateEvCommand>
    {
        public UpdateEvValidator()
        {
            //RuleFor(x => x.Baslik).NotEmpty();
            //RuleFor(x => x.Url).NotEmpty();
            //RuleFor(x => x.Photo).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();
            //RuleFor(x => x.Dil).NotEmpty();

        }
    }
}