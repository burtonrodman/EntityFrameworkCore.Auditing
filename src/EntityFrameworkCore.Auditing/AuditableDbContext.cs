using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace burtonrodman.EntityFrameworkCore.Auditing;

public abstract class AuditableDbContext : DbContext
{
    private readonly ICurrentUserAccessor _currentUserAccessor;

    protected AuditableDbContext(
        DbContextOptions<AuditableDbContext> options,
        ICurrentUserAccessor currentUserAccessor
    ) : base(options)
    {
        _currentUserAccessor = currentUserAccessor;
    }

    public override int SaveChanges()
        => ApplyAuditingAndSave(true);

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
        => ApplyAuditingAndSave(acceptAllChangesOnSuccess);
    
    private int ApplyAuditingAndSave(bool acceptAllChangesOnSuccess)
    {
        var deletedEntries = ApplyAuditing();
        var affectedRows = base.SaveChanges(acceptAllChangesOnSuccess);

        return affectedRows + (RemarkDeletedEntries(deletedEntries) ?
            base.SaveChanges(acceptAllChangesOnSuccess) : 0);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => ApplyAuditingAndSaveAsync(true, cancellationToken);
    
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        => ApplyAuditingAndSaveAsync(acceptAllChangesOnSuccess, cancellationToken);

    private async Task<int> ApplyAuditingAndSaveAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        var deletedEntries = ApplyAuditing();
        var affectedRows = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);

        return affectedRows + (RemarkDeletedEntries(deletedEntries) ?
            await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken) : 0);
    }

    private static bool RemarkDeletedEntries(List<EntityEntry> deletedEntries)
    {
        if (deletedEntries.Any())
        {
            foreach (var entry in deletedEntries)
            {
                entry.State = EntityState.Deleted;
            }
            return true;
        }
        return false;
    }

    private List<EntityEntry> ApplyAuditing()
    {
        var deletedEntries = new List<EntityEntry>();
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is AuditingEntityBase auditable)
            {
                if (entry.State == EntityState.Deleted)
                {
                    // mark delete rows unchanged so all fields aren't updated.
                    entry.State = EntityState.Unchanged;
                    deletedEntries.Add(entry);
                }
                auditable.ModifiedBy = _currentUserAccessor.GetUserName();
            }
        }
        return deletedEntries;
    }
}
