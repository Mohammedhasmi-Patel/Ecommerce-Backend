namespace Ecommerce.API.Entities;
public class ProductImage : BaseEntity
{
    public Guid ProductId { get; set; }
    public string FileName { get; set; } = null!;
    public string FilePath { get; set; } = null!;
    public string FileExtension { get; set; } = null!;
    public long FileSize { get; set; }
    public string MimeType { get; set; } = null!;
    public int SortOrder { get; set; } = 0;
    public bool IsPrimary { get; set; } = false;

    public virtual Product? Product { get; set; } 
}
