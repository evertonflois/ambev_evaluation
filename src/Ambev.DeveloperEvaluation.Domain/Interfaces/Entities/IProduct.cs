using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Interfaces.Entities;

/// <summary>
/// Represents a product with details such as ID, title, price, description, category, image, and rating.
/// </summary>
public interface IProduct
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the product.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Gets or sets the description of the product.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets the category of the product.
    /// </summary>
    public string Category { get; set; }

    /// <summary>
    /// Gets or sets the URL of the product image.
    /// </summary>
    public string Image { get; set; }

    /// <summary>
    /// Gets or sets the rating of the product.
    /// </summary>
    public ProductRating? Rating { get; set; }
}
