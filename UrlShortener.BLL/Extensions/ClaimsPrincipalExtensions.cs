using System.Security.Claims;

namespace UrlShortener.BLL.Extensions;

public static class ClaimsPrincipalExtensions
{
  public static int? GetUserId(this ClaimsPrincipal user)
    => int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
}