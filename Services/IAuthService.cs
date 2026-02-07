using ProjectManagementAPI.DTOs;
using ProjectManagementAPI.Models;

namespace ProjectManagementAPI.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> Login(LoginDto loginDto);
        Task<AuthResponseDto> Register(RegisterDto registerDto);
        string GenerateJwtToken(User user);
    }
}
