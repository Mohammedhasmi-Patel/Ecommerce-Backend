namespace Ecommerce.Domain.Entities;


public class City : BaseEntity
{
    public Guid StateId { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public bool IsActive { get; set; } = true;

    public virtual State? State { get; set; }
}

