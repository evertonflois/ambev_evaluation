namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;


/// <summary>
/// Represents a product with details such as title, price, description, category, image, and rating.
/// </summary>
public class UpdateProductResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the product.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the title of the product.
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    public double Price { get; set; }

    /// <summary>
    /// Gets or sets the description of the product.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the category of the product.
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL of the product image.
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the rating of the product.
    /// </summary>
    public UpdateProductRating? Rating { get; set; }
}
