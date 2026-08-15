using Ecommerce.API.Database;
using Ecommerce.API.DTO;
using Ecommerce.API.DTO.Auth;
using Ecommerce.API.Exceptions;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Mapster;
using Ecommerce.API.RepoContracts;
using Microsoft.Extensions.Configuration;

namespace Ecommerce.API.Services;

public class AuthService : IAuthService
{
    private readonly IStorageService _storageService;
    private readonly IAppUserRepository _appUserRepository;
    private readonly ITokenService _tokenService;
    private readonly AppDbContext _dbContext;
    private readonly string _baseUrl;


    public AuthService(
        IStorageService storageService,
        IAppUserRepository appUserRepository,
        ITokenService tokenService,
        AppDbContext dbContext,
        IConfiguration configuration)
    {
        _storageService = storageService;
        _appUserRepository = appUserRepository;
        _tokenService = tokenService;
        _dbContext = dbContext;
        _baseUrl = configuration["BackendUrl"] ?? string.Empty;
    }

    public async Task<ApiResponse<LoginUserResponseDTO>> LoginUserAsync(LoginUserRequestDTO request, CancellationToken cancellationToken)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
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

        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
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
}
