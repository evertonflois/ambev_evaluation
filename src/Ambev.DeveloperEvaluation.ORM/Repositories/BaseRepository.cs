using Ambev.DeveloperEvaluation.Domain.Repositories;

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
    public async Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
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
    public async Task<T?> GetByIdAsync(TID id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>().FindAsync([id], cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Deletes a record from the database
    /// </summary>
    /// <param name="id">The unique identifier of the record to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>True if the record was deleted, false if not found</returns>
    public async Task<bool> DeleteAsync(TID id, CancellationToken cancellationToken = default)
    {
        var record = await GetByIdAsync(id, cancellationToken);
        if (record == null)
            return false;

        _context.Set<T>().Remove(record);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
