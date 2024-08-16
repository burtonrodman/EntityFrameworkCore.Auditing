namespace ConsoleApp1;

public class User : AuditableEntityBase
{
    public int UserId { get; set; }
    public required string UserName { get; set; }
}
