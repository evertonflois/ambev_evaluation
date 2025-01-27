using Ambev.DeveloperEvaluation.Domain.Entities;

using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Product entity operations
/// </summary>
public interface IProductRepository : IBaseRepository<Product, int>
{
    Task<(IEnumerable<Product> Items, int TotalCount)> GetFilteredAndPaginatedAsync(
        Expression<Func<Product, bool>> filterExpression = null,
        Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = null,
        int pageNumber = 1,
        int pageSize = 10);
}
