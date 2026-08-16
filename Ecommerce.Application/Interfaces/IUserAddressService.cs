
using Ecommerce.Application.DTOs;
using Ecommerce.Application.DTOs.UserAddresses;

namespace Ecommerce.Application.Interfaces;

public interface IUserAddressService
{
    Task<ApiResponse<UserAddressResponseDTO>> CreateAddressAsync(string email, UserAddressRequestDTO request, CancellationToken cancellationToken = default);
    Task<ApiResponse<List<UserAddressResponseDTO>>> GetAddressesByUserAsync(string email, CancellationToken cancellationToken = default);
    Task<ApiResponse<UserAddressResponseDTO>> GetAddressByIdAsync(string email, Guid addressId, CancellationToken cancellationToken = default);
    Task<ApiResponse<UserAddressResponseDTO>> UpdateAddressAsync(string email, Guid addressId, UserAddressRequestDTO request, CancellationToken cancellationToken = default);
    Task<ApiResponse<object>> DeleteAddressAsync(string email, Guid addressId, CancellationToken cancellationToken = default);
}
