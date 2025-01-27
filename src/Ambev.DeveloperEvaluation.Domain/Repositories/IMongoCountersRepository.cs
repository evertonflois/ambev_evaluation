using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

/// <summary>
/// Repository interface for Counters entity operations
/// </summary>
public interface IMongoCountersRepository : IBaseMongoDBRepository<MongoCounters>
{    
}
