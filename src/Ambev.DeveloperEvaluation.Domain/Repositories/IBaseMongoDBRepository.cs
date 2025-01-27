namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IBaseMongoDBRepository<T>
{
    Task<T> GetByIdAsync(string id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(string id);
    Task<int> GetNextSequence(string counterName);
}
