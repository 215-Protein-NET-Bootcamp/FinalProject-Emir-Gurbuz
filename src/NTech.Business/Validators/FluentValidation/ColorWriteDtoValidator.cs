using FluentValidation;
using NTech.Dto.Concrete;

namespace NTech.Business.Validators.FluentValidation
{
    public class ColorWriteDtoValidator : AbstractValidator<ColorWriteDto>
    {
        public ColorWriteDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotNull()
                .NotEmpty();
            RuleFor(b => b.Name)
                .MaximumLength(50);
        }
    }
}
