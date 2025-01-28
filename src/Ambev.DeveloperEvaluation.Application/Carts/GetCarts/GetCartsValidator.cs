using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetCarts;

/// <summary>
/// Validator for GetCartsCommand
/// </summary>
public class GetCartsValidator : AbstractValidator<GetCartsCommand>
{
    /// <summary>
    /// Initializes validation rules for GetCartsCommand
    /// </summary>
    public GetCartsValidator()
    {
        RuleFor(x => x._page)
            .NotNull()
            .WithMessage("Page is required");

        RuleFor(x => x._size)
            .NotNull()
            .WithMessage("Size is required");
    }
}
