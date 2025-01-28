using Ambev.DeveloperEvaluation.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of IBaseRepository using Entity Framework Core
/// </summary>
public class BaseRepository<T, TID> : IBaseRepository<T, TID> where T : class
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of BaseRepository
    /// </summary>
    /// <param name="context">The database context</param>
    public BaseRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new record in the database
    /// </summary>
    /// <param name="entity">The record to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created record</returns>
    public virtual async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {        
        await _context.Set<T>().AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    /// <summary>
    /// Retrieves a record by their unique identifier
    /// </summary>
    /// <param name="id">The unique identifier of the record</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The record if found, null otherwise</returns>
    public virtual async Task<T?> GetByIdAsync(TID id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>().FindAsync([id], cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Updates a record in the database
    /// </summary>
    /// <param name="entity">The record to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated record</returns>
    public virtual async Task<T> UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        _context.Set<T>().Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }

    /// <summary>
    /// Deletes a record from the database
    /// </summary>
    /// <param name="id">The unique identifier of the record to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the record was deleted, false if not found</returns>
    public virtual async Task<bool> DeleteAsync(TID id, CancellationToken cancellationToken = default)
    {
        var record = await GetByIdAsync(id, cancellationToken);
        if (record == null)
            return false;

        _context.Set<T>().Remove(record);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <summary>
    /// Builds a filter for multiple values (IN clause).
    /// </summary>
    /// <param name="propertyName">The property name.</param>
    /// <param name="values">The list of values to filter by.</param>
    /// <returns>A filter expression.</returns>
    public virtual Expression<Func<T, bool>> BuildInFilter<TValue>(string propertyName, IEnumerable<TValue> values)
    {
        if (values == null || !values.Any())
            return null;

        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, propertyName);

        // Create a list of constant expressions for each value
        var valueConstants = values.Select(value => Expression.Constant(value, property.Type));

        // Build OR expressions for each value
        var body = valueConstants
            .Select(value => Expression.Equal(property, value))
            .Aggregate(Expression.OrElse);

        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    /// <summary>
    /// Builds a comparison filter (e.g., ==, >, <).
    /// </summary>
    /// <param name="propertyName">The property to compare.</param>
    /// <param name="comparisonOperator">Comparison operator (e.g., "==", ">", "<").</param>
    /// <param name="value">The value to compare.</param>
    /// <returns>A filter expression.</returns>
    public virtual Expression<Func<T, bool>> BuildComparisonFilter(string propertyName, string comparisonOperator, object value)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, propertyName);

        // Handle nullability for value types
        var constant = Expression.Constant(value, property.Type);

        // Build comparison
        Expression comparison = comparisonOperator switch
        {
            "==" => Expression.Equal(property, constant),
            ">" => Expression.GreaterThan(property, constant),
            "<" => Expression.LessThan(property, constant),
            ">=" => Expression.GreaterThanOrEqual(property, constant),
            "<=" => Expression.LessThanOrEqual(property, constant),
            _ => throw new ArgumentException("Invalid comparison operator.")
        };

        return Expression.Lambda<Func<T, bool>>(comparison, parameter);
    }

    /// <summary>
    /// Combines multiple filters using logical operators (AND/OR).
    /// </summary>
    /// <param name="filters">List of individual filters.</param>
    /// <param name="useAndOperator">True for AND logic, False for OR logic.</param>
    /// <returns>A combined filter expression.</returns>
    public virtual Expression<Func<T, bool>> CombineFilters(IEnumerable<Expression<Func<T, bool>>> filters, bool useAndOperator = true)
    {
        if (filters == null || !filters.Any())
        {
            return null;
        }

        var parameter = Expression.Parameter(typeof(T), "x");

        // Combine expressions
        var combined = filters
            .Select(filter => ReplaceParameter(filter.Body, filter.Parameters[0], parameter))
            .Aggregate((left, right) => useAndOperator ? Expression.AndAlso(left, right) : Expression.OrElse(left, right));

        return Expression.Lambda<Func<T, bool>>(combined, parameter);
    }

    private Expression ReplaceParameter(Expression expression, ParameterExpression oldParameter, ParameterExpression newParameter)
    {
        return new ParameterReplacer(oldParameter, newParameter).Visit(expression);
    }

    private class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _oldParameter;
        private readonly ParameterExpression _newParameter;

        public ParameterReplacer(ParameterExpression oldParameter, ParameterExpression newParameter)
        {
            _oldParameter = oldParameter;
            _newParameter = newParameter;
        }

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node == _oldParameter ? _newParameter : base.VisitParameter(node);
        }
    }
}
