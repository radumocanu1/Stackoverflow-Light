using Microsoft.EntityFrameworkCore;
using Stackoverflow_Light.Entities;

namespace Stackoverflow_Light.Configurations;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Question> YourEntities { get; set; }
}