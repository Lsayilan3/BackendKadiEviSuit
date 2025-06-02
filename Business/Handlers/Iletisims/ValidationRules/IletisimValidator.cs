
using Business.Handlers.Iletisims.Commands;
using FluentValidation;

namespace Business.Handlers.Iletisims.ValidationRules
{

    public class CreateIletisimValidator : AbstractValidator<CreateIletisimCommand>
    {
        public CreateIletisimValidator()
        {
            //RuleFor(x => x.Isim).NotEmpty();
            //RuleFor(x => x.SoyIsim).NotEmpty();
            //RuleFor(x => x.Mail).NotEmpty();
            //RuleFor(x => x.Soru).NotEmpty();
            //RuleFor(x => x.Mesaj).NotEmpty();
            //RuleFor(x => x.CraeteDate).NotEmpty();

        }
    }
    public class UpdateIletisimValidator : AbstractValidator<UpdateIletisimCommand>
    {
        public UpdateIletisimValidator()
        {
            //RuleFor(x => x.Isim).NotEmpty();
            //RuleFor(x => x.SoyIsim).NotEmpty();
            //RuleFor(x => x.Mail).NotEmpty();
            //RuleFor(x => x.Soru).NotEmpty();
            //RuleFor(x => x.Mesaj).NotEmpty();
            //RuleFor(x => x.CraeteDate).NotEmpty();

        }
    }
}