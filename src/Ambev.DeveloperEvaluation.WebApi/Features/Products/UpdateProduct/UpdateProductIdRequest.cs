namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

/// <summary>
/// Request model for updating a product
/// </summary>
public class UpdateProductIdRequest
{
    /// <summary>
    /// The unique identifier of the product to update
    /// </summary>
    public int Id { get; set; }
}
