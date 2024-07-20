using Stackoverflow_Light.Configurations;
using Stackoverflow_Light.Entities;
using Stackoverflow_Light.Exceptions;
using Stackoverflow_Light.Repositories;
using Stackoverflow_Light.Utils;

namespace Stackoverflow_Light.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly TokenClaimsExtractor _tokenClaimsExtractor;

    public UserService(IUserRepository userRepository, TokenClaimsExtractor tokenClaimsExtractor)
    {
        _userRepository = userRepository;
        _tokenClaimsExtractor = tokenClaimsExtractor;
    }

    public async Task<User> CreateMappingAsync(string token)
    {
        var subClaim = _tokenClaimsExtractor.ExtractClaim(token, "sub");
        var username = _tokenClaimsExtractor.ExtractClaim(token, "preferred_username");
        if (subClaim == null || username == null)
        {
            throw new ArgumentException(String.Format(ApplicationConstants.OIDC_CLAIMS_EXTRACTION_ERROR, subClaim, username));
        }

        var user = new User
        {
            Username = username,
            OidcUserMapping = new OidcUserMapping { SubClaim = subClaim }
        };
        return await _userRepository.CreateUserAsync(user);
    }

    public async Task<Guid> GetUserIdFromSubClaimAsync(string subClaim)
    {
        var oidcUserMapping = await _userRepository.GetOidcUserMappingFromSubClaimAsync(subClaim);
        return oidcUserMapping.UserId;
    }
    
}
