using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TranslationManager.Models;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Translation> Translations { get; set; }
    public DbSet<TranslationValue> TranslationValues { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<Translation>()
            .HasIndex(t => t.Key)
            .IsUnique();
            
        builder.Entity<TranslationValue>()
            .HasIndex(tv => new { tv.TranslationId, tv.Culture })
            .IsUnique();
    }
}