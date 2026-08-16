using Ecommerce.Application.DTOs;
using Ecommerce.Application.DTOs.Wishlists;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Exceptions;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Mappers;
using Ecommerce.Application.RepoContracts;

namespace Ecommerce.Application.Services;

public class WishlistService : IWishlistService
{
    private readonly IAppUserRepository _appUserRepo;
    private readonly IProductRepository _productRepo;
    private readonly IWishlistRepository _wishlistRepo;
    private readonly IUnitOfWork _unitOfWork;

    public WishlistService(IAppUserRepository appUserRepo, IProductRepository productRepo, IWishlistRepository wishlistRepo, IUnitOfWork unitOfWork)
    {
        _appUserRepo = appUserRepo;
        _productRepo = productRepo;
        _wishlistRepo = wishlistRepo;
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse<WishlisttemResponseDTO>> AddWishlistItemAsync(string email, AddToWishListRequestDTO wishlistRequest)
    {
        // 1. Validation Guards
        var user = await _appUserRepo.GetAppUserByEmailAsync(email) ?? throw new UnauthorizedException("Invalid Token");
        var product = await _productRepo.GetByIdAsync(wishlistRequest.ProductId) ?? throw new NotFoundException("Product not found");

        await using var transaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
            var wishlistItem = await GetOrCreateWishlistItemAsync(user.Id, wishlistRequest);
            await _unitOfWork.SaveChangesAsync();
            var response = MapToResponse(wishlistItem, product);
            await transaction.CommitAsync();
            return ApiResponse<WishlisttemResponseDTO>.SuccessResponse(response, "Wishlist item added successfully");
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private async Task<WishlistItem> GetOrCreateWishlistItemAsync(Guid userId, AddToWishListRequestDTO request)
    {
        var wishlist = await _wishlistRepo.GetWishlistByUserId(userId);
        if (wishlist == null)
        {
            wishlist = request.ToWishlist(userId);
            await _wishlistRepo.AddWishlistAsync(wishlist);
            return wishlist.WishlistItems.First();
        }

        var wishlistItem = wishlist.WishlistItems.FirstOrDefault(wi => wi.ProductId == request.ProductId);
        if (wishlistItem == null)
        {
            wishlistItem = new WishlistItem
            {
                WishlistId = wishlist.Id,
                ProductId = request.ProductId
            };
            return await _wishlistRepo.AddWishlistItemAsync(wishlistItem);
        }

        return wishlistItem;
    }

    private static WishlisttemResponseDTO MapToResponse(WishlistItem wishlistItem, Product product)
    {
        return new WishlisttemResponseDTO
        {
            WishlistItemId = wishlistItem.Id,
            ProductId = product.Id,
            ProductName = product.Name,
            ProductImage = product.ProductImages.FirstOrDefault(p => p.IsPrimary)?.FilePath ?? "",
            Price = product.SellPrice
        };
    }

    public async Task<ApiResponse<List<WishlisttemResponseDTO>>> GetWishlistByUserAsync(string email)
    {
        var user = await _appUserRepo.GetAppUserByEmailAsync(email) ?? throw new UnauthorizedException("Invalid Token");
        var wishlist = await _wishlistRepo.GetWishlistByUserId(user.Id);
        if (wishlist == null)
        {
            return ApiResponse<List<WishlisttemResponseDTO>>.SuccessResponse(new List<WishlisttemResponseDTO>(), "Wishlist is empty");
        }

        var response = wishlist.WishlistItems.Select(wi => MapToResponse(wi, wi.Product)).ToList();
        return ApiResponse<List<WishlisttemResponseDTO>>.SuccessResponse(response, "Wishlist fetched successfully");
    }

    public async Task<ApiResponse<object>> DeleteWishlistItemAsync(string email, Guid wishlistItemId)
    {
        var user = await _appUserRepo.GetAppUserByEmailAsync(email) ?? throw new UnauthorizedException("Invalid Token");
        var wishlist = await _wishlistRepo.GetWishlistByUserId(user.Id);
        if (wishlist == null)
        {
            return ApiResponse<object>.ErrorResponse("Wishlist not found");
        }

        var wishlistItem = wishlist.WishlistItems.FirstOrDefault(wi => wi.Id == wishlistItemId);
        if (wishlistItem == null)
        {
            return ApiResponse<object>.ErrorResponse("Wishlist item not found");
        }

        await using var transaction = await _unitOfWork.BeginTransactionAsync();
        try
        {
            await _wishlistRepo.DeleteWishlistItemAsync(wishlistItemId);
            await _unitOfWork.SaveChangesAsync();
            await transaction.CommitAsync();
            return ApiResponse<object>.SuccessResponse(null, "Wishlist item deleted successfully");
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}
