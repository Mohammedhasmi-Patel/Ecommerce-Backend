namespace Ecommerce.API.DTO;

public class ApiResponse<T> where T : class
{
    public bool Success { get; set; } = false;
    public string Message { get; set; } = null!;
    public int StatusCode { get; set; }
    public T? Data { get; set; }

    public static ApiResponse<T> SuccessResponse(
        T data,
        string message,
        int statusCode = 200)
    {
        return new ApiResponse<T>
        {
            Success = true,
            Message = message,
            StatusCode = statusCode,
            Data = data
        };
    }

    public static ApiResponse<T> ErrorResponse(
        string message,
        int statusCode = 400)
    {
        return new ApiResponse<T>
        {
            Success = false,
            Message = message,
            StatusCode = statusCode,
            Data = null
        };
    }
}