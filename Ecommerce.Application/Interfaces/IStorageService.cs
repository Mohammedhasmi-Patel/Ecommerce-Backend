using Microsoft.AspNetCore.Http;
using Ecommerce.Application.DTOs.Common.Storage;

namespace Ecommerce.Application.Interfaces;

public interface IStorageService
{
    public Task<FileStorageResponse> UploadFileAsync(IFormFile file, string uploadFolder, CancellationToken cancellationToken);
    public Task<bool> DeleteFileAsync(string filePath, CancellationToken cancellationToken = default);
}

