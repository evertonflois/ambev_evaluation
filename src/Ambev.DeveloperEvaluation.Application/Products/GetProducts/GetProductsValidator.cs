using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts;

/// <summary>
/// Validator for GetUserCommand
/// </summary>
public class GetProductsValidator : AbstractValidator<GetProductsCommand>
{
    /// <summary>
    /// Initializes validation rules for GetUserCommand
    /// </summary>
    public GetProductsValidator()
    {
        RuleFor(x => x._page)
            .NotNull()
            .WithMessage("Page is required");

        RuleFor(x => x._size)
            .NotNull()
            .WithMessage("Size is required");
    }
}
