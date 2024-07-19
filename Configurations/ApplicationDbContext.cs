using Microsoft.EntityFrameworkCore;
using Stackoverflow_Light.Entities;

namespace Stackoverflow_Light.Configurations;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<Question> YourEntities { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<OidcUserMapping> OidcUserMappings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<OidcUserMapping>()
            .HasOne(oum => oum.User)
            .WithOne(u => u.OidcUserMapping)
            .HasForeignKey<OidcUserMapping>(oum => oum.UserId);

      
    }
}