using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleApp1;

public class BlogPost : AuditableEntityBase
{
    public int BlogPostId { get; set; }
    public required string Title { get; set; }
    public required string Body { get; set; }

    [NotMapped]
    public DateTime DeleteDate { get; set; }
}