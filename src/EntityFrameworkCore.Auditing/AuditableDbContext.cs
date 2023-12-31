﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace burtonrodman.EntityFrameworkCore.Auditing;

public abstract class AuditableDbContext : DbContext
{
    private readonly ICurrentUserAccessor _currentUserAccessor;

    protected AuditableDbContext(
        DbContextOptions options,
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
        var deletedEntriesThatAreNotDetached = deletedEntries.Where(x => x.State != EntityState.Detached);

        if (deletedEntriesThatAreNotDetached.Any())
        {
            foreach (var entry in deletedEntriesThatAreNotDetached)
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
        var userName = _currentUserAccessor.GetUserName();
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is AuditableEntityBase auditable)
            {
                switch (entry.State)
                {
                    case EntityState.Deleted:
                        // mark delete rows unchanged so all fields aren't updated.
                        entry.State = EntityState.Unchanged;
                        deletedEntries.Add(entry);
                        auditable.ModifiedBy = userName;
                        break;
                    case EntityState.Modified:
                    case EntityState.Added:
                        auditable.ModifiedBy = userName;
                        break;
                }
            }
        }
        return deletedEntries;
    }
}
