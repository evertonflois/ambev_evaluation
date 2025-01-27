using Ambev.DeveloperEvaluation.Application.Products.GetProducts;
using Ambev.DeveloperEvaluation.Domain.Entities;

using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts;

/// <summary>
/// Profile for mapping between Application and API CreateProduc responses
/// </summary>
public class GetProductsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for CreateProduc feature
    /// </summary>
    public GetProductsProfile()
    {
        CreateMap<GetProductsRequest, GetProductsCommand>();
        CreateMap<GetProductsResult, GetProductsResponse>();
        CreateMap<ProductRating, GetProductsRating>();
    }
}
