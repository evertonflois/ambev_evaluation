using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateCart;

public class CreateCartCommand : IRequest<CreateCartResult>
{
    private readonly IProductRepository? _productRepository;

    public CreateCartCommand()
    { 
    }

    public CreateCartCommand(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }


    /// <summary>
    /// Gets or sets the unique identifier for the user placing the order.
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date of the order in string format (e.g., "yyyy-MM-dd").
    /// If using DateTime, consider parsing this string into DateTime.
    /// </summary>
    public string Date { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list of products included in the order.
    /// Each product contains product details and quantity.
    /// </summary>
    public List<OrderItem> Products { get; set; } = [];

    public OrderCustomer Customer { get; set; } = new() { };

    public string SaleBranch { get; set; } = string.Empty;

    public bool Cancelled { get; set; } = false;

    public ValidationResultDetail Validate()
    {
        var validator = new CreateCartCommandValidator(_productRepository);
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}
