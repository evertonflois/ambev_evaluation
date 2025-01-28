using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services.Interfaces;

using Microsoft.Extensions.Configuration;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IConfiguration _configuration;

    public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, IConfiguration configuration)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _configuration = configuration;
    }

    public async Task<Order> CreateCart(Order order)
    {
        order.Id = await _orderRepository.GetNextSequence("orderid");

        var itemsList = GroupItems(order.Products);

        itemsList = SetPricesAndDiscounts(itemsList);
        
        order.Products = itemsList;

        return await _orderRepository.CreateAsync(order);
    }

    public async Task<Order> UpdateCart(int id, Order order)
    {
        var itemsList = GroupItems(order.Products);

        itemsList = SetPricesAndDiscounts(itemsList);

        order.Products = itemsList;

        var success = await _orderRepository.UpdateAsync(o => o.Id == id, order);

        if (success)
            return await _orderRepository.GetByIdAsync(o => o.Id == id);

        return order;
    }

    private List<OrderItem> SetPricesAndDiscounts(List<OrderItem> orderItems)
    {
        var discountAbove4 = _configuration.GetSection("SalesParameters").GetValue<double>("DiscountAbove4") / 100;
        var discountAbove10 = _configuration.GetSection("SalesParameters").GetValue<double>("DiscountAbove10") / 100;
        
        orderItems.ForEach(async item =>
        {
            var product = await _productRepository.GetByIdAsync(item.ProductId);

            if (product != null)
            {
                item.UnitPrice = product.Price;
                item.Amount = product.Price * item.Quantity;

                if (item.Quantity > 3 && item.Quantity < 10)
                    item.Discount = item.Amount * discountAbove4;
                else if (item.Quantity > 9 && item.Quantity < 21)
                    item.Discount = item.Amount * discountAbove10;
            }
        });

        return orderItems;
    }


    private List<OrderItem> GroupItems(List<OrderItem> orderItems)
    {
        return orderItems
                .GroupBy(i => i.ProductId)
                .Select(grp => grp.ToList())
                .First();        
    }
}
