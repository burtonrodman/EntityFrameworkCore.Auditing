using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace burtonrodman.EntityFrameworkCore.Auditing;

public abstract class AuditableEntityBase
{
    public const string PeriodStart = nameof(PeriodStart);
    public const string PeriodEnd = nameof(PeriodEnd);

    [Required]
    public string? ModifiedBy { get; set; }

    [NotMapped]
    public DateTime LastModifiedDate { get; set; }
}