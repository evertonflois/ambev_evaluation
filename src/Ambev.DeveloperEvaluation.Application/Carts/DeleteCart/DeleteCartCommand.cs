using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;

/// <summary>
/// Command for deleting a user
/// </summary>
public record DeleteCartCommand : IRequest<DeleteCartResponse>
{
    /// <summary>
    /// The unique identifier of the cart to delete
    /// </summary>
    public int Id { get; }

    /// <summary>
    /// Initializes a new instance of DeleteCartCommand
    /// </summary>
    /// <param name="id">The ID of the user to delete</param>
    public DeleteCartCommand(int id)
    {
        Id = id;
    }
}
