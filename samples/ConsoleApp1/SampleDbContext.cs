using burtonrodman.EntityFrameworkCore.Auditing;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1;

public class SampleDbContext : AuditableDbContext
{
    public SampleDbContext(
        DbContextOptions<SampleDbContext> dbContextOptions,
        ICurrentUserAccessor currentUserAccessor)
        : base(dbContextOptions, currentUserAccessor)
    { }

    public DbSet<BlogPost> BlogPosts { get; set; }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        var shouldAddShadowProperties = !this.Database.IsSqlServer();
        modelBuilder.Entity<BlogPost>(entity =>
        {
            entity.ToTable(nameof(BlogPosts), b => b.IsTemporalExplicit(nameof(BlogPosts)))
                .TryAddPeriodShadowProperties(shouldAddShadowProperties);
        });
    }
}