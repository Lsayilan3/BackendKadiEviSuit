
using Business.Handlers.Olanaklars.Commands;
using FluentValidation;

namespace Business.Handlers.Olanaklars.ValidationRules
{

    public class CreateOlanaklarValidator : AbstractValidator<CreateOlanaklarCommand>
    {
        public CreateOlanaklarValidator()
        {
            //RuleFor(x => x.Baslik).NotEmpty();
            //RuleFor(x => x.Aciklama).NotEmpty();
            //RuleFor(x => x.Photo).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();
            //RuleFor(x => x.Dil).NotEmpty();

        }
    }
    public class UpdateOlanaklarValidator : AbstractValidator<UpdateOlanaklarCommand>
    {
        public UpdateOlanaklarValidator()
        {
            //RuleFor(x => x.Baslik).NotEmpty();
            //RuleFor(x => x.Aciklama).NotEmpty();
            //RuleFor(x => x.Photo).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();
            //RuleFor(x => x.Dil).NotEmpty();

        }
    }
}