using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

/// <summary>
/// Validator for UpdateProductIdRequest
/// </summary>
public class UpdateProductIdRequestValidator : AbstractValidator<UpdateProductIdRequest>
{
    /// <summary>
    /// Initializes validation rules for UpdateProductIdRequest
    /// </summary>
    public UpdateProductIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product ID is required");
    }
}
