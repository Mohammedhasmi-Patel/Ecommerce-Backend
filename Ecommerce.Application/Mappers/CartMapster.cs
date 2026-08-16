using Ecommerce.Application.DTOs.Carts;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Application.Mappers;

public static class CartMapster
{
    public static Cart ToCart(this AddToCartRequestDTO request, Guid userId)
    {
        return new Cart
        {
            UserId = userId,
            CartItems = new List<CartItem>
            {
                new CartItem
                {
                    ProductId = request.ProductId,
                    Quantity = request.Quantity
                }
            }
        };
    }
}

