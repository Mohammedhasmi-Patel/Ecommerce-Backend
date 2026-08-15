using System.Text.Json;
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
            ApiResponse<object> res;
            if (ex.StatusCode is StatusCodes.Status500InternalServerError)
            {
                res = new ApiResponse<object>()
                {
                    Success = false,
                    StatusCode = ex.StatusCode,
                    Message = "Something went wrong.",
                    Data = null
                };
            }
            else
            {
                res = new ApiResponse<object>()
                {
                    Success = false,
                    StatusCode = ex.StatusCode,
                    Message = ex.Message,
                    Data = null
                };
            }

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            string json = JsonSerializer.Serialize(res, options);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = ex.StatusCode;
            await context.Response.WriteAsync(json);
        }
    }

}
