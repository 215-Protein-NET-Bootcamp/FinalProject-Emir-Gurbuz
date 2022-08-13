using Core.Dto.Concrete;
using FluentValidation;

namespace NTech.Business.Validators.FluentValidation
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(r => r.FirstName)
                .NotNull()
                .NotEmpty();

            RuleFor(r => r.FirstName)
                .MaximumLength(30);

            RuleFor(r => r.LastName)
                .NotNull()
                .NotEmpty();

            RuleFor(r => r.LastName)
                .MaximumLength(30);

            RuleFor(r => r.Email)
                .NotNull()
                .NotEmpty();
            RuleFor(r => r.Email)
                .EmailAddress();

            RuleFor(r => r.DateOfBirth)
                .NotNull()
                .NotEmpty();

            RuleFor(r => r.Password)
                .NotNull()
                .NotEmpty();
            RuleFor(r => r.Password)
                .MaximumLength(20);
            RuleFor(r => r.Password)
                .MinimumLength(8);

        }
    }
}
