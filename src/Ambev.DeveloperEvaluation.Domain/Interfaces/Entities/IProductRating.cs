namespace Ambev.DeveloperEvaluation.Domain.Interfaces.Entities;

/// <summary>
/// Represents a rating with a score and a count of reviews.
/// </summary>
public interface IProductRating
{
    /// <summary>
    /// Gets or sets the product id of the rating.
    /// </summary>
    public int ProductId { get; set; }

    /// <summary>
    /// Gets or sets the average rating score of the product.
    /// </summary>
    public double Rate { get; set; }

    /// <summary>
    /// Gets or sets the number of ratings for the product.
    /// </summary>
    public int Count { get; set; }
}
