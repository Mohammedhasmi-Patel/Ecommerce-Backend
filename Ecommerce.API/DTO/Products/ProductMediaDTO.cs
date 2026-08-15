namespace Ecommerce.API.DTO.Products;

public class ProductMediaDTO
{
    public Guid Id { get; set; }
    public string Url { get; set; } = null!;
    public bool IsThumbnail { get; set; }

    public int? SortOrder { get; set; }
}
