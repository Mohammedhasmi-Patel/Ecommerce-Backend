using Ecommerce.Application.DTOs;
using Ecommerce.Application.DTOs.Auth;
using Ecommerce.Application.Interfaces;
using Ecommerce.Application.Mappers;
using Ecommerce.Application.RepoContracts;
using Ecommerce.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.Application.Services;

public class AuthService : IAuthService
{
    private readonly IStorageService _storageService;
    private readonly IAppUserRepository _appUserRepository;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly string _baseUrl;


    public AuthService(
        IStorageService storageService,
        IAppUserRepository appUserRepository,
        ITokenService tokenService,
        IUnitOfWork unitOfWork,
        IConfiguration configuration)
    {
        _storageService = storageService;
        _appUserRepository = appUserRepository;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
        _baseUrl = configuration["BackendUrl"] ?? string.Empty;
    }

    public async Task<ApiResponse<LoginUserResponseDTO>> LoginUserAsync(LoginUserRequestDTO request, CancellationToken cancellationToken)
    {
        await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var foundUser = await _appUserRepository.GetAppUserByEmailAsync(request.Email) ?? throw new NotFoundException("User with this email not exist.");

            bool isPasswordValid = await _appUserRepository.VerifyPasswordAsync(foundUser, request.Password);
            if (!isPasswordValid)
            {
                throw new ConflictException("Invalid credentials.");
            }

            string token = await _tokenService.GenerateTokenAsync(foundUser);

            var authResponse = foundUser.ToLoginUserResponseDTO(token, _baseUrl);

            await transaction.CommitAsync(cancellationToken);
            return ApiResponse<LoginUserResponseDTO>.SuccessResponse(authResponse, "User logged in successfully.");
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }

    }


    public async Task<ApiResponse<RegisterUserResponseDTO>> RegisterUserAsync(RegisterUserRequestDTO request, CancellationToken cancellationToken)
    {
        IFormFile avatarFile = request.Avatar;

        string email = request.Email;
        bool isUserExist = await _appUserRepository.EmailExistsAsync(email);
        if (isUserExist)
        {
            throw new ConflictException("Email already taken");
        }

        var res = await _storageService.UploadFileAsync(avatarFile, "users", cancellationToken);
        if (res.Success == false)
        {
            throw new BadRequestException(res.Message);
        }

        var appUser = request.ToAppUserFromRegister(res.FilePath!);

        await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
        try
        {
            var createdUser = await _appUserRepository.CreateAppUser(appUser, request.Password);
            string token = await _tokenService.GenerateTokenAsync(appUser);

            var authResponse = createdUser.ToRegisterUserResponseDTO(token, _baseUrl);

            await transaction.CommitAsync(cancellationToken);
            return ApiResponse<RegisterUserResponseDTO>.SuccessResponse(authResponse, "User created successfully.");
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            if (!string.IsNullOrEmpty(res.FilePath))
            {
                await _storageService.DeleteFileAsync(res.FilePath, cancellationToken);
            }
            throw;
        }

    }

    public async Task<ApiResponse<UpdateUserResponseDTO>> UpdateUserAsync(UpdateUserRequestDTO request, CancellationToken cancellationToken)
    {
        var user = await _appUserRepository.GetAppUserByEmailAsync(request.Email)
            ?? throw new NotFoundException("User with this email does not exist.");

        await using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
        string? newAvatarPath = null;
        try
        {
            if (request.Avatar != null && request.Avatar.Length > 0)
            {
                if (!string.IsNullOrEmpty(user.Avatar))
                {
                    var res = await _storageService.DeleteFileAsync(user.Avatar, cancellationToken);
                    if (!res) throw new BadRequestException("Failed to delete old avatar");
                }

                var uploadedFile = await _storageService.UploadFileAsync(request.Avatar, "users", cancellationToken);
                if (!uploadedFile.Success)
                {
                    throw new BadRequestException("Failed to upload new avatar");
                }
                newAvatarPath = uploadedFile.FilePath;
            }

            user.ToAppUserFromUpdate(request, newAvatarPath);
            var updatedUser = await _appUserRepository.UpdateAppUser(user, request);
            var response = updatedUser.ToUpdateUserResponseDTO(_baseUrl);

            await transaction.CommitAsync(cancellationToken);
            return ApiResponse<UpdateUserResponseDTO>.SuccessResponse(response, "User updated successfully.");
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            if (!string.IsNullOrEmpty(newAvatarPath))
            {
                await _storageService.DeleteFileAsync(newAvatarPath, cancellationToken);
            }
            throw;
        }
    }
}
