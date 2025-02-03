using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Account.PasswordReset;

public class PasswordResetCommandRequest : IRequest<PasswordResetCommandResponse>
{
    public required string Email { get; set; }
}