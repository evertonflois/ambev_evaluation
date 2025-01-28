namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;

/// <summary>
/// Request model for updating a cart
/// </summary>
public class UpdateCartIdRequest
{
    /// <summary>
    /// The unique identifier of the cart to update
    /// </summary>
    public int Id { get; set; }
}
