
using Business.Handlers.OdaOlanaks.Commands;
using FluentValidation;

namespace Business.Handlers.OdaOlanaks.ValidationRules
{

    public class CreateOdaOlanakValidator : AbstractValidator<CreateOdaOlanakCommand>
    {
        public CreateOdaOlanakValidator()
        {
            //RuleFor(x => x.EvId).NotEmpty();
            //RuleFor(x => x.Baslik).NotEmpty();
            //RuleFor(x => x.Icon).NotEmpty();
            //RuleFor(x => x.Aciklama).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();
            //RuleFor(x => x.Dil).NotEmpty();

        }
    }
    public class UpdateOdaOlanakValidator : AbstractValidator<UpdateOdaOlanakCommand>
    {
        public UpdateOdaOlanakValidator()
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