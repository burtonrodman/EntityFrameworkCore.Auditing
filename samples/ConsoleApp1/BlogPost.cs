using System.ComponentModel.DataAnnotations.Schema;
using burtonrodman.EntityFrameworkCore.Auditing;

namespace ConsoleApp1;

public class BlogPost : AuditingEntityBase
{
    public int BlogPostId { get; set; }
    public string Title { get; set; }
    public string Body { get; set; }

    [NotMapped]
    public DateTime DeleteDate { get; set; }
}