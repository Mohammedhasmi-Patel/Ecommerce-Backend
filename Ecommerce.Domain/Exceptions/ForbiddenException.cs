using Microsoft.AspNetCore.Http;
namespace Ecommerce.Domain.Exceptions;

public class ForbiddenException : AppException
{
    public ForbiddenException(string message) : base(message,StatusCodes.Status403Forbidden)
    {
        
    }
}


