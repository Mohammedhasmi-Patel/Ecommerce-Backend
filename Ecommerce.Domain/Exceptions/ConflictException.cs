using Microsoft.AspNetCore.Http;
namespace Ecommerce.Domain.Exceptions;

public class ConflictException : AppException
{
    public ConflictException(string message) : base(message,StatusCodes.Status409Conflict)
    {
        
    }
}


