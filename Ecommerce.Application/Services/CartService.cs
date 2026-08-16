using Ecommerce.Application.DTOs;
using Ecommerce.Application.DTOs.Carts;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Exceptions;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Mappers;
using Ecommerce.Application.RepoContracts;

namespace Ecommerce.Application.Services;

public class CartService : ICartService
{
    private readonly IAppUserRepository _appUserRepo;
    private readonly IProductRepository _productRepo;
    private readonly ICartRepository _cartRepo;
    private readonly IUnitOfWork _unitOfWork;

    public CartService(IAppUserRepository appUserRepo, IProductRepository productRepo, ICartRepository cartRepo, IUnitOfWork unitOfWork)
    {
        _appUserRepo = appUserRepo;
        _productRepo = productRepo;
        _cartRepo = cartRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse<UserCartItemResponseDTO>> AddCartItemAsync(string email, AddToCartRequestDTO cartRequest)
    {
        // 1. Validation Guards
        var user = await _appUserRepo.GetAppUserByEmailAsync(email) ?? throw new UnauthorizedException("Invalid Token");
        if (cartRequest.Quantity < 1)
        {
            throw new BadRequestException("Quantity must be greater than 0");
        }

        var product = await _productRepo.GetByIdAsync(cartRequest.ProductId) ?? throw new NotFoundException("Product not found");

        var cartItem = await GetOrCreateCartItemAsync(user.Id, cartRequest);
        await _unitOfWork.SaveChangesAsync();
        var response = MapToResponse(cartItem, product);
        return ApiResponse<UserCartItemResponseDTO>.SuccessResponse(response, "Cart item added successfully");
    }

    private async Task<CartItem> GetOrCreateCartItemAsync(Guid userId, AddToCartRequestDTO request)
    {
        var cart = await _cartRepo.GetCartByUserId(userId);
        if (cart == null)
        {
            cart = request.ToCart(userId);
            await _cartRepo.AddCartAsync(cart);
            return cart.CartItems.First();
        }

        var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == request.ProductId);
        if (cartItem == null)
        {
            cartItem = new CartItem
            {
                CartId = cart.Id,
                ProductId = request.ProductId,
                Quantity = request.Quantity
            };
            return await _cartRepo.AddToCartItemAsync(cartItem);
        }

        cartItem.Quantity += request.Quantity;
        return await _cartRepo.UpdateCartItemAsync(cartItem);
    }

    private static UserCartItemResponseDTO MapToResponse(CartItem cartItem, Product product)
    {
        return new UserCartItemResponseDTO
        {
            CartItemId = cartItem.Id,
            ProductId = product.Id,
            ProductName = product.Name,
            ProductImage = product.ProductImages.FirstOrDefault(p => p.IsPrimary == true)?.FilePath ?? "",
            Quantity = (int)cartItem.Quantity,
            Price = product.SellPrice
        };
    }

    public async Task<ApiResponse<List<UserCartItemResponseDTO>>> GetCartByUser(string email)
    {
        var user = await _appUserRepo.GetAppUserByEmailAsync(email) ?? throw new UnauthorizedException("Invalid Token");
        var cart = await _cartRepo.GetCartByUserId(user.Id);
        if (cart == null)
        {
            return ApiResponse<List<UserCartItemResponseDTO>>.SuccessResponse(new List<UserCartItemResponseDTO>(), "Cart is empty");
        }

        var response = cart.CartItems.Select(ci => MapToResponse(ci, ci.Product)).ToList();
        return ApiResponse<List<UserCartItemResponseDTO>>.SuccessResponse(response, "Cart fetched successfully");
    }

    public async Task<ApiResponse<object>> DeleteCartItemAsync(string email, Guid cartItemId)
    {
        // throw new NotImplementedException();
        var user = await _appUserRepo.GetAppUserByEmailAsync(email) ?? throw new UnauthorizedException("Invalid Token");
        var cart = await _cartRepo.GetCartByUserId(user.Id);
        if (cart == null)
        {
            return ApiResponse<object>.ErrorResponse("Cart not found");
        }

        var cartItem = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);
        if (cartItem == null)
        {
            return ApiResponse<object>.ErrorResponse("Cart item not found");
        }

        await _cartRepo.DeleteCartItemAsync(cartItemId);
        await _unitOfWork.SaveChangesAsync();
        return ApiResponse<object>.SuccessResponse(null, "Cart item deleted successfully");
    }
}

