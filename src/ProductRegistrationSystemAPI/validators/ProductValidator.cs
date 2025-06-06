using FluentValidation;
using ProductRegistrationSystemAPI.data.entities;

namespace ProductRegistrationSystemAPI.validators;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(p => p.Name).NotEmpty();
        RuleFor(p => p.Price).GreaterThan(0);
    }
}
