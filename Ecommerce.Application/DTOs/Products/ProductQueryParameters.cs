namespace Ecommerce.Application.DTOs.Products;

public class ProductQueryParameters
{
    private const int MaxPageSize = 50;
    private int _pageSize = 10;

    public string? SearchQuery { get; set; }
    public int PageNumber { get; set; } = 1;

    public string? Category { get; set; } // slug
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }
}

