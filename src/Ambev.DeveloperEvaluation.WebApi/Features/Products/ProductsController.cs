using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProducts;
using Ambev.DeveloperEvaluation.Application.Products.GetProducts;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products;

/// <summary>
/// Controller for managing products operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class ProductsController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of ProductsController
    /// </summary>
    /// <param name="mediator">The mediator instance</param>
    /// <param name="mapper">The AutoMapper instance</param>
    public ProductsController(IMediator mediator, IMapper mapper)
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
    [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateProductCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<CreateProductResponse>
        {
            Success = true,
            Message = "Product created successfully",
            Data = _mapper.Map<CreateProductResponse>(response)
        });
    }

    /// <summary>
    /// Retrieves a list of products
    /// </summary>
    /// <param name="request">Filters of products</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The products list</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedList<GetProductsResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProducts([FromQuery] GetProductsRequest request, CancellationToken cancellationToken)
    {
        //var request = new GetUserRequest { Id = id };
        var validator = new GetProductsRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetProductsCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);

        var paginatedList = new PaginatedList<GetProductsResponse>(_mapper.Map<IEnumerable<GetProductsResponse>>(response.Item1).ToList(), 
                                                        response.Item2,
                                                        request._page,
                                                        request._size);

        return OkPaginated(paginatedList);
    }

    /// <summary>
    /// Retrieves a product by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the product</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The product details if found</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProduct([FromRoute] int id, CancellationToken cancellationToken)
    {
        var request = new GetProductRequest { Id = id };
        var validator = new GetProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<GetProductCommand>(request.Id);
        var response = await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponseWithData<GetProductResponse>
        {
            Success = true,
            Message = "Product retrieved successfully",
            Data = _mapper.Map<GetProductResponse>(response)
        });
    }

    /// <summary>
    /// Deletes a product by their ID
    /// </summary>
    /// <param name="id">The unique identifier of the product to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Success response if the product was deleted</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProduct([FromRoute] int id, CancellationToken cancellationToken)
    {
        var request = new DeleteProductRequest { Id = id };
        var validator = new DeleteProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<DeleteProductCommand>(request.Id);
        await _mediator.Send(command, cancellationToken);

        return Ok(new ApiResponse
        {
            Success = true,
            Message = "Product deleted successfully"
        });
    }

    /// <summary>
    /// Update a product
    /// </summary>
    /// <param name="request">The product update request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated product details</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateProductResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var requestId = new UpdateProductIdRequest() { Id = id };
        var validatorId = new UpdateProductIdRequestValidator();
        var validationIdResult = await validatorId.ValidateAsync(requestId, cancellationToken);

        if (!validationIdResult.IsValid)
            return BadRequest(validationIdResult.Errors);

        var validator = new UpdateProductRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<UpdateProductCommand>(request);
        command.SetId(id);
        var response = await _mediator.Send(command, cancellationToken);

        return Created(string.Empty, new ApiResponseWithData<UpdateProductResponse>
        {
            Success = true,
            Message = "Product updated successfully",
            Data = _mapper.Map<UpdateProductResponse>(response)
        });
    }
}
