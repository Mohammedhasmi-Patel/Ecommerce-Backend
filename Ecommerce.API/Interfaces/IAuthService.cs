using Ecommerce.API.DTO;
using Ecommerce.API.DTO.Auth;

namespace Ecommerce.API.Interfaces;

public interface IAuthService
{
    public Task<ApiResponse<RegisterUserResponseDTO>> RegisterUserAsync(RegisterUserRequestDTO request,CancellationToken cancellationToken);
    public Task<ApiResponse<LoginUserResponseDTO>> LoginUserAsync(LoginUserRequestDTO request,CancellationToken cancellationToken);
}
