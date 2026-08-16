namespace Ecommerce.Domain.Entities;

public class Country : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string PhoneCode { get; set; } = null!;
    public string CurrencyCode { get; set; } = null!;
    public bool IsActive { get; set; } = true;
}

