using Core.Dto.Concrete;
using FluentValidation;

namespace NTech.Business.Validators.FluentValidation
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(l => l.Email)
                .NotEmpty()
                .NotNull();
            RuleFor(l => l.Email)
                .EmailAddress();

            RuleFor(l => l.Password)
                .NotEmpty()
                .NotNull();
            RuleFor(l => l.Password)
                .MaximumLength(20);
            RuleFor(l => l.Password)
                .MinimumLength(8);

        }
    }
}
