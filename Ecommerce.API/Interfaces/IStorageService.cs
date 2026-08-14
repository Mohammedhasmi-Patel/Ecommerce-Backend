using Ecommerce.API.DTO.Common.Storage;

namespace Ecommerce.API.Interfaces;

public interface IStorageService
{
    public Task<FileStorageResponse> UploadFileAsync(IFormFile file, string uploadFolder, CancellationToken cancellationToken);
    public Task<bool> DeleteFileAsync(string filePath, CancellationToken cancellationToken = default);
}
