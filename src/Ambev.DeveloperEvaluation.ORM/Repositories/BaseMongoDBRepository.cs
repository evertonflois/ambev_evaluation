using Ambev.DeveloperEvaluation.Domain.Repositories;

using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class BaseMongoDBRepository<T> : IBaseMongoDBRepository<T>
{
    private readonly IMongoCollection<T> _collection;
    private readonly MongoDBContext _context;

    public BaseMongoDBRepository(MongoDBContext context, string collectionName)
    {
        _collection = context.GetCollection<T>(collectionName);
        _context = context;
    }

    public async Task<T> GetByIdAsync(Expression<Func<T, bool>> key)
    {
        return await _collection.Find(key).FirstOrDefaultAsync();
    }

    public async Task<(IEnumerable<T> Items, int TotalCount)> GetAllAsync(int pageNumber, int pageSize, IEnumerable<Expression<Func<T, bool>>> filters)
    {
        var query = _collection.AsQueryable();

        foreach (var filter in filters)
        {
            query = query.Where(filter);
        }

        // Get total count
        var totalCount = await query.CountAsync();

        return (await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(), totalCount);
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task<bool> UpdateAsync(Expression<Func<T, bool>> key, T entity)
    {
        return (await _collection.ReplaceOneAsync(key, entity)).ModifiedCount > 0;        
    }

    public async Task<bool> DeleteAsync(Expression<Func<T, bool>> key)
    {
        return (await _collection.DeleteOneAsync(key)).DeletedCount > 0;
    }

    public async Task<int> GetNextSequence(string counterName)
    {   // The new sequence value
        return await _context.GetNextSequenceValue(counterName);
    }
}
