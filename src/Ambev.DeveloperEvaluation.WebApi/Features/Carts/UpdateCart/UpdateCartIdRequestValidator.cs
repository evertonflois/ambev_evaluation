using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

/// <summary>
/// Validator for UpdateCartIdRequest
/// </summary>
public class UpdateCartIdRequestValidator : AbstractValidator<UpdateCartIdRequest>
{
    /// <summary>
    /// Initializes validation rules for UpdateCartIdRequest
    /// </summary>
    public UpdateCartIdRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Cart ID is required");
    }
}
