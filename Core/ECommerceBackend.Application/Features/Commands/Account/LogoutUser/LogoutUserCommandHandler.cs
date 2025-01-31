using ECommerceBackend.Application.Abstractions.Services;
using MediatR;

namespace ECommerceBackend.Application.Features.Commands.Account.LogoutUser;

public class LogoutUserCommandHandler(IAccountService accountService) : IRequestHandler<LogoutUserCommandRequest, LogoutUserCommandResponse>
{
    readonly IAccountService _accountService = accountService;

    public async Task<LogoutUserCommandResponse> Handle(LogoutUserCommandRequest request, CancellationToken cancellationToken)
    {

        await _accountService.LogoutUserAsync();

        return new();
    }
}