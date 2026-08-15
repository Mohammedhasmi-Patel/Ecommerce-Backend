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
            SellPrice = product.SellPrice
        };
    }
}
