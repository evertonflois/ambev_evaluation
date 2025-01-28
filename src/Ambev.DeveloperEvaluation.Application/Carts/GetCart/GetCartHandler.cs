using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetCart;

/// <summary>
/// Handler for processing GetCartCommand requests
/// </summary>
public class GetCartHandler : IRequestHandler<GetCartCommand, GetCartResult>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of GetCartHandler
    /// </summary>
    /// <param name="orderRepository">The cart repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for GetCartCommand</param>
    public GetCartHandler(
        IOrderRepository orderRepository,
        IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetCartCommand request
    /// </summary>
    /// <param name="request">The GetCart command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cart details if found</returns>
    public async Task<GetCartResult> Handle(GetCartCommand request, CancellationToken cancellationToken)
    {
        var validator = new GetCartValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var cart = await _orderRepository.GetByIdAsync(o => o.Id == request.Id);
        if (cart == null)
            throw new KeyNotFoundException($"Cart with ID {request.Id} not found");

        return _mapper.Map<GetCartResult>(cart);
    }
}
