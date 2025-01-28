using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

/// <summary>
/// Handler for processing DeleteCartCommand requests
/// </summary>
public class DeleteCartHandler : IRequestHandler<DeleteCartCommand, DeleteCartResponse>
{
    private readonly IOrderRepository _orderRepository;

    /// <summary>
    /// Initializes a new instance of DeleteCartHandler
    /// </summary>
    /// <param name="userRepository">The order repository</param>
    /// <param name="validator">The validator for DeleteCartCommand</param>
    public DeleteCartHandler(
        IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    /// <summary>
    /// Handles the DeleteCartCommand request
    /// </summary>
    /// <param name="request">The DeleteCart command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The result of the delete operation</returns>
    public async Task<DeleteCartResponse> Handle(DeleteCartCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteCartValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _orderRepository.DeleteAsync(o => o.Id == request.Id);
        if (!success)
            throw new KeyNotFoundException($"Cart with ID {request.Id} not found");

        return new DeleteCartResponse { Success = true };
    }
}
