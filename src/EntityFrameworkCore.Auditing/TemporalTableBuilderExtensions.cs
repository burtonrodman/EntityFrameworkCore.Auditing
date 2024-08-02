using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace burtonrodman.EntityFrameworkCore.Auditing;

public static class TemporalTableBuilderExtensions
{
    /// <summary>
    /// mark a table as temporal, explicitly specifying period columns and History table names per our convention to prevent migrations from inventing its own.
    ///
    public static TableBuilder IsTemporalExplicit(
      this TableBuilder builder,
      string tableName,
      string? periodStart = null,
      string? periodEnd = null)
    {
        builder.IsTemporal(ttb =>
        {
            ttb.HasPeriodStart(periodStart ?? AuditableEntityBase.PeriodStart);
            ttb.HasPeriodEnd(periodEnd ?? AuditableEntityBase.PeriodEnd);
            ttb.UseHistoryTable($"{tableName}History");
        });
        return builder;
    }

    /// <summary>
    /// for testing scenarios (in-memory, sqlite), sub out PeriodStart and PeriodEnd shadow properties added by the SqlServer provider.
    /// use DbContext.Database.IsInMemory or !DbContext.Database.IsSqlServer for the value of shouldAddShadowProperties
    /// </summary>
    public static void TryAddPeriodShadowProperties<TEntity>(
        this EntityTypeBuilder<TEntity> builder,
        bool shouldAddShadowProperties) where TEntity : class
    {
        if (shouldAddShadowProperties)
        {
            builder.Property<DateTime>(AuditableEntityBase.PeriodStart);
            builder.Property<DateTime>(AuditableEntityBase.PeriodEnd);
        }
    }
}