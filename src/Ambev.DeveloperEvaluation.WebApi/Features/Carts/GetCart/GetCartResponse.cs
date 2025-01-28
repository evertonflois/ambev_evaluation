using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateCart;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;

/// <summary>
/// API response model for GetCart operation
/// </summary>
public class GetCartResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the order.
    /// </summary>
    public int Id { get; set; }

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
    public List<CreateCartItemResponse> Products { get; set; } = [];

    public OrderCustomer Customer { get; set; } = new() { };

    public string SaleBranch { get; set; } = string.Empty;

    public bool Cancelled { get; set; } = false;
}
