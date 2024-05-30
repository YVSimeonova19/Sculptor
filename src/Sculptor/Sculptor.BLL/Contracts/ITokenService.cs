using System.IdentityModel.Tokens.Jwt;

namespace Sculptor.BLL.Contracts;

public interface ITokenService
{
    // Create a token for the logged user asyncronously
    Task<JwtSecurityToken> CreateTokenForUserAsync(string username);
}
