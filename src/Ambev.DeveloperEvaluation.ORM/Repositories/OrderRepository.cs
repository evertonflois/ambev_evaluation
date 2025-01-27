using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class OrderRepository : BaseMongoDBRepository<Order>, IOrderRepository
{
    public OrderRepository(MongoDBContext context) : base(context, "Orders")
    {
    }
}
