using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Orders.UpdateCart;

public class UpdateCartCommandValidator : AbstractValidator<UpdateCartCommand>
{       
    /// <summary>
    /// Initializes a new instance of the UpdateCartCommandValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - UserId: Required, length must be 36
    /// - Date: Required
    /// - Products: at least one ocurrence     
    /// </remarks>
    public UpdateCartCommandValidator(IProductRepository productRepository)
    {
        RuleFor(order => order.UserId).NotEmpty().Length(36, 36);
        RuleFor(order => order.Date).NotEmpty();
        RuleFor(order => order.Products).NotNull().Must(i => i.Count > 0);
        RuleFor(order => order.Products).ForEach(i => i.SetValidator(new OrderItemValidator(productRepository)));
    }
}

public class OrderItemValidator : AbstractValidator<OrderItem>
{
    public OrderItemValidator(IProductRepository productRepository)
    {
        RuleFor(li => li.ProductId).MustAsync(async (id, cancellation) => (await productRepository.GetByIdAsync(id, cancellation)) != null)
            .WithMessage("Product does not exists.");
    }
}