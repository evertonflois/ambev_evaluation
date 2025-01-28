using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

/// <summary>
/// Validator for UpdateProductRequest that defines validation rules for user creation.
/// </summary>
public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    /// <summary>
    /// Initializes a new instance of the UpdateProductRequestValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:    
    /// - Title: Required, length between 3 and 50 characters
    /// - Price: Must greater than 0
    /// - Category: Required, length between 3 and 20 characters
    /// - Rating: if not null, must rate and count greater than 0    
    /// </remarks>
    public UpdateProductRequestValidator()
    {        
        RuleFor(product => product.Title).NotEmpty().Length(3, 50);
        RuleFor(product => product.Price).GreaterThan(0);
        RuleFor(product => product.Category).NotEmpty().Length(3, 20);
        RuleFor(product => product.Rating).NotNull().Must(rating => rating?.Rate > 0).Must(rating => rating?.Count > 0);
    }
}