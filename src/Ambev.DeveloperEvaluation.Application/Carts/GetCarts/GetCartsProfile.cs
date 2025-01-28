using Ambev.DeveloperEvaluation.Domain.Entities;

using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.GetCarts;

public class GetCartsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetCarts operation
    /// </summary>
    public GetCartsProfile()
    {
        CreateMap<Order, GetCartsResult>();
    }
}
