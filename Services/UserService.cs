using Stackoverflow_Light.Configurations;
using Stackoverflow_Light.Entities;
using Stackoverflow_Light.Exceptions;
using Stackoverflow_Light.Repositories;
using Stackoverflow_Light.Utils;
using Stackoverflow_Light.Utils.Interfaces;

namespace Stackoverflow_Light.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenClaimsExtractor _tokenClaimsExtractor;

    public UserService(IUserRepository userRepository, ITokenClaimsExtractor tokenClaimsExtractor)
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
        // check if the user mapping was not previously created ( to avoid created multiple user DB instances for the same Keycloak user)
        try
        {
            await GetUserIdFromSubClaimAsync(subClaim);
        }
        catch (OidcUserMappingNotFound _)
        {
            var user = new User
            {
                Username = username,
                OidcUserMapping = new OidcUserMapping { SubClaim = subClaim }
            };
            return await _userRepository.CreateUserAsync(user);
        }

        throw new OidcUserMappingAlreadyCreated(ApplicationConstants.OIDC_MAPPING_ALREADY_CREATED);

    }

    public async Task<Guid> GetUserIdFromSubClaimAsync(string subClaim)
    {
        var oidcUserMapping = await _userRepository.GetOidcUserMappingFromSubClaimAsync(subClaim);
        return oidcUserMapping.UserId;
    }
    
}
