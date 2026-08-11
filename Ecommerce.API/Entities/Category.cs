namespace Ecommerce.API.Entities;


public class Category : BaseEntity
{
    public string Name { get; set; } = null!;  
    public string Slug { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int SortOrder { get; set; } = 0;
    public bool IsFeatured { get; set; } = false;
    public Guid? ParentId { get; set; }  // The categories 

    // Navigation properties
    public virtual ICollection<Category>? SubCategories { get; set; } = new List<Category>();
}
