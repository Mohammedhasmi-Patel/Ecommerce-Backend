using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Domain.Entities;

public class AppRole : IdentityRole<Guid>
{
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt {get;set;}

}

