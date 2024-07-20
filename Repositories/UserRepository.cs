using Microsoft.EntityFrameworkCore;
using Stackoverflow_Light.Configurations;
using Stackoverflow_Light.Entities;
using Stackoverflow_Light.Exceptions;

namespace Stackoverflow_Light.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User> CreateUserAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<OidcUserMapping> GetOidcUserMappingFromSubClaimAsync(string subClaim)
    {
        var oidcUserMapping = await _context.OidcUserMappings.FirstOrDefaultAsync(o => o.SubClaim == subClaim);
        if (oidcUserMapping == null)
            throw new OidcUserMappingNotFound(ApplicationConstants.OIDC_MAPPING_NOT_CREATED);

        return oidcUserMapping;


    }
    
}
