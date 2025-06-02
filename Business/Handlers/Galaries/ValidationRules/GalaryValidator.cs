
using Business.Handlers.Galaries.Commands;
using FluentValidation;

namespace Business.Handlers.Galaries.ValidationRules
{

    public class CreateGalaryValidator : AbstractValidator<CreateGalaryCommand>
    {
        public CreateGalaryValidator()
        {
            //RuleFor(x => x.EvId).NotEmpty();
            //RuleFor(x => x.Photo).NotEmpty();
            //RuleFor(x => x.Baslik).NotEmpty();
            //RuleFor(x => x.Aciklama).NotEmpty();
            //RuleFor(x => x.ResimTipiId).NotEmpty();

        }
    }
    public class UpdateGalaryValidator : AbstractValidator<UpdateGalaryCommand>
    {
        public UpdateGalaryValidator()
        {
            //RuleFor(x => x.EvId).NotEmpty();
            //RuleFor(x => x.Photo).NotEmpty();
            //RuleFor(x => x.Baslik).NotEmpty();
            //RuleFor(x => x.Aciklama).NotEmpty();
            //RuleFor(x => x.ResimTipiId).NotEmpty();

        }
    }
}