using FluentValidation;
using NTech.Dto.Concrete;

namespace NTech.Business.Validators.FluentValidation
{
    public class ProductWriteDtoValidator : AbstractValidator<ProductWriteDto>
    {
        public ProductWriteDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotNull()
                .NotEmpty();
            RuleFor(p => p.Name)
                .MaximumLength(100);

            RuleFor(p => p.Description)
                .NotNull()
                .NotEmpty();
            RuleFor(p => p.Description)
                .MaximumLength(500);

            RuleFor(p => p.CategoryId)
                .NotEmpty()
                .NotNull();

            RuleFor(p => p.UsingStatusId)
                .NotEmpty()
                .NotNull();

            RuleFor(p => p.Price)
                .NotNull()
                .NotEmpty();
            RuleFor(p => p.Price)
                .GreaterThan(0);

            RuleFor(p => p.Rating)
                .InclusiveBetween((byte)0, (byte)5);

        }
    }
}
