using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, CreateOrderResult>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMongoCountersRepository _mongoCountersRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of CreateProductHandler
    /// </summary>
    /// <param name="productRepository">The user repository</param>
    /// <param name="mapper">The AutoMapper instance</param>    
    public CreateOrderHandler(IOrderRepository orderRepository, IMongoCountersRepository mongoCountersRepository, IMapper mapper)
    {
        _orderRepository = orderRepository;
        _mongoCountersRepository = mongoCountersRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the CreateOrderCommand request
    /// </summary>
    /// <param name="command">The CreateUser command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user details</returns>
    public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateOrderCommandValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var order = _mapper.Map<Order>(command);

        order.Id = await _orderRepository.GetNextSequence("orderid");

        var createdOrder = await _orderRepository.CreateAsync(order);
        var result = _mapper.Map<CreateOrderResult>(createdOrder);
        return result;
    }
}
