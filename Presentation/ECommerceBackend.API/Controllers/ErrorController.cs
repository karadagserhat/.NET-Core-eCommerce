using System.Security.Claims;
using ECommerceBackend.Application.DTOs;
using ECommerceBackend.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceBackend.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ErrorController : ControllerBase
{
  [HttpGet("unauthorized")]
  public IActionResult GetUnauthorized()
  {
    return Unauthorized();
  }

  [HttpGet("badrequest")]
  public IActionResult GetBadRequest()
  {
    return BadRequest("Not a good request");
  }

  [HttpGet("notfound")]
  public IActionResult GetNotFound()
  {
    return NotFound();
  }

  [HttpGet("internalerror")]
  public IActionResult GetInternalError()
  {
    throw new Exception("This is a test exception");
  }

  // [HttpPost("validationerror")]
  // public IActionResult GetValidationError(CreateProductDto product)
  // {
  //   return Ok();
  // }

  [Authorize]
  [HttpGet("secret")]
  public IActionResult GetSecret()
  {
    var name = User.FindFirst(ClaimTypes.Name)?.Value;
    var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    return Ok("Hello " + name + " with the id of " + id);
  }

  [Authorize(Roles = "Admin")]
  [HttpGet("admin-secret")]
  public IActionResult GetAdminSecret()
  {
    var name = User.FindFirst(ClaimTypes.Name)?.Value;
    var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var isAdmin = User.IsInRole("Admin");
    var roles = User.FindFirstValue(ClaimTypes.Role);
    return Ok(new
    {
      name,
      id,
      isAdmin,
      roles
    });
  }
}