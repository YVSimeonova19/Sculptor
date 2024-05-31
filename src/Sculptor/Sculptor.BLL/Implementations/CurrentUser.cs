using Microsoft.AspNetCore.Http;
using Sculptor.BLL.Contracts;
using System.Security.Claims;

namespace Sculptor.BLL.Implementations;

internal class CurrentUser : ICurrentUser
{
    // Keep the user currently logged in
    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        this.UserId = httpContextAccessor?
            .HttpContext?
            .User
            .Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
            .Value!;
    }

    public string UserId { get; }
}