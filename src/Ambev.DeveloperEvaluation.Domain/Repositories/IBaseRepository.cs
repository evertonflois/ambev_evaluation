using Ambev.DeveloperEvaluation.Domain.Entities;

using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for entity operations
/// </summary>
public interface IBaseRepository<T, TID>
{
    /// <summary>
    /// Creates a new record in the repository
    /// </summary>
    /// <param name="entity">The record to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user</returns>
    Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Retrieves a record by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the record</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user if found, null otherwise</returns>
    Task<T?> GetByIdAsync(TID id, CancellationToken cancellationToken = default);
    

    /// <summary>
    /// Deletes a record from the repository
    /// </summary>
    /// <param name="id">The unique identifier of the user to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the user was deleted, false if not found</returns>
    Task<bool> DeleteAsync(TID id, CancellationToken cancellationToken = default);

    Expression<Func<T, bool>> BuildInFilter<TValue>(string propertyName, IEnumerable<TValue> values);

    Expression<Func<T, bool>> BuildComparisonFilter(string propertyName, string comparisonOperator, object value);

    Expression<Func<T, bool>> CombineFilters(IEnumerable<Expression<Func<T, bool>>> filters, bool useAndOperator = true);
}
