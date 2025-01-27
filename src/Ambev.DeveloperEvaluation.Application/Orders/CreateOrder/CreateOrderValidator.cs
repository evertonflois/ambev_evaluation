using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateOrderCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - UserId: Required, length must be 36
    /// - Date: Required
    /// - Products: at least one ocurrence     
    /// </remarks>
    public CreateOrderCommandValidator()
    {
        RuleFor(order => order.UserId).NotEmpty().Length(36, 36);
        RuleFor(order => order.Date).NotEmpty();
        RuleFor(order => order.Products).NotNull().Must(i => i.Count > 0);
    }
}
