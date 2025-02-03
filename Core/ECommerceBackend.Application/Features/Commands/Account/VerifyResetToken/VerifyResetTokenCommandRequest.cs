using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Account.VerifyResetToken;

public class VerifyResetTokenCommandRequest : IRequest<VerifyResetTokenCommandResponse>
{
    public required string ResetToken { get; set; }
    public required string UserId { get; set; }
}