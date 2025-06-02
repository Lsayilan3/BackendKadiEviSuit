
using Business.Handlers.ResimTipis.Commands;
using FluentValidation;

namespace Business.Handlers.ResimTipis.ValidationRules
{

    public class CreateResimTipiValidator : AbstractValidator<CreateResimTipiCommand>
    {
        public CreateResimTipiValidator()
        {
            //RuleFor(x => x.Adi).NotEmpty();

        }
    }
    public class UpdateResimTipiValidator : AbstractValidator<UpdateResimTipiCommand>
    {
        public UpdateResimTipiValidator()
        {
            //RuleFor(x => x.Adi).NotEmpty();

        }
    }
}