using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Validator for CreateUserCommand that defines validation rules for user creation command.
/// </summary>
public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateProductCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Title: Required, length between 3 and 50 characters
    /// - Price: Must greater than 0
    /// - Category: Required, length between 3 and 20 characters
    /// - Rating: if not null, must rate and count greater than 0    
    /// </remarks>
    public CreateProductCommandValidator()
    {
        RuleFor(product => product.Title).NotEmpty().Length(3, 50);
        RuleFor(product => product.Price).GreaterThan(0);
        RuleFor(product => product.Category).NotEmpty().Length(3, 20);
        RuleFor(product => product.Rating).NotNull().Must(rating => rating?.Rate > 0).Must(rating => rating?.Count > 0);
    }
}