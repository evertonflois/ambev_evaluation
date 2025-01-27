using Ambev.DeveloperEvaluation.Application.Common;

using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts;

public class GetProductsCommand : CommandQueryParameters, IRequest<(IEnumerable<GetProductsResult>, int)>
{
    /// <summary>
    /// Gets or sets the title of the product.
    /// </summary>
    public string[] Title { get; set; } = [];

    /// <summary>
    /// Gets or sets the price of the product.
    /// </summary>
    public double[] Price { get; set; } = [];

    /// <summary>
    /// Gets or sets the min price of the product.
    /// </summary>
    public double? _MinPrice { get; set; }

    /// <summary>
    /// Gets or sets the max price of the product.
    /// </summary>
    public double? _MaxPrice { get; set; }

    /// <summary>
    /// Gets or sets the description of the product.
    /// </summary>
    public string[] Description { get; set; } = [];

    /// <summary>
    /// Gets or sets the category of the product.
    /// </summary>
    public string[] Category { get; set; } = [];
}
