using FluentValidation;
using NTech.Dto.Concrete;

namespace NTech.Business.Validators.FluentValidation
{
    public class OfferWriteDtoValidator : AbstractValidator<OfferWriteDto>
    {
        public OfferWriteDtoValidator()
        {
            RuleFor(o => o.ProductId)
                .NotNull()
                .NotEmpty();

            //RuleFor(o => o.OfferedPrice)
            //    .NotNull()
            //    .NotEmpty();

            //RuleFor(o => o.OfferedPrice)
            //    .GreaterThan(0);
        }

    }
}
