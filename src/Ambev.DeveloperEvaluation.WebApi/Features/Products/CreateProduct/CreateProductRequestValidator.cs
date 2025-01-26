using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Validator for CreateProductRequest that defines validation rules for user creation.
/// </summary>
public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    /// <summary>
    /// Initializes a new instance of the CreateProductRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:    
    /// - Title: Required, length between 3 and 50 characters
    /// - Price: Must greater than 0
    /// - Category: Required, length between 3 and 20 characters
    /// - Rating: if not null, must rate and count greater than 0    
    /// </remarks>
    public CreateProductRequestValidator()
    {        
        RuleFor(product => product.Title).NotEmpty().Length(3, 50);
        RuleFor(product => product.Price).GreaterThan(0);
        RuleFor(product => product.Category).NotEmpty().Length(3, 20);
        RuleFor(product => product.Rating).NotNull().Must(rating => rating?.Rate > 0).Must(rating => rating?.Count > 0);
    }
}