
using Business.Handlers.Services.Commands;
using FluentValidation;

namespace Business.Handlers.Services.ValidationRules
{

    public class CreateServiceValidator : AbstractValidator<CreateServiceCommand>
    {
        public CreateServiceValidator()
        {
            //RuleFor(x => x.Photo).NotEmpty();

        }
    }
    public class UpdateServiceValidator : AbstractValidator<UpdateServiceCommand>
    {
        public UpdateServiceValidator()
        {
            //RuleFor(x => x.Photo).NotEmpty();

        }
    }
}