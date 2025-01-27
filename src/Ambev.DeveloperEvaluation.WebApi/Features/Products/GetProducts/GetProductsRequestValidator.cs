using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts;

/// <summary>
/// Validator for GetProductsRequest
/// </summary>
public class GetProductsRequestValidator : AbstractValidator<GetProductsRequest>
{
    /// <summary>
    /// Initializes validation rules for GetProductsRequest
    /// </summary>
    public GetProductsRequestValidator()
    {
        RuleFor(x => x._page)
            .NotNull()
            .WithMessage("Page is required");

        RuleFor(x => x._size)
            .NotNull()
            .WithMessage("Size is required");
    }
}
