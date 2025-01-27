using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class MongoCountersRepository : BaseMongoDBRepository<MongoCounters>, IMongoCountersRepository
{
    public MongoCountersRepository(MongoDBContext context) : base(context, "Counters")
    {
    }
}
