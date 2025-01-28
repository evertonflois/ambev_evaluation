using Ambev.DeveloperEvaluation.Application.Orders.CreateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;

using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateCart;

/// <summary>
/// Profile for mapping between Application and API CreateCart responses
/// </summary>
public class CreateCartProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateCart feature
    /// </summary>
    public CreateCartProfile()
    {
        CreateMap<CreateCartRequest, CreateCartCommand>();
        CreateMap<CreateCartResult, CreateCartResponse>();
        CreateMap<CreateCartItem, OrderItem>();
        CreateMap<OrderItem, CreateCartItem>();
        CreateMap<OrderItem, CreateCartItemResponse>();
        CreateMap<OrderCustomer, CreateCartCustomer>();
    }
}
