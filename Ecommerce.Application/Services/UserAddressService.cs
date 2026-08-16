
using Ecommerce.Application.DTOs;
using Ecommerce.Application.DTOs.UserAddresses;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Mappers;
using Ecommerce.Application.RepoContracts;
using Ecommerce.Domain.Exceptions;

namespace Ecommerce.Application.Services;

public class UserAddressService : IUserAddressService
{
    private readonly IUserAddressesRepository _userAddressesRepo;
    private readonly IAppUserRepository _appUserRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserAddressService(
        IUserAddressesRepository userAddressesRepo,
        IAppUserRepository appUserRepository,
        IUnitOfWork unitOfWork)
    {
        _userAddressesRepo = userAddressesRepo;
        _appUserRepository = appUserRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse<UserAddressResponseDTO>> CreateAddressAsync(
        string email,
        UserAddressRequestDTO request,
        CancellationToken cancellationToken = default)
    {
        var user = await _appUserRepository.GetAppUserByEmailAsync(email) ?? throw new UnauthorizedException("Invalid Token");
        var isLocationValid = await _userAddressesRepo.ValidateLocationAsync(request.CountryId, request.StateId, request.CityId, cancellationToken);
        if (!isLocationValid) throw new BadRequestException("Invalid location hierarchy details.");

        await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var existingAddresses = await _userAddressesRepo.GetByUserIdAsync(user.Id, cancellationToken);

            var userAddress = request.ToUserAddress(user.Id);

            if (!existingAddresses.Any())
            {
                userAddress.IsDefault = true;
            }
            else if (userAddress.IsDefault)
            {
                foreach (var address in existingAddresses.Where(a => a.IsDefault))
                {
                    address.IsDefault = false;
                    address.UpdatedAt = DateTime.UtcNow;
                    _userAddressesRepo.Update(address);
                }
            }

            await _userAddressesRepo.AddAsync(userAddress, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            var fetched = await _userAddressesRepo.GetByIdAsync(userAddress.Id, cancellationToken);
            return ApiResponse<UserAddressResponseDTO>.SuccessResponse(fetched!.ToUserAddressResponseDTO(), "Address created successfully.", 201);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<ApiResponse<List<UserAddressResponseDTO>>> GetAddressesByUserAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        var user = await _appUserRepository.GetAppUserByEmailAsync(email);
        if (user == null)
        {
            throw new UnauthorizedException("Invalid Token");
        }

        var addresses = await _userAddressesRepo.GetByUserIdAsync(user.Id, cancellationToken);
        var response = addresses.Select(a => a.ToUserAddressResponseDTO()).ToList();

        return ApiResponse<List<UserAddressResponseDTO>>.SuccessResponse(
            response,
            "Addresses retrieved successfully.");
    }

    public async Task<ApiResponse<UserAddressResponseDTO>> GetAddressByIdAsync(
        string email,
        Guid addressId,
        CancellationToken cancellationToken = default)
    {
        var user = await _appUserRepository.GetAppUserByEmailAsync(email);
        if (user == null)
        {
            throw new UnauthorizedException("Invalid Token");
        }

        var address = await _userAddressesRepo.GetByIdAsync(addressId, cancellationToken);
        if (address == null || address.UserId != user.Id)
        {
            throw new NotFoundException("Address not found.");
        }

        return ApiResponse<UserAddressResponseDTO>.SuccessResponse(
            address.ToUserAddressResponseDTO(),
            "Address retrieved successfully.");
    }

    public async Task<ApiResponse<UserAddressResponseDTO>> UpdateAddressAsync(
        string email,
        Guid addressId,
        UserAddressRequestDTO request,
        CancellationToken cancellationToken = default)
    {
        var user = await _appUserRepository.GetAppUserByEmailAsync(email);
        if (user == null)
        {
            throw new UnauthorizedException("Invalid Token");
        }

        var address = await _userAddressesRepo.GetByIdAsync(addressId, cancellationToken);
        if (address == null || address.UserId != user.Id)
        {
            throw new NotFoundException("Address not found.");
        }

        var isLocationValid = await _userAddressesRepo.ValidateLocationAsync(
            request.CountryId,
            request.StateId,
            request.CityId,
            cancellationToken);

        if (!isLocationValid)
        {
            throw new BadRequestException("Invalid location hierarchy details.");
        }

        await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var existingAddresses = await _userAddressesRepo.GetByUserIdAsync(user.Id, cancellationToken);

            address.UpdateUserAddress(request);

            if (address.IsDefault)
            {
                // Unset all other defaults
                foreach (var otherAddress in existingAddresses.Where(a => a.Id != address.Id && a.IsDefault))
                {
                    otherAddress.IsDefault = false;
                    otherAddress.UpdatedAt = DateTime.UtcNow;
                    _userAddressesRepo.Update(otherAddress);
                }
            }
            else
            {
                // If they are unsetting the default address, but it's the only one, make it default again
                if (existingAddresses.Count == 1)
                {
                    address.IsDefault = true;
                }
            }

            _userAddressesRepo.Update(address);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            var fetched = await _userAddressesRepo.GetByIdAsync(address.Id, cancellationToken);
            return ApiResponse<UserAddressResponseDTO>.SuccessResponse(
                fetched!.ToUserAddressResponseDTO(),
                "Address updated successfully.");
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<ApiResponse<object>> DeleteAddressAsync(
        string email,
        Guid addressId,
        CancellationToken cancellationToken = default)
    {
        var user = await _appUserRepository.GetAppUserByEmailAsync(email);
        if (user == null)
        {
            throw new UnauthorizedException("Invalid Token");
        }

        var address = await _userAddressesRepo.GetByIdAsync(addressId, cancellationToken);
        if (address == null || address.UserId != user.Id)
        {
            throw new NotFoundException("Address not found.");
        }

        await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            if (address.IsDefault)
            {
                var remaining = (await _userAddressesRepo.GetByUserIdAsync(user.Id, cancellationToken))
                    .Where(a => a.Id != address.Id)
                    .ToList();

                if (remaining.Any())
                {
                    var newDefault = remaining.First();
                    newDefault.IsDefault = true;
                    newDefault.UpdatedAt = DateTime.UtcNow;
                    _userAddressesRepo.Update(newDefault);
                }
            }

            _userAddressesRepo.Delete(address);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return ApiResponse<object>.SuccessResponse(null!, "Address deleted successfully.");
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
