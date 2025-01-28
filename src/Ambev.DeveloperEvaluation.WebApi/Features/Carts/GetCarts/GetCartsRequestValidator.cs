using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCarts;

/// <summary>
/// Validator for GetCartsRequest
/// </summary>
public class GetCartsRequestValidator : AbstractValidator<GetCartsRequest>
{
    /// <summary>
    /// Initializes validation rules for GetCartsRequest
    /// </summary>
    public GetCartsRequestValidator()
    {
        RuleFor(x => x._page)
            .NotNull()
            .WithMessage("Page is required");

        RuleFor(x => x._size)
            .NotNull()
            .WithMessage("Size is required");
    }
}
