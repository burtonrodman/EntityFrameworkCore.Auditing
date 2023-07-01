# EntityFrameworkCore.Auditing
A base class and utilities to implement auditing with SQL Server temporal tables.

# Getting Started

## Add NuGet Packages
- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Design`
- `burtonrodman.EntityFrameworkCore.Auditing`

## Create the CurrentUserAccessor
1. Create a class that implements the `ICurrentUserAccessor` interface and returns the current user's identifier (whatever that means in your domain).  This will often be the Name property of the current ClaimsPrincipal, but may vary in your domain.
```
public class CurrentUserAccessor : ICurrentUserAccessor
{
    public string GetUserName() => "current.user@domain.com";
}
```
2. Register it in the DI container.  Example:
```
services.AddScoped<ICurrentUserAccessor, CurrentUserAccessor>();
```

## Setup your DbContext
1. Add a using for `burtonrodman.EntityFrameworkCore.Auditing` to your DbContext file or global usings.
```
using burtonrodman.EntityFrameworkCore.Auditing;
```
2. Change the base type of your DbContext to `AuditableDbContext` and add or modify the constructor to take the instance of `ICurrentUserAccessor` and pass it to the base constructor.
```
public class SampleDbContext : AuditableDbContext
{
    public SampleDbContext(
        DbContextOptions<SampleDbContext> dbContextOptions,
        ICurrentUserAccessor currentUserAccessor)
        : base(dbContextOptions, currentUserAccessor)
    { }
```
3. On any entity types that you want to be auditable, set the base class to `AuditableEntityBase`.
```
public class BlogPost : AuditingEntityBase
```
4. determine if the current run-time context is talking to SQL Server:

for testing scenarios where you may be using an in-memory or sqlite provider, the shadow properties provided by the SQL Server provider will not be available.  We have the `TryAddPeriodShadowProperties` extension that adds them so your queries using the PeriodStart and PeriodEnd columns do not break during testing.
```
var shouldAddShadowProperties = !this.Database.IsSqlServer();
```
5. For each entity type, in `OnModelCreating` call the `IsTemporalExplicit` method:
```
modelBuilder.Entity<BlogPost>(entity =>
{
    entity.ToTable(nameof(BlogPosts), b => b.IsTemporalExplicit(nameof(BlogPosts)))
        .TryAddPeriodShadowProperties(shouldAddShadowProperties);
});

```

## Create A Migration
Now that your DbContext and entities are configured, you may generate a new migration that will apply System-versioning and also add the ModifiedBy column to your table(s).
```
dotnet ef migrations add AddAuditing
dotnet ef database update
```

# How Does It Work?
SQL Server has a feature called "Temporal Tables" or "System-versioned Tables".  When enabled on a table, 2 columns are added to the existing table -- their names may vary, but I have chosen PeriodStart and PeriodEnd.  In addition, another table with the same schema and the suffix `History` is created.  From then on, any insert the PeriodStart is populated with the current server time; any update inserts a new row with PeriodStart as the current server time and moves the old version of the row to the History table, with PeriodEnd also set; any delete removes the row from the current table and inserts into the `History` table with the PeriodEnd set.

In order to create a full audit log (including when AND who), this library adds the ModifiedBy field. `SaveChanges[Async]` is overridden and updates the ModifiedBy column.  For deleted rows, the state is changed to Modified and the ModifiedBy field is updated before finally deleting the row.  This gives full auditing of inserts, updates and deletes.

# Troubleshooting
- PROBLEM:  TODO
    - SOLUTION:  TODO

# Contributing
I welcome Pull Requests for any improvement or bug fixes.  Please open an Issue for discussion if you plan on adding any features, so that we can collaborate on design.  For bug reports, a Pull Request with a failing unit test is ideal.

Thanks!