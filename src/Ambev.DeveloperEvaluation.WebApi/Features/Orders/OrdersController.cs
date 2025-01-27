using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Orders.CreateOrder;
using Ambev.DeveloperEvaluation.Application.Orders.CreateOrder;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Orders;

/// <summary>
/// Controller for managing user operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OrdersController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of ProductsController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public OrdersController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new user
    /// </summary>
    /// <param name="request">The user creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateOrderResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateOrderRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateOrderCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateOrderResponse>
        {
            Success = true,
            Message = "Order created successfully",
            Data = _mapper.Map<CreateOrderResponse>(response)
        });
    }

    /// <summary>
    /// Retrieves a list of products
    /// </summary>
    /// <param name="request">Filters of products</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The products list</returns>
    //[HttpGet]
    //[ProducesResponseType(typeof(PaginatedList<GetProductsResponse>), StatusCodes.Status200OK)]
    //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    //public async Task<IActionResult> GetProducts([FromQuery] GetProductsRequest request, CancellationToken cancellationToken)
    //{
    //    //var request = new GetUserRequest { Id = id };
    //    var validator = new GetProductsRequestValidator();
    //    var validationResult = await validator.ValidateAsync(request, cancellationToken);

    //    if (!validationResult.IsValid)
    //        return BadRequest(validationResult.Errors);

    //    var command = _mapper.Map<GetProductsCommand>(request);
    //    var response = await _mediator.Send(command, cancellationToken);

    //    var paginatedList = new PaginatedList<GetProductsResponse>(_mapper.Map<IEnumerable<GetProductsResponse>>(response.Item1).ToList(), 
    //                                                    response.Item2,
    //                                                    request._page,
    //                                                    request._size);

    //    return OkPaginated(paginatedList);
    //}
}
