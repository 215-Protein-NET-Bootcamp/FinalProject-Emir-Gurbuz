using FluentValidation;
using NTech.Dto.Concrete;

namespace NTech.Business.Validators.FluentValidation
{
    public class CategoryWriteDtoValidator : AbstractValidator<CategoryWriteDto>
    {
        public CategoryWriteDtoValidator()
        {
            RuleFor(b => b.Name)
                .NotNull()
                .NotEmpty();
            RuleFor(b => b.Name)
                .MaximumLength(50);
        }
    }
}
