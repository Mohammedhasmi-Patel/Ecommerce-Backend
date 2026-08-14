namespace Ecommerce.API.DTO.Common.Storage;

public class FileStorageResponse
{
    public bool Success { get; set; } = false;
    public string Message {get;set;} = "Success";
    
    public string? OriginalFileName { get; set; }

    public string? StoredFileName { get; set; }

    public string? FilePath { get; set; }

    public string? ContentType { get; set; }

    public long FileSize { get; set; }

    public string? Extension { get; set; }

}
