namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

/// <summary>
/// Represents a rating with a score and a count of reviews.
/// </summary>
public class UpdateProductRating
{
    /// <summary>
    /// Gets or sets the average rating score of the product.
    /// </summary>
    public double Rate { get; set; }

    /// <summary>
    /// Gets or sets the number of ratings for the product.
    /// </summary>
    public int Count { get; set; }
}
