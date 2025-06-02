
using Business.Handlers.OdaEkServices.Commands;
using FluentValidation;

namespace Business.Handlers.OdaEkServices.ValidationRules
{

    public class CreateOdaEkServiceValidator : AbstractValidator<CreateOdaEkServiceCommand>
    {
        public CreateOdaEkServiceValidator()
        {
            //RuleFor(x => x.EvId).NotEmpty();
            //RuleFor(x => x.Baslik).NotEmpty();
            //RuleFor(x => x.Icon).NotEmpty();
            //RuleFor(x => x.Aciklama).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();
            //RuleFor(x => x.Dil).NotEmpty();

        }
    }
    public class UpdateOdaEkServiceValidator : AbstractValidator<UpdateOdaEkServiceCommand>
    {
        public UpdateOdaEkServiceValidator()
        {
            //RuleFor(x => x.EvId).NotEmpty();
            //RuleFor(x => x.Baslik).NotEmpty();
            //RuleFor(x => x.Icon).NotEmpty();
            //RuleFor(x => x.Aciklama).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();
            //RuleFor(x => x.Dil).NotEmpty();

        }
    }
}