namespace Ecommerce.API.Entities;


public class Coupon : BaseEntity
{
    public string Code { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string DiscountType { get; set; } = null!;
    public decimal DiscountValue { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsActive { get; set; }
}
