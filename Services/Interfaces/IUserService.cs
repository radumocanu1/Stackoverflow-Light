using Stackoverflow_Light.Entities;

namespace Stackoverflow_Light.Services;

public interface IUserService
{
    Task<User> CreateMappingAsync(string token);
    Task<Guid> GetUserIdFromSubClaimAsync(string subClaim);

}