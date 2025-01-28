using Ambev.DeveloperEvaluation.Domain.Interfaces.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class OrderCustomer : IOrderCustomer
{
    public string Document { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;
}
