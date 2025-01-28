using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.Application.Products.GetCarts;

/// <summary>
/// Handler for processing GetCartsCommand requests
/// </summary>
public class GetCartsHandler : IRequestHandler<GetCartsCommand, (IEnumerable<GetCartsResult>, int)>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetCartsHandler
    /// </summary>
    /// <param name="orderRepository">The order repository</param>
    /// <param name="userRepository">The user repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for GetCartsCommand</param>
    public GetCartsHandler(
        IOrderRepository orderRepository,
        IProductRepository productRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetCartsCommand request
    /// </summary>
    /// <param name="request">The GetCarts command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The user details if found</returns>
    public async Task<(IEnumerable<GetCartsResult>, int)> Handle(GetCartsCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetCartsValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        // all filters
        List<Expression<Func<Order, bool>>> filters = [];
        
        if (request.UserId.Length > 0)
        {
            foreach (var item in request.UserId)
            {
                filters.Add(o => o.UserId.Equals(item));
            }
        }

        if (request.Date.Length > 0)
        {
            foreach (var item in request.Date)
            {
                filters.Add(o => o.Date.Equals(item));
            }
        }

        if (request.ProductId.Length > 0)
        {
            foreach (var item in request.ProductId)
            {
                filters.Add(o => o.Products.Count(i => i.ProductId.Equals(item)) > 0);
            }
        }

        if (request.Customer.Length > 0)
        {
            foreach (var item in request.Customer)
            {
                filters.Add(o => o.Customer.Document.Equals(item) || o.Customer.Name.Equals(item));
            }
        }

        if (request.SaleBranch.Length > 0)
        {
            foreach (var item in request.SaleBranch)
            {
                filters.Add(o => o.SaleBranch.Equals(item));
            }
        }

        if (request.Cancelled)
            filters.Add(f => f.Cancelled == request.Cancelled);        

        
        var orderBy = (Func<IQueryable<Order>, IOrderedQueryable<Order>>)(q => q.OrderBy(o => o.Date));

        var orders = await _orderRepository.GetAllAsync(
                            pageNumber: request._page,
                            pageSize: request._size,
                            filters
                        );

        return (_mapper.Map<IEnumerable<GetCartsResult>>(orders.Items), orders.TotalCount);
    }
}
