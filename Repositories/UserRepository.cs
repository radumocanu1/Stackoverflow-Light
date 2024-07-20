using Microsoft.EntityFrameworkCore;
using Stackoverflow_Light.Configurations;
using Stackoverflow_Light.Entities;

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

    public async Task<User> GetUserBySubClaimAsync(string subClaim)
    {
        return await _context.Users
            .Include(u => u.OidcUserMapping)
            .FirstOrDefaultAsync(u => u.OidcUserMapping.SubClaim == subClaim);
    }
    
}
