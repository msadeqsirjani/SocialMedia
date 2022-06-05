using SocialMedia.Common.Common;
using SocialMedia.Common.Exceptions;

namespace SocialMedia.Application.Extensions;


public static class HttpContextExtensions
{
    public static string GetAuthenticationToken(this HttpContext context)
    {
        var authorizationHeader = context.Request.Headers["Authorization"].ToString();

        var token = string.Empty;

        if (authorizationHeader != null && string.IsNullOrEmpty(authorizationHeader)) return token;

        token = authorizationHeader?["Bearer ".Length..];

        return !string.IsNullOrEmpty(token) ? token : throw new UnAuthorizedException(Statement.UnAuthorized);
    }
}
