﻿namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;

/// <summary>
/// Represents a rating with a score and a count of reviews.
/// </summary>
public class CreateProductRating
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
