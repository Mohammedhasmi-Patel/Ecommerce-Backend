using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Domain.Enums;

namespace Ecommerce.Domain.Entities;

public class Payment : BaseEntity
{
    public Guid OrderId { get; set; }
    public string Provider { get; set; } = null!;
    public string PaymentMethod { get; set; } = null!;
    public string ProviderPaymentId { get; set; } = null!;

    [Column(TypeName = "decimal(10,2)")]
    public decimal Amount { get; set; }

    public string Currency { get; set; } = null!;

    public PaymentStatus Status { get; set; }

    public DateTime PaidAt { get; set; }

    // navigation property
    public Order Order { get; set; } = null!;

}

