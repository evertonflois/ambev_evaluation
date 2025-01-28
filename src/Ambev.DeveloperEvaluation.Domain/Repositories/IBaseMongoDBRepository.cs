using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IBaseMongoDBRepository<T>
{
    Task<T> GetByIdAsync(Expression<Func<T, bool>> key);
    Task<(IEnumerable<T> Items, int TotalCount)> GetAllAsync(int pageNumber, int pageSize, IEnumerable<Expression<Func<T, bool>>> filters);
    Task<T> CreateAsync(T entity);
    Task<bool> UpdateAsync(Expression<Func<T, bool>> key, T entity);
    Task<bool> DeleteAsync(Expression<Func<T, bool>> key);
    Task<int> GetNextSequence(string counterName);
}
