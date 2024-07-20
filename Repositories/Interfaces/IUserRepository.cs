using Stackoverflow_Light.Entities;

namespace Stackoverflow_Light.Repositories;

public interface IUserRepository
{
    Task<User> CreateUserAsync(User user);
    Task<OidcUserMapping> GetOidcUserMappingFromSubClaimAsync(string subClaim);
}
