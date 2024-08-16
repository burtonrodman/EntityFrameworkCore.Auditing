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
    public DbSet<User> Users { get; set; }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(SampleDbContext).Assembly);
      ConfigureTemporalTables(modelBuilder);
    }
}