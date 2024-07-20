using Microsoft.EntityFrameworkCore;
using Stackoverflow_Light.Entities;

namespace Stackoverflow_Light.Configurations;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<OidcUserMapping> OidcUserMappings { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<OidcUserMapping>()
            .HasOne(oum => oum.User)
            .WithOne(u => u.OidcUserMapping)
            .HasForeignKey<OidcUserMapping>(oum => oum.UserId);

        modelBuilder.Entity<User>()
            .HasMany(u => u.Questions)
            .WithOne(q => q.User)
            .HasForeignKey(q => q.UserId);
        
        modelBuilder.Entity<Question>()
            .HasMany(q => q.Answers)
            .WithOne(a => a.Question)
            .HasForeignKey(a => a.QuestionId);
    
    }
}