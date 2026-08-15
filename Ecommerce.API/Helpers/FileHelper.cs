namespace Ecommerce.API.Helpers;

public static class FileHelper
{
    public static string GetAvatarUrl(string? avatar, string baseUrl)
    {
        if (string.IsNullOrEmpty(avatar))
            return string.Empty;

        if (avatar.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
            avatar.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            return avatar;

        return $"{baseUrl.TrimEnd('/')}/{avatar.TrimStart('/')}";
    }
}
