using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts;

/// <summary>
/// Handler for processing GetProductsCommand requests
/// </summary>
public class GetProductsHandler : IRequestHandler<GetProductsCommand, (IEnumerable<GetProductsResult>, int)>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetProductsHandler
    /// </summary>
    /// <param name="userRepository">The user repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for GetProductsCommand</param>
    public GetProductsHandler(
        IProductRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetProductsCommand request
    /// </summary>
    /// <param name="request">The GetProducts command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user details if found</returns>
    public async Task<(IEnumerable<GetProductsResult>, int)> Handle(GetProductsCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetProductsValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // all filters
        List<Expression<Func<Product, bool>>> filters = [];
        
        if (request.Title.Length > 0)                     
            filters.Add(_productRepository.BuildInFilter("Title", request.Title));

        if (request.Price.Length > 0)
            filters.Add(_productRepository.BuildInFilter("Price", request.Price));

        if (request.Description.Length > 0)
            filters.Add(_productRepository.BuildInFilter("Description", request.Description));

        if (request.Category.Length > 0)
            filters.Add(_productRepository.BuildInFilter("Category", request.Category));

        if (request._MinPrice > 0)
            filters.Add(_productRepository.BuildComparisonFilter("Price", ">=", request._MinPrice));

        if (request._MaxPrice > 0)
            filters.Add(_productRepository.BuildComparisonFilter("Price", "<=", request._MaxPrice));

        var combinedFilter = _productRepository.CombineFilters(filters, useAndOperator: true);

        // Define ordering (e.g., order by price ascending)
        var orderBy = (Func<IQueryable<Product>, IOrderedQueryable<Product>>)(q => q.OrderBy(p => p.Price));

        var products = await _productRepository.GetFilteredAndPaginatedAsync(
                            filterExpression: combinedFilter,
                            orderBy: orderBy,
                            pageNumber: request._page,
                            pageSize: request._size
                        );

        return (_mapper.Map<IEnumerable<GetProductsResult>>(products.Items), products.TotalCount);
    }
}
