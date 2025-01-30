using ECommerceBackend.Application.Features.Commands.Product.CreateProduct;
using ECommerceBackend.Application.Features.Commands.Product.RemoveProduct;
using ECommerceBackend.Application.Features.Commands.Product.UpdateProduct;
using ECommerceBackend.Application.Features.Queries.Product.GetAllProducts;
using ECommerceBackend.Application.Features.Queries.Product.GetByIdProduct;
using ECommerceBackend.Application.Features.Queries.Product.GetProductsBrands;
using ECommerceBackend.Application.Features.Queries.Product.GetProductsTypes;
using ECommerceBackend.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackend.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IMediator mediator) : ControllerBase
{
  readonly IMediator _mediator = mediator;

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] GetAllProductQueryRequest getAllProductQueryRequest)
  {
    GetAllProductQueryResponse response = await _mediator.Send(getAllProductQueryRequest);
    return Ok(response);
  }

  [HttpGet("{id:int}")]
  public async Task<ActionResult<Product>> GetProduct([FromRoute] GetByIdProductQueryRequest getByIdProductQueryRequest)
  {
    GetByIdProductQueryResponse response = await _mediator.Send(getByIdProductQueryRequest);
    return Ok(response);

  }

  [HttpPost]
  public async Task<ActionResult<Product>> CreateProduct([FromBody] CreateProductCommandRequest createProductCommandRequest)
  {
    CreateProductCommandResponse response = await _mediator.Send(createProductCommandRequest);
    return Ok(response);
  }

  [HttpPut("{id:int}")]
  public async Task<ActionResult> UpdateProduct([FromBody] UpdateProductCommandRequest updateProductCommandRequest)
  {
    UpdateProductCommandResponse response = await _mediator.Send(updateProductCommandRequest);
    return Ok();
  }

  [HttpDelete("{id:int}")]
  public async Task<ActionResult> DeleteProduct([FromRoute] RemoveProductCommandRequest removeProductCommandRequest)
  {
    RemoveProductCommandResponse response = await _mediator.Send(removeProductCommandRequest);
    return Ok();
  }

  [HttpGet("brands")]
  public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
  {
    GetProductsBrandsQueryResponse response = await _mediator.Send(new GetProductsBrandsQueryRequest());
    return Ok(response);

  }

  [HttpGet("types")]
  public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
  {
    GetProductsTypesQueryResponse response = await _mediator.Send(new GetProductsTypesQueryRequest());
    return Ok(response);
  }
}

