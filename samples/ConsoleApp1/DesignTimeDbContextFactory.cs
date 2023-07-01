using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ConsoleApp1;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SampleDbContext>
{
    public SampleDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SampleDbContext>();
        optionsBuilder.UseSqlServer("server=(localdb)\\mssqllocaldb;database=ConsoleApp1Db;Trusted_Connection=true;");

        return new SampleDbContext(optionsBuilder.Options, new CurrentUserAccessor());
    }
}