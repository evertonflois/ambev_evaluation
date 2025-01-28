using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateCart;
using Ambev.DeveloperEvaluation.Application.Orders.CreateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCarts;
using Ambev.DeveloperEvaluation.Application.Products.GetCarts;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetCart;
using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.Application.Carts.DeleteCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.UpdateCart;
using Ambev.DeveloperEvaluation.Application.Orders.UpdateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;
namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders;

/// <summary>
/// Controller for managing user operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CartsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of ProductsController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public CartsController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new cart
    /// </summary>
    /// <param name="request">The cart creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created cart details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateCartResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCart([FromBody] CreateCartRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateCartCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateCartResponse>
        {
            Success = true,
            Message = "Cart created successfully",
            Data = _mapper.Map<CreateCartResponse>(response)
        });
    }

    /// <summary>
    /// Retrieves a list of carts
    /// </summary>
    /// <param name="request">Filters of products</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The carts list</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<GetCartsResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCarts([FromQuery] GetCartsRequest request, CancellationToken cancellationToken)
    {
        //var request = new GetCartRequest { Id = id };
        var validator = new GetCartsRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetCartsCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        var paginatedList = new PaginatedList<GetCartsResponse>(_mapper.Map<IEnumerable<GetCartsResponse>>(response.Item1).ToList(),
                                                        response.Item2,
                                                        request._page,
                                                        request._size);

        return OkPaginated(paginatedList);
    }

    /// <summary>
    /// Retrieves a cart by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the cart</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cart details if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetCartResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCart([FromRoute] int id, CancellationToken cancellationToken)
    {
        var request = new GetCartRequest { Id = id };
        var validator = new GetCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetCartCommand>(request.Id);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<GetCartResponse>
        {
            Success = true,
            Message = "Cart retrieved successfully",
            Data = _mapper.Map<GetCartResponse>(response)
        });
    }

    /// <summary>
    /// Deletes a cart by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the cart to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the cart was deleted</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCart([FromRoute] int id, CancellationToken cancellationToken)
    {
        var request = new DeleteCartRequest { Id = id };
        var validator = new DeleteCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteCartCommand>(request.Id);
        await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Cart deleted successfully"
        });
    }

    /// <summary>
    /// Update a cart
    /// </summary>
    /// <param name="request">The cart update request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated cart details</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateCartResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateCart([FromRoute] int id, [FromBody] UpdateCartRequest request, CancellationToken cancellationToken)
    {
        var requestId = new UpdateCartIdRequest() { Id = id };
        var validatorId = new UpdateCartIdRequestValidator();
        var validationIdResult = await validatorId.ValidateAsync(requestId, cancellationToken);

        if (!validationIdResult.IsValid)
            return BadRequest(validationIdResult.Errors);

        var validator = new UpdateCartRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<UpdateCartCommand>(request);
        command.SetId(id);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<UpdateCartResponse>
        {
            Success = true,
            Message = "Cart updated successfully",
            Data = _mapper.Map<UpdateCartResponse>(response)
        });
    }
}
