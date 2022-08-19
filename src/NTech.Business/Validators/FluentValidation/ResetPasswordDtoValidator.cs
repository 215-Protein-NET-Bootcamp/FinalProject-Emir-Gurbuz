using Core.Dto.Concrete;
using FluentValidation;

namespace NTech.Business.Validators.FluentValidation
{
    public class ResetPasswordDtoValidator : AbstractValidator<ResetPasswordDto>
    {
        public ResetPasswordDtoValidator()
        {
            RuleFor(r => r.UserId)
                .NotEmpty()
                .NotNull();

            RuleFor(r => r.OldPassword)
                .NotEmpty()
                .NotNull();
            RuleFor(l => l.OldPassword)
                .MaximumLength(20);
            RuleFor(l => l.OldPassword)
                .MinimumLength(8);

            RuleFor(r => r.NewPassword)
                .NotEmpty()
                .NotNull();
            RuleFor(l => l.NewPassword)
                .MaximumLength(20);
            RuleFor(l => l.NewPassword)
                .MinimumLength(8);
        }
    }
}
