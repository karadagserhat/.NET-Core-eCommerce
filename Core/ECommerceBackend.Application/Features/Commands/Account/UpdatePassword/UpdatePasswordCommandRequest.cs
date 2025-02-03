using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Account.UpdatePassword;

public class UpdatePasswordCommandRequest : IRequest<UpdatePasswordCommandResponse>
{
    public required string UserId { get; set; }
    public required string ResetToken { get; set; }
    public required string Password { get; set; }
    public required string PasswordConfirm { get; set; }
}