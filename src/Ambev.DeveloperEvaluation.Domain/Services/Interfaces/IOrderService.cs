using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services.Interfaces;

/// <summary>
/// Service interface for Order entity operations
/// </summary>
public interface IOrderService
{
    Task<Order> CreateCart(Order order);
    Task<Order> UpdateCart(int id, Order order);
}
