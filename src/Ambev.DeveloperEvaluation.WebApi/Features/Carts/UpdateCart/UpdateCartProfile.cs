using Ambev.DeveloperEvaluation.Application.Orders.UpdateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;

using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.UpdateCart;

/// <summary>
/// Profile for mapping between Application and API UpdateCart responses
/// </summary>
public class UpdateCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for UpdateCart feature
    /// </summary>
    public UpdateCartProfile()
    {
        CreateMap<UpdateCartRequest, UpdateCartCommand>();
        CreateMap<UpdateCartResult, UpdateCartResponse>();
        CreateMap<UpdateCartItem, OrderItem>();
        CreateMap<OrderItem, UpdateCartItem>();
        CreateMap<OrderItem, UpdateCartItemResponse>();
        CreateMap<OrderCustomer, UpdateCartCustomer>();
    }
}
