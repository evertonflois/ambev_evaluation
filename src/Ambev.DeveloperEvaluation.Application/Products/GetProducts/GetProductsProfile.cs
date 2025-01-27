using Ambev.DeveloperEvaluation.Domain.Entities;

using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProducts;

public class GetProductsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for GetProducts operation
    /// </summary>
    public GetProductsProfile()
    {
        CreateMap<Product, GetProductsResult>();
    }
}
