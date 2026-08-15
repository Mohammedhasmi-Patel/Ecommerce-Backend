using Ecommerce.API.DTO.Carts;
using Ecommerce.API.Entities;

namespace Ecommerce.API.Mapster;

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
