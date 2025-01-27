using Ambev.DeveloperEvaluation.Domain.Entities;


namespace Ambev.DeveloperEvaluation.Domain.Interfaces.Entities;

public interface IOrder
{
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the unique identifier for the user placing the order.
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// Gets or sets the date of the order in string format (e.g., "yyyy-MM-dd").
    /// If using DateTime, consider parsing this string into DateTime.
    /// </summary>
    public string Date { get; set; }

    /// <summary>
    /// Gets or sets the list of products included in the order.
    /// Each product contains product details and quantity.
    /// </summary>
    public List<OrderItem> Products { get; set; }
}
