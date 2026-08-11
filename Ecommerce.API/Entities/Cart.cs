namespace Ecommerce.API.Entities;

public class Cart 
{
    public Guid Id {get;set;} = Guid.NewGuid();
    public Guid UserId {get;set;}
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt { get; set; }

    public AppUser User { get; set; } = null!;
    public ICollection<CartItem> CartItems { get; set; } = [];
}
