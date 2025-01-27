using Ambev.DeveloperEvaluation.Domain.Interfaces.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class MongoCounters : IMongoCounters
{
    public int Seq { get; set; }
}
