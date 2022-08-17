using FluentValidation;
using NTech.Dto.Concrete;

namespace NTech.Business.Validators.FluentValidation
{
    public class BrandWriteDtoValidator : AbstractValidator<BrandWriteDto>
    {
        public BrandWriteDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotNull()
                .NotEmpty();
            RuleFor(b => b.Name)
                .MaximumLength(50);
        }
    }
}
