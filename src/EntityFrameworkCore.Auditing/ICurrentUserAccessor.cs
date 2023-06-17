namespace burtonrodman.EntityFrameworkCore.Auditing;

public interface ICurrentUserAccessor
{
    string GetUserName();
}