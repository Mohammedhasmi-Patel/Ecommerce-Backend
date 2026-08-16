namespace Ecommerce.Domain.Entities;
public class UserAddresses : BaseEntity
{
    public Guid UserId { get; set; }
    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string AddressLine1 { get; set; } = null!;
    public string AddressLine2 { get; set; } = null!;
    public Guid CountryId { get; set; }
    public Guid StateId { get; set; }
    public Guid CityId { get; set; }
    public string PostalCode { get; set; } = null!;
    public bool IsDefault { get; set; } = false;

    public  AppUser? User { get; set; }  
    public  Country? Country { get; set; }  
    public  State? State { get; set; }  
    public  City? City { get; set; }
}

