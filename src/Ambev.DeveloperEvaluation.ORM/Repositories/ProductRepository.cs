using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IProductRepository using Entity Framework Core
/// </summary>
public class ProductRepository : BaseRepository<Product, int>, IProductRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of ProductRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public ProductRepository(DefaultContext context) : base(context)
    {
        _context = context;
    }

    /// <summary>
    /// Filters and paginates the data using dynamic filters.
    /// </summary>
    /// <param name="filterExpression">The combined filter expression.</param>
    /// <param name="orderBy">Order function to sort the data.</param>
    /// <param name="pageNumber">Page number (1-based).</param>
    /// <param name="pageSize">Page size.</param>
    /// <returns>Paged list of items.</returns>
    public async Task<(IEnumerable<Product> Items, int TotalCount)> GetFilteredAndPaginatedAsync(
        Expression<Func<Product, bool>> filterExpression = null,
        Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null,
        int pageNumber = 1,
        int pageSize = 10)
    {
        if (pageNumber <= 0) pageNumber = 1;
        if (pageSize <= 0) pageSize = 10;

        IQueryable<Product> query = _context.Products;

        // Apply filters
        if (filterExpression != null)
        {
            query = query.Where(filterExpression);
        }

        // Get total count before pagination
        var totalCount = await query.CountAsync();

        // Apply ordering
        if (orderBy != null)
        {
            query = orderBy(query);
        }

        // Apply pagination
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (items, totalCount);
    }



}
