﻿using Ambev.DeveloperEvaluation.Application.Common;

using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetCarts;

public class GetCartsCommand : CommandQueryParameters, IRequest<(IEnumerable<GetCartsResult>, int)>
{
    /// <summary>
    /// Gets or sets the unique identifier for the user placing the order.
    /// </summary>
    public string[] UserId { get; set; } = [];

    /// <summary>
    /// Gets or sets the date of the order in string format (e.g., "yyyy-MM-dd").
    /// If using DateTime, consider parsing this string into DateTime.
    /// </summary>
    public string[] Date { get; set; } = [];

    /// <summary>
    /// Gets or sets the unique identifier for the product.
    /// </summary>
    public int[] ProductId { get; set; } = [];

    /// <summary>
    /// Gets or sets the customer
    /// </summary>
    public string[] Customer { get; set; } = [];

    /// <summary>
    /// Gets or sets the sale branch
    /// </summary>
    public string[] SaleBranch { get; set; } = [];

    public bool Cancelled { get; set; } = false;
}