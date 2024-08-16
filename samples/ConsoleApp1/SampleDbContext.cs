using burtonrodman.EntityFrameworkCore.Auditing;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp1;

public class SampleDbContext : AuditableDbContext
{
    // OPTIONAL: provide constants for use cases like AutoMapper Profile
    public const string PeriodStartColumnName = "SysStartTime";
    public const string PeriodEndColumnName = "SysEndTime";

    public SampleDbContext(
        DbContextOptions<SampleDbContext> dbContextOptions,
        ICurrentUserAccessor currentUserAccessor)
        : base(dbContextOptions, currentUserAccessor)
    { 
      // OPTIONAL:  override the default Period column names
      this.PeriodStart = PeriodStartColumnName;
      this.PeriodEnd = PeriodEndColumnName;
    }

    public DbSet<BlogPost> BlogPosts { get; set; }
    public DbSet<User> Users { get; set; }

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.ApplyConfigurationsFromAssembly(typeof(SampleDbContext).Assembly);

      ConfigureTemporalTables(modelBuilder);
    }
}