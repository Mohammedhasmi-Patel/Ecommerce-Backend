using Ecommerce.Application.DTOs;
using Ecommerce.Application.DTOs.Auth;

namespace Ecommerce.Application.Interfaces;

public interface IAuthService
{
    public Task<ApiResponse<RegisterUserResponseDTO>> RegisterUserAsync(RegisterUserRequestDTO request,CancellationToken cancellationToken);
    public Task<ApiResponse<LoginUserResponseDTO>> LoginUserAsync(LoginUserRequestDTO request,CancellationToken cancellationToken);
}

