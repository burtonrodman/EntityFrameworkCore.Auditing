using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace burtonrodman.EntityFrameworkCore.Auditing;

public abstract class AuditableEntityBase
{
    public static string PeriodStart { get; set; } = nameof(PeriodStart);
    public static string PeriodEnd { get; set; } = nameof(PeriodEnd);

    [Required]
    [MaxLength(100)]
    public string? ModifiedBy { get; set; }

    [NotMapped]
    public DateTime LastModifiedDate { get; set; }
}