using ECommerceBackend.Application.DTOs;

namespace ECommerceBackend.Application.Abstractions.Services;

public interface IAccountService
{
    Task<RegisterUserResponseDTO> RegisterUserAsync(RegisterDTO registerDto);
    Task LogoutUserAsync();
    Task<GetUserInfoDTO> GetUserInfoAsync();
    GetAuthStateDTO GetAuthState();
    Task<AddressDto> CreateOrUpdateAddress(AddressDto addressDto);

    Task PasswordResetAsync(string email);
    Task<bool> VerifyResetTokenAsync(string resetToken, string userId);
    Task UpdatePasswordAsync(string userId, string resetToken, string newPassword);
}