namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCarts;

/// <summary>
/// Represents a item of a cart.
/// </summary>
public class GetCartsItem
{
    // <summary>
    /// Gets or sets the unique identifier for the product.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the product in the order.
    /// </summary>
    public int Quantity { get; set; }

    public double UnitPrice { get; set; }

    public double Discount { get; set; }

    public double Amount { get; set; }
}
