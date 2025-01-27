using Ambev.DeveloperEvaluation.Domain.Repositories;

using MongoDB.Bson;
using MongoDB.Driver;

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

    public async Task<T> GetByIdAsync(string id)
    {
        //return await _collection.Find(x => x.Id == id).FirstOrDefaultAsync();
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }

    public async Task<T> CreateAsync(T entity)
    {
        await _collection.InsertOneAsync(entity);
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        //await _collection.ReplaceOneAsync(x => x.Id == ((dynamic)entity).Id, entity);
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(string id)
    {
        //await _collection.DeleteOneAsync(x => x.Id == id);
        throw new NotImplementedException();
    }

    public async Task<int> GetNextSequence(string counterName)
    {   // The new sequence value
        return await _context.GetNextSequenceValue(counterName);
    }
}
