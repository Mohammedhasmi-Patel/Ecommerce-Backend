using Ecommerce.API.DTO;
using Ecommerce.API.Exceptions;

namespace Ecommerce.API.Middleware;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (AppException ex)
        {
            if (ex.StatusCode is StatusCodes.Status500InternalServerError)
            {
                ApiResponse<object> res = new ApiResponse<object>()
                {
                    Success = false,
                    StatusCode = ex.StatusCode,
                    Message = "Something went wrong.",
                    Data = null
                };
            }
        }
    }

}
