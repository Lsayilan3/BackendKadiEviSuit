
using Business.Handlers.Girises.Commands;
using FluentValidation;

namespace Business.Handlers.Girises.ValidationRules
{

    public class CreateGirisValidator : AbstractValidator<CreateGirisCommand>
    {
        public CreateGirisValidator()
        {
            //RuleFor(x => x.Baslik).NotEmpty();
            //RuleFor(x => x.PBir).NotEmpty();
            //RuleFor(x => x.PIki).NotEmpty();
            //RuleFor(x => x.Photo).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();
            //RuleFor(x => x.Dil).NotEmpty();

        }
    }
    public class UpdateGirisValidator : AbstractValidator<UpdateGirisCommand>
    {
        public UpdateGirisValidator()
        {
            //RuleFor(x => x.Baslik).NotEmpty();
            //RuleFor(x => x.PBir).NotEmpty();
            //RuleFor(x => x.PIki).NotEmpty();
            //RuleFor(x => x.Photo).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();
            //RuleFor(x => x.Dil).NotEmpty();

        }
    }
}