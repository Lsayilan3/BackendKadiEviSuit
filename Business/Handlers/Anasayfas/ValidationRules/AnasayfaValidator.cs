
using Business.Handlers.Anasayfas.Commands;
using FluentValidation;

namespace Business.Handlers.Anasayfas.ValidationRules
{

    public class CreateAnasayfaValidator : AbstractValidator<CreateAnasayfaCommand>
    {
        public CreateAnasayfaValidator()
        {
            //RuleFor(x => x.Aciklama).NotEmpty();
            //RuleFor(x => x.Photo).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();
            //RuleFor(x => x.Dil).NotEmpty();

        }
    }
    public class UpdateAnasayfaValidator : AbstractValidator<UpdateAnasayfaCommand>
    {
        public UpdateAnasayfaValidator()
        {
            //RuleFor(x => x.Aciklama).NotEmpty();
            //RuleFor(x => x.Photo).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();
            //RuleFor(x => x.Dil).NotEmpty();

        }
    }
}