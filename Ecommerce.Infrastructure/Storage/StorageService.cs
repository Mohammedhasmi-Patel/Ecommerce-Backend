using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Ecommerce.Application.DTOs.Common.Storage;
using Ecommerce.Application.Interfaces;

namespace Ecommerce.Infrastructure.Storage;

public class StorageService : IStorageService
{
    private readonly IWebHostEnvironment _environment;

    public StorageService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    public Task<bool> DeleteFileAsync(string filePath, CancellationToken cancellationToken = default)
    {
        // throw new NotImplementedException();
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }


    public async Task<FileStorageResponse> UploadFileAsync(IFormFile file, string uploadFolder, CancellationToken cancellationToken)
    {
        FileStorageResponse fileStorageResponse = new FileStorageResponse();
        if (file == null || file.Length == 0)
        {
            fileStorageResponse.Success = false;
            fileStorageResponse.Message = "File is required";
            return fileStorageResponse; // Fix: Use FromResult        }
        }
        string folderPath = Path.Combine(_environment.WebRootPath, "uploads", uploadFolder);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string extension = Path.GetExtension(file.FileName);
        var storedFileName = $"{Guid.NewGuid():N}{extension}";
        string fileStorePath = Path.Combine(folderPath, storedFileName);

        await using var stream = new FileStream(fileStorePath, FileMode.Create);
        await file.CopyToAsync(stream, cancellationToken);
        var filePath = $"/uploads/{uploadFolder}/{storedFileName}";
        return new FileStorageResponse()
        {
            Success = true,
            Message = "File uploaded successfully.",
            OriginalFileName = file.FileName,
            StoredFileName = storedFileName,
            FilePath = filePath,
            ContentType = file.ContentType,
            FileSize = file.Length,
            Extension = extension
        };
    }
}
