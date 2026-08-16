using System;

namespace Ecommerce.Application.DTOs.UserAddresses;

public class UserAddressResponseDTO
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string AddressType { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string AddressLine1 { get; set; } = null!;
    public string AddressLine2 { get; set; } = null!;
    public Guid CountryId { get; set; }
    public string CountryName { get; set; } = null!;
    public Guid StateId { get; set; }
    public string StateName { get; set; } = null!;
    public Guid CityId { get; set; }
    public string CityName { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
    public bool IsDefault { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
