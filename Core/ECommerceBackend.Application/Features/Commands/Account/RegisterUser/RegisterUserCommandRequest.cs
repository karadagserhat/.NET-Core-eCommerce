using System.ComponentModel.DataAnnotations;
using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Account.RegisterUser;

public class RegisterUserCommandRequest : IRequest<RegisterUserCommandResponse>
{
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}