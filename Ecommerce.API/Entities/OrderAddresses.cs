namespace Ecommerce.API.Entities;

public class OrderAddresses : BaseEntity
{
    public Guid OrderId { get; set; }
    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string AddressLine1 { get; set; } = null!;
    public string AddressLine2 { get; set; } = null!;
    public string CountryName { get; set; } = null!;
    public string StateName { get; set; } = null!;
    public string CityName { get; set; } = null!;
    public string PostalCode { get; set; } = null!;
}
