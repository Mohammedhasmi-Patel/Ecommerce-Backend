using System;
using Ecommerce.Application.DTOs.UserAddresses;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Mappers;

public static class UserAddressMapper
{
    public static UserAddresses ToUserAddress(this UserAddressRequestDTO request, Guid userId)
    {
        return new UserAddresses
        {
            UserId = userId,
            AddressType = request.AddressType,
            FullName = request.FullName,
            PhoneNumber = request.PhoneNumber,
            AddressLine1 = request.AddressLine1,
            AddressLine2 = request.AddressLine2 ?? string.Empty,
            CountryId = request.CountryId,
            StateId = request.StateId,
            CityId = request.CityId,
            PostalCode = request.PostalCode,
            IsDefault = request.IsDefault,
            CreatedAt = DateTime.UtcNow
        };
    }

    public static UserAddresses UpdateUserAddress(this UserAddresses address, UserAddressRequestDTO request)
    {
        address.AddressType = request.AddressType;
        address.FullName = request.FullName;
        address.PhoneNumber = request.PhoneNumber;
        address.AddressLine1 = request.AddressLine1;
        address.AddressLine2 = request.AddressLine2 ?? string.Empty;
        address.CountryId = request.CountryId;
        address.StateId = request.StateId;
        address.CityId = request.CityId;
        address.PostalCode = request.PostalCode;
        address.IsDefault = request.IsDefault;
        address.UpdatedAt = DateTime.UtcNow;

        return address;
    }

    public static UserAddressResponseDTO ToUserAddressResponseDTO(this UserAddresses address)
    {
        return new UserAddressResponseDTO
        {
            Id = address.Id,
            UserId = address.UserId,
            AddressType = address.AddressType,
            FullName = address.FullName,
            PhoneNumber = address.PhoneNumber,
            AddressLine1 = address.AddressLine1,
            AddressLine2 = address.AddressLine2,
            CountryId = address.CountryId,
            CountryName = address.Country?.Name ?? string.Empty,
            StateId = address.StateId,
            StateName = address.State?.Name ?? string.Empty,
            CityId = address.CityId,
            CityName = address.City?.Name ?? string.Empty,
            PostalCode = address.PostalCode,
            IsDefault = address.IsDefault,
            CreatedAt = address.CreatedAt,
            UpdatedAt = address.UpdatedAt
        };
    }
}
