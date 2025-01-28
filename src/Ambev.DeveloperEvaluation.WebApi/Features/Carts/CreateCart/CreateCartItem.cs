namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateCart;

public class CreateCartItem
{
    /// <summary>
    /// Gets or sets the unique identifier for the product.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product in the order.
    /// </summary>
    public int Quantity { get; set; }
}
