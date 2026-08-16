namespace Ecommerce.Application.DTOs.Common.Pagination;

public class PaginationResponseDTO<T>
{
    public List<T>? Items { get; set; }
    public int TotalPages { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public bool HasNext => PageNumber < TotalPages;
    public bool HasPrevious => PageNumber > 1;

    
}

