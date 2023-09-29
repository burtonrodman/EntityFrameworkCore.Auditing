namespace burtonrodman.EntityFrameworkCore.Auditing;

public class DelegateCurrentUserAccessor : ICurrentUserAccessor
{
    private readonly Func<string>? _getUserNameFunc;

    private readonly Func<IServiceProvider, string>? _getUserNameWithServiceProviderFunc;
    private readonly IServiceProvider? _serviceProvider;

    public DelegateCurrentUserAccessor(
        Func<string> getUserNameFunc)
    {
        _getUserNameFunc = getUserNameFunc ?? throw new ArgumentNullException(nameof(getUserNameFunc));
    }

    public DelegateCurrentUserAccessor(
        Func<IServiceProvider, string> getUserNameFunc,
        IServiceProvider serviceProvider)
    {
        _getUserNameWithServiceProviderFunc = getUserNameFunc ?? throw new ArgumentNullException(nameof(getUserNameFunc));
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public string GetUserName()
    {
        if (_getUserNameFunc is not null)
        {
            return _getUserNameFunc.Invoke();
        }
        else if (_getUserNameWithServiceProviderFunc is not null)
        {
            return _getUserNameWithServiceProviderFunc.Invoke(_serviceProvider!);
        }
        throw new InvalidOperationException();
    }
}