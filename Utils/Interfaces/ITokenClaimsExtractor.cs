namespace Stackoverflow_Light.Utils.Interfaces;

public interface ITokenClaimsExtractor
{ 
    string ExtractClaim(string token, string claimName);
}