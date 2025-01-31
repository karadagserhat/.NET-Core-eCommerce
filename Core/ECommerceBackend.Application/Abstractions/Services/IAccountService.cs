using ECommerceBackend.Application.DTOs;

namespace ECommerceBackend.Application.Abstractions.Services;

public interface IAccountService
{
    Task<RegisterUserResponseDTO> RegisterUserAsync(RegisterDTO registerDto);
    Task LogoutUserAsync();
    Task<GetUserInfoDTO> GetUserInfoAsync();
    GetAuthStateDTO GetAuthState();
    Task<AddressDto> CreateOrUpdateAddress(AddressDto addressDto);
}