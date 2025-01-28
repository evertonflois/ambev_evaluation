using Ambev.DeveloperEvaluation.Domain.Entities;

using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateCart;

public class CreateCartProfile : Profile
{
    public CreateCartProfile()
    {
        CreateMap<CreateCartCommand, Order>();
        CreateMap<Order, CreateCartResult>();
    }
}
