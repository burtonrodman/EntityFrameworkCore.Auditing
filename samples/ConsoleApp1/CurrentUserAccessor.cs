using burtonrodman.EntityFrameworkCore.Auditing;

namespace ConsoleApp1;

public class CurrentUserAccessor : ICurrentUserAccessor
{
    public string GetUserName() => "current.user@domain.com";
}