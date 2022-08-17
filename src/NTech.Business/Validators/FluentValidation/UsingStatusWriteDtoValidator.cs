using FluentValidation;
using NTech.Dto.Concrete;

namespace NTech.Business.Validators.FluentValidation
{
    public class UsingStatusWriteDtoValidator : AbstractValidator<UsingStatusWriteDto>
    {
        public UsingStatusWriteDtoValidator()
        {
            RuleFor(b => b.Status)
                .NotNull()
                .NotEmpty();
            RuleFor(b => b.Status)
                .MaximumLength(50);
        }
    }
}
