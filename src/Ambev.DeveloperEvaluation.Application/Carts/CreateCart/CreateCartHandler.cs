using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services.Interfaces;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateCart;

public class CreateCartHandler : IRequestHandler<CreateCartCommand, CreateCartResult>
{
    private readonly IOrderService _orderService;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CreateProductHandler
    /// </summary>
    /// <param name="productRepository">The user repository</param>
    /// <param name="mapper">The AutoMapper instance</param>    
    public CreateCartHandler(IOrderService orderService, IProductRepository productRepository, IMapper mapper)
    {
        _orderService = orderService;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the CreateCartCommand request
    /// </summary>
    /// <param name="command">The CreateUser command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user details</returns>
    public async Task<CreateCartResult> Handle(CreateCartCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateCartCommandValidator(_productRepository);
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var order = _mapper.Map<Order>(command);        

        var createdOrder = await _orderService.CreateCart(order);
        var result = _mapper.Map<CreateCartResult>(createdOrder);
        return result;
    }
}
