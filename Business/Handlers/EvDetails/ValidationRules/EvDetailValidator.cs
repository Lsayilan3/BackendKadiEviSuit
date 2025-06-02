
using Business.Handlers.EvDetails.Commands;
using FluentValidation;

namespace Business.Handlers.EvDetails.ValidationRules
{

    public class CreateEvDetailValidator : AbstractValidator<CreateEvDetailCommand>
    {
        public CreateEvDetailValidator()
        {
            //RuleFor(x => x.EvId).NotEmpty();
            //RuleFor(x => x.Baslik).NotEmpty();
            //RuleFor(x => x.P).NotEmpty();
            //RuleFor(x => x.CocukBaslik).NotEmpty();
            //RuleFor(x => x.CocukP).NotEmpty();
            //RuleFor(x => x.Editor).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();
            //RuleFor(x => x.Dil).NotEmpty();

        }
    }
    public class UpdateEvDetailValidator : AbstractValidator<UpdateEvDetailCommand>
    {
        public UpdateEvDetailValidator()
        {
            //RuleFor(x => x.EvId).NotEmpty();
            //RuleFor(x => x.Baslik).NotEmpty();
            //RuleFor(x => x.P).NotEmpty();
            //RuleFor(x => x.CocukBaslik).NotEmpty();
            //RuleFor(x => x.CocukP).NotEmpty();
            //RuleFor(x => x.Editor).NotEmpty();
            //RuleFor(x => x.Sira).NotEmpty();
            //RuleFor(x => x.Dil).NotEmpty();

        }
    }
}