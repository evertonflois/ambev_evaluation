using Ambev.DeveloperEvaluation.Application.Products.GetCarts;
using Ambev.DeveloperEvaluation.Domain.Entities;

using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCarts;

/// <summary>
/// Profile for mapping between Application and API GetCarts responses
/// </summary>
public class GetCartsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetCarts feature
    /// </summary>
    public GetCartsProfile()
    {
        CreateMap<GetCartsRequest, GetCartsCommand>();
        CreateMap<GetCartsResult, GetCartsResponse>();        
    }
}
