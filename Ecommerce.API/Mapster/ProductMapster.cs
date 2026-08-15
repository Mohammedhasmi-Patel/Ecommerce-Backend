using Ecommerce.API.DTO.Products;
using Ecommerce.API.Entities;

namespace Ecommerce.API.Mapster;

public static class ProductMapster
{
    public static ProductResponseDTO MapToProductResponseDTO(this Product product)
    {
        return new ProductResponseDTO
        {
            Id = product.Id,
            Name = product.Name,
            Slug = product.Slug,
            Description = product.Description,
            OriginalPrice = product.OriginalPrice,
            SellPrice = product.SellPrice,
            ThumbnailUrl = product.ProductImages.FirstOrDefault(p => p.IsPrimary)?.FilePath,
        };
    }

    public static ProductDetailResponseDTO MapToProductDetailResponseDTO(this Product product)
    {
        return new ProductDetailResponseDTO
        {
            Id = product.Id,
            Name = product.Name,
            Slug = product.Slug,
            Description = product.Description,
            OriginalPrice = product.OriginalPrice,
            SellPrice = product.SellPrice,
            Media = product.ProductImages.Select(p => new ProductMediaDTO
            {
                Id = p.Id,
                Url = p.FilePath,
                IsThumbnail = p.IsPrimary,
                SortOrder = p.SortOrder
            }).ToList()
        };
    }
}
