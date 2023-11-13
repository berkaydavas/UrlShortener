using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MySql.EntityFrameworkCore.Extensions;
using UrlShortener.DAL.Entities;

namespace UrlShortener.DAL.Contexts;

public class ApplicationDbContext : IdentityUserContext<User, int>
{
  public DbSet<ShortedUrl> ShortedUrls => Set<ShortedUrl>();

  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);

    modelBuilder.HasCharSet("utf8mb4_general_ci");

    // User
    modelBuilder.Entity<User>(e => e.ToTable("Users"));
    modelBuilder.Entity<IdentityUserClaim<int>>(e => e.ToTable("UserClaims"));
    modelBuilder.Entity<IdentityUserLogin<int>>(e => e.ToTable("UserLogins"));
    modelBuilder.Entity<IdentityUserToken<int>>(e => e.ToTable("UserTokens"));
  }

  void SaveChangesInterceptor()
  {
    foreach (EntityEntry entry in ChangeTracker.Entries())
    {
      Type type = entry.Entity.GetType();

      switch (entry.State)
      {
        case EntityState.Added:
          type.GetProperty("CreatedDate")?.SetValue(entry.Entity, DateTime.Now);
          break;
        case EntityState.Modified:
          type.GetProperty("UpdatedDate")?.SetValue(entry.Entity, DateTime.Now);
          break;
      }
    }
  }

  public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    SaveChangesInterceptor();
    return base.SaveChangesAsync(cancellationToken);
  }

  public override int SaveChanges()
  {
    SaveChangesInterceptor();
    return base.SaveChanges();
  }
}