using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(product => product.Title).NotEmpty();
        
        RuleFor(product => product.Price).GreaterThan(0);

        RuleFor(product => product.Category).NotEmpty();
    }
}
