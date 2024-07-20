using System.IdentityModel.Tokens.Jwt;

namespace Stackoverflow_Light.Utils;

public class TokenClaimsExtractor
{
    public string ExtractClaim(string token, string claimName)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        return jwtToken?.Claims.FirstOrDefault(c => c.Type == claimName)?.Value;
    }
}